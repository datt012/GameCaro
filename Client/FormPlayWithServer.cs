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
    public partial class FormPlayWithServer : Form
    {
        private int quick = 600;
        private BoardWithServer boardWithServer;
        private System.Windows.Forms.Timer timerCountdown;

        ///<summary>
        ///@funtion FormPlayWithServer: FormPlayWithServer constructor
        ///<para></para>
        ///@param clientName: This client's username
        ///<para></para>
        public FormPlayWithServer(string clientName)
        {
            InitializeComponent();
            namePlayer.Text = clientName;
            nameServer.Text = "Server";
            boardWithServer = new BoardWithServer(this, namePlayer, nameServer);
            boardWithServer.clientTurn = Constants.TURN_O;
            boardWithServer.playerMarked += playerMarked;
            boardWithServer.serverMarked += serverMarked;
            prcbCoolDown.Step = Constants.COOL_DOWN_STEP;
            prcbCoolDown.Maximum = Constants.COOL_DOWN_TIME;
            prcbCoolDown.Value = 0;
            tmCoolDown.Interval = Constants.COOL_DOWN_INTERVAL;
            boardWithServer.drawBoard(panelBoard);
            this.changeActivePictureBox(Constants.TURN_O);
            timerCountdown = new System.Windows.Forms.Timer();
            timerCountdown.Interval = 1;
            timerCountdown.Tick += new EventHandler(timerEvent);
            timerCountdown.Enabled = true;
            EventManager.eventManager.Result += EventManager_Result;
            this.FormClosing += new FormClosingEventHandler(FormPlay_FormClosing);
            this.FormClosed += new FormClosedEventHandler(FormPlay_FormClosed);
        }

        ///<summary>
        ///@funtion EventManager_SignUp: Triggered when there is a reply from server about the match result
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void EventManager_Result(object sender, SuperEventArgs e)
        {
            FormMain.App.BeginInvoke((MethodInvoker)(() =>
            {
                tmCoolDown.Stop();
                timerCountdown.Stop();
                EventManager.eventManager.Result -= EventManager_Result;
                boardWithServer.playerMarked -= playerMarked;
                boardWithServer.serverMarked -= serverMarked;
                this.FormClosing -= FormPlay_FormClosing;
                if (String.Compare(e.ReturnText, namePlayer.Text) == 0) /// Player wins///
                {
                    MessageBox.Show("Congrats " + namePlayer.Text + ", you won!", "Winner", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (String.Compare(e.ReturnText, nameServer.Text) == 0) /// Server wins///
                {
                    MessageBox.Show("Sorry " + namePlayer.Text + ", you lost!", "Loser", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else MessageBox.Show("It's a draw!", "Draw", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); /// Draw///
                this.Close();
            }));
        }

        ///<summary>
        ///@funtion openSaveFileDialog: Open the saveFileDialog
        /// </summary>
        private void openSaveFileDialog()
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = FormMain.App.getSaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt";
            saveFileDialog.Title = "Choose a place to save match log";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                FileManager.startSaveFile(saveFileDialog);
                SocketManager.socketManager.sendData(new Message(Constants.OPCODE_FILE));
            }
        }

        ///<summary>
        ///@funtion FormPlay_FormClosing: Triggered when form is closing
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void FormPlay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            tmCoolDown.Stop();
            timerCountdown.Stop();
            this.FormClosing -= FormPlay_FormClosing;
            boardWithServer.playerMarked -= playerMarked;
            boardWithServer.serverMarked -= serverMarked;
            SocketManager.socketManager.sendData(new Message(Constants.OPCODE_SURRENDER_WITH_SERVER));
            MessageBox.Show("What a shame " + namePlayer.Text + ", you lost!", "Loser", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        ///<summary>
        ///@funtion FormPlay_FormClosed: Triggered when form is closed
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void FormPlay_FormClosed(object sender, FormClosedEventArgs e)
        {
            EventManager.eventManager.Result -= EventManager_Result;
            FormManager.openForm(Constants.FORM_MAIN);
            openSaveFileDialog();
            this.Close();
        }

        ///<summary>
        ///@funtion changeActivePictureBox: Change the active player
        ///<para></para>
        ///@param type: The type of player to change
        /// </summary>
        public void changeActivePictureBox(int type)
        {
            if (type == Constants.TURN_X)
            {
                pictureBoxX.Margin = new Padding(3, 3, 3, 10);
                pictureBoxO.Margin = new Padding(3, 3, 3, 0);
            }
            else
            {
                pictureBoxO.Margin = new Padding(3, 3, 3, 10);
                pictureBoxX.Margin = new Padding(3, 3, 3, 0);
            }

            if (type == boardWithServer.clientTurn)
            {
                changeStatus("It's your turn to move!");
            }
            else
            {
                changeStatus("Waiting server to make a move...");
            }
        }

        ///<summary>
        ///@funtion changeStatus: Change the toolStripStatusLabel content
        ///<para></para>
        ///@param status: The status content
        /// </summary>
        public void changeStatus(string status)
        {
            this.toolStripStatusLabel.Text = status;
        }

        ///<summary>
        ///@funtion tmCoolDownTick: Time for processbar 
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void tmCoolDownTick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep();
            if (prcbCoolDown.Value >= prcbCoolDown.Maximum)
            {
                tmCoolDown.Stop();
                timerCountdown.Stop();
                this.FormClosing -= FormPlay_FormClosing;
                boardWithServer.playerMarked -= playerMarked;
                boardWithServer.serverMarked -= serverMarked;
                SocketManager.socketManager.sendData(new Message(Constants.OPCODE_SURRENDER_WITH_SERVER));
                MessageBox.Show("What a shame " + namePlayer.Text + ", you lost!", "Loser", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
            }
        }

        ///<summary>
        ///@funtion timeEvent: Event timer count down
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void timerEvent(object sender, EventArgs e)
        {
            quick--;
            labelTime.Text = (((quick / 60) >= 10) ? (quick / 60).ToString() : "0" + (quick / 60)) + ":" + ((quick % 60) >= 10 ? (quick % 60).ToString() : "0" + (quick % 60));
            if (quick == 0) SocketManager.socketManager.sendData(new Message(Constants.OPCODE_TIMER_DRAW_WITH_SERVER));
        }

        ///<summary>
        ///@funtion serverMarked: Event process bar when server receive marked
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void serverMarked(object sender, EventArgs e)
        {
            prcbCoolDown.Value = 0;
            tmCoolDown.Start();
        }

        ///<summary>
        ///@funtion playerMarked: Event process bar when player marked
        ///<para></para>
        ///@param sender: The object that trigger the event
        ///<para></para>
        ///@param e: The events argument sent when the function is triggered
        /// </summary>
        private void playerMarked(object sender, EventArgs e)
        {
            tmCoolDown.Stop();
            prcbCoolDown.Value = 0;
        }
    }

    

}
