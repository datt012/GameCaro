using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class FormPassword : Form
    {
        public FormPassword()
        {
            InitializeComponent();
            EventManager.eventManager.Password += EventManager_Password;
            this.FormClosed += new FormClosedEventHandler(FormPasword_FormClosed);
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

        ///<summary>
        ///@funtion EventManager_Password: Triggered when there is a reply from server after the result change password
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void EventManager_Password(object sender, SuperEventArgs e)
        {
            FormMain.App.BeginInvoke((MethodInvoker)(() =>
            {
                if (e.ReturnCode == Constants.OPCODE_CHANGE_PASSWORD_SUCCESS)
                {
                    MessageBox.Show("Change password success!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    switch (e.ReturnCode)
                    {
                        case Constants.OPCODE_CHANGE_PASSWORD_INVALID:
                            MessageBox.Show("Invalid password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case Constants.OPCODE_CHANGE_PASSWORD_WRONG:
                            MessageBox.Show("Incorrect old password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case Constants.OPCODE_CHANGE_PASSWORD_OLDNEW:
                            MessageBox.Show("Old password and new password are the same!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case Constants.OPCODE_CHANGE_DIFFERENT_NEWPASSWORD:
                            MessageBox.Show("New password and new repassword are different!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        default:
                            break;
                    }
                }
            }));
        }

        ///<summary>
        ///@funtion changePasswordButton_Click: Triggered when the button change password is clicked
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        private void changePasswordButton_Click(object sender, EventArgs e)
        {
            string payload = hashPassword(oldPasswordTextBox.Text) + " " + hashPassword(newPasswordTextBox.Text) + " " + hashPassword(newRepasswordTextBox.Text);
            Message sentMessage = new Message(Constants.OPCODE_CHANGE_PASSWORD, (ushort)payload.Length, payload);
            SocketManager.socketManager.sendData(sentMessage);
        }

        ///<summary>
        ///@funtion FormPassword_FormClosed: Triggered when form password is closed
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void FormPasword_FormClosed(object sender, FormClosedEventArgs e)
        {
            EventManager.eventManager.Password -= EventManager_Password;
            FormManager.openForm(Constants.FORM_MAIN);
            this.Close();
        }

        ///<summary>
        ///@funtion oldPasswordTextBox_Validating: Validate old password
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void oldPasswordTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (string.IsNullOrEmpty(this.oldPasswordTextBox.Text))
            {
                error = "Please enter an old password!";
                e.Cancel = true;
            }
            errorProvider.SetError((Control)sender, error);
        }

        ///<summary>
        ///@funtion newPasswordTextBox_Validating: Validate new password
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void newPasswordTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (string.IsNullOrEmpty(this.newPasswordTextBox.Text))
            {
                error = "Please enter a new password!";
                e.Cancel = true;
            }
            errorProvider.SetError((Control)sender, error);
        }

        ///<summary>
        ///@funtion newRepasswordTextBox_Validating: Validate new repassword 
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void newRepasswordTextBox_Validating(object sender, CancelEventArgs e)
        {
            string error = null;
            if (string.IsNullOrEmpty(this.newRepasswordTextBox.Text))
            {
                error = "Please enter a new repassword!";
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
    }
}
