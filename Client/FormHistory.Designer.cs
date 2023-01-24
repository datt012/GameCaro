namespace Client
{
    partial class FormHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.listHistory = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.player1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.player2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matchEndBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.winner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.listHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // listHistory
            // 
            this.listHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.listHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.listHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.player1,
            this.player2,
            this.matchEndBy,
            this.winner,
            this.timeStart,
            this.timeEnd});
            this.listHistory.Location = new System.Drawing.Point(0, 101);
            this.listHistory.Name = "listHistory";
            this.listHistory.RowHeadersVisible = false;
            this.listHistory.RowHeadersWidth = 51;
            this.listHistory.RowTemplate.Height = 24;
            this.listHistory.Size = new System.Drawing.Size(1159, 395);
            this.listHistory.TabIndex = 0;
            this.listHistory.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.listHistory_RowPostPaint);
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.id.FillWeight = 20F;
            this.id.HeaderText = "ID";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
            // 
            // player1
            // 
            this.player1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.player1.FillWeight = 70F;
            this.player1.HeaderText = "Player 1";
            this.player1.MinimumWidth = 10;
            this.player1.Name = "player1";
            // 
            // player2
            // 
            this.player2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.player2.FillWeight = 70F;
            this.player2.HeaderText = "Player 2";
            this.player2.MinimumWidth = 10;
            this.player2.Name = "player2";
            // 
            // matchEndBy
            // 
            this.matchEndBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.matchEndBy.FillWeight = 70F;
            this.matchEndBy.HeaderText = "Match End By";
            this.matchEndBy.MinimumWidth = 10;
            this.matchEndBy.Name = "matchEndBy";
            // 
            // winner
            // 
            this.winner.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.winner.FillWeight = 70F;
            this.winner.HeaderText = "Winner";
            this.winner.MinimumWidth = 10;
            this.winner.Name = "winner";
            // 
            // timeStart
            // 
            this.timeStart.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.timeStart.HeaderText = "Time Start";
            this.timeStart.MinimumWidth = 10;
            this.timeStart.Name = "timeStart";
            // 
            // timeEnd
            // 
            this.timeEnd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.timeEnd.HeaderText = "Time End";
            this.timeEnd.MinimumWidth = 10;
            this.timeEnd.Name = "timeEnd";
            // 
            // FormHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 533);
            this.Controls.Add(this.listHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormHistory";
            this.Text = "History Match";
            ((System.ComponentModel.ISupportInitialize)(this.listHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView listHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn player1;
        private System.Windows.Forms.DataGridViewTextBoxColumn player2;
        private System.Windows.Forms.DataGridViewTextBoxColumn matchEndBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn winner;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeEnd;
    }
}