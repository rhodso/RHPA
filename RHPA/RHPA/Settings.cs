using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RHPA {
    public class Settings : ContentPage {
        ControlVars c = new ControlVars();
        List<string> alertTypes;
        Picker alertTypePicker;
        Entry proximityEntry;
        Entry durationEntry;
        Entry endTime;

        public Settings() {
            Content=buildContent();
            alertTypes=c.getAlertTypeDescriptions();

            if(c.getLightMode()) {
                BackgroundColor=Color.White;
            } else {
                BackgroundColor=Color.FromHex("282828");
            }
        }

        StackLayout buildContent() {
            StackLayout content = new StackLayout();

            List<View> gridViews=new List<View>();
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

            Switch darkMode = new Switch() { IsToggled=(!c.getLightMode()), HorizontalOptions=LayoutOptions.Center};
            endTime=new Entry { Keyboard=Keyboard.Numeric,Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand };            
            alertTypePicker=new Picker { ItemsSource=alertTypes,Margin=5,SelectedIndex=0,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand };
            proximityEntry=new Entry { Keyboard=Keyboard.Numeric,Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand };

            gridViews.Add(new Label() { Text="Toggle dark mode",Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(darkMode);
            gridViews.Add(new Label() { Text="\nDefault alert settings",Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(new Label { Text="Alert type",Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(alertTypePicker);
            gridViews.Add(new Label { Text="Proximity (m)",Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(proximityEntry);
            gridViews.Add(new Label { Text="Duration (mins)",Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(endTime);
            
            //Add views to grid
            grid.Children.Add(gridViews[0],0,0);
            grid.Children.Add(gridViews[1],2,0);
            grid.Children.Add(gridViews[2],0,1);
            grid.Children.Add(gridViews[3],0,2);
            grid.Children.Add(gridViews[4],1,2);
            grid.Children.Add(gridViews[5],0,3);
            grid.Children.Add(gridViews[6],1,3);
            grid.Children.Add(gridViews[7],0,4);
            grid.Children.Add(gridViews[8],1,4);

            //Set Spans
            Grid.SetColumnSpan(gridViews[0],2);
            Grid.SetColumnSpan(gridViews[2],3);
            Grid.SetColumnSpan(gridViews[4],2);
            Grid.SetColumnSpan(gridViews[6],2);
            Grid.SetColumnSpan(gridViews[8],2);
            

            //Add the grid
            content.Children.Add(grid);

            Button saveSettingsButton = new Button() { Text="Save settings" };
            if(c.getLightMode()) {
                saveSettingsButton.BackgroundColor=Color.FromHex(c.getLightModeButtonColor());
                saveSettingsButton.TextColor=Color.Black;
            } else {
                saveSettingsButton.BackgroundColor=Color.FromHex(c.getDarkModeButtonColor());
                saveSettingsButton.TextColor=Color.White;
            }
            saveSettingsButton.Clicked+=SaveSettingsButton_Clicked;
            content.Children.Add(saveSettingsButton);

            return content;
        }

        private void SaveSettingsButton_Clicked(object sender,EventArgs e) {
            DateTime expiryTime;
            expiryTime=DateTime.Now;
            expiryTime.AddSeconds(60*(int.Parse(endTime.Text)));
            c.setDefAlert(new Alert(null,null,alertTypePicker.SelectedIndex.ToString(), expiryTime));
        }
    }
}