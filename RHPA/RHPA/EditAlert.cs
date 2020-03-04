using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace RHPA {
    public class EditAlert : ContentPage {
        ControlVars c = new ControlVars();
        List<View> gridViews;
        Picker alertTypePicker;
        Entry durationEntry;
        TimePicker endTime;
        List<string> alertTypes;


        public EditAlert() {
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

            //Create a list of views and a grid for the views
            gridViews=new List<View>();
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
            grid.RowDefinitions.Add(new RowDefinition { Height=GridLength.Auto });
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
            endTime=new TimePicker { Time=(ts),Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand };
            alertTypePicker=new Picker { ItemsSource=alertTypes,Margin=5,SelectedIndex=0,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand };
            var items = new List<string>();
            foreach(string s in c.getAlertTypeDescriptions()) {
                items.Add(s);
            }
            alertTypePicker.ItemsSource=items;
            
            //Add views
            gridViews.Add(new Label { Text="Latitude",Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(new Label { Text=getLat(),Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(new Label { Text="Longitude",Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(new Label { Text=getLon(),Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(new Label { Text="Alert type",Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(alertTypePicker);
            gridViews.Add(new Label { Text="End time",Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(endTime);

            //Add views to grid
            grid.Children.Add(gridViews[0],0,0);
            grid.Children.Add(gridViews[1],1,0);
            grid.Children.Add(gridViews[2],0,1);
            grid.Children.Add(gridViews[3],1,1);
            grid.Children.Add(gridViews[4],0,2);
            grid.Children.Add(gridViews[5],1,2);
            grid.Children.Add(gridViews[6],0,3);
            grid.Children.Add(gridViews[7],1,3);

            //Set Spans
            Grid.SetColumnSpan(gridViews[1],2);
            Grid.SetColumnSpan(gridViews[3],2);
            Grid.SetColumnSpan(gridViews[5],2);
            Grid.SetColumnSpan(gridViews[7],2);

            //Add the grid
            content.Children.Add(grid);

            //Add alert button
            var updateAlertButton = new Button();
            updateAlertButton.Text="Update alert";
            if(c.getLightMode()) {
                updateAlertButton.BackgroundColor=Color.FromHex(c.getLightModeButtonColor());
                updateAlertButton.TextColor=Color.Black;
            } else {
                updateAlertButton.BackgroundColor=Color.FromHex(c.getDarkModeButtonColor());
                updateAlertButton.TextColor=Color.White;
            }
            updateAlertButton.Clicked+=async (sender,args) => UpdateAlertButtonClicked();
            content.Children.Add(updateAlertButton);

            return content;
        }

        async void UpdateAlertButtonClicked() {
            //Check if things aren't set
            string errorStr = "";
            if(alertTypePicker.SelectedItem.ToString().Equals("")) {
                errorStr+="Alert type not set\n";
            }
            if(endTime.Time<=TimeSpan.Parse(DateTime.Now.ToString())) {
                errorStr+="End time is before start time\n";
            }
            //If things aren't set, show error
            //If they are then create an alert
            if(!errorStr.Equals("")) {
                errorStr="Coult not create alert\n"+errorStr;
                await DisplayAlert("Error",errorStr,"OK");
            } else { //Create alert here
                try {
                    bool f = true;
                    if(c.getAlert().getActive()==true) {
                        f=await DisplayAlert("Error","You already have an active alert. Do you want to cancel it?","Yes","No");
                    }
                    if(f) {
                        c.setAlert(new Alert(getLat(),getLon(),alertTypePicker.SelectedItem.ToString(),DateTime.Parse(endTime.Time.ToString()),true));
                        //Then update the website

                    }
                } catch(Exception e) {
                    await DisplayAlert("Error","Could not create alert\n"+e.Message,"OK");
                }
            }
        }

        private string getLon() {
            return locationHandler.getLongitude();
        }

        private string getLat() {
            return locationHandler.getLatidute();
        }

        async void UpdateAsync(CancellationToken ct) {
            
        }

        
    }
}