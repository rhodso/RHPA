using System;
using System.Collections.Generic;
using System.Text;

namespace RHPA {
    class ControlVars {
        //Vars
        private static bool lightMode;
        private static Alert myAlert;
        private static Alert defaultAlert;
        private string darkModeButtonColor = "4F4F4F";
        private string lightModeButtonColor = "AAAAAA";

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
        public List<string> getAlertTypes() {
            List<string> aList = new List<string>();
            aList.Add("Collision");
            aList.Add("Horse Rider");
            aList.Add("Road Work");
            aList.Add("Road Obstruction");
            aList.Add("Other");
            return aList;
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
    }
}
