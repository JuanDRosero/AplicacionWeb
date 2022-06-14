/*
 * Controlador para las vistan de Login y Registro
 */
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
        private readonly RelacionesContext _context;    //DB Context de la aplicación
        private readonly List<Tipo> tipos;  //Lista de tipos de identificación

        public HomeController(ILogger<HomeController> logger, RelacionesContext context)
        {
            _logger = logger;
            _context = context; //Inicialización del DB Context
            tipos = _context.Tipos.ToList();    //Inicialización lista de tipos
        }
        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("ID", -1); //Reinicio de la sesión
            return View();
        }
        [HttpPost]
        public IActionResult Ingresar(string usuario, string contraseña)    //Metodo post de la validación
        {

            var user = _context.Usuarios.Where(x => x.Usuario1 == usuario && x.Contraseña == contraseña).ToList();
            if (user.Count() != 0)
            {
                Persona p = _context.Personas.Where(x => x.Id == user.First().Persona).FirstOrDefault();
                HttpContext.Session.SetInt32("ID", user.First().Id);    //Guarda el Id del usuario en la sesión
                HttpContext.Session.SetString("Name", p.PrimerNombre + " " + p.PrimerApellido); //Guarda el nombre del usuario en la sesión
                return RedirectToAction("Index", "Usuario");    //Redirección a la vista principal del suario
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
            foreach (var item in tipos) //Metodo para llenar el SelectedListItem de rmv con los tipps de Identificación
            {
                tp.Add(new SelectListItem { Text=item.Descripcion, Value=item.Id.ToString()});
            }
            rvm.tipos = tp;
            return View(rvm);   //Paso de rmv como parametro a la vista
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar([Bind("persona,usuario,tipo")] RegistroViewModel modelo)
        {
            modelo.persona.TipoI = Int32.Parse(modelo.tipo);
            if (!ModelState.IsValid)    //Revisa si el modelo es valido antes de continuar
            {
                List<SelectListItem> tp = new List<SelectListItem>();
                foreach (var item in tipos)
                {
                    tp.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
                }
                modelo.tipos = tp;
                return View(modelo);    //Redirige nuevamente a la vista
            }
            await _context.Personas.AddAsync(modelo.persona);
            await _context.SaveChangesAsync();  //Se añade la persona a la BD
            var num = _context.Personas.Where(x=>x.Identificacion==modelo.persona.Identificacion).First().Id;
            modelo.usuario.Persona = num;
            await _context.Usuarios.AddAsync(modelo.usuario);
            await _context.SaveChangesAsync();  //Se añade el suario a la BD
            return RedirectToAction("Index");   //Redireción a la vista principal

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