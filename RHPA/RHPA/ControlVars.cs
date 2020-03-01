using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RHPA {
    class ControlVars {
        //Vars
        private static bool lightMode;
        private static Alert myAlert;
        private static Alert defaultAlert;
        private string darkModeButtonColor = "4F4F4F";
        private string lightModeButtonColor = "AAAAAA";
        private List<alertTypeObject> alertTypes;

        public ControlVars() {
            alertTypes=getAlertTypeList();
        }

        //Getters
        public bool getLightMode() {
            return lightMode;
        }
        public Alert getAlert() {
            return myAlert;
        }
        public Alert getDefAlert() {
            return defaultAlert;
        }
        public List<string> getAlertTypeDescriptions() {
            List<string> aList = new List<string>();

            //Get from stored value
            alertTypes=serverConnection.getAlertTypes();
            List<alertTypeObject> alertTypeObjects = alertTypes;
            foreach(alertTypeObject a in alertTypeObjects) {
                aList.Add(a.getDescription());
                Console.WriteLine(a.getDescription());
            }
            return aList;
        }
        public List<alertTypeObject> getAlertTypeList() {
            return serverConnection.getAlertTypes();
        }
        public string getDarkModeButtonColor() {
            return darkModeButtonColor;
        }
        public string getLightModeButtonColor() {
            return lightModeButtonColor;
        }
        //Setters
        public void setLightMode(bool b) {
            lightMode=b;
        }
        public void setAlert(Alert a) {
            myAlert=a;
        }
        public void setDefAlert(Alert a) {
            defaultAlert=a;
        }

        public TimeSpan dateTimeToTimeSpan(DateTime date) {
            TimeSpan ts;
            long Timestamp = date.Ticks-new DateTime(1970,1,1).Ticks;
            Timestamp/=TimeSpan.TicksPerSecond;
            ts = TimeSpan.FromTicks(Timestamp);
            return ts;
        }

        public DateTime timeSpanToDateTime(TimeSpan _ts) {
            double dTimeSpan = Convert.ToDouble(_ts);
            DateTime dtReturn = new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc).AddSeconds(Math.Round(dTimeSpan/1000d)).ToLocalTime();
            return dtReturn;
        }
    }
}
