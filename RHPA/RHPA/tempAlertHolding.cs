using System;
using System.Collections.Generic;
using System.Text;

namespace RHPA {
    class tempAlertHolding {
        public int AlertID {
            get; set;
        }
        public string StartDate {
            get; set;
        }
        public string AlertType {
            get; set;
        }
        public string Description {
            get; set;
        }
        public double Lat {
            get; set;
        }
        public double Lng {
            get; set;
        }
        public double Distance {
            get; set;
        }
    }
}
