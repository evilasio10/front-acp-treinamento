using Newtonsoft.Json;
using NLog;
using NLog.Filters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using treinamento_applicationService.Helpers;
using treinamento_Domain.DTOs;
using treinamento_Domain.Enums;
using treinamento_Domain.Extentions;
using treinamento_Domain.Filter;
using treinamento_Domain.Models;

namespace treinamento_applicationService.Business
{
    public class UsuarioBusiness
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();


        #region Listar Usuarios
        public ResultModel<DadosUsuarioDTO> ListarUsuarios()
        {
            ResultModel<DadosUsuarioDTO> data;
            try
            {
                string url = "/Usuarios";
                var client = new RestClient(Settings.UrlManageUsers + url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + SessionHelper.CurrentUser.token);
                IRestResponse response = client.Execute(request);
                var obj = JsonConvert.DeserializeObject<List<DadosUsuarioDTO>>(response.Content);

                if (obj != null)
                {
                    data = new ResultModel<DadosUsuarioDTO>();
                    data.Items.AddRange(obj);
                }
                else
                {
                    data = new ResultModel<DadosUsuarioDTO>(false);
                    data.Messages.Add(new SystemMessageModel { Message = "Usuarios não localizados!", Type = SystemMessageTypeEnum.Error });
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
        #endregion

        public ResultModel<DadosUsuarioDTO> GetUsuario(int id)
        {
            ResultModel<DadosUsuarioDTO> data;
            try
            {
                string url = "/Usuarios/" + id;
                var client = new RestClient(Settings.UrlManageUsers + url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + SessionHelper.CurrentUser.token);
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
                    data.Messages.Add(new SystemMessageModel { Message = "Usuario não localizado!", Type = SystemMessageTypeEnum.Error });
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

        public ResultModel<DadosUsuarioDTO> SalvarUsuario(UsuarioFilter filter)
        {
            ResultModel<DadosUsuarioDTO> data;
            UsuarioModel usuarioModel = new UsuarioModel();
            usuarioModel.Role = filter.perfil;
            usuarioModel.Email = filter.email;
            usuarioModel.UserName = filter.nome;

            if (filter.ide != null)
            {
                usuarioModel.ide_usuario = filter.ide.Value;
            }
            else
            {
                int senhaGeradaNaoEncryptada = new Random().Next(100000, 999999);
                string senhaEncryptada = Security.SHA256(senhaGeradaNaoEncryptada.ToString());
                usuarioModel.Password = senhaEncryptada;

                string mensagem = $"<strong>Olá,</strong><br>" +
                $"<br>" +
                $"Dados de acesso Sistema de Gerenciamento de Usuarios é:<br>" +
                $"<br>" +
                $"<strong>Usuário:</strong> {filter.nome}<br>" +
                $"<strong>Senha:</strong> {senhaGeradaNaoEncryptada}<br>" +
                $"<br>" +
                $"<p>Obrigado.";

                //Enviar(usuarioModel.Email, "Login e Senha - Gerenciamento de Usuarios", mensagem);
            }

            try
            {
                string url = "/Usuarios";
                url = filter.ide != null ? url + "/" + filter.ide : url;
                var client = new RestClient(Settings.UrlManageUsers + url);
                client.Timeout = -1;
                var request = new RestRequest(filter.ide != null ? Method.PUT : Method.POST);
                request.AddHeader("Authorization", "Bearer " + SessionHelper.CurrentUser.token);

                var body = JsonConvert.SerializeObject(usuarioModel, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode ==HttpStatusCode.OK)
                {
                    var obj = JsonConvert.DeserializeObject<DadosUsuarioDTO>(response.Content);
                    data = new ResultModel<DadosUsuarioDTO>();   
                    
                }
                if(response.StatusCode == HttpStatusCode.NoContent)
                {
                    data = new ResultModel<DadosUsuarioDTO>(false);
                    data.Messages.Add(new SystemMessageModel { Message = "Nome de usuario ou email ja esta sendo utilizado!", Type = SystemMessageTypeEnum.Error });
                }
                else
                {
                    
                    data = new ResultModel<DadosUsuarioDTO>(false);
                    data.Messages.Add(new SystemMessageModel { Message = filter.ide != null ? "Falha ao atualizar dados de usuarios!":"Falha ao cadastrar novo usuario!!", Type = SystemMessageTypeEnum.Error });
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

        public ResultModel<DadosUsuarioDTO> RemoverUsuario(int id)
        {
            ResultModel<DadosUsuarioDTO> data;
            try
            {
                string url = "/Usuarios/" + id;
                var client = new RestClient(Settings.UrlManageUsers + url);
                client.Timeout = -1;
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("Authorization", "Bearer " + SessionHelper.CurrentUser.token);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    data = new ResultModel<DadosUsuarioDTO>();
                }
                else
                {
                    data = new ResultModel<DadosUsuarioDTO>(false);
                    if(response.StatusCode == HttpStatusCode.Forbidden)
                        data.Messages.Add(new SystemMessageModel { Message = "Usuario sem permissão para remover!!", Type = SystemMessageTypeEnum.Error });
                    else
                        data.Messages.Add(new SystemMessageModel { Message = "Falha ao remover usuario!!", Type = SystemMessageTypeEnum.Error });
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

