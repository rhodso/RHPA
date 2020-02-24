using System;
using System.Collections.Generic;
using System.Text;

namespace RHPA {
    class alertTypeObject {
        private int alertTypeID;
        private string description;

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
