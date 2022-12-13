using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Message
    {
        private byte opcode;
        private ushort length;
        private byte[] payload;

        public byte Opcode {
            get {
                return opcode;
            }

            set {
                opcode = value;
            }
        }

        public ushort Length {
            get {
                return length;
            }

            set {
                length = value;
            }
        }

        public byte[] Payload {
            get {
                return payload;
            }

            set {
                payload = value;
            }
        }

        ///<summary>
        ///@funtion Message: The Message constructor. Create a Message from a received buff.
        ///<para></para>
        ///@param recvBuff: The buff that store the message content 
        /// </summary>
        public Message(byte[] recvBuff)
        {
            this.Opcode = recvBuff[0];
            this.Length = BitConverter.ToUInt16(recvBuff, Constants.OPCODE_SIZE);
            this.Payload = new byte[Constants.BUFFER_SIZE];
            if(this.Length > 0)
            {
                Array.Copy(recvBuff, Constants.OPCODE_SIZE + Constants.LENGTH_SIZE, this.Payload, 0, this.Length);
                this.Payload[this.length] = 0;
            }
        }

        ///<summary>
        ///@funtion Message: The Message constructor. Create a Message with opcode, length and player username
        ///<para></para>
        ///@param opcode: The opcode
        ///<para></para>
        ///@param length: The payload's length
        ///<para></para>
        ///@param name: The usrname
        /// </summary>
        public Message(byte opcode, ushort length, string name)
        {
            this.Opcode = opcode;
            this.Length = length;
            this.Payload = new byte[Constants.BUFFER_SIZE - Constants.OPCODE_SIZE - Constants.LENGTH_SIZE];
            Array.Copy(Encoding.ASCII.GetBytes(name), 0, this.Payload, 0, this.Length);
        }

        ///<summary>
        ///@funtion Message: The Message constructor. Create a Message with opcode.
        ///<para></para>
        ///@param opcode: The opcode
        /// </summary>
        public Message(byte opcode)
        {
            this.Opcode = opcode;
            this.Length = 0;
            this.Payload = new byte[1];
            this.Payload[0] = 0;
        }

        ///<summary>
        ///@funtion Message: The Message constructor. Create a Message with opcode, length and player's move coordinate
        ///<para></para>
        ///@param opcode: The opcode
        ///<para></para>
        ///@param length: The payload's length
        ///<para></para>
        ///@param locationX: The x-axis coordinate
        ///<para></para>
        ///@param locationY: The y-axis coordinate
        /// </summary>
        public Message(byte opcode, ushort length, byte locationX, byte locationY) {
            this.Opcode = opcode;
            this.Length = length;
            this.Payload = new byte[] { locationX, locationY }; 

        }
    }
}
