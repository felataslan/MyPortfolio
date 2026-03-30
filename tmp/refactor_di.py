import os
import re

def process_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    # Find where context is instantiated
    match = re.search(r'MyPortfolioContext\s+(\w+)\s*=\s*new\s+MyPortfolioContext\(\);', content)
    if not match:
        return

    var_name = match.group(1) # usually 'context' or 'portfolioContext'

    # Find class name
    class_match = re.search(r'public\s+class\s+(\w+)', content)
    if not class_match:
        return
    class_name = class_match.group(1)

    original_line = match.group(0)
    
    has_constructor = re.search(rf'public\s+{class_name}\s*\(', content)
    if has_constructor:
        print(f"Skipping {filepath} because constructor already exists.")
        return

    new_declaration = f"private readonly MyPortfolioContext {var_name};"
    constructor = f"\n        public {class_name}(MyPortfolioContext _context)\n        {{\n            {var_name} = _context;\n        }}"

    content = content.replace(original_line, new_declaration + constructor)

    with open(filepath, 'w', encoding='utf-8') as f:
        f.write(content)
    print(f"Refactored DI in {filepath}")

for root, dirs, files in os.walk('.'):
    if 'tmp' in root or 'bin' in root or 'obj' in root:
        continue
    for file in files:
        if file.endswith('.cs'):
            process_file(os.path.join(root, file))
