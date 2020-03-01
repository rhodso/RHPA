using System;
using System.Collections.Generic;
using System.Text;

namespace RHPA {
    class alertTypeObject {
        private int alertTypeID;
        private string description;

        public alertTypeObject(int alertTypeID, string description)
        {
            this.alertTypeID = alertTypeID;
            this.description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public int getAlertTypeID() {
            return this.alertTypeID;
        }
        public string getDescription() {
            return this.description;
        }
        public void setAlertTypeID(int _alertTypeID) {
            this.alertTypeID=_alertTypeID;
        }
        public void setDescription(string _description) {
            this.description=_description;
        }
        
    }
}
