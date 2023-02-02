using System;
using System.Windows.Forms;
namespace Client
{
    partial class FormPlay : System.Windows.Forms.Form
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


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelBoard = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxX = new System.Windows.Forms.PictureBox();
            this.pictureBoxO = new System.Windows.Forms.PictureBox();
            this.namePlayer1 = new System.Windows.Forms.Label();
            this.namePlayer2 = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.prcbCoolDown = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tmCoolDown = new System.Windows.Forms.Timer(this.components);
            this.statusStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxO)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBoard
            // 
            this.panelBoard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelBoard.Location = new System.Drawing.Point(12, 4);
            this.panelBoard.Margin = new System.Windows.Forms.Padding(0);
            this.panelBoard.Name = "panelBoard";
            this.panelBoard.Size = new System.Drawing.Size(320, 320);
            this.panelBoard.TabIndex = 0;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 415);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(344, 26);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(143, 20);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxX, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxO, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.namePlayer1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.namePlayer2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelTime, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.prcbCoolDown, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 324);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(336, 95);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pictureBoxX
            // 
            this.pictureBoxX.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pictureBoxX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxX.Enabled = false;
            this.pictureBoxX.Image = global::Client.Properties.Resources.x;
            this.pictureBoxX.Location = new System.Drawing.Point(255, 22);
            this.pictureBoxX.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pictureBoxX.Name = "pictureBoxX";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBoxX, 2);
            this.pictureBoxX.Size = new System.Drawing.Size(42, 42);
            this.pictureBoxX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxX.TabIndex = 2;
            this.pictureBoxX.TabStop = false;
            // 
            // pictureBoxO
            // 
            this.pictureBoxO.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pictureBoxO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxO.Enabled = false;
            this.pictureBoxO.Image = global::Client.Properties.Resources.o;
            this.pictureBoxO.Location = new System.Drawing.Point(37, 22);
            this.pictureBoxO.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pictureBoxO.Name = "pictureBoxO";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBoxO, 2);
            this.pictureBoxO.Size = new System.Drawing.Size(42, 42);
            this.pictureBoxO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxO.TabIndex = 3;
            this.pictureBoxO.TabStop = false;
            // 
            // namePlayer1
            // 
            this.namePlayer1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.namePlayer1.AutoSize = true;
            this.namePlayer1.BackColor = System.Drawing.Color.Transparent;
            this.namePlayer1.Location = new System.Drawing.Point(30, 71);
            this.namePlayer1.Name = "namePlayer1";
            this.namePlayer1.Size = new System.Drawing.Size(56, 16);
            this.namePlayer1.TabIndex = 6;
            this.namePlayer1.Text = "Player 1";
            // 
            // namePlayer2
            // 
            this.namePlayer2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.namePlayer2.AutoSize = true;
            this.namePlayer2.BackColor = System.Drawing.Color.Transparent;
            this.namePlayer2.Location = new System.Drawing.Point(248, 71);
            this.namePlayer2.Name = "namePlayer2";
            this.namePlayer2.Size = new System.Drawing.Size(56, 16);
            this.namePlayer2.TabIndex = 7;
            this.namePlayer2.Text = "Player 2";
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.Location = new System.Drawing.Point(120, 68);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(94, 23);
            this.labelTime.TabIndex = 9;
            this.labelTime.Text = "60:00";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // prcbCoolDown
            // 
            this.prcbCoolDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.prcbCoolDown.Location = new System.Drawing.Point(120, 31);
            this.prcbCoolDown.Name = "prcbCoolDown";
            this.prcbCoolDown.Size = new System.Drawing.Size(94, 18);
            this.prcbCoolDown.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panelBoard, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(344, 441);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // tmCoolDown
            // 
            this.tmCoolDown.Tick += new System.EventHandler(this.tmCoolDownTick);
            // 
            // FormPlay
            // 
            this.ClientSize = new System.Drawing.Size(344, 441);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tableLayoutPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPlay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Match";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxO)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public Panel panelBoard;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBoxX;
        private PictureBox pictureBoxO;
        private TableLayoutPanel tableLayoutPanel2;
        public Label namePlayer1;
        public Label namePlayer2;
        private ProgressBar prcbCoolDown;
        private Timer tmCoolDown;
        private Label labelTime;
    }
}