using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

using Xamarin.Forms;

namespace RHPA {
    public class DrivingMode : ContentPage {

        ControlVars c = new ControlVars();
        serverConnection conn = new serverConnection();
        List<View> gridViews;
        List<string> alertTypes;
        Label alertsLabel;

        private readonly string darkModeButtonColor = "4F4F4F";
        private readonly string lightModeButtonColor = "AAAAAA";

        public DrivingMode() {
            Content=BuildContent();
            alertTypes=getAlertTypes();

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

            Label alertsLabel = new Label() { Text="App is in driving mode\nYou will be audibly alerted to any hazards as to not distract you while driving",HorizontalOptions=LayoutOptions.FillAndExpand,VerticalOptions=LayoutOptions.StartAndExpand,Margin=5,TextColor=textCol,FontSize=18,VerticalTextAlignment=TextAlignment.Start };
            content.Children.Add(alertsLabel);
                
            return content;
        }

        private List<string> getAlertTypes() {
            List<string> aList = new List<string>();
            aList.Add("Collision");
            aList.Add("Horse Rider");
            aList.Add("Road Work");
            aList.Add("Road Obstruction");
            aList.Add("Other");
            return aList;
        }

        /*
        void checkForUpdate() {
            string decodedReply = null;
            try {
                //Recieve reply
                string reply = conn.sendRevcMessage("1|"+getLon()+"|"+getLat());

                //Split up hazards
                string[] hazards = reply.Split('|');

                List<string[]> hazardsList = new List<string[]>(0);

                foreach(string s in hazards) {
                    //hazardsList.Add(s.Split(','));
                    List<string> hazardInfo = new List<string>(0);
                    string[] str = s.Split(',');
                    foreach(string stri in str) {
                        hazardInfo.Add(stri);
                    }
                    try {
                        hazardInfo.Add(DistanceAlgorithm.DistanceBetweenPlaces(
                            double.Parse(str[0]),
                            double.Parse(str[1]),
                            double.Parse(getLat()),
                            double.Parse(getLon())).ToString());
                    } catch(Exception ex) { }
                    hazardsList.Add(hazardInfo.ToArray());
                }

            } catch(ArgumentNullException e) {
                    //Handle this somehow
            } catch(SocketException e) {
                   //Handle this somehow
            }

            alertsLabel.Text=decodedReply;
        }
        */

        private string getLon() {
            return locationHandler.getLongitude();
        }

        private string getLat() {
            return locationHandler.getLatidute();
        }
    }
}