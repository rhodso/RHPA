using System;
using System.Collections.Generic;
using System.Text;

namespace RHPA {
    class ControlVars {
        //Vars
        private static bool lightMode;
        private static Alert myAlert;
        private static Alert defaultAlert;

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
