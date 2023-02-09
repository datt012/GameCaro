using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class FormMain : Form
    {
        private static FormMain _app;
        public string playerName;
        public string opponentName;
        private bool isFree;
        private Dictionary<string, KeyValuePair<string, string>> dictionary = new Dictionary<string, KeyValuePair<string, string>>();

        /// <summary>
        /// Store one instance of FormMain
        /// </summary>
        public static FormMain App
        {
            get
            {
                if (_app == null)
                {
                    _app = new FormMain();
                }
                return _app;
            }
        }

        public FormMain() {
            InitializeComponent();
            EventManager.eventManager.Challenge += EventManager_Challenge;
            EventManager.eventManager.Server += EventManager_Server;
            EventManager.eventManager.Invite += EventManager_Invite;
            EventManager.eventManager.List += EventManager_List;
            EventManager.eventManager.SignOut += EventManager_SignOut;
            EventManager.eventManager.Info += EventManager_Info;
            EventManager.eventManager.History += EventManager_History;
            this.isFree = false;
            this.Shown += FormMain_Shown;
            this.FormClosing += new FormClosingEventHandler(FormMain_FormClosing);
            this.FormClosed += new FormClosedEventHandler(FormMain_FormClosed);
        }

        ///<summary>
        ///@funtion setPlayerName: Change the player name
        ///<para></para>
        ///@param name: The player's username
        /// </summary>
        public void setPlayerName(string name)
        {
            this.userNameInfo.Text = name;
            this.toolStripStatusLabel.Text = "Welcome player " + name + "!";
        }

        ///<summary>
        ///@funtion getSaveFileDialog: Get the form SaveFileDialog instance
        ///<para></para>
        ///@return The form's SaveFileDialog
        /// </summary>
        public System.Windows.Forms.SaveFileDialog getSaveFileDialog()
        {
            return this.saveFileDialog;
        }

        /// <summary>
        ///@funtion getPlayerName: Get the the player's username
        /// </summary>
        public string getPlayerName()
        {
            return this.userNameInfo.Text;
        }

        ///<summary>
        ///@funtion FormMain_Shown: Triggered when the FormMain instance is shown the first time
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        public void FormMain_Shown(Object sender, EventArgs e)
        {
            FormManager.openForm(Constants.FORM_ACCOUNT);
            this.isFree = true;
        }

        ///<summary>
        ///@funtion FormMain_FormClosing: Triggered when the FormMain instance is closing
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        public void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Confirm exit?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.OK)
                e.Cancel = true;
        }

        ///<summary>
        ///@funtion FormMain_FormClosed: Triggered when the FormMain instance is closed
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SocketManager.socketManager.closeSocket();
            Application.Exit();
        }

        ///<summary>
        ///@funtion signOutButton_Click: Triggered when the signOutButton is clicked
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void signOutButton_Click(object sender, EventArgs e) {
            DialogResult dialogResult = MessageBox.Show("Do you confirm to log out?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.listPlayer.Clear();
                signOutButton.Enabled = false;
                SocketManager.socketManager.sendData(new Message(Constants.OPCODE_SIGN_OUT));
            }
        }

        ///<summary>
        ///@funtion historyButton_Click: Triggered when the historyButton is clicked
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void historyButton_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FormHistory>().FirstOrDefault() != null)
            {
                Application.OpenForms.OfType<FormHistory>().FirstOrDefault().Close();
            }
            else
            {
                SocketManager.socketManager.sendData(new Message(Constants.OPCODE_HISTORY));
            }
        }
        private void listPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listPlayer.SelectedItems.Count > 0)
            {
                challengedPlayerName.Text = listPlayer.SelectedItems[0].Text;
                var valuePair = dictionary[challengedPlayerName.Text];
                string score = valuePair.Key;
                string rank = valuePair.Value;
                listPlayer.SelectedItems[0].ToolTipText = "Score: " + score + "\nRank: " + rank;
            }
            else
            {
                challengedPlayerName.Text = "";
            }
        }
        private void challengeBtn_Click(object sender, EventArgs e)
        {
            string challengedUsername = challengedPlayerName.Text;
            opponentName = challengedUsername;
            Message sentMessage = new Message(Constants.OPCODE_CHALLENGE, (ushort) challengedUsername.Length, challengedUsername);
            SocketManager.socketManager.sendData(sentMessage);
        }

        ///<summary>
        ///@funtion EventManager_Challenge: Triggered when there is a reply from server after a player sent or received a challenge
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void EventManager_Challenge(object sender, SuperEventArgs e) 
        {
            FormMain.App.BeginInvoke((MethodInvoker)(() =>
            {
                if (e.ReturnCode == Constants.OPCODE_CHALLENGE_ACCEPT)
                {
                    this.Hide();
                    bool formHistoryAlready = false;
                    if (Application.OpenForms.OfType<FormHistory>().FirstOrDefault() != null)
                    {
                        Application.OpenForms.OfType<FormHistory>().FirstOrDefault().Close();
                        formHistoryAlready = true;
                    }
                    SocketManager.socketManager.sendData(new Message(Constants.OPCODE_LIST));
                    if (String.Compare(e.ReturnText, "") == 0)
                    { 
                        FormManager.openForm(Constants.FORM_PLAY, e);  
                    }
                    else
                    {
                        opponentName = e.ReturnText;
                        FormManager.openForm(Constants.FORM_PLAY, e);
                    }
                    this.Show();
                    SocketManager.socketManager.sendData(new Message(Constants.OPCODE_LIST));
                    if(formHistoryAlready == true)
                    {
                        SocketManager.socketManager.sendData(new Message(Constants.OPCODE_HISTORY));
                    }  
                }
                else
                {
                    if (e.ReturnCode == Constants.OPCODE_CHALLENGE_REFUSE)
                    {
                        MessageBox.Show("Your challenge is refused!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (e.ReturnCode == Constants.OPCODE_CHALLENGE_INVALID_RANK)
                    {
                        MessageBox.Show("Rank difference can't be more than 10 !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (e.ReturnCode == Constants.OPCODE_CHALLENGE_BUSY)
                    {
                        MessageBox.Show("The player is playing!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (e.ReturnCode == Constants.OPCODE_CHALLENGE_NOT_FOUND)
                    {
                        MessageBox.Show("Sorry, we can't find that player!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }));
        }
        ///<summary>
        ///@funtion EventManager_Info: Triggered when there is a reply from server the player's info
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void EventManager_Info(object sender, SuperEventArgs e)
        {
            FormMain.App.BeginInvoke((MethodInvoker)(() =>
            {
                string[] words = e.ReturnText.Split(' ');
                userRankInfo.Text = words[1];
                userScoreInfo.Text = words[0];
            }));
        }

        ///<summary>
        ///@funtion EventManager_Info: Triggered when there is a reply from server contain invitation from another players
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void EventManager_Invite(object sender, SuperEventArgs e)
        {
            this.opponentName = e.ReturnText;
            if(!isFree)
            {
                Message sentMessage = new Message(Constants.OPCODE_CHALLENGE_REFUSE, (ushort)opponentName.Length, opponentName);
                SocketManager.socketManager.sendData(sentMessage);
                return;
            }
            string msg = opponentName.Substring(0,opponentName.Length-1) + " sent a challenged. Accept?";
            DialogResult dialogResult = MessageBox.Show(msg, "Challenge incoming!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);   
            if (dialogResult == DialogResult.Yes)
            {
                Message sentMessage = new Message(Constants.OPCODE_CHALLENGE_ACCEPT, (ushort) opponentName.Length, opponentName);
                SocketManager.socketManager.sendData(sentMessage);
            }
            else if (dialogResult == DialogResult.No)
            {
                Message sentMessage = new Message(Constants.OPCODE_CHALLENGE_REFUSE, (ushort) opponentName.Length, opponentName);
                SocketManager.socketManager.sendData(sentMessage);
            }                
        }

        ///<summary>
        ///@funtion EventManager_List: Triggered when there is a reply from server the online players list
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void EventManager_List(object sender, SuperEventArgs e)
        {
            FormMain.App.BeginInvoke((MethodInvoker)(() =>
            {
                challengedPlayerName.Text = "";
                string listInfos = e.ReturnText;
                listPlayer.Items.Clear();
                dictionary.Clear();
                string[] listInfo = listInfos.Split('|');
                if(listInfo.Length > 1)
                {
                    this.playerListStatus.Hide();
                    for (int i = 0; i < listInfo.Length - 1; i++)
                    {
                        string[] listItem = listInfo[i].Split(',');
                        listPlayer.Items.Add(listItem[0]);
                        dictionary.Add(listItem[0], new KeyValuePair<string, string>(listItem[1], listItem[2]));
                    }
                }
                else
                {
                    this.playerListStatus.Show();
                }
            }));
            
        }

        private void EventManager_History(object sender, SuperEventArgs e)
        {
            FormMain.App.BeginInvoke((MethodInvoker)(() =>
            {
                FormManager.openForm(Constants.FORM_HISTORY, e);
                historyButton.Enabled = true;
            }));
        }

        private void EventManager_Server(object sender, SuperEventArgs e)
        {
            FormMain.App.BeginInvoke((MethodInvoker)(() =>
            {
                if (e.ReturnCode == Constants.OPCODE_CHALLENGE_WITH_SERVER_PLAY)
                {
                    this.Hide();
                    bool formHistoryAlready = false;
                    if (Application.OpenForms.OfType<FormHistory>().FirstOrDefault() != null)
                    {
                        Application.OpenForms.OfType<FormHistory>().FirstOrDefault().Close();
                        formHistoryAlready = true;
                    }
                    SocketManager.socketManager.sendData(new Message(Constants.OPCODE_LIST));
                    FormManager.openForm(Constants.FORM_PLAY_WITH_SERVER);
                    this.Show();
                    SocketManager.socketManager.sendData(new Message(Constants.OPCODE_LIST));
                    if (formHistoryAlready == true)
                    {
                        SocketManager.socketManager.sendData(new Message(Constants.OPCODE_HISTORY));
                    }
                }
                else
                {
                    MessageBox.Show("Sorry, server is overload", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));
        }

        ///<summary>
        ///@funtion EventManager_SignOut: Triggered when there is a reply from server after player wants to sign out
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void EventManager_SignOut(object sender, SuperEventArgs e)
        {
            FormMain.App.BeginInvoke((MethodInvoker)(() =>
            {
                signOutButton.Enabled = true;
                if (e.ReturnCode == Constants.OPCODE_SIGN_OUT_SUCCESS)
                {
                    SocketManager.socketManager.sendData(new Message(Constants.OPCODE_LIST));
                    FormManager.openForm(Constants.FORM_ACCOUNT, e);
                }
                else if (e.ReturnCode == Constants.OPCODE_SIGN_OUT_NOT_LOGGED_IN)
                {
                    MessageBox.Show("You didn't log in!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));
        }

        ///<summary>
        ///@funtion changeStatus: Change the toolStripStatusLabel1 content
        /// </summary>
        private void changeStatus(string status)
        {
            this.toolStripStatusLabel.Text = status;
        }

        private void buttonPlayWithServer_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to play with Server?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Message sentMessage = new Message(Constants.OPCODE_CHALLENGE_WITH_SERVER);
                SocketManager.socketManager.sendData(sentMessage);  
            }
        }
    }
}

