using JSIStudios.SimpleRESTServices.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RHPA {
    class serverConnection{
        HttpClient _client;
        static readonly string url = "https://richard.keithsoft.com/proximity/api.php";


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

        public async Task<int> addUser(User u, string name) {
            var uri = new Uri(url + 
                "?type=adduser&email="+u.getEmail()+
                "&password="+u.getPassword()+
                "&name="+name
                ));
            var response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                return 1;
            } else {
                return int.Parse(response.StatusCode.ToString());
            }
        }

        public async Task<int> clearAllAlerts(User u) {
            var uri = new Uri(url +
                "?type=clearalerts&email="+u.getEmail()+
                "&password="+u.getPassword()
                ));
            var response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                return 1;
            } else {
                return int.Parse(response.StatusCode.ToString());
            }
        }

        public async Task<int> createNewAlert(Alert a, User u) {
            var uri = new Uri(url +
                "?type=addalert&email=" + u.getEmail() + 
                "&password=" + u.getPassword() +
                "&alerttypeid=" + Alert.getAlertTypeIDFromName(a.GetAlertType()) +
                "&description=" + a.GetAlertType() +
                "&lat=" + a.GetLat() +
                "&lng=" + a.GetLon() + 
                "&radius=" + a.GetProximity() +
                "&enddate=" + a.GetExipryTime().ToString("yyyy-MM-dd HH:mm:ss")
                ));
            var response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                return 1;
            } else {
                return int.Parse(response.StatusCode.ToString());
            }
        }

        public async Task<List<Alert>> getAlertListAsync() {
            var uri = new Uri(url +
                "?type=alerttypes"
                ));
            var response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                //Parse the string
            }
        }

        public async Task<List<string>> getAlertTypes() {
            var uri = new Uri(url +
                "?type=alerttypes"
                ));
            var response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                //Parse the string
            }
        }

        public async Task<int> updateAlert(User u,Alert a) {
            var uri = new Uri(url +
                "?type=alerttypes"
                ));
            var response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                //Parse the string
            }
        }
    }

    

}
