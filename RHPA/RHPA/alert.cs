using System;
using System.Collections.Generic;
using System.Text;

namespace RHPA {
    class Alert {
        private int alertID;
        private string lat;
        private string lon;
        private string alertType;
        private User user;
        private DateTime exipryTime;
        private DateTime startTime;
        private bool active;

        public Alert(string lat,string lon,string type,DateTime exipryTime) {
            this.lat=lat;
            this.lon=lon;
            this.alertType=type;
            this.exipryTime=exipryTime;
            this.startTime=DateTime.Now;
            this.active=getActive();
        }

        public Alert(string lat,string lon,string type,DateTime exipryTime,bool isActive){
            this.lat=lat;
            this.lon=lon;
            this.alertType=type;
            this.exipryTime=exipryTime;
            this.startTime=DateTime.Now;
            this.active=isActive;
        }

        public int getID() {
            return alertID;
        }
        public bool getActive() {
            if(this.lat.Equals("NONE")) {
                return false;
            }
            bool isActive = TimeSpan.Parse(DateTime.Now.ToString())>TimeSpan.Parse(DateTime.Now.ToString());
            this.active=isActive;
            return isActive;
        }
        public DateTime GetExipryTime() {
            return exipryTime;
        }
        public DateTime GetStartTime() {
            return startTime;
        }
        public string GetLat() {
            return lat;
        }
        public string GetLon() {
            return lon;
        }
        public string GetAlertType() {
            return alertType;
        }
        public User GetUser() {
            return user;
        }
        public void setID(int _alertID) {
            alertID=_alertID;
        }
        public void SetExipryTime(DateTime value) {
            exipryTime=value;
        }
        public void SetLat(string value) {
            lat=value;
        }
        public void SetLon(string value) {
            lon=value;
        }
        public void SetAlertType(string value) {
            alertType=value;
        }
        public void SetUser(User _user) {
            user=_user;
        }

        public static string getAlertTypeNameFromID(int alertTypeID) {
            alertTypeID++; //Add one to match server
            ControlVars c = new ControlVars();
            List<string> alertTypes = c.getAlertTypeDescriptions();
            return alertTypes[alertTypeID];
        }
        public static int getAlertTypeIDFromName(string alertTypeName) {
            ControlVars c = new ControlVars();
            List<string> alertTypes = c.getAlertTypeDescriptions();
            int index = 1;
            foreach(string s in alertTypes) {
                if(s.Equals(alertTypeName)) {
                    return index;
                } else {
                    index++;
                }
            }
            //If not found
            return -1;
        }
    }

}
