using treinamento_Domain.DTOs;
using treinamento_Domain.Helpers;
using AppContext = treinamento_Domain.Helpers.AppContext;

namespace treinamento_applicationService.Helpers
{
    public class SessionHelper
    {
        public static DadosUsuarioDTO CurrentUser
        {
            get
            {
                if (AppContext.Current == null || AppContext.Current.Session.GetObjectFromJson<DadosUsuarioDTO>("Usuario") == null)
                {
                    return new DadosUsuarioDTO();
                }

                return AppContext.Current.Session.GetObjectFromJson<DadosUsuarioDTO>("Usuario");
            }
            set => AppContext.Current.Session.SetObjectAsJson("Usuario", (DadosUsuarioDTO)value);
        }
        public static void Remove()
        {
            try
            {
                if (AppContext.Current?.Session != null)
                {
                    CurrentUser = null;
                    AppContext.Current.Session.Remove("Usuario");
                    AppContext.Current.Session.Clear();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}