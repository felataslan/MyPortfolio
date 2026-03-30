import os
import re

admin_controllers = [
    'ToDoListController.cs',
    'StatisticController.cs',
    'MessageController.cs',
    'ExperienceController.cs'
]

for item in admin_controllers:
    path = os.path.join('.', 'Controllers', item)
    if os.path.exists(path):
        with open(path, 'r', encoding='utf-8') as f:
            content = f.read()
        
        # Add Using Microsoft.AspNetCore.Authorization;
        if 'using Microsoft.AspNetCore.Authorization;' not in content:
            content = 'using Microsoft.AspNetCore.Authorization;\n' + content
        
        # Add [Authorize]
        if '[Authorize]' not in content:
            content = re.sub(r'(public\s+class\s+\w+\s*:\s*Controller)', r'[Authorize]\n\t\1', content)
            
        with open(path, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Secured {item}")
