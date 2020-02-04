using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Android.Gms.Common;
using Android.Gms.Location;

namespace RHPA {
    class locationHandler {

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

        bool IsGooglePlayServicesInstalled() {
            var queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if(queryResult==ConnectionResult.Success) {
                Log.Info("MainActivity","Google Play Services is installed on this device.");
                return true;
            }

            if(GoogleApiAvailability.Instance.IsUserResolvableError(queryResult)) {
                // Check if there is a way the user can resolve the issue
                var errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                Log.Error("MainActivity","There is a problem with Google Play Services on this device: {0} - {1}",
                          queryResult,errorString);

                // Alternately, display the error to the user.
            }

            return false;
        }
    }
}
