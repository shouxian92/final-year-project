using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FYP_MATRIC_READ_WRITE
{
    public partial class Main : Form
    {
        public static mifare sm132 = new mifare();
        private static byte BLOCK_NUMBER = 1;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            sm132.OpenPort("COM6", 19200);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            sm132.ClosePort();
        }

        private void findTag()
        {
            byte TagType;
            byte[] TagSerial = new byte[4];
            byte ReturnCode = 0;

            sm132.CMD_SelectTag(out TagType, out TagSerial, out ReturnCode);
        }

        private void authTag()
        {
            byte ReturnCode = 0;

            byte AuthSource = 0xFF;
            byte[] Key = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                Key[i] = byte.Parse("FF", System.Globalization.NumberStyles.HexNumber);
            }

            AuthSource = (byte)ASource.KeyTypeB;
            sm132.CMD_Authenticate(AuthSource, Key, BLOCK_NUMBER, out ReturnCode);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            findTag();
            authTag();

            byte ReturnCode = 0;
            byte[] BlockData = new byte[16];
            if (sm132.CMD_ReadBlock(BLOCK_NUMBER, out BlockData, out ReturnCode))
            {
                txtStatus.Text = "Read Successful";

                string strMatricNumber = string.Empty;
                for (int i = 0; i < 9; i++)
                {
                    strMatricNumber += Convert.ToChar(Convert.ToUInt32(BlockData[i].ToString("X2"), 16));
                }

                txtRead.Text = strMatricNumber;
            }
            else
            {
                txtStatus.Text = "Error";
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            findTag();
            authTag();

            byte[] BlockData = new byte[16];
            byte ReturnCode = 0;

            byte[] inputBytes = Encoding.UTF8.GetBytes(txtWrite.Text);
            for (int i = 0; i < 9; i++)
            {
                BlockData[i] = byte.Parse(string.Format("{0:x2}", inputBytes[i]), System.Globalization.NumberStyles.HexNumber);
            }

            if (sm132.CMD_WriteBlock(BLOCK_NUMBER, BlockData, out ReturnCode))
            {
                txtStatus.Text = "Write Successful";
            }
            else
            {
                txtStatus.Text = "Error";
            }
        }
    }
}
