using JSIStudios.SimpleRESTServices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RHPA {
    class serverConnection{
        private static HttpClient _client;
        private static readonly string url = "http://richard.keithsoft.com/proximity/api.php";

        public serverConnection() {
            _client=new HttpClient();
        }

        /*
        public async Task<object> requestExample() {
            var uri = new Uri(url+"WhateverTheEndBitShouldBe");
            var response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            } else {
                return response.StatusCode.ToString();
            }
        }
        */

        public static async Task<int> addUser(User u, string name) {
            var uri = new Uri(url + 
                "?type=adduser &email="+u.getEmail()+
                " &password="+u.getPassword()+
                " &name="+name
                );
            var response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                return 1;
            } else {
                return int.Parse(response.StatusCode.ToString());
            }
        }

        public static async Task<int> clearAllAlerts(User u) {
            var uri = new Uri(url +
                "?type=clearalerts" + 
                " &email="+u.getEmail()+
                " &password="+u.getPassword()
                );
            var response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                return 1;
            } else {
                return int.Parse(response.StatusCode.ToString());
            }
        }

        public static async Task<int> createNewAlert(Alert a, User u) {
            var uri = new Uri(url +
                "?type=addalert&email=" + u.getEmail() + 
                " &password=" + u.getPassword() +
                " &alerttypeid=" + Alert.getAlertTypeIDFromName(a.GetAlertType()) +
                " &description=" + a.GetAlertType() +
                " &lat=" + a.GetLat() +
                " &lng=" + a.GetLon() + 
                " &radius=" + a.GetProximity() +
                " &enddate=" + a.GetExipryTime().ToString("yyyy-MM-dd HH:mm:ss")
                );
            var response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                return 1;
            } else {
                return int.Parse(response.StatusCode.ToString());
            }
        }

        public static async Task<List<Alert>> getAlertListAsync(string lat, string lon) {
            List<Alert> alertList = new List<Alert>(0);
            var uri = new Uri(url +
                "?type=alerts " + 
                " &lat=" + lat +  
                " &lng=-2.280 " + lon + 
                " &radius=500 "
                );
            var response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                
            }

            return alertList;
        }

        public static async Task<List<alertTypeObject>> getAlertTypes() {
            //Setup a list for the alert types
            List<alertTypeObject> alertTypes = new List<alertTypeObject>(0);

            //Create the url to go to the API with
            var uri = new Uri(url +
                "?type=alerttypes"
                );

            //await the reply
            var response = await _client.GetAsync(uri);
            
            //If it worked
            if(response.IsSuccessStatusCode) {

                //Read the content, and create a temporary list for the content to go into
                var content = await response.Content.ReadAsStringAsync();
                List<alertTypeObject> alertTypeList = new List<alertTypeObject>(0);

                //Regex pattern to get each individual part of the regex in case it's a list (Which it is here) and get the matches
                Regex pattern = new Regex("({)([^{}]*)(})");
                Match match = pattern.Match(content);

                //Create a list for the regex matches
                List<string> regexMatches = new List<string>(0);

                //Get all the matches and add them to the list
                while(match.Success) {
                    regexMatches.Add(match.Value);
                    match=match.NextMatch();
                }

                //For each match, deserialize that into an alertTypeObject, and add to list
                foreach(string s in regexMatches) {
                    alertTypeList.Add(JsonConvert.DeserializeObject<alertTypeObject>(s));
                }

                //Copy the temporary list into this list
                alertTypes=alertTypeList;
            }

            //Return
            return alertTypes;
        }

        public static async Task<int> updateAlert(User u,Alert a) {
            //api.php?type=updatealert &email= &password= &lat=53 &lng=-2 &alertid=&enddate=2020-02-21+20%3A28
            var uri = new Uri(url +
                "?type=alerttypes" + 
                " &email=" + u.getEmail() + 
                " &password=" + u.getPassword() + 
                " &lat=" + a.GetLat() + 
                " &lng=" + a.GetLon() + 
                " &alertid=" + a.getID() +
                " &enddate="+a.GetExipryTime().ToString("yyyy-MM-dd HH:mm:ss")
                );
            var response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                if(content.Equals("OK")) {
                    return 1;
                } else {
                    return 0;
                }
            } else {
                return int.Parse(response.StatusCode.ToString());
            }
        }
    }
}
