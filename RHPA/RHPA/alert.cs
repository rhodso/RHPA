using System;
using System.Collections.Generic;
using System.Text;

namespace RHPA {
    class Alert {
        private double lat;
        private double lon;
        private string alertType;
        private int user;
        private int proximity;
        private DateTime exipryTime;
        private DateTime startTime;
        private bool active;

        public Alert(double lat,double lon,string type,int proximity,DateTime exipryTime) {
            this.lat=lat;
            this.lon=lon;
            this.alertType=type;
            this.proximity=proximity;
            this.exipryTime=exipryTime;
            this.startTime=DateTime.Now;
            this.active=getActive();
        }

        public bool getActive() {
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
        public double GetLat() {
            return lat;
        }
        public double GetLon() {
            return lon;
        }
        public string GetAlertType() {
            return alertType;
        }
        public int GetUser() {
            return user;
        }
        public int GetProximity() {
            return proximity;
        }
        public void SetExipryTime(DateTime value) {
            exipryTime=value;
        }
        public void SetLat(double value) {
            lat=value;
        }
        public void SetLon(double value) {
            lon=value;
        }
        public void SetAlertType(string value) {
            alertType=value;
        }
        public void SetUser(int value) {
            user=value;
        }
        public void SetProximity(int value) {
            proximity=value;
        }

    }

}
