using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Navegacao
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HamburgerMenu : Page
    {
        //Cria uma instância que substitui o frame da página pelo RootFrame definido no App.xaml.cs
        public new Frame Frame => App.RootFrame;

        private ObservableCollection<Contato> _listaDeContatos;

        public ObservableCollection<Contato> ListaDeContatos
        {
            get { return _listaDeContatos; }
            set { _listaDeContatos = value; }
        }


        public HamburgerMenu()
        {
            this.InitializeComponent();

            //Evento de navegação
            this.Frame.Navigated += Frame_Navigated;
        }

        private async void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            //Atribui o Frame para o conteúdo do SplitView
            MySplitView.Content = Frame;

            //Define a visibilidade do botão voltar
            if(Frame.CanGoBack)
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Visible;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;

            if (ListaDeContatos == null)
                ListaDeContatos = new ObservableCollection<Contato>();

            if (ListaDeContatos.Count > 0)
                return;
            
            //Definição e leitura do arquivo / conversão do JSON para a classe. Obs.: é necessário adicionar o newton.Json no projeto
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///ListaDeContatos.json"));

            using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
            {
                string json = await sRead.ReadToEndAsync();
                ListaDeContatos = JsonConvert.DeserializeObject<ObservableCollection<Contato>>(json);
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen; 
        }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ObservableCollection<Contato> contatos = new ObservableCollection<Contato>(ListaDeContatos.Where(x => x.IsFavorito).OrderBy(x => x.Nome));
            Frame.Navigate(typeof(Favoritos), contatos);
        }

        private void ListViewItem_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Todos), ListaDeContatos);
        }

        private void ListViewItem_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            ObservableCollection<Contato> contatos = new ObservableCollection<Contato>(ListaDeContatos.Where(x => !string.IsNullOrWhiteSpace(x.Email)).OrderBy(x => x.Nome));
            Frame.Navigate(typeof(Emails), contatos);
        }

        private void ListViewItem_Tapped_3(object sender, TappedRoutedEventArgs e)
        {
            ObservableCollection<Contato> contatos = new ObservableCollection<Contato>(ListaDeContatos.Where(x => !string.IsNullOrWhiteSpace(x.Telefone)).OrderBy(x => x.Nome));
            Frame.Navigate(typeof(Telefones), contatos);
        }
    }
}
