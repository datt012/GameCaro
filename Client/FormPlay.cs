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
    public partial class FormPlay : Form
    {
        private const int PLAYER_1 = 1;
        private const int PLAYER_2 = 2;
        public static string player1;
        public static string player2;
        private Board board;

        ///<summary>
        ///@funtion FormPlay: FormPlay constructor
        ///<para></para>
        ///@param opponentName: This client's opponent username
        ///<para></para>
        ///@param clientName: This client's username
        ///<para></para>
        ///@param clientGoFirst: If this client go first or it's opponent
        /// </summary>
        public FormPlay(string opponentName, string clientName, bool clientGoFirst)
        {
            InitializeComponent();
            if (clientGoFirst)
            {
                player1 = clientName;
                player2 = opponentName;
            } 
            else
            {
                player1 = opponentName;
                player2 = clientName;
            }
            namePlayer1.Text = player1;
            namePlayer2.Text = player2;
            board = new Board(this, namePlayer1, namePlayer2);
            board.clientTurn = clientGoFirst ? Constants.TURN_O : Constants.TURN_X;
            board.drawBoard(panelBoard);
            this.changeActivePictureBox(Constants.TURN_O);
            EventManager.eventManager.Result += EventManager_Result;
            this.FormClosing += FormPlay_FormClosing;
            this.FormClosed += FormPlay_FormClosed;
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
                if (String.Compare(e.ReturnText, namePlayer1.Text) == 0) /// Player 1 wins///
                {
                    if (this.board.clientTurn == Constants.TURN_O) MessageBox.Show("Congrats " + namePlayer1.Text + ", you won!");
                    else MessageBox.Show("Sorry " + namePlayer2.Text + ", you lost!");
                }
                else if (String.Compare(e.ReturnText, namePlayer2.Text) == 0) /// Player 2 wins///
                {
                    if (this.board.clientTurn == Constants.TURN_X) MessageBox.Show("Congrats " + namePlayer2.Text + ", you won!");
                    else MessageBox.Show("Sorry " + namePlayer1.Text + ", you lost!");
                }
                else MessageBox.Show("It's a draw!"); /// Draw///
                EventManager.eventManager.Result -= EventManager_Result;
                this.FormClosing -= FormPlay_FormClosing;
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
            if (MessageBox.Show("Are you sure about giving up?", "Question", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK) e.Cancel = true;
            else {
                this.FormClosing -= FormPlay_FormClosing;
                if (this.board.clientTurn == 1) MessageBox.Show("What a shame " + namePlayer1.Text + ", you lost!");
                else MessageBox.Show("What a shame " + namePlayer2.Text + ", you lost!");
                SocketManager.socketManager.sendData(new Message(Constants.OPCODE_SURRENDER));
            }
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
            SocketManager.socketManager.sendData(new Message(Constants.OPCODE_INFO_ONL));
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
            if(type == Constants.TURN_X)
            {
                pictureBoxX.Margin = new Padding(3, 3, 3, 10);
                pictureBoxO.Margin = new Padding(3, 3, 3, 0);
            }
            else
            {
                pictureBoxO.Margin = new Padding(3, 3, 3, 10);
                pictureBoxX.Margin = new Padding(3, 3, 3, 0);
            }

            if(type == board.clientTurn)
            {
                changeStatus("It's your turn to move!");
            } else
            {
                changeStatus("Waiting your opponent to make a move...");
            }
        }

        ///<summary>
        ///@funtion changeStatus: Change the toolStripStatusLabel1 content
        ///<para></para>
        ///@param status: The status content
        /// </summary>
        public void changeStatus(string status)
        {
            this.toolStripStatusLabel1.Text = status;
        }
    }
}
