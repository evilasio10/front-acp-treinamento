using Newtonsoft.Json;
using NLog;
using RestSharp;
using System.Text;
using treinamento_Domain.DTOs;
using treinamento_Domain.Enums;
using treinamento_Domain.Extentions;
using treinamento_Domain.Filter;
using treinamento_Domain.Models;

namespace treinamento_applicationService.Business
{
    public class LoginBusiness
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public ResultModel<DadosUsuarioDTO> Login(LoginFilter loginFilter)
        {
            ResultModel<DadosUsuarioDTO> data;
            try
            {
                var client = new RestClient(Settings.UrlAutenticao+ "/Login");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                var body = JsonConvert.SerializeObject(loginFilter, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                var obj = JsonConvert.DeserializeObject<DadosUsuarioDTO>(response.Content);
                
                if (obj != null)
                {
                    data = new ResultModel<DadosUsuarioDTO>();
                    data.Items.Add(obj);
                }
                else
                {
                    data = new ResultModel<DadosUsuarioDTO>(false);
                    data.Messages.Add(new SystemMessageModel { Message = "Login ou senha invalidos!", Type = SystemMessageTypeEnum.Error });
                }
            }
            catch (Exception ex)
            {
                data = new ResultModel<DadosUsuarioDTO>(false);
                data.Messages.Add(new SystemMessageModel { Message = ex.Message, Type = SystemMessageTypeEnum.Error });
                logger.Debug(ex.Message);
            }
            return data;
        }      
    }
}
