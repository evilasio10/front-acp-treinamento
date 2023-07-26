using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using treinamento_applicationService.Business;
using treinamento_applicationService.Helpers;
using treinamento_Domain.DTOs;
using treinamento_Domain.Extentions;
using treinamento_Domain.Filter;

namespace front_acp_treinamento.Controllers
{
    public class LoginController : Controller
    {
        private LoginBusiness _loginBusiness = new LoginBusiness();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Acessar(string login, string senha, string return_url)
        {
            var is_action = false;
            var error = string.Empty;
            var usuarioDTO = new DadosUsuarioDTO();

            if (string.IsNullOrWhiteSpace(login))
                return Json(new { is_action = false, nome = string.Empty, url = string.Empty, error = "Informar o login" });

            if (string.IsNullOrWhiteSpace(senha))
                return Json(new { is_action = false, nome = string.Empty, url = string.Empty, error = "Informar a senha" });
            
            try
            {
                string secret = Security.SHA256(senha);
                var rest = _loginBusiness.Login(new LoginFilter { UserName = login, Password = secret });

                if (rest.IsOk)
                {
                    usuarioDTO = rest.Items.FirstOrDefault();
                    SessionHelper.CurrentUser = usuarioDTO;
                    is_action = true;
                }
                else
                {
                    return Json(new { is_action, nome = string.Empty, url = "", error = rest.Messages.FirstOrDefault().Message });
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return Json(new { is_action, usuarioDTO.UserName, url = Url.Action("AutenticarUsuario", "Home"), error });
        }

        public IActionResult Logout()
        {
            var is_action = false;
            var error = string.Empty;

            try
            {
                LimparCache();
                is_action = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new { is_action, url = Url.Action("Index", "Login"), error });
        }
        private void LimparCache()
        {
            _ = HttpContext.SignOutAsync();
            SessionHelper.Remove();
        }
    }
}
