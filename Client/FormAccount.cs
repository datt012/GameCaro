using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class FormAccount : Form
    {
        public FormAccount()
        {
            InitializeComponent();
            EventManager.eventManager.SignIn += EventManager_SignIn;
            this.FormClosed += new FormClosedEventHandler(FormAccount_FormClosed);

            this.userNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.userNameTextBox_Validating);
            this.passwordTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.passwordTextBox_Validating);
        }

        ///<summary>
        ///@funtion FormAccount_FormClosed: Triggered when form is closed
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void FormAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        ///<summary>
        ///@funtion EventManager_SignUp: Triggered when there is a reply from server after sign-up
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void EventManager_SignUp(object sender, SuperEventArgs e)
        {
            EventManager.eventManager.SignUp -= EventManager_SignUp;
            FormMain.App.BeginInvoke((MethodInvoker)(() =>
            {
                switch (e.ReturnCode)
                {
                    case Constants.OPCODE_SIGN_UP_SUCESS:
                        MessageBox.Show("Account has been created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case Constants.OPCODE_SIGN_UP_INVALID_USERNAME:
                        MessageBox.Show("Invalid username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case Constants.OPCODE_SIGN_UP_INVALID_PASSWORD:
                        MessageBox.Show("Invalid password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case Constants.OPCODE_SIGN_UP_DUPLICATED_USERNAME:
                        MessageBox.Show("Username is already used!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    default:
                        MessageBox.Show("Sign up failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }));
        }

        ///<summary>
        ///@funtion EventManager_SignIn: Triggered when there is a reply from server after sign-in
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void EventManager_SignIn(object sender, SuperEventArgs e)
        {
            EventManager.eventManager.SignIn -= EventManager_SignIn;
            FormMain.App.BeginInvoke((MethodInvoker)(() =>
            {
                if (e.ReturnCode == Constants.OPCODE_SIGN_IN_SUCESS)
                {
                    this.FormClosed -= FormAccount_FormClosed;
                    this.Close();
                    SocketManager.socketManager.sendData(new Message(Constants.OPCODE_LIST));
                    SocketManager.socketManager.sendData(new Message(Constants.OPCODE_INFO, (ushort)userNameTextBox.Text.Length, userNameTextBox.Text));
                    FormMain.App.setPlayerName(this.userNameTextBox.Text);
                    FormManager.openForm(Constants.FORM_MAIN);
                }
                else
                {
                    EventManager.eventManager.SignIn += EventManager_SignIn;
                    switch (e.ReturnCode)
                    {
                        case Constants.OPCODE_SIGN_IN_ALREADY_LOGGED_IN:
                            MessageBox.Show("Already login!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case Constants.OPCODE_SIGN_IN_INVALID_USERNAME:
                            MessageBox.Show("Invalid username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case Constants.OPCODE_SIGN_IN_INVALID_PASSWORD:
                            MessageBox.Show("Invalid password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case Constants.OPCODE_SIGN_IN_USERNAME_NOT_FOUND:
                            MessageBox.Show("Can't find username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case Constants.OPCODE_SIGN_IN_WRONG_PASSWORD:
                            MessageBox.Show("Incorrect password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        default:
                            MessageBox.Show("Sign in failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
            }));
        }

        ///<summary>
        ///@funtion signInButton_Click: Triggered when the signInButton is clicked
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void signInButton_Click(object sender, EventArgs e)
        {
            string username = userNameTextBox.Text;
            if (username.Length < 20)
            {
                for (int i = username.Length; i < 20; i++)
                {
                    username = username + " ";
                }
            }
            string payload = username + Constants.SPACE + passwordTextBox.Text;
            Message sentMessage = new Message(Constants.OPCODE_SIGN_IN, (ushort)payload.Length, payload);
            SocketManager.socketManager.sendData(sentMessage);
        }

        ///<summary>
        ///@funtion signUpButton_Click: Triggered when the signUpButton is clicked
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void signUpButton_Click(object sender, EventArgs e)
        {
            EventManager.eventManager.SignUp += EventManager_SignUp;
            string username = userNameTextBox.Text;
            if (username.Length < 20)
            {
                for (int i = username.Length; i < 20; i++)
                {
                    username = username + " ";
                }
            }
            string payload = username + Constants.SPACE + passwordTextBox.Text;
            Message sentMessage = new Message(Constants.OPCODE_SIGN_UP, (ushort)payload.Length, payload);
            SocketManager.socketManager.sendData(sentMessage);
        }

        ///<summary>
        ///@funtion userNameTextBox_Validating: Validate username when submitted
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        void userNameTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (userNameTextBox.Text.Length == 0)
            {
                error = "Please enter an username!";
                e.Cancel = true;
            }
            else if (userNameTextBox.Text.Contains(" "))
            {
                error = "Username contains invalid character!";
                e.Cancel = true;
            }
            errorProvider1.SetError((Control)sender, error);
        }

        ///<summary>
        ///@funtion passwordTextBox_Validating: Validate password when submitted
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        void passwordTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (passwordTextBox.Text.Length == 0)
            {
                error = "Please enter a password!";
                e.Cancel = true;
            }
            else if (passwordTextBox.Text.Contains(" "))
            {
                error = "Password contains invalid character!";
                e.Cancel = true;
            }
            errorProvider1.SetError((Control)sender, error);
        }

    }
}
