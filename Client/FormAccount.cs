using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Client
{
    public partial class FormAccount : Form
    {
        public FormAccount()
        {
            InitializeComponent();
            EventManager.eventManager.SignIn += EventManager_SignIn;
            this.FormClosed += new FormClosedEventHandler(FormAccount_FormClosed);
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
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
                        panelSignIn.Visible = true;
                        panelSignUp.Visible = false;
                        break;
                    case Constants.OPCODE_SIGN_UP_INVALID_USERNAME:
                        MessageBox.Show("Invalid username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case Constants.OPCODE_SIGN_UP_INVALID_PASSWORD:
                        MessageBox.Show("Invalid password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case Constants.OPCODE_SIGN_UP_DUPLICATED_USERNAME:
                        MessageBox.Show("Username is already used!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("Sign up failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    SocketManager.socketManager.sendData(new Message(Constants.OPCODE_INFO, (ushort)usernameTextBoxIn.Text.Length, usernameTextBoxIn.Text));
                    FormMain.App.setPlayerName(this.usernameTextBoxIn.Text);
                    FormManager.openForm(Constants.FORM_MAIN);
                }
                else
                {
                    EventManager.eventManager.SignIn += EventManager_SignIn;
                    switch (e.ReturnCode)
                    {
                        case Constants.OPCODE_SIGN_IN_ALREADY_LOGGED_IN:
                            MessageBox.Show("Already login!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case Constants.OPCODE_SIGN_IN_INVALID_USERNAME:
                            MessageBox.Show("Invalid username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case Constants.OPCODE_SIGN_IN_INVALID_PASSWORD:
                            MessageBox.Show("Invalid password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case Constants.OPCODE_SIGN_IN_USERNAME_NOT_FOUND:
                            MessageBox.Show("Can't find username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case Constants.OPCODE_SIGN_IN_WRONG_PASSWORD:
                            MessageBox.Show("Incorrect password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        default:
                            MessageBox.Show("Sign in failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if(!string.IsNullOrEmpty(this.usernameTextBoxIn.Text) && !usernameTextBoxIn.Text.Contains(" ") && 
               !string.IsNullOrEmpty(this.passwordTextBoxIn.Text) && !passwordTextBoxIn.Text.Contains(" "))
            {
                string payload = usernameTextBoxIn.Text + " " + hashPassword(passwordTextBoxIn.Text);
                Message sentMessage = new Message(Constants.OPCODE_SIGN_IN, (ushort)payload.Length, payload);
                SocketManager.socketManager.sendData(sentMessage);
            }  
        }

        ///<summary>
        ///@funtion signUpButton_Click: Triggered when the button sign up is clicked
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void signUpButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.usernameTextBoxUp.Text) && !usernameTextBoxUp.Text.Contains(" ") &&
                !string.IsNullOrEmpty(this.passwordTextBoxUp.Text) && !passwordTextBoxUp.Text.Contains(" ") &&
                !string.IsNullOrEmpty(this.repasswordTextBoxUp.Text) && !repasswordTextBoxUp.Text.Contains(" "))
            {
                string payload = usernameTextBoxUp.Text + " " + hashPassword(passwordTextBoxUp.Text);
                if (!passwordTextBoxUp.Text.Equals(repasswordTextBoxUp.Text)) MessageBox.Show("Password and repassword are not the same", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    EventManager.eventManager.SignUp += EventManager_SignUp;
                    Message sentMessage = new Message(Constants.OPCODE_SIGN_UP, (ushort)payload.Length, payload);
                    SocketManager.socketManager.sendData(sentMessage);
                }
            }   
        }

        ///<summary>
        ///@funtion userNameTextBox_Validating: Validate username when sign in
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void usernameTextBoxIn_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (string.IsNullOrEmpty(this.usernameTextBoxIn.Text))
            {
                error = "Please enter an username!";
                e.Cancel = true;
            }
            else if (usernameTextBoxIn.Text.Contains(" "))
            {
                error = "Username contains invalid character!";
                e.Cancel = true;
            }
            errorProvider.SetError((Control)sender, error);
        }

        ///<summary>
        ///@funtion userNameTextBox_Validating: Validate username when sign up
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void usernameTextBoxUp_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (string.IsNullOrEmpty(this.usernameTextBoxUp.Text))
            {
                error = "Please enter an username!";
                e.Cancel = true;
            }
            else if (usernameTextBoxUp.Text.Contains(" "))
            {
                error = "Username contains invalid character!";
                e.Cancel = true;
            }
            errorProvider.SetError((Control)sender, error);
        }

        ///<summary>
        ///@funtion passwordTextBox_Validating: Validate password when sign in
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void passwordTextBoxIn_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (string.IsNullOrEmpty(this.passwordTextBoxIn.Text))
            {
                error = "Please enter a password!";
                e.Cancel = true;
            }
            else if (passwordTextBoxIn.Text.Contains(" "))
            {
                error = "Password contains invalid character!";
                e.Cancel = true;
            }
            errorProvider.SetError((Control)sender, error);
        }

        ///<summary>
        ///@funtion passwordTextBox_Validating: Validate password when sign up
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void passwordTextBoxUp_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (string.IsNullOrEmpty(this.passwordTextBoxUp.Text) || string.IsNullOrEmpty(this.repasswordTextBoxUp.Text))
            {
                error = "Please enter a password!";
                e.Cancel = true;
            }
            else if (passwordTextBoxUp.Text.Contains(" ") || repasswordTextBoxUp.Text.Contains(" "))
            {
                error = "Password contains invalid character!";
                e.Cancel = true;
            }
            errorProvider.SetError((Control)sender, error);
        }
        ///<summary>
        ///@funtion hashPassword: Hasing password before send to server
        ///<para></para>
        ///@param password: Password of user
        ///<para></para>
        ///@return : Password hashed
        /// </summary>
        private string hashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }

        ///<summary>
        ///@funtion backButton_Click: Triggered when the button back is clicked
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void backButton_Click(object sender, EventArgs e)
        {

            panelSignIn.Visible = true;
            panelSignUp.Visible = false;
        }

        ///<summary>
        ///@funtion FormAccount_Load: Triggered when the form account is loading
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        private void FormAccount_Load(object sender, EventArgs e)
        {
            panelSignIn.Visible = true;
            panelSignUp.Visible = false;
        }

        ///<summary>
        ///@funtion linkLabel_Click: Triggered when the link label is clicked
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void linkLabel_Click(object sender, EventArgs e)
        {
            this.linkLabelSignUp.LinkVisited = true;
            panelSignIn.Visible = false;
            panelSignUp.Visible = true;
        }
    }
}