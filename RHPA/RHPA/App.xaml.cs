using Android.Content;
using Android.Locations;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RHPA {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            //Initialise control vars
            ControlVars c = new ControlVars();
            //c.setLightMode(true);

            //MainPage = new MainPage();
            MainPage=new NavigationPage(new MainPage());
        }

        protected override void OnStart() {
            
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
