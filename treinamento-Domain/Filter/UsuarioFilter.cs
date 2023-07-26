using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace treinamento_Domain.Filter
{
    public class UsuarioFilter
    {
        public int? ide { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string perfil { get; set; }
    }
}
