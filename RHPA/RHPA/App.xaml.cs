using System;
using System.Timers;
using Xamarin.Forms;

namespace RHPA {
    public partial class App : Application {

        Timer _timer = new Timer();

        public App() {
            InitializeComponent();

            //Initialise control vars
            ControlVars c = new ControlVars();
            c.setAlert(new Alert("NONE","NONE","NONE",0,DateTime.Now,false));

            //Initialise server connection
            serverConnection conn = new serverConnection();
            
            /*
            Task<string> task = serverConnection.testRequest();
            task.Wait();
            string x = task.Result;
            Console.WriteLine(x);
            */

            //MainPage = new MainPage();
            MainPage=new NavigationPage(new MainPage());
        }

        protected override void OnStart() {
            //Start updating the GPS every second
            _timer.Interval=1000;
            _timer.Elapsed+=OnTimedEvent;
            _timer.Enabled=true;
        }

        protected override void OnSleep() {
            //Stop updating the GPS every second
            _timer.Stop();
        }

        protected override void OnResume() {
            //Start updating the GPS every second
            _timer.Interval=1000;
            _timer.Elapsed+=OnTimedEvent;
            _timer.Enabled=true;
        }

        private void OnTimedEvent(object sender,ElapsedEventArgs e) {
           
        }
    }
}
