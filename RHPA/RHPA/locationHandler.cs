using Android.Content;
using Android.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace RHPA {

    public class locationHandler{
        
        public locationHandler(LocationManager locationManager) {
            
        }

        static public bool testLocation() {
            return true;
        }
        static public string getLongitude() {
            //For now
            return "ALongitude";
        }
        static public string getLatidute() {
            return "ALatitude";
        }


        
    }
}
