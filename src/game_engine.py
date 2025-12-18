import json
import math
import sys
import os
from PIL import Image

# --- Configuration & Rules ---
TERRAIN_COLORS = {
    (0, 0, 255): "Rio",
    (0, 255, 0): "Floresta",
    (128, 128, 128): "Cidade",
    (255, 255, 0): "Estrada",
    # Default: Campo Aberto
}

# Speed (km/h) and Fuel (L/kg per km) Modifiers
# Base values are in unit definition, these are MULTIPLIERS or OVERRIDES
# Structure: { UnitType: { Terrain: { speed_factor: float, cost_factor: float } } }
# Derived from panzergeneral.md
TERRAIN_RULES = {
    "INF": {
        "Campo Aberto": {"speed_factor": 1.0, "cost": 0.5},
        "Floresta":     {"speed_factor": 0.5, "cost": 1.0}, # 3.8 vs 7.5
        "Cidade":       {"speed_factor": 1.0, "cost": 0.5},
        "Montanha":     {"speed_factor": 0.33, "cost": 1.5},
        "Rio":          {"speed_factor": 0.2, "cost": 2.0}, # Fording
        "Estrada":      {"speed_factor": 1.1, "cost": 0.4}, # Bonus
        "Default":      {"speed_factor": 1.0, "cost": 0.5}
    },
    "TNK": {
        "Campo Aberto": {"speed_factor": 1.0, "cost": 2.5},
        "Floresta":     {"speed_factor": 0.5, "cost": 5.0},
        "Cidade":       {"speed_factor": 1.0, "cost": 2.5}, # Fast in city? Rules say 10km/h.. ok.
        "Montanha":     {"speed_factor": 0.1, "cost": 10.0}, # Very hard
        "Rio":          {"speed_factor": 0.0, "cost": 0.0}, # Blocked usually, unless bridge/estrada
        "Estrada":      {"speed_factor": 1.2, "cost": 1.5}, # Efficiency efficiency
        "Default":      {"speed_factor": 1.0, "cost": 2.5}
    },
    # Default for others (RCN, ART similarly)
    "DEFAULT": {
        "Campo Aberto": {"speed_factor": 1.0, "cost": 1.0},
        "Floresta":     {"speed_factor": 0.6, "cost": 1.5},
        "Cidade":       {"speed_factor": 0.8, "cost": 1.2},
        "Estrada":      {"speed_factor": 1.2, "cost": 0.8},
        "Rio":          {"speed_factor": 0.1, "cost": 3.0},
        "Default":      {"speed_factor": 1.0, "cost": 1.0}
    }
}

def get_terrain_at(img, x_meters, y_meters, map_width, map_height):
    if not img:
        return "Campo Aberto"
        
    img_w, img_h = img.size
    
    # Map World (Meters) -> Image Pixels
    # Y axis inverted in world? Usually 0,0 bottom-left in our def, but image 0,0 top-left
    # check index.html: 
    # const worldX = (imgX / imgWidth) * mapWidthMeters;
    # const worldY = ((imgHeight - imgY) / imgHeight) * mapHeightMeters;
    
    # So:
    # imgX = (worldX / mapWidthMeters) * imgWidth
    # imgY = imgHeight - (worldY / mapHeightMeters) * imgHeight
    
    px = int((x_meters / map_width) * img_w)
    py = int(img_h - (y_meters / map_height) * img_h)
    
    # Clamp
    px = max(0, min(img_w - 1, px))
    py = max(0, min(img_h - 1, py))
    
    try:
        pixel = img.getpixel((px, py))
        # Handle RGBA or RGB
        if len(pixel) > 3:
            pixel = pixel[:3]
            
        # Exact match logic (simplistic)
        for col, name in TERRAIN_COLORS.items():
            # Allow small tolerance? No, exact color for mask usually.
            if pixel == col:
                return name
    except Exception:
        pass
        
    return "Campo Aberto"

def calculate_distance(p1, p2):
    return math.sqrt((p1['x'] - p2['x'])**2 + (p1['y'] - p2['y'])**2)

def process_turn(game_state):
    # Parameters
    params = game_state.get('parametros', {})
    turn_hours = params.get('horasPorTurno', 1.0)
    
    # Map Info
    map_info = game_state.get('mapa', {})
    map_name = map_info.get('nome', 'GreenValley')
    map_w = map_info.get('larguraMetros', 10000)
    map_h = map_info.get('alturaMetros', 8000)
    
    # Load Terrain Image
    terrain_img_path = os.path.join(f"src/mapas/{map_name}/terreno.png")
    terrain_img = None
    if os.path.exists(terrain_img_path):
        try:
            terrain_img = Image.open(terrain_img_path).convert('RGB')
        except Exception as e:
            print(f"Warning: Could not load terrain map: {e}")
    else:
        # Fallback: Try absolute path relative to script location if necessary
        # Assuming script run from repo root
        if os.path.exists(f"src/mapas/{map_name}/terreno.png"):
             terrain_img = Image.open(f"src/mapas/{map_name}/terreno.png").convert('RGB')
        else:
            print(f"Warning: Terrain map not found at src/mapas/{map_name}/terreno.png")

    units = game_state.get('unidades', [])
    
    for unit in units:
        if 'rota' not in unit or not unit['rota']:
            continue
            
        unit_type = unit.get('tipo', 'INF')
        rules = TERRAIN_RULES.get(unit_type, TERRAIN_RULES["DEFAULT"])
        base_speed = unit.get('velocidadeKmH', 5.0)
        
        # We process movement in small time steps to account for terrain changes
        # Step size: e.g. 0.1 hours or distance based?
        # Better: Distance steps. 50 meters?
        STEP_METERS = 50.0
        
        # Calculate max potential distance in this turn (ideal conditions)
        # Just loop until time runs out
        time_elapsed = 0.0
        
        while time_elapsed < turn_hours and unit['rota']:
            next_cp = unit['rota'][0]
            start_pos = {'x': unit['posicaoX'], 'y': unit['posicaoY']}
            
            dist_to_cp = calculate_distance(start_pos, next_cp)
            
            # Step distance is min(STEP_METERS, dist_to_cp)
            step_dist = min(STEP_METERS, dist_to_cp)
            
            # Determine terrain at CURRENT position (approximation for the step)
            terrain = get_terrain_at(terrain_img, start_pos['x'], start_pos['y'], map_w, map_h)
            
            # Look up modifier
            rule = rules.get(terrain, rules["Default"])
            
            # Calculate effective speed
            effective_speed = base_speed * rule['speed_factor']
            if effective_speed <= 0.1: effective_speed = 0.1 # Min speed to prevent infinite loop
            
            # Calculate time taken for this step
            # time = distance / speed
            # distance is in meters, speed in km/h -> speed * 1000 meters/h
            step_time = step_dist / (effective_speed * 1000.0)
            
            # Do we have enough time left?
            if time_elapsed + step_time > turn_hours:
                # Can only move partial distance
                time_pixels = turn_hours - time_elapsed
                # dist = time * speed
                step_dist = time_pixels * (effective_speed * 1000.0)
                step_time = time_pixels # Consume rest of time
            
            # Move unit along vector
            ratio = step_dist / dist_to_cp if dist_to_cp > 0 else 0
            new_x = start_pos['x'] + (next_cp['x'] - start_pos['x']) * ratio
            new_y = start_pos['y'] + (next_cp['y'] - start_pos['y']) * ratio
            
            unit['posicaoX'] = new_x
            unit['posicaoY'] = new_y
            
            # Consume Fuel
            # cost per km * distance_km
            fuel_cost_per_km = rule['cost'] # Simplification, logic is cost/km
            # If unit has cost defined in JSON, check consistency. panzergeneral.md says "Consumo: X L/km"
            # We use the rule multiplier against base cost? 
            # Or rule['cost'] IS the usage? 
            # Let's say: Fuel Consumed = (dist_km) * rule['cost'] 
            # unit.custoUSDKm is financial cost. 
            # We need Fuel Consumed.
            # Inf: 0.5 kg/km (Field)
            # Tnk: 2.5 L/km (Field)
            # So rule['cost'] matches the table directly.
            
            fuel_burned = (step_dist / 1000.0) * rule['cost']
            current_fuel = unit.get('combustivelAtual', 100)
            unit['combustivelAtual'] = max(0, current_fuel - fuel_burned)
            
            # Advance Time
            time_elapsed += step_time
            
            # Checkpoint reached?
            # Recalculate dist
            dist_remaining_cp = calculate_distance({'x':unit['posicaoX'], 'y':unit['posicaoY']}, next_cp)
            if dist_remaining_cp < 1.0: # 1 meter tolerance
                unit['rota'].pop(0)

            # If out of fuel, stop?
            if unit['combustivelAtual'] <= 0:
                print(f"Unit {unit['id']} out of fuel!")
                break
                
    return game_state

if __name__ == "__main__":
    import argparse
    
    parser = argparse.ArgumentParser(description="Process Game Turn")
    parser.add_argument('--input', help="Input JSON file path")
    parser.add_argument('--output', help="Output JSON file path")
    args = parser.parse_args()

    # Check for Pillow
    try:
        import PIL
    except ImportError:
        print("Error: Pillow (PIL) library not found. Run 'pip install Pillow'.", file=sys.stderr)

    input_data = ""
    # Input handling
    if args.input:
        with open(args.input, 'r', encoding='utf-8') as f:
            input_data = f.read()
    elif not sys.stdin.isatty():
        input_data = sys.stdin.read()
    else:
        print("Paste JSON Game State and press Ctrl+Z (Windows) or Ctrl+D (Linux/Mac) to run:")
        input_data = sys.stdin.read()

    if not input_data.strip():
        print("No data received.", file=sys.stderr)
        sys.exit(1)

    try:
        game_state = json.loads(input_data)
        new_state = process_turn(game_state)
        
        output_json = json.dumps(new_state, indent=2)
        
        if args.output:
            with open(args.output, 'w', encoding='utf-8') as f:
                f.write(output_json)
            print(f"Turn processed. Output saved to {args.output}")
        else:
            print("\n--- NEW GAME STATE (Copy this) ---\n")
            print(output_json)
            
    except json.JSONDecodeError:
        print("Error: Invalid JSON input.", file=sys.stderr)
        sys.exit(1)
    except Exception as e:
        import traceback
        traceback.print_exc()
        print(f"Error: {e}", file=sys.stderr)
        sys.exit(1)
