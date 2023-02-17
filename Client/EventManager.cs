using System;
namespace Client
{
    public class EventManager
    {
        private static EventManager _eventManager;
        private event EventHandler<SuperEventArgs> _signup;
        private event EventHandler<SuperEventArgs> _signin;
        private event EventHandler<SuperEventArgs> _challenge;
        private event EventHandler<SuperEventArgs> _info;
        private event EventHandler<SuperEventArgs> _move;
        private event EventHandler<SuperEventArgs> _result;
        private event EventHandler<SuperEventArgs> _invite;
        private event EventHandler<SuperEventArgs> _list;
        private event EventHandler<SuperEventArgs> _signout;
        private event EventHandler<SuperEventArgs> _history;
        private event EventHandler<SuperEventArgs> _server;
        private event EventHandler<SuperEventArgs> _password;

        public static EventManager eventManager {
            get
            {
                if (_eventManager == null)
                {
                    _eventManager = new EventManager();
                }
                return _eventManager;
            }
        }

        public event EventHandler<SuperEventArgs> SignUp {
            add
            {
                _signup += value;
            }
            remove
            {
                _signup -= value;
            }
        }
        public event EventHandler<SuperEventArgs> SignIn {
            add {
                _signin += value;
            }
            remove {
                _signin -= value;
            }
        }

        public event EventHandler<SuperEventArgs> SignOut {
            add
            {
                _signout += value;
            }
            remove
            {
                _signout -= value;
            }
        }

        public event EventHandler<SuperEventArgs> Challenge {
            add {
                _challenge += value;
            }
            remove {
                _challenge -= value;
            }
        }

        public event EventHandler<SuperEventArgs> Info {
            add
            {
                _info += value;
            }
            remove
            {
                _info -= value;
            }
        }

        public event EventHandler<SuperEventArgs> Result {
            add {
                _result += value;
            }
            remove {
                _result -= value;
            }
        }

        public event EventHandler<SuperEventArgs> Move {
            add {
                _move += value;
            }
            remove {
                _move -= value;
            }
        }

        public event EventHandler<SuperEventArgs> Invite {
            add {
                _invite += value;
            }
            remove {
                _invite -= value;
            }
        }

        public event EventHandler<SuperEventArgs> List {
            add {
                _list += value;
            }
            remove {
                _list -= value;
            }
        }

        public event EventHandler<SuperEventArgs> History {
            add
            {
                _history += value;
            }
            remove
            {
                _history -= value;
            }
        }

        public event EventHandler<SuperEventArgs> Server
        {
            add
            {
                _server += value;
            }
            remove
            {
                _server -= value;
            }
        }

        public event EventHandler<SuperEventArgs> Password
        {
            add
            {
                _password += value;
            }
            remove
            {
                _password -= value;
            }
        }

        ///<summary>
        ///@funtion notifySignUp: Notify the sign up result to the event object when receiving a message
        ///<para></para>
        ///@param code: Opcode of the meassage
        ///</summary>
        public void notifySignUp(int result)
        {
            if (_signup != null)
                _signup(this, new SuperEventArgs(result));
        }

        ///<summary>
        ///@funtion notifySignIn: Notify the sign in result to the event object when receiving a message
        ///<para></para>
        ///@param code: Opcode of the meassage
        ///</summary>
        public void notifySignIn(int code) {
            if (_signin != null)
                _signin(this, new SuperEventArgs(code));
        }

        ///<summary>
        ///@funtion notifySignOut: Notify the sign out result to the event object when receiving a message
        ///<para></para>
        ///@param code: Opcode of the meassage
        ///</summary>
        public void notifySignout(int code)
        {
            if (_signout != null)
                _signout(this, new SuperEventArgs(code));
        }

        ///<summary>
        ///@funtion notifyChallenge: Notify the respond of other player to the event objecct when receiving a message
        ///<para></para>
        ///@param code: Opcode of the meassage
        ///@param name: Name of the other player
        ///</summary>
        public void notifyChallenge(int code, string name) {
            if (_challenge != null)
                _challenge(this, new SuperEventArgs(code, name));
        }

        /// <summary>
        ///@funtion notifyInfo: Notify the info player to the event object
        ///<para></para>
        ///@param code: Opcode of the meassage 
        /// </summary>
        public void notifyInfo(string info)
        {
            if (_info != null)
                _info(this, new SuperEventArgs(info));
        }

        ///<summary>
        ///@funtion notifyMove: Notify the move of opponent to the event object when receiving a message
        ///<para></para>
        ///@param move: String containing position of the move
        /// </summary>
        public void notifyMove(string move) {
            if (_move != null)
            {
                _move(this, new SuperEventArgs(move));
            }           
        }

        ///<summary>
        ///@funtion notifyResult: Notify the result of the game to the event object
        ///<para></para>
        ///@param name: Name of the winner 
        ///</summary>
        public void notifyResult(string name) {
            if (_result != null)
                _result(this, new SuperEventArgs(name));
        }

        ///<summary>
        ///@funtion notifyInvite: Notify the challenger received 
        ///<para></para>
        ///@param name: Name of player who is sending challenge
        ///</summary>
        public void notifyInvite(string name) {
            if (_invite != null)
                _invite(this, new SuperEventArgs(name));
        }

        ///<summary>
        ///@funtion notifyList: Notify the list player to the event object
        ///<para></para>
        ///@param listInfo: String containing the list info
        /// </summary>
        public void notifyList(string listInfo) {
            if (_list != null)
                _list(this, new SuperEventArgs(listInfo));
        }

        ///<summary>
        ///@funtion notifyHistory: Notify the list history match to the event object
        ///<para></para>
        ///@param listHistory: String containing the list history
        /// </summary>
        public void notifyHistory(string listHistory)
        {
            if (_history != null)
                _history(this, new SuperEventArgs(listHistory));
        }

        ///<summary>
        ///@funtion notifyServer: Notify the respond of server to the event object when receiving a message
        ///<para></para>
        ///@param code: Opcode of the meassage
        ///</summary>
        public void notifyServer(int code)
        {
            if (_server != null)
                _server(this, new SuperEventArgs(code));
        }

        ///<summary>
        ///@funtion notifyPassword: Notify the result change password to the event object when receiving a message
        ///<para></para>
        ///@param code: Opcode of the meassage
        ///</summary>
        public void notifyPassword(int code)
        {
            if (_password != null)
                _password(this, new SuperEventArgs(code));
        }
    }

    public class SuperEventArgs : EventArgs
    {
        private int returnCode;
        private string returnText;

        ///<summary>
        ///@funtion SuperEventArgs: 
        ///<para></para>
        ///@param returnCode: string containing the return code 
        /// </summary>
        public SuperEventArgs(int returnCode) {

            this.ReturnCode = returnCode;
        }

        ///<summary>
        ///@funtion SuperEventArgs: 
        ///<para></para>
        ///@param returnName: string containing the text
        /// </summary>
        public SuperEventArgs(string returnText) {
            this.ReturnText = returnText;
        }

        ///<summary>
        ///@funtion SuperEventArgs: 
        ///<para></para>
        ///@param returnCode: string containing the return code 
        ///@param returnName: string containing the text 
        /// </summary>
        public SuperEventArgs(int returnCode, string returnText) {
            this.ReturnCode = returnCode;
            this.ReturnText = returnText;
        }

        public int ReturnCode {
            get {
                return returnCode;
            }

            set {
                returnCode = value;
            }
        }

        public string ReturnText {
            get {
                return returnText;
            }

            set {
                returnText = value;
            }
        }
    }
}
