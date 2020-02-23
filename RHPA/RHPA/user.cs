using System;
using System.Collections.Generic;
using System.Text;

namespace RHPA {
    class User {
        private string email;
        private string password;

        public User(string username,string password) {
            this.email=username??throw new ArgumentNullException(nameof(username));
            this.password=password??throw new ArgumentNullException(nameof(password));
        }

        public string getEmail() { return this.email; }
        public string getPassword() { return this.password; }
        public void setEmail(string _email) { this.email = _email; }
        public void setPassword(string _password) { this.password = _password; }
       
    }
}
