using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Timers;

namespace RHPA {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //Instance of controlVars
        private static ControlVars controlVars = new ControlVars();

        //Static vars
        private readonly string darkModeButtonColor = "4F4F4F";
        private readonly string lightModeButtonColor = "AAAAAA";
        private bool lightMode;
        
        //UI Elements
        private Image logoImage;
        private Button newAlert;
        private Button editAlert;
        private Button drivingMode;
        private Button settings;
        
        public MainPage()
        {
            //List<tempAlertHolding> list = serverConnection.getAlertList(locationHandler.getLatidute(),locationHandler.getLongitude());

            if(!locationHandler.testLocation()) {
                BackgroundColor=Color.Gray;
                StackLayout sl = new StackLayout();
                sl.Children.Add(new Label() { Text="An error has occured and location is not available" });
                Content=sl;
            } else {
                //Setup
                lightMode=controlVars.getLightMode();
                lightMode=false;
                //Build Layout
                Content=BuildLayout();
                if(lightMode) {
                    BackgroundColor=Color.White;
                } else {
                    BackgroundColor=Color.FromHex("282828");
                }
            }
        }
        StackLayout BuildLayout()
        {
            StackLayout layout = new StackLayout();

            //1st spacing label
            Label l1 = new Label()
            {
                HeightRequest = 50,
                Text = ""
            };

            //Logo image
            logoImage = new Image
            {
                Source = ImageSource.FromFile("NoBackground.png"),
                HeightRequest = 212,
                Aspect = Aspect.AspectFit
            };

            //2nd spacing label
            Label l2 = new Label()
            {
                HeightRequest = 25,
                Text = ""
            };

            //Grid for the three buttons so that they're all the same length
            Grid g = new Grid();
            g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star)});
            g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star)});
            g.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star)});
            g.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });

            g.HorizontalOptions = LayoutOptions.CenterAndExpand;

            //New alert button
            newAlert = new Button()
            {
                Text = "New Alert",
                FontSize = 20,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.FromHex(lightModeButtonColor),
                TextColor = Color.Black
            };
            if (lightMode)
            {
                newAlert.BackgroundColor = Color.FromHex(lightModeButtonColor);
                newAlert.TextColor = Color.Black;
            }
            else
            {
                newAlert.BackgroundColor = Color.FromHex(darkModeButtonColor);
                newAlert.TextColor = Color.White;
            }
            newAlert.Clicked += new EventHandler(NewAlertAction);

            //Edit alert button
            editAlert = new Button()
            {
                Text = "Edit Alert",
                FontSize = 20,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.FromHex(lightModeButtonColor),
                TextColor = Color.Black
            };
            if (lightMode)
            {
                editAlert.BackgroundColor = Color.FromHex(lightModeButtonColor);
                editAlert.TextColor = Color.Black;
            }
            else
            {
                editAlert.BackgroundColor = Color.FromHex(darkModeButtonColor);
                editAlert.TextColor = Color.White;
            }
            editAlert.Clicked += new EventHandler(EditAlertAction);

            //Driving mode button
            drivingMode = new Button()
            {
                Text = "Driving Mode",
                FontSize = 20, 
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.FromHex(lightModeButtonColor)
            };
            if (lightMode)
            {
                drivingMode.BackgroundColor = Color.FromHex(lightModeButtonColor);
                drivingMode.TextColor = Color.Black;
            }
            else
            {
                drivingMode.BackgroundColor = Color.FromHex(darkModeButtonColor);
                drivingMode.TextColor = Color.White;
            }
            drivingMode.Clicked += new EventHandler(DrivingModeAction);

            //3rd spacing label
            Label l3 = new Label()
            {
                Text = "",
                HeightRequest = 50
            };

            //Settings button
            settings = new Button()
            {
                Text = "",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
            };
            if (lightMode)
            {
                settings.Image = (FileImageSource)ImageSource.FromFile("settings_gear_dark_theme.png");
                settings.BackgroundColor = Color.White;
            }
            else
            {
                settings.Image = (FileImageSource)ImageSource.FromFile("settings_gear_light_theme.png");
                settings.BackgroundColor = Color.FromHex("282828");
            }
            settings.Clicked += new EventHandler(SettingsAction);

            //Add elements to UI
            layout.Children.Add(l1);
            layout.Children.Add(logoImage);
            layout.Children.Add(l2);

            //Add elements to grid, then add grid to UI
            g.Children.Add(newAlert, 0, 0);
            g.Children.Add(editAlert, 0, 1);
            g.Children.Add(drivingMode, 0, 2);
            layout.Children.Add(g);
            
            //Add final UI components
            layout.Children.Add(l3);
            layout.Children.Add(settings);

            //Return the layout
            return layout;
        }

        async private void NewAlertAction(object Sender, EventArgs args)
        {
            //DisplayAlert("Button pressed", "'New Alert' button pressed", "OK");
            await Navigation.PushAsync(new NewAlert());
        }

        async private void EditAlertAction(object Sender, EventArgs args)
        {
            await Navigation.PushAsync(new EditAlert());
        }

        async private void DrivingModeAction(object Sender, EventArgs args)
        {
            await Navigation.PushAsync(new DrivingMode());
        }

        async private void SettingsAction(object Sender, EventArgs args)
        {
            await Navigation.PushAsync(new Settings());
        }
    }}
