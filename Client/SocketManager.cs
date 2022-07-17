using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public class SocketManager
    {
        Socket client;

        //@funtion connectServer: connect to server
        //@return true if success, false if false
        public bool connectServer() {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(Cons.IP), Cons.port);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                client.Connect(iep);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //@funtion closeSocket: close the socket
        public void closeSocket() {
            if (client == null) return;
            client.Close();
        }

        //@funtion sendData: send data to server
        //@param data
        //@return ret: length of data 
        public int sendData(string data) {
            var sendBuffer = Encoding.ASCII.GetBytes(data);
            int ret = client.Send(sendBuffer);
            return ret;
        }

        //@funtion receiveData: receive data from server
        //@return revBuff: buffer containing the data
        private string receiveData() {
            string rcvBuff;
            int size;
            byte[] receiveBuffer = new byte[Cons.BUFFER_SIZE];
            int lengthRcv = client.Receive(receiveBuffer);
            rcvBuff = Encoding.ASCII.GetString(receiveBuffer, 0, lengthRcv);

            string length = rcvBuff.Substring(Cons.OPCODE_SIZE, Cons.LENGTH_SIZE);
            size = Convert.ToInt32(length);
            size -= lengthRcv - Cons.OPCODE_SIZE - Cons.LENGTH_SIZE;

            while (size != 0)
            {
                receiveBuffer = new byte[Cons.BUFFER_SIZE];
                lengthRcv = client.Receive(receiveBuffer);
                rcvBuff = Encoding.ASCII.GetString(receiveBuffer, 0, lengthRcv);

                length = rcvBuff.Substring(Cons.OPCODE_SIZE, Cons.LENGTH_SIZE);
                size -= lengthRcv;
            }
            return rcvBuff;
        }

        //@funtion Listen: listen message from data
        //@param eventManager: event object that will notify to system when received a message
        private void Listen(EventManager eventManager) {
            try
            {
                string rcvBuff;
                rcvBuff = receiveData();
                processData(rcvBuff, eventManager);
             }
             catch { }    
        }

        //@funtion ListenThread: create a thread to listen from server
        //@param eventManager: event object that will notify to system when received a message
        public void ListenThread(EventManager eventManager) {
            Thread listenThread = new Thread(() =>
            {
                Listen(eventManager);
            });
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        //@funtion processData: process data received
        //@param mess: message received
        //@param eventManager: event object that will notify to system when received a message
        private void processData(string mess, EventManager eventManager) {
            Message rcvMess = new Message(mess);

            int opcode = Convert.ToInt32(rcvMess.Opcode);
            string payload = rcvMess.Payload;
            //MessageBox.Show(mess);
            switch (opcode)
            {
                case 11:case 12:case 13:
                    eventManager.notifSignup(opcode);
                    break;
                case 21:case 22:case 23:case 24:
                    eventManager.notifSignin(opcode);
                    break;
                case 31:case 32:
                    eventManager.notifSignout(opcode);
                    break;
                case 41:
                    eventManager.notifList(payload);
                    break;
                case 50:
                    eventManager.notifInvite(payload);
                    break;
                case 51:case 52:case 53:case 54:case 55:case 61:              
                    eventManager.notifRespone(opcode, payload);
                    break;
                case 71:                    
                    eventManager.notifMove(payload);
                    break;
                case 90:
                    eventManager.notifResult(payload);
                    break;
                default:
                    break;
            }
        }
    }
}
