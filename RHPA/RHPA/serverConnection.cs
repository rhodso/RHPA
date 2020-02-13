using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace RHPA {
    class serverConnection {
        private static string targetIP;
        private static int targetPort;

        public serverConnection() {}
        public serverConnection(string _targetIP,int _targetPort) {
            targetIP=_targetIP;
            targetPort=_targetPort;
        }
        
        public void sendMessage(string message) {
            try {
                TcpClient client = new TcpClient(targetIP,targetPort);

                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data,0,data.Length);

                // Close everything.
                stream.Close();
                client.Close();
            } catch(ArgumentNullException e) {
                throw e;
            } catch(SocketException e) {
                throw e;
            }
        }

        public string sendRevcMessage(string message) {
            string reply = null;

            try {
                TcpClient client = new TcpClient(targetIP,targetPort);

                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data,0,data.Length);

                Console.WriteLine("Sent: {0}",message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data=new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data,0,data.Length);
                responseData=System.Text.Encoding.ASCII.GetString(data,0,bytes);

                reply=responseData;

                // Close everything.
                stream.Close();
                client.Close();
            } catch(ArgumentNullException e) {
                throw e;
            } catch(SocketException e) {
                throw e;
            }

            return reply;
        }

    }
}
