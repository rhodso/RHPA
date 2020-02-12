﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RHPA {
    public class DrivingMode : ContentPage {

        ControlVars c = new ControlVars();
        List<View> gridViews;
        List<string> alertTypes;

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
            StackLayout co = new StackLayout();
            StackLayout content = new StackLayout();
            ScrollView con = new ScrollView();

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

            //Add views
            gridViews.Add(new Label { Text="Latitude",Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(new Label { Text=getLat(),Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(new Label { Text="Longitude",Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });
            gridViews.Add(new Label { Text=getLon(),Margin=5,TextColor=textCol,FontSize=18,MinimumHeightRequest=20,VerticalOptions=LayoutOptions.Center,HorizontalOptions=LayoutOptions.FillAndExpand });

            //Set Spans
            Grid.SetColumnSpan(gridViews[1],2);
            Grid.SetColumnSpan(gridViews[3],2);

            //Add the grid
            content.Children.Add(grid);

            con.Content=content;
            co.Children.Add(con);
            return co;
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

        private string getLon() {
            return locationHandler.getLongitude();
        }

        private string getLat() {
            return locationHandler.getLatidute();
        }
    }
}