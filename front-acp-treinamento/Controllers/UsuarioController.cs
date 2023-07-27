using Microsoft.AspNetCore.Mvc;
using treinamento_applicationService.Business;
using treinamento_applicationService.Helpers;
using treinamento_Domain.Filter;

namespace front_acp_treinamento.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            if (SessionHelper.CurrentUser.ide_usuario == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            if (SessionHelper.CurrentUser.role != "Admin" && SessionHelper.CurrentUser.role != "Atendimento")
            {
                return RedirectToAction("Index", "Home");
            }
            var result = new UsuarioBusiness().ListarUsuarios();
            return View(result.Items);
        }
        public IActionResult Edit(int id)
        {
            if (SessionHelper.CurrentUser.ide_usuario == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            if (SessionHelper.CurrentUser.role != "Admin" && SessionHelper.CurrentUser.role != "Atendimento")
            {
                return RedirectToAction("Index", "Home");
            }
            var result = new UsuarioBusiness().GetUsuario(id);
            return View(result.Items.FirstOrDefault());
        }
        public IActionResult Create()
        {
            if (SessionHelper.CurrentUser.ide_usuario == 0)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
        public IActionResult CadastroUsuario(UsuarioFilter usuarioFilter)
        {
            if (SessionHelper.CurrentUser.ide_usuario == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            string error = "";
            bool is_action = true;            
            try
            {                
                var resultado = new UsuarioBusiness().SalvarUsuario(usuarioFilter);
                if (!resultado.IsOk) throw new Exception(resultado.Messages[0].Message);                
            }
            catch (Exception e)
            {
                error = e.Message;
                is_action = false;
            }
            return Json(new { error, is_action });
        }

        public IActionResult RemoverUsuario(int ide)
        {
            if (SessionHelper.CurrentUser.ide_usuario == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            string error = "";
            bool is_action = true;

            try
            {
                var resultado = new UsuarioBusiness().RemoverUsuario(ide);
                if (!resultado.IsOk) throw new Exception(resultado.Messages[0].Message);
            }
            catch (Exception e)
            {
                error = e.Message;
                is_action = false;
            }

            return Json(new { error, is_action });
        }

    }
}
