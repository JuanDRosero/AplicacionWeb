using AplicacionWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AplicacionWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly RelacionesContext _contex;     //DB Context de la aplicación
        private readonly List<Tipo> tipos;              //Lista de tipos de identificación
        private readonly List<Parentesco> relaciones;   //Lista con los tipos de relaciones

        public UsuarioController(RelacionesContext contex)
        {
            _contex = contex;   //Inicialización del DBContext
            tipos = _contex.Tipos.ToList(); //Inicialización de la lista de tipos
            relaciones = _contex.Parentescos.ToList();  //Inicialiación de la lista relaciones
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("ID") == -1)   //Revisa si la sesión ya caducó
            {
                HttpContext.Session.SetString("Error", "Su sessión ha caducado");
                return RedirectToAction("Index", "Home");
            }
            var Familiares = _contex.Personas.Where(x => x.Familiar == HttpContext.Session.GetInt32("ID")).ToList();
            ListaViewModel lvm = new ListaViewModel();
            lvm.familiares = Familiares;
            lvm.relaciones = relaciones;
            return View(lvm);
        }
        public IActionResult Ver(int Id)
        {
            if (HttpContext.Session.GetInt32("ID") == -1)   //Revisa si la sesión ya caducó
            {
                HttpContext.Session.SetString("Error", "Su sessión ha caducado");
                return RedirectToAction("Index", "Home");
            }
            RegistroViewModel rvm = new RegistroViewModel();
            rvm.persona = _contex.Personas.Where(x => x.Id == Id).First();
            rvm.usuario = _contex.Usuarios.Where(x=>x.Persona== Id).First();
            return View(rvm);   //Se obtiene la persona y el usuario y se envia como parametro
        }
        public IActionResult Editar(int Id)
        {
            if (HttpContext.Session.GetInt32("ID") == -1)   //Revisa si la sesión ya caducó
            {
                HttpContext.Session.SetString("Error", "Su sessión ha caducado");
                return RedirectToAction("Index", "Home");
            }
            var rvm = new RegistroViewModel();
            rvm.persona = _contex.Personas.Where(x => x.Id == Id).First();
            rvm.usuario = _contex.Usuarios.Where(x => x.Persona == Id).First();
            List<SelectListItem> tp = new List<SelectListItem>();
            foreach (var item in tipos)
            {
                tp.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
            }
            rvm.tipos = tp;
            return View(rvm);   //Se obtiene la persona y el usuario y se envia como parametro
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar([Bind("persona,usuario,tipos,tipo")] RegistroViewModel modelo)
        {
            if (HttpContext.Session.GetInt32("ID") == -1)   //Revisa si la sesión ya caducó
            {
                HttpContext.Session.SetString("Error", "Su sessión ha caducado");
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)    //Revisa si el modelo es valido
            {
                List<SelectListItem> tp = new List<SelectListItem>();
                foreach (var item in tipos)
                {
                    tp.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
                }
                modelo.tipos = tp;
                return View(modelo);
            }
            _contex.Personas.Update(modelo.persona);    //Actualización de la persona
            _contex.Usuarios.Update(modelo.usuario);    //Actualización del usuario
            await _contex.SaveChangesAsync();
            return RedirectToAction("Ver");             //Redireción a la vista principal
        }
        [HttpGet]
        public IActionResult AgregarF()
        {
            if (HttpContext.Session.GetInt32("ID") == -1)   //Revisa si la sesión ya caducó
            {
                HttpContext.Session.SetString("Error", "Su sessión ha caducado");
                return RedirectToAction("Index", "Home");
            }
            AgregarViewModel avm = new AgregarViewModel();
            List<SelectListItem> tp = new List<SelectListItem>();
            foreach (var item in tipos)
            {
                tp.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
            }
            avm.tipos = tp;
            List<SelectListItem> tp2 = new List<SelectListItem>();
            foreach (var item in relaciones)
            {
                tp2.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
            }
            avm.relaciones = tp2;
            return View(avm);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AgregarF([Bind("Id,TipoI,Identificacion,PrimerNombre,SegundoNombre," +
            "PrimerApellido,SegundoApellido,Edad,Relacion")]AgregarViewModel model)
        {
            if (HttpContext.Session.GetInt32("ID") == -1)   //Revisa si la sesión ya caducó
            {
                HttpContext.Session.SetString("Error", "Su sessión ha caducado");
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)        //Revisa si el modelo es valido
            {
                List<SelectListItem> tp = new List<SelectListItem>();
                foreach (var item in tipos)
                {
                    tp.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
                }
                model.tipos = tp;
                List<SelectListItem> tp2 = new List<SelectListItem>();
                foreach (var item in relaciones)
                {
                    tp2.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString() });
                }
                model.relaciones = tp2;
                return View(model);
            }
            //Traducción del modelo Bindado al modelo Persona para poder insertar los cambios
            Persona p = new Persona();
            p.Id = model.Id;
            p.TipoI= model.TipoI;
            p.Identificacion= model.Identificacion;
            p.PrimerNombre= model.PrimerNombre;
            p.SegundoNombre=model.SegundoNombre;
            p.PrimerApellido= model.PrimerApellido;
            p.SegundoApellido=model.SegundoApellido;
            p.Edad= model.Edad;
            p.Relacion= model.Relacion;
            p.Familiar = HttpContext.Session.GetInt32("ID");
            _contex.Personas.Add(p);
            await _contex.SaveChangesAsync();   //Agregación del familiar con la relación indicada
            return RedirectToAction("Index");
        }
    }
}
