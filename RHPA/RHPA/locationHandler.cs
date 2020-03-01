using System;
using Xamarin.Essentials;

namespace RHPA {

    public class locationHandler{

        static string longitude;
        static string latitude;

        public locationHandler() {
            longitude="ALongitude";
            latitude="ALatitude";
        }

        public static async void getLocation() {
            try {
                /*
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                Xamarin.Essentials.Location location = await Geolocation.GetLocationAsync(request);
                */

                Location location = new Location();
                location.Latitude = 53.110691;
                location.Longitude = -2.380795;

                if(location!=null) {
                    longitude=location.Longitude.ToString();
                    latitude=location.Latitude.ToString();
                } else {
                    longitude="ALongitude";
                    latitude="ALatitude";
                }
            } catch(Exception e) {
                throw e;
            }    
        }

        static public bool testLocation() {
            getLocation();
            if(latitude=="ALatitude") {
                return false;
            } else {
                return true;
            }
        }
        static public string getLongitude() {
            //For now
            return longitude;
        }
        static public string getLatidute() {
            return latitude;
        }


        
    }
}

