using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Allies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameOverPage : ContentPage
    {
        public GameOverPage(Game game)
        {
            InitializeComponent();
        }

        private void btnStartOver_Clicked(object sender, EventArgs e)
        {
            var navPage = new NavigationPage(new MainPage());
            Application.Current.MainPage = navPage;
        }
    }
}