import os
import glob

files = glob.glob('/Users/felataslan/Projects/MyPortfolio/Views/**/*.cshtml', recursive=True)
for file in files:
    try:
        with open(file, 'r', encoding='utf-8') as f:
            content = f.read()
        if '~/Views/Shared/_AdminLayout.cshtml' in content:
            content = content.replace('~/Views/Shared/_AdminLayout.cshtml', '~/Views/Layout/Index.cshtml')
            with open(file, 'w', encoding='utf-8') as f:
                f.write(content)
            print(f"Fixed {file}")
    except Exception as e:
        pass
