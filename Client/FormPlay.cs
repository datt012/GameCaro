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
        ChessBoardManager chessBoard;
        SocketManager client;
        EventManager eventManager;
        int num;

        public FormPlay(string name1, string name2, SocketManager client, EventManager eventManager, int num)
        {
            InitializeComponent();

            namePlayer1.Text = name1;
            namePlayer2.Text = name2;

            this.eventManager = eventManager;
            this.client = client;
            this.num = num;

            chessBoard = new ChessBoardManager(panelBoard, namePlayer1, namePlayer2, client, this.eventManager);

            this.eventManager.Result += EventManager_Result;

            chessBoard.drawBoard(panelBoard);  
            
            if (num == 2)
            {
                panelBoard.Enabled = false;
                client.ListenThread(eventManager);
            }
            client.ListenThread(eventManager);                    
        }

        //@funtion EventManager_Result: show the result of the game when finishing
        private void EventManager_Result(object sender, SuperEventArgs e) {
            this.Invoke((MethodInvoker)(()=>{
                if (String.Compare(e.ReturnText, namePlayer1.Text) == 0)
                {
                    if (num == 1) MessageBox.Show(namePlayer1.Text + " win!");
                    else MessageBox.Show(namePlayer2.Text + " lose!");
                }
                else if (String.Compare(e.ReturnText, namePlayer2.Text) == 0)
                {
                    if(num == 1) MessageBox.Show(namePlayer1.Text + " lose!");
                    else MessageBox.Show(namePlayer2.Text + " win!");
                }
                else MessageBox.Show("Draw!");
            }));
            client.ListenThread(eventManager);
            this.eventManager.Result -= EventManager_Result;
            this.FormClosing -= FormPlay_FormClosing;
            this.FormClosed -= FormPlay_FormClosed;                   
            this.Close();
        }

        //@funtion surrenderButton_Click: send the surrender message to server
        private void surrenderButton_Click(object sender, EventArgs e) {
            DialogResult dialogResult = MessageBox.Show("Do you want to surrender?", "Question", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (num == 1) MessageBox.Show(namePlayer1.Text + " lose!");
                else MessageBox.Show(namePlayer2.Text + " lose!");
                Message mess = new Message(Cons.SURRENDER, Cons.SAMPLE_0000, "");
                client.sendData(mess.convertToString());
                client.ListenThread(eventManager);
                this.eventManager.Result -= EventManager_Result;
                this.FormClosing -= FormPlay_FormClosing;
                this.FormClosed -= FormPlay_FormClosed;
                this.Close();
            }         
        }

        //@funtion FormPlay_FormClosing: show the form to make sure that you really want to quit the game
        private void FormPlay_FormClosing(object sender, FormClosingEventArgs e) {
            if (MessageBox.Show("If exit, you'll lose. You want to exit?", "Warning", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
                e.Cancel = true;                
        }

        //@funtion FormPlay_FormClosed: send to server that player quited the game
        private void FormPlay_FormClosed(object sender, FormClosedEventArgs e) {            
            Message mess = new Message(Cons.SURRENDER, Cons.SAMPLE_0000, "");
            client.sendData(mess.convertToString());
            client.ListenThread(eventManager);
            this.eventManager.Result -= EventManager_Result;
        }
    }
}
