using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Client
{
    class FileManager
    {
        public static byte[] _buff;
        private static int _length;
        private static System.Windows.Forms.SaveFileDialog _saveFileDialog;

        ///<summary>
        ///@funtion appendToBuff: Append a buff data to the main buff
        ///<para></para>
        ///@param append: The buffer to append to the main buff
        ///<para></para>
        ///@param length: The length of the append buffer
        /// </summary>
        public static void appendToBuff(byte[] append, int length)
        {
            if (_buff == null)
            {
                _buff = new byte[Constants.BUFFER_SIZE];
                _length = 0;
            };
            Buffer.BlockCopy(append, 0, _buff, _length, length);
            _length += length;
        }

        ///<summary>
        ///@funtion startSaveFile: Save the saveFileDialog instance that user interacted for later use
        ///<para></para>
        ///@param saveFileDialog: The saveFileDialog
        /// </summary>
        public static void startSaveFile(System.Windows.Forms.SaveFileDialog saveFileDialog)
        {
            _saveFileDialog = saveFileDialog;
        }

        ///<summary>
        ///@funtion saveFile: Get the file stream from saveFileDialog and transfer data from buff to file
        /// </summary>
        public static void saveFile()
        {
            System.IO.FileStream fs = (System.IO.FileStream)_saveFileDialog.OpenFile();
            fs.Write(_buff, 0, _length);
            fs.Close();
        }

    }
}
