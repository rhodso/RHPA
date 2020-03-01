using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Android.Net;
using Xamarin.Forms;


namespace RHPA {
    class serverConnection{
        private static HttpClient _client;
        private static readonly string url = "https://richard.keithsoft.com/proximity/api.php";

        private static List<alertTypeObject> alertTypeList;

        public serverConnection() {
            _client=new HttpClient();
            _client.Timeout=new TimeSpan(0,0,30);
        }

        /*
        public static async Task<object> requestExample() {
            string urlSuffex = "";
            Task<string> task = doRequest(urlSuffix);
            task.Wait();
            string response = task.Result;
            //Do response processing
        }
        */

        private static async Task<string> doRequest(string urlSuffix) {
            HttpClient client = new HttpClient(new AndroidClientHandler());
            client.Timeout = TimeSpan.FromSeconds(1);
            Uri uri = new Uri(url+urlSuffix);
            string response;
            try
            {
                response = await client.GetStringAsync(uri);
            } catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public static int addUser(User u, string name) {
            string urlSuffix = 
                "?type=adduser &email="+u.getEmail()+
                " &password="+u.getPassword()+
                " &name="+name;
            Task<string> task = doRequest(urlSuffix);
            task.Wait();
            string response = task.Result;

            if(response.Equals("OK")) {
                return 1;
            } else if(response.Equals("User already exists")) {
                return 0;
            } else {
                return -1;
            }
        }

        public static int clearAllAlerts(User u) {
            string urlSuffix = 
                "?type=clearalerts" + 
                " &email="+u.getEmail()+
                " &password="+u.getPassword();
            Task<string> task = doRequest(urlSuffix);
            task.Wait();
            string response = task.Result;

            if(response.Equals("OK")) {
                return 1;
            } else if(response.Equals("Bad email or password")) {
                return 0;
            } else {
                return -1;
            }
        }

        public static int createNewAlert(Alert a, User u) {
            string urlSuffix = 
                "?type=addalert&email=" + u.getEmail() + 
                " &password=" + u.getPassword() +
                " &alerttypeid=" + Alert.getAlertTypeIDFromName(a.GetAlertType()) +
                " &description=" + a.GetAlertType() +
                " &lat=" + a.GetLat() +
                " &lng=" + a.GetLon() + 
                " &radius=" + a.GetProximity() +
                " &enddate=" + a.GetExipryTime().ToString("yyyy-MM-dd HH:mm:ss");
            Task<string> task = doRequest(urlSuffix);
            task.Wait();
            string response = task.Result;
            try {
                int alertID=int.Parse(response);
                return alertID;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public static List<tempAlertHolding> getAlertList(string lat, string lon) {
            List<tempAlertHolding> alertList;
            string urlSuffix =
                "?type=alerts "+ 
                " &lat=" + lat +  
                " &lng=-2.280 " + lon + 
                " &radius=500 ";
            Task<string> task = doRequest(urlSuffix);
            task.Wait();
            string response = task.Result;
            alertList=JsonConvert.DeserializeObject<List<tempAlertHolding>>(response);
            return alertList;
        }

        public static List<alertTypeObject> getAlertTypes() {
            if(alertTypeList!=null) {
                return alertTypeList;
            } else {
                List<alertTypeObject> alertTypes;
                //HttpClient client = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler());

                string urlSuffix = "?type=alerttypes";
                try
                {
                    //await the reply
                    /*
                    Task<string> task = doRequest(urlSuffix);
                    task.Wait();
                    string response = task.Result;
                    */
                
                    /*
                    string response = "[
                    {\"AlertTypeID\":4,\"Description\":\"Flood\"},
                    {\"AlertTypeID\":3,\"Description\":\"Hedge Cutter\"},
                    {\"AlertTypeID\":2,\"Description\":\"Horse Riders\"},
                    {\"AlertTypeID\":1,\"Description\":\"Road Traffic Accident\"}]";
                    List<alertTypeObject> tempAlertTypeList = JsonConvert.DeserializeObject<List<alertTypeObject>>(response);
                    */

                    List<alertTypeObject> tempAlertTypeList = new List<alertTypeObject>();
                    tempAlertTypeList.Add(new alertTypeObject(4,"Flood"));
                    tempAlertTypeList.Add(new alertTypeObject(3,"Hedge Cutter"));
                    tempAlertTypeList.Add(new alertTypeObject(2,"Horse Riders"));
                    tempAlertTypeList.Add(new alertTypeObject(1,"Road Traffic Accident"));
                    
                    //Copy the temporary list into lists
                    alertTypeList = tempAlertTypeList;
                    alertTypes= tempAlertTypeList;

                } catch(Exception ex) {
                    throw ex;
                }

                //Return
                return alertTypes;
            }
        }

        public static int updateAlert(User u,Alert a) {
            //api.php?type=updatealert &email= &password= &lat=53 &lng=-2 &alertid=&enddate=2020-02-21+20%3A28
            string urlSuffix =
                "?type=alerttypes"+ 
                " &email=" + u.getEmail() + 
                " &password=" + u.getPassword() + 
                " &lat=" + a.GetLat() + 
                " &lng=" + a.GetLon() + 
                " &alertid=" + a.getID() +
                " &enddate="+a.GetExipryTime().ToString("yyyy-MM-dd HH:mm:ss");
            Task<string> task = doRequest(urlSuffix);
            task.Wait();
            string response = task.Result;
            if(response.Equals("OK")) {
                return 1;
            } else if(response.Equals("Bad email or password")) {
                return 0;
            } else {
                return -1;
            }
        }

        public static async Task<string> testRequest() {
            try {
                HttpClient theclient = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler());
                string _url = "https://richard.keithsoft.com/proximity/api.php?type=alerttypes";
                var _uri = new Uri(_url);
                var response = await theclient.GetStringAsync(_uri);
                return response;
            } catch (HttpRequestException ex) {
                throw ex;
            }
        }
    }
}
