namespace Client
{
    partial class FormPassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPassword));
            this.panelSignUp = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.oldPasswordTextBox = new System.Windows.Forms.TextBox();
            this.newPasswordTextBox = new System.Windows.Forms.TextBox();
            this.newRepasswordTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.changePasswordButton = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelSignUp.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSignUp
            // 
            this.panelSignUp.Controls.Add(this.tableLayoutPanel3);
            this.panelSignUp.Controls.Add(this.tableLayoutPanel4);
            resources.ApplyResources(this.panelSignUp, "panelSignUp");
            this.panelSignUp.Name = "panelSignUp";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.oldPasswordTextBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.newPasswordTextBox, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.newRepasswordTextBox, 1, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // oldPasswordTextBox
            // 
            resources.ApplyResources(this.oldPasswordTextBox, "oldPasswordTextBox");
            this.oldPasswordTextBox.Name = "oldPasswordTextBox";
            this.oldPasswordTextBox.UseSystemPasswordChar = true;
            this.oldPasswordTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.oldPasswordTextBox_Validating);
            // 
            // newPasswordTextBox
            // 
            resources.ApplyResources(this.newPasswordTextBox, "newPasswordTextBox");
            this.newPasswordTextBox.Name = "newPasswordTextBox";
            this.newPasswordTextBox.UseSystemPasswordChar = true;
            this.newPasswordTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.newPasswordTextBox_Validating);
            // 
            // newRepasswordTextBox
            // 
            resources.ApplyResources(this.newRepasswordTextBox, "newRepasswordTextBox");
            this.newRepasswordTextBox.Name = "newRepasswordTextBox";
            this.newRepasswordTextBox.UseSystemPasswordChar = true;
            this.newRepasswordTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.newRepasswordTextBox_Validating);
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.changePasswordButton, 1, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // changePasswordButton
            // 
            resources.ApplyResources(this.changePasswordButton, "changePasswordButton");
            this.changePasswordButton.Name = "changePasswordButton";
            this.changePasswordButton.UseVisualStyleBackColor = true;
            this.changePasswordButton.Click += new System.EventHandler(this.changePasswordButton_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // FormPassword
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelSignUp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPassword";
            this.panelSignUp.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelSignUp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox newPasswordTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox newRepasswordTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button changePasswordButton;
        private System.Windows.Forms.TextBox oldPasswordTextBox;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}