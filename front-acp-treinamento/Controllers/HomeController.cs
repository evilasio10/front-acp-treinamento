using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using treinamento_applicationService.Helpers;

namespace front_acp_treinamento.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult AutenticarUsuario()
        {
            var is_action = true;
            var error = string.Empty;

            try
            {
                if (SessionHelper.CurrentUser.ide_usuario > 0)
                    SetClaims(SessionHelper.CurrentUser.ide_usuario, SessionHelper.CurrentUser.UserName);
                else
                {
                    LimparCache();
                    return Json(new { is_action, url = Url.Action("Index", "Login"), error });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);                
            }

            return RedirectToAction("Index");
        }

        private void SetClaims(long idUsuario, string usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario)
            };

            var userIdentity = new ClaimsIdentity(claims, "login");

            var principal = new ClaimsPrincipal(userIdentity);
            _ = HttpContext.SignInAsync(principal);
        }
        private void LimparCache()
        {
            _ = HttpContext.SignOutAsync();
            SessionHelper.Remove();
        }

        public IActionResult Index()
        {
            if (SessionHelper.CurrentUser.ide_usuario == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}
