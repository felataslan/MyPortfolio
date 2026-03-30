import os
import re

entities = ['About', 'Feature', 'Portfolio', 'Skill', 'Testimonial', 'SocialMedia', 'Contact']

def get_properties(entity):
    filepath = f"DAL/Entities/{entity}.cs"
    with open(filepath, 'r') as f:
        content = f.read()
    matches = re.findall(r'public\s+([a-zA-Z0-9_\?\[\]]+)\s+([a-zA-Z0-9_]+)\s*\{\s*get;\s*set;\s*\}', content)
    return matches

def generate_controller(entity):
    controller_content = f"""using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{{
    [Authorize]
    public class {entity}Controller : Controller
    {{
        private readonly MyPortfolioContext _context;

        public {entity}Controller(MyPortfolioContext context)
        {{
            _context = context;
        }}

        public IActionResult {entity}List()
        {{
            var values = _context.{entity}s.ToList();
            return View(values);
        }}

        [HttpGet]
        public IActionResult Create{entity}()
        {{
            return View();
        }}

        [HttpPost]
        public IActionResult Create{entity}({entity} entity)
        {{
            _context.{entity}s.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("{entity}List");
        }}

        public IActionResult Delete{entity}(int id)
        {{
            var value = _context.{entity}s.Find(id);
            if(value != null) {{
                _context.{entity}s.Remove(value);
                _context.SaveChanges();
            }}
            return RedirectToAction("{entity}List");
        }}

        [HttpGet]
        public IActionResult Update{entity}(int id)
        {{
            var value = _context.{entity}s.Find(id);
            return View(value);
        }}

        [HttpPost]
        public IActionResult Update{entity}({entity} entity)
        {{
            _context.{entity}s.Update(entity);
            _context.SaveChanges();
            return RedirectToAction("{entity}List");
        }}
    }}
}}
"""
    with open(f"Controllers/{entity}Controller.cs", "w") as f:
        f.write(controller_content)

def generate_views(entity):
    os.makedirs(f"Views/{entity}", exist_ok=True)
    props = get_properties(entity)
    id_prop = props[0][1] # Usually Id, [Entity]Id
    
    # List View
    th_tags = "".join([f"<th>{p[1]}</th>\n                                    " for p in props if p[1] != id_prop])
    td_tags = "".join([f"<td>@item.{p[1]}</td>\n                                        " for p in props if p[1] != id_prop])
    
    list_view = f"""@model List<MyPortfolio.DAL.Entities.{entity}>
@{{
    ViewData["Title"] = "{entity}List";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";
}}

<div class="content">
    <div class="container-fluid">
        <h4 class="page-title">{entity} Sayfası</h4>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title">{entity} Listesi</div>
                    </div>
                    <div class="card-body">
                        <div class="card-sub">
                            Bu alandan tabloda kayıtlı olan tüm verileri görebilirsiniz.
                        </div>
                        <table class="table mt-3">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    {th_tags}<th>Sil</th>
                                    <th>Güncelle</th>
                                </tr>
                            </thead>
                            <tbody>
                                @foreach (var item in Model)
                                {{
                                    <tr>
                                        <td>@item.{id_prop}</td>
                                        {td_tags}<td><a href="/{entity}/Delete{entity}/@item.{id_prop}" class="btn btn-danger">Sil</a></td>
                                        <td><a href="/{entity}/Update{entity}/@item.{id_prop}" class="btn btn-warning">Güncelle</a></td>
                                    </tr>
                                }}
                            </tbody>
                        </table>
                        <a href="/{entity}/Create{entity}/" class="btn btn-primary">Yeni Ekle</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
"""
    with open(f"Views/{entity}/{entity}List.cshtml", "w") as f:
        f.write(list_view)

    # Create View
    input_tags = ""
    for p_type, p_name in props:
        if p_name == id_prop: continue
        input_tags += f"""                            <div class="form-group">
                                <label>{p_name}</label>
                                <input type="text" class="form-control" name="{p_name}" />
                            </div>\n"""

    create_view = f"""@model MyPortfolio.DAL.Entities.{entity}
@{{
    ViewData["Title"] = "Create{entity}";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";
}}

<div class="content">
    <div class="container-fluid">
        <h4 class="page-title">Yeni {entity} Ekle</h4>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title">Bilgileri Doldurun</div>
                    </div>
                    <div class="card-body">
                        <form method="post">
{input_tags}                            <button class="btn btn-success">Kaydet</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
"""
    with open(f"Views/{entity}/Create{entity}.cshtml", "w") as f:
        f.write(create_view)

    # Update View
    update_inputs = f"""<input type="hidden" name="{id_prop}" value="@Model.{id_prop}" />\n"""
    for p_type, p_name in props:
        if p_name == id_prop: continue
        update_inputs += f"""                            <div class="form-group">
                                <label>{p_name}</label>
                                <input type="text" class="form-control" name="{p_name}" value="@Model.{p_name}" />
                            </div>\n"""

    update_view = f"""@model MyPortfolio.DAL.Entities.{entity}
@{{
    ViewData["Title"] = "Update{entity}";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";
}}

<div class="content">
    <div class="container-fluid">
        <h4 class="page-title">{entity} Güncelle</h4>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title">Bilgileri Güncelleyin</div>
                    </div>
                    <div class="card-body">
                        <form method="post">
                            {update_inputs}
                            <button class="btn btn-primary">Güncelle</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
"""
    with open(f"Views/{entity}/Update{entity}.cshtml", "w") as f:
        f.write(update_view)

for e in entities:
    generate_controller(e)
    generate_views(e)
    print(f"Scaffolded {e} successfully.")
