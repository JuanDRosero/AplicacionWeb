using AplicacionWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AplicacionWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RelacionesContext _context;
        private readonly List<Tipo> tipos;

        public HomeController(ILogger<HomeController> logger, RelacionesContext context)
        {
            _logger = logger;
            _context = context;
            tipos = _context.Tipos.ToList();
        }
        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("ID", -1);
            return View();
        }
        public IActionResult Ingresar(string usuario, string contraseña)
        {

            var user = _context.Usuarios.Where(x => x.Usuario1 == usuario && x.Contraseña == contraseña).ToList();
            if (user.Count() != 0)
            {
                Persona p = _context.Personas.Where(x => x.Id == user.First().Persona).FirstOrDefault();
                HttpContext.Session.SetInt32("ID", user.First().Id);
                HttpContext.Session.SetString("Name", p.PrimerNombre + " " + p.PrimerApellido);
                return RedirectToAction("Index", "Usuario");
            }
            HttpContext.Session.SetString("Error", "Usuario no encontrado");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            RegistroViewModel rvm = new RegistroViewModel();
            rvm.persona = new Persona();
            rvm.usuario = new Usuario();
            List<SelectListItem> tp = new List<SelectListItem>();
            foreach (var item in tipos)
            {
                tp.Add(new SelectListItem { Text=item.Descripcion, Value=item.Id.ToString()});
            }
            rvm.tipos = tp;
            return View(rvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar([Bind("persona,usuario,tipo")] RegistroViewModel modelo)
        {
            modelo.persona.TipoI = Int32.Parse(modelo.tipo);
            if (!ModelState.IsValid)
            {
                List<SelectListItem> tp = new List<SelectListItem>();
                foreach (var item in tipos)
                {
                    tp.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
                }
                modelo.tipos = tp;
                return View(modelo);
            }
            await _context.Personas.AddAsync(modelo.persona);
            await _context.SaveChangesAsync();
            var num = _context.Personas.Where(x=>x.Identificacion==modelo.persona.Identificacion).First().Id;
            modelo.usuario.Persona = num;
            await _context.Usuarios.AddAsync(modelo.usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}