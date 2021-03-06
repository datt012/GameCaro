using System;
namespace Client
{
    public class EventManager
    {
        private event EventHandler<SuperEventArgs> _signup;
        private event EventHandler<SuperEventArgs> _signin;
        private event EventHandler<SuperEventArgs> _respone;
        private event EventHandler<SuperEventArgs> _move;
        private event EventHandler<SuperEventArgs> _result;
        private event EventHandler<SuperEventArgs> _invite;
        private event EventHandler<SuperEventArgs> _list;
        private event EventHandler<SuperEventArgs> _signout;


        public event EventHandler<SuperEventArgs> Signup
        {
            add
            {
                _signup += value;
            }
            remove
            {
                _signup -= value;
            }
        }
        public event EventHandler<SuperEventArgs> Signin
        {
            add {
                _signin += value;
            }
            remove {
                _signin -= value;
            }
        }

        public event EventHandler<SuperEventArgs> Signout
        {
            add
            {
                _signout += value;
            }
            remove
            {
                _signout -= value;
            }
        }

        public event EventHandler<SuperEventArgs> Respone {
            add {
                _respone += value;
            }
            remove {
                _respone -= value;
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
        public void notifSignup(int result)
        {
            if (_signup != null)
                _signup(this, new SuperEventArgs(result));
        }

        //@funtion notifLogin: notify the login result to the event object when receiving a message
        //@param result: result
        public void notifSignin(int result) {
            if (_signin != null)
                _signin(this, new SuperEventArgs(result));
        }

        public void notifSignout(int result)
        {
            if (_signout != null)
                _signout(this, new SuperEventArgs(result));
        }

        //@funtion notifRespone: notify the respond of other player to the event objecct when receiving a message
        //@param code: opcode of the meassage
        //@param name: name of the other player
        public void notifRespone(int code, string name) {
            if (_respone != null)
                _respone(this, new SuperEventArgs(code, name));
        }

        //@funtion notifMove: notify the move of opponent to the event objecct when receiving a message
        //@param move: string containing position of the move
        public void notifMove(string move) {
            if (_move != null)
            {
                _move(this, new SuperEventArgs(move));
            }
               
        }

        //@funtion notifResult: notify the result of the game to the event object
        //@param name: name of the winner 
        public void notifResult(string name) {
            if (_result != null)
                _result(this, new SuperEventArgs(name));
        }

        //@funtion notifInvite: notify the challenger received
        //@param name: name of player challenging you
        public void notifInvite(string name) {
            if (_invite != null)
                _invite(this, new SuperEventArgs(name));
        }

        //@funtion notifList: notify the list player to event object
        //@param listname: string containing the list
        public void notifList(string listname) {
            if (_list != null)
                _list(this, new SuperEventArgs(listname));
        }
    }

    public class SuperEventArgs : EventArgs
    {
        private int returnCode;
        private string returnText;

        public SuperEventArgs(int returnCode) {
            this.ReturnCode = returnCode;
        }

        public SuperEventArgs(string returnName) {
            this.ReturnText = returnName;
        }

        public SuperEventArgs(int returnCode, string returnName) {
            this.returnCode = returnCode;
            this.returnText = returnName;
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
