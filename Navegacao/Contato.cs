using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

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

        public static async Task<ObservableCollection<Contato>> GetContactList()
        { 
            ObservableCollection<Contato> ListaDeContatos = new ObservableCollection<Contato>();

            //Definição e leitura do arquivo / conversão do JSON para a classe. Obs.: é necessário adicionar o newton.Json no projeto
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///ListaDeContatos.json"));

            using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
            {
                string json = await sRead.ReadToEndAsync();
                ListaDeContatos = JsonConvert.DeserializeObject<ObservableCollection<Contato>>(json);
            }

            return ListaDeContatos;
        }
    }
}
