using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navegacao
{
    public class Parametros
    {
        public string parametroString { get; set; }

        public List<Contato> parametroContatos { get; set; }

        public Parametros()
        {
            parametroContatos = new List<Contato>();
        }
    }
}
