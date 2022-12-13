using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Player
    {
        private string name;
        private Image mark;

        ///<summary>
        ///Get and set for player username
        ///</summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        ///<summary>
        /// Get and set for player mark (the icon X or O)
        ///</summary>
        public Image Mark
        {
            get { return mark;}
            set { mark = value; }
        }
        ///<summary>
        ///@funtion :Constructor for Player
        ///<para></para>
        ///@param name: Player username
        ///<para></para>
        ///@param name: Player mark
        ///</summary>
        public Player(string name, Image mark)
        {
            this.Name = name;
            this.Mark = mark;
        }
    }
}
