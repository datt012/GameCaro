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
        private static SocketManager _socketManager;
        private static Socket client;
        ///
        ///
        ///
        public static SocketManager socketManager
        {
            get
            {
                if (_socketManager == null)
                {
                    _socketManager = new SocketManager();
                }
                return _socketManager;
            }
        }

        ///<summary>
        ///@funtion connectServer: connect to server
        ///<para></para>
        ///@return true if success, false if false
        /// </summary>
        public void connectServer() {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(Constants.IP), Constants.port);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(iep);
        }

        ///<summary>
        ///@funtion closeSocket: Close
        /// </summary>
        public void closeSocket() {
            if (client == null) return;
            client.Close();
        }

        ///<summary>
        ///@funtion sendData: Send message to server
        ///<para></para>
        ///@param aMessage: The message to send
        /// </summary>
        public int sendData(Message aMessage) {
            byte[] sendBuff, opcode, length;
            sendBuff = new byte[Constants.BUFFER_SIZE];
            opcode = BitConverter.GetBytes(aMessage.Opcode);
            length = BitConverter.GetBytes(aMessage.Length);
            opcode.CopyTo(sendBuff, 0);
            length.CopyTo(sendBuff, Constants.OPCODE_SIZE);
            aMessage.Payload.CopyTo(sendBuff, Constants.OPCODE_SIZE + Constants.LENGTH_SIZE);
            int ret, bytesToSend, bytesSent;
            ret = 0;
            bytesSent = 0;
            bytesToSend = aMessage.Length + Constants.OPCODE_SIZE + Constants.LENGTH_SIZE;
            while(bytesSent < bytesToSend){
                ret = client.Send(sendBuff, bytesToSend - bytesSent, 0);
                if(ret <= 0) break;
                bytesSent += ret;
            }
            return ret;
        }

        ///<summary>
        ///@funtion recvData: The Receive() wrapper
        /// </summary>
        

        ///<summary>
        ///@funtion Listen: Listen for message server
        /// </summary>
        private void Listen()
        {
            while(true)
            {
                Message aMessage = recvData();
                if (aMessage == null) break;
                processRecv(aMessage);
            }
        }

        ///<summary>
        ///@funtion ListenThread: Start a new thread to listen to server 
        /// </summary>
        public void ListenThread()
        {
            Thread listenThread = new Thread(() =>
            {
                try
                {
                    Listen();

                } catch
                {

                }
            });
            listenThread.IsBackground = true;
            listenThread.Name = "Listen To Server";
            listenThread.Start();
        }

        ///<summary>
        ///@funtion processData: process data received
        ///<para></para>
        ///@param mess: message received
        ///<para></para>
        ///@param eventManager: event object that will notify to system when received a message
        /// </summary>
        private void processRecv(Message aMessage) {
            byte opcode = aMessage.Opcode;
            string payload = System.Text.Encoding.Default.GetString(aMessage.Payload, 0, aMessage.Length);
            // Handle background 
            if (opcode == Constants.OPCODE_FILE_DATA)
            {
                if (aMessage.Length == 0)
                {
                    FileManager.saveFile();
                    MessageBox.Show("Your match log is ready.", "Download completed");
                    SocketManager.socketManager.sendData(new Message(Constants.OPCODE_LIST));
                    SocketManager.socketManager.sendData(new Message(Constants.OPCODE_INFO, (ushort)FormMain.App.getPlayerName().Length, FormMain.App.getPlayerName()));
                }
                else
                {
                    FileManager.appendToBuff(aMessage.Payload, aMessage.Length);
                }
                return;
            }

            // Handle to foregound
            switch (FormManager.currentForm)
            {
                case Constants.FORM_MAIN:
                    processRecvMain(aMessage);
                    break;
                case Constants.FORM_PLAY:
                    processRecvPlay(aMessage);
                    break;
                case Constants.FORM_ACCOUNT:
                    processRecvAccount(aMessage);
                    break;
                default:
                    break;
            }

        }

        ///<summary>
        ///@funtion processRecvMain: process data received while in MainForm
        ///<para></para>
        ///@param mess: The message received
        /// </summary>
        private void processRecvMain(Message aMessage)
        {
            byte opcode = aMessage.Opcode;
            string payload = System.Text.Encoding.Default.GetString(aMessage.Payload, 0, aMessage.Length);
            switch(opcode) {
                case Constants.OPCODE_SIGN_OUT_SUCCESS:
                case Constants.OPCODE_SIGN_OUT_NOT_LOGGED_IN:
                case Constants.OPCODE_SIGN_OUT_ERROR_UNKNOWN:
                    EventManager.eventManager.notifySignout(opcode);
                    break;
                case Constants.OPCODE_LIST_REPLY:
                    EventManager.eventManager.notifyList(payload);
                    break;
                case Constants.OPCODE_CHALLENGE:
                    EventManager.eventManager.notifyInvite(payload);
                    break;
                case Constants.OPCODE_CHALLENGE_ACCEPT:
                case Constants.OPCODE_CHALLENGE_REFUSE:
                case Constants.OPCODE_CHALLENGE_INVALID_RANK:
                case Constants.OPCODE_CHALLENGE_BUSY:
                case Constants.OPCODE_CHALLENGE_NOT_FOUND:
                case Constants.OPCODE_CHALLENGE_DUPLICATED_USERNAME:
                    EventManager.eventManager.notifyChallenge(opcode, payload);
                    break;
                case Constants.OPCODE_INFO_FOUND:
                case Constants.OPCODE_INFO_NOT_FOUND:
                    EventManager.eventManager.notifyInfo(opcode, payload);
                    break;
                default:
                    break;
            }
        }

        ///<summary>
        ///@funtion processRecvMain: process data received while in FormAccount
        ///<para></para>
        ///@param mess: The message received
        /// </summary>
        private void processRecvAccount(Message aMessage)
        {
            byte opcode = aMessage.Opcode;
            string payload = System.Text.Encoding.Default.GetString(aMessage.Payload, 0, aMessage.Length);
            switch (opcode)
            {
                case Constants.OPCODE_SIGN_IN_SUCESS:
                case Constants.OPCODE_SIGN_IN_ALREADY_LOGGED_IN:
                case Constants.OPCODE_SIGN_IN_USERNAME_NOT_FOUND:
                case Constants.OPCODE_SIGN_IN_INVALID_USERNAME:
                case Constants.OPCODE_SIGN_IN_INVALID_PASSWORD:
                case Constants.OPCODE_SIGN_IN_WRONG_PASSWORD:
                case Constants.OPCODE_SIGN_IN_UNKNOWN_ERROR:
                    EventManager.eventManager.notifySignIn(opcode);
                    break;
                case Constants.OPCODE_SIGN_UP_SUCESS:
                case Constants.OPCODE_SIGN_UP_DUPLICATED_USERNAME:
                case Constants.OPCODE_SIGN_UP_INVALID_USERNAME:
                case Constants.OPCODE_SIGN_UP_INVALID_PASSWORD:
                case Constants.OPCODE_SIGN_UP_UNKNOWN_ERROR:
                    EventManager.eventManager.notifySignUp(opcode);
                    break; 
                default:
                    break;
            }
        }

        ///<summary>
        ///@funtion processRecvMain: process data received while in FormPlay
        ///<para></para>
        ///@param mess: The message received
        /// </summary>
        private void processRecvPlay(Message aMessage)
        {
            byte opcode = aMessage.Opcode;
            string payload = System.Text.Encoding.Default.GetString(aMessage.Payload, 0, aMessage.Length);

            switch (opcode)
            {
                case Constants.OPCODE_PLAY_OPPONENT:
                    EventManager.eventManager.notifyMove(payload);
                    break;
                case Constants.OPCODE_RESULT:
                    EventManager.eventManager.notifyResult(payload);
                    break;
                default:
                    break;
            }
        }
    }
}
