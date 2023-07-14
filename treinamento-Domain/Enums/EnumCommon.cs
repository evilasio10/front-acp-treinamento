using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace treinamento_Domain.Enums
{
    public enum SystemMessageTypeEnum
    {
        Success,
        Info,
        Error,
    };

    public enum MensagemTipo
    {
        Success = 1,
        Danger,
        Info,
        Warning
    }
    public enum PessoaTipo
    {
        [Description("Pessoa Física")]
        F = 1,
        [Description("Pessoa Jurídica")]
        J
    }


    public enum TipoDocumentoPessoa
    {
        CPF = 1,
        CNPJ = 2,
    }


    public enum Sexo
    {
        [Description("Masculino")]
        M,
        [Description("Feminino")]
        F
    }
    public enum EstadoCivil
    {
        [Description("Solteiro")]
        S,
        [Description("Casado")]
        C,
        [Description("Divorciado")]
        D,
        [Description("Viúvo")]
        V,
        [Description("Outros")]
        O
    }
}