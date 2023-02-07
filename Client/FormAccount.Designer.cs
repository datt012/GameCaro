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
    partial class FormAccount
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAccount));
            this.panelSignIn = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.usernameTextBoxIn = new System.Windows.Forms.TextBox();
            this.passwordTextBoxIn = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.signInButton = new System.Windows.Forms.Button();
            this.linkLabelSignUp = new System.Windows.Forms.LinkLabel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.usernameTextBoxUp = new System.Windows.Forms.TextBox();
            this.passwordTextBoxUp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.repasswordTextBoxUp = new System.Windows.Forms.TextBox();
            this.panelSignUp = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.backButton = new System.Windows.Forms.Button();
            this.signUpButton = new System.Windows.Forms.Button();
            this.panelSignIn.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.panelSignUp.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSignIn
            // 
            this.panelSignIn.Controls.Add(this.label1);
            this.panelSignIn.Controls.Add(this.tableLayoutPanel1);
            this.panelSignIn.Controls.Add(this.tableLayoutPanel2);
            this.panelSignIn.Controls.Add(this.linkLabelSignUp);
            this.panelSignIn.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelSignIn.Location = new System.Drawing.Point(1, 2);
            this.panelSignIn.Margin = new System.Windows.Forms.Padding(4);
            this.panelSignIn.Name = "panelSignIn";
            this.panelSignIn.Size = new System.Drawing.Size(405, 247);
            this.panelSignIn.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 44, 4, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Welcome to Caro Game";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.63636F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.usernameTextBoxIn, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.passwordTextBoxIn, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 89);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(401, 66);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Username";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Password";
            // 
            // usernameTextBoxIn
            // 
            this.usernameTextBoxIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameTextBoxIn.Location = new System.Drawing.Point(166, 5);
            this.usernameTextBoxIn.Margin = new System.Windows.Forms.Padding(4);
            this.usernameTextBoxIn.Name = "usernameTextBoxIn";
            this.usernameTextBoxIn.Size = new System.Drawing.Size(136, 22);
            this.usernameTextBoxIn.TabIndex = 2;
            this.usernameTextBoxIn.WordWrap = false;
            this.usernameTextBoxIn.Validating += new System.ComponentModel.CancelEventHandler(this.usernameTextBoxIn_Validating);
            // 
            // passwordTextBoxIn
            // 
            this.passwordTextBoxIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBoxIn.Location = new System.Drawing.Point(166, 38);
            this.passwordTextBoxIn.Margin = new System.Windows.Forms.Padding(4);
            this.passwordTextBoxIn.Name = "passwordTextBoxIn";
            this.passwordTextBoxIn.Size = new System.Drawing.Size(136, 22);
            this.passwordTextBoxIn.TabIndex = 3;
            this.passwordTextBoxIn.UseSystemPasswordChar = true;
            this.passwordTextBoxIn.WordWrap = false;
            this.passwordTextBoxIn.Validating += new System.ComponentModel.CancelEventHandler(this.passwordTextBoxIn_Validating);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.signInButton, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 155);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(401, 44);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // signInButton
            // 
            this.signInButton.AutoSize = true;
            this.signInButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.signInButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.signInButton.Location = new System.Drawing.Point(124, 4);
            this.signInButton.Margin = new System.Windows.Forms.Padding(4);
            this.signInButton.Name = "signInButton";
            this.signInButton.Size = new System.Drawing.Size(153, 36);
            this.signInButton.TabIndex = 0;
            this.signInButton.Text = "Sign In";
            this.signInButton.UseVisualStyleBackColor = true;
            this.signInButton.Click += new System.EventHandler(this.signInButton_Click);
            // 
            // linkLabelSignUp
            // 
            this.linkLabelSignUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelSignUp.AutoSize = true;
            this.linkLabelSignUp.Location = new System.Drawing.Point(3, 203);
            this.linkLabelSignUp.Name = "linkLabelSignUp";
            this.linkLabelSignUp.Size = new System.Drawing.Size(403, 16);
            this.linkLabelSignUp.TabIndex = 4;
            this.linkLabelSignUp.TabStop = true;
            this.linkLabelSignUp.Text = "You don\'t have account ? Sign up";
            this.linkLabelSignUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelSignUp.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabelSignUp.Click += new System.EventHandler(this.linkLabel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.63636F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel3.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label6, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.usernameTextBoxUp, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.passwordTextBoxUp, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.label7, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.repasswordTextBoxUp, 2, 2);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 89);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(401, 99);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(84, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Username";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(84, 41);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Password";
            // 
            // usernameTextBoxUp
            // 
            this.usernameTextBoxUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameTextBoxUp.Location = new System.Drawing.Point(166, 5);
            this.usernameTextBoxUp.Margin = new System.Windows.Forms.Padding(4);
            this.usernameTextBoxUp.Name = "usernameTextBoxUp";
            this.usernameTextBoxUp.Size = new System.Drawing.Size(136, 22);
            this.usernameTextBoxUp.TabIndex = 2;
            this.usernameTextBoxUp.WordWrap = false;
            this.usernameTextBoxUp.Validating += new System.ComponentModel.CancelEventHandler(this.usernameTextBoxUp_Validating);
            // 
            // passwordTextBoxUp
            // 
            this.passwordTextBoxUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBoxUp.Location = new System.Drawing.Point(166, 38);
            this.passwordTextBoxUp.Margin = new System.Windows.Forms.Padding(4);
            this.passwordTextBoxUp.Name = "passwordTextBoxUp";
            this.passwordTextBoxUp.Size = new System.Drawing.Size(136, 22);
            this.passwordTextBoxUp.TabIndex = 3;
            this.passwordTextBoxUp.UseSystemPasswordChar = true;
            this.passwordTextBoxUp.WordWrap = false;
            this.passwordTextBoxUp.Validating += new System.ComponentModel.CancelEventHandler(this.passwordTextBoxUp_Validating);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(84, 74);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Repass";
            // 
            // repasswordTextBoxUp
            // 
            this.repasswordTextBoxUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.repasswordTextBoxUp.Location = new System.Drawing.Point(166, 71);
            this.repasswordTextBoxUp.Margin = new System.Windows.Forms.Padding(4);
            this.repasswordTextBoxUp.Name = "repasswordTextBoxUp";
            this.repasswordTextBoxUp.Size = new System.Drawing.Size(136, 22);
            this.repasswordTextBoxUp.TabIndex = 5;
            this.repasswordTextBoxUp.UseSystemPasswordChar = true;
            this.repasswordTextBoxUp.WordWrap = false;
            this.repasswordTextBoxUp.Validating += new System.ComponentModel.CancelEventHandler(this.passwordTextBoxUp_Validating);
            // 
            // panelSignUp
            // 
            this.panelSignUp.Controls.Add(this.label4);
            this.panelSignUp.Controls.Add(this.tableLayoutPanel3);
            this.panelSignUp.Controls.Add(this.tableLayoutPanel4);
            this.panelSignUp.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelSignUp.Location = new System.Drawing.Point(1, 2);
            this.panelSignUp.Margin = new System.Windows.Forms.Padding(4);
            this.panelSignUp.Name = "panelSignUp";
            this.panelSignUp.Size = new System.Drawing.Size(405, 247);
            this.panelSignUp.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(53, 44);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 44, 4, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(302, 31);
            this.label4.TabIndex = 2;
            this.label4.Text = "Welcome to Caro Game";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel4.Controls.Add(this.backButton, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.signUpButton, 2, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 188);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(401, 44);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // backButton
            // 
            this.backButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.backButton.AutoSize = true;
            this.backButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backButton.Location = new System.Drawing.Point(124, 6);
            this.backButton.Margin = new System.Windows.Forms.Padding(4);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(72, 32);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // signUpButton
            // 
            this.signUpButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.signUpButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.signUpButton.Location = new System.Drawing.Point(204, 6);
            this.signUpButton.Margin = new System.Windows.Forms.Padding(4);
            this.signUpButton.Name = "signUpButton";
            this.signUpButton.Size = new System.Drawing.Size(72, 32);
            this.signUpButton.TabIndex = 1;
            this.signUpButton.Text = "Sign Up";
            this.signUpButton.UseVisualStyleBackColor = true;
            this.signUpButton.Click += new System.EventHandler(this.signUpButton_Click);
            // 
            // FormAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 251);
            this.Controls.Add(this.panelSignIn);
            this.Controls.Add(this.panelSignUp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Caro";
            this.panelSignIn.ResumeLayout(false);
            this.panelSignIn.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panelSignUp.ResumeLayout(false);
            this.panelSignUp.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelSignIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox usernameTextBoxIn;
        private System.Windows.Forms.TextBox passwordTextBoxIn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private FlowLayoutPanel panelSignUp;
        private Label label4;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label5;
        private Label label6;
        private TextBox usernameTextBoxUp;
        private TextBox passwordTextBoxUp;
        private Label label7;
        private TextBox repasswordTextBoxUp;
        private TableLayoutPanel tableLayoutPanel4;
        private Button backButton;
        private Button signUpButton;
        private Button signInButton;
        private LinkLabel linkLabelSignUp;
    }
}
