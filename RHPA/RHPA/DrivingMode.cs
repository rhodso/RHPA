using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace RHPA {
    public class DrivingMode : ContentPage {

        ControlVars c = new ControlVars();
        serverConnection conn = new serverConnection();
        List<View> gridViews;
        List<string> alertTypes;
        Label alertsLabel;
        ScrollView alertsListView;
        private Timer locationTimer;
        Grid alertsGrid;

        private readonly string darkModeButtonColor = "4F4F4F";
        private readonly string lightModeButtonColor = "AAAAAA";

        public DrivingMode() {
            NavigationPage.SetHasNavigationBar(this, false);
            Content=BuildContent();
            alertTypes=c.getAlertTypeDescriptions();

            if(c.getLightMode()) {
                BackgroundColor=Color.White;
            } else {
                BackgroundColor=Color.FromHex("282828");
            }
        }

        StackLayout BuildContent() {
            //Create content
            StackLayout content = new StackLayout();

            Button backButton = new Button() { Text = "Back" };
            if (c.getLightMode())
            {
                backButton.BackgroundColor = Color.FromHex(lightModeButtonColor);
                backButton.TextColor = Color.Black;
            }
            else
            {
                backButton.BackgroundColor = Color.FromHex(darkModeButtonColor);
                backButton.TextColor = Color.White;
            }
            backButton.Clicked += BackButton_Clicked;
            content.Children.Add(backButton);

            //Create a list of views and a grid for the views
            gridViews =new List<View>();
            var grid = new Grid {
                HeightRequest=750,
                WidthRequest=500
            };

            //Setup a 2xA grid
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width=new GridLength(1,GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width=new GridLength(1,GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width=new GridLength(1,GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height=GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height=GridLength.Auto });
            
            Color textCol;

            if(c.getLightMode()) {
                textCol=Color.Black;
            } else {
                textCol=Color.White;
            }

            //Add variable views
            TimeSpan ts = DateTime.Now.TimeOfDay;
            ts.Add(TimeSpan.FromHours(1));
            
            //Add views
            gridViews.Add(new Label { Text="Latitude",Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(new Label { Text=getLat(),Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(new Label { Text="Longitude",Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(new Label { Text=getLon(),Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            
            //Add views to grid
            grid.Children.Add(gridViews[0],0,0);
            grid.Children.Add(gridViews[1],1,0);
            grid.Children.Add(gridViews[2],0,1);
            grid.Children.Add(gridViews[3],1,1);
            
            //Set Spans
            Grid.SetColumnSpan(gridViews[1],2);
            Grid.SetColumnSpan(gridViews[3],2);
            
            //Add the grid
            content.Children.Add(grid);

            alertsListView = new ScrollView();
            
            //alerts grid
            alertsGrid = new Grid();
            alertsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            alertsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            alertsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            List<View> alertsGridViews = new List<View>(0);
            alertsGridViews.Add(new Label { Text = "Alert", TextColor = textCol, FontSize = 18, MinimumHeightRequest = 20, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand });
            alertsGridViews.Add(new Label { Text = "Distance", TextColor = textCol, FontSize = 18, MinimumHeightRequest = 20, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand });
            


            alertsListView.Content = alertsGrid;

            Label alertsLabel = new Label() { Text="App is in driving mode\nYou will be audibly alerted to any hazards as to not distract you while driving",HorizontalOptions=LayoutOptions.FillAndExpand,VerticalOptions=LayoutOptions.StartAndExpand,Margin=5,TextColor=textCol,FontSize=18,VerticalTextAlignment=TextAlignment.Start };
            content.Children.Add(alertsLabel);

            startTimer();

            return content;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            stopTimer();
            Navigation.PopAsync();
        }

        private void startTimer()
        {
            locationTimer = new Timer();
            locationTimer.Interval = 5000;
            locationTimer.Elapsed += async (sender, e) => await timerEvent();
            locationTimer.Start();
        }

        private void stopTimer()
        {
            locationTimer.Stop();
        }

        private async Task<int> timerEvent()
        {
            await Task.Run(locationHandler.getLocation);
            List<tempAlertHolding> alertList = serverConnection.getAlertList(getLat(), getLon());
            
            return 1;
        }        

        private void updateAlertList(List<tempAlertHolding> alertList)
        {

            foreach (tempAlertHolding a in alertList)
            {

            }
        }

        private string getLon() {
            return locationHandler.getLongitude();
        }

        private string getLat() {
            return locationHandler.getLatidute();
        }
    }
}