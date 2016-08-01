using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navegacao
{
    public class Contato
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public bool IsFavorito { get; set; }

        public Contato(string Nome, string Email, string Telefone = "", bool IsFavorito = false)
        {
            this.Nome = Nome;
            this.Email = Email;
            this.Telefone = Telefone;
            this.IsFavorito = IsFavorito;
        }

    }
}
