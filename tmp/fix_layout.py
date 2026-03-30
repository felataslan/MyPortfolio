import os

for root, dirs, files in os.walk('Views'):
    for file in files:
        if file.endswith('.cshtml'):
            path = os.path.join(root, file)
            with open(path, 'r', encoding='utf-8') as f:
                content = f.read()
            if 'Layout = "~/Views/Shared/_AdminLayout.cshtml";' in content:
                content = content.replace('Layout = "~/Views/Shared/_AdminLayout.cshtml";', 'Layout = "~/Views/Layout/Index.cshtml";')
                with open(path, 'w', encoding='utf-8') as f:
                    f.write(content)
                print(f"Fixed {path}")
