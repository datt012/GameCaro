namespace Client
{
    public class Cons
    {
        public static int BUTTON_WIDTH = 20;
        public static int BUTTON_HEIGHT = 20;
        public static int BOARD_SIZE = 16;

        public static int OPCODE_SIZE = 2;
        public static int LENGTH_SIZE = 4;
        public static int LOCATION_SIZE = 2;
        public static string SAMPLE_0000 = "0000";
        public static string SAMPLE_00 = "00";
        public static string SPACE = " ";

        public static string IP = "127.0.0.1";
        public static int port = 5500;
        public static int BUFFER_SIZE = 1024;

        public static string SIGNUP = "10";
        public static string SIGNIN = "20";
        public static string SIGNOUT = "30";
        public static string LIST = "40";
        public static string CHALLENGE = "50";
        public static string ACCEPT = "51";
        public static string REFUSE = "52";
        public static string RESULT = "60";
        public static string MOVE = "70";
        public static string SURRENDER = "80";
    }
}
