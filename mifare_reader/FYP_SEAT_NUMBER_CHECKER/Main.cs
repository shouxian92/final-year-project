using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using FYP_SEAT_NUMBER_CHECKER;
using System.Threading;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using CoAP;
using CoAP.Server.Resources;
using CoAP.Server;

namespace FYP_SEAT_NUMBER_CHECKER
{

    public partial class Main : Form
    {
        class LookupMatricCardResource : Resource
        {
            Main instance;
            public LookupMatricCardResource(Main instance)
                : base("lookup-matric")
            {
                // constructor takes in an instance of main so that we are able to use the lookupSeatNumber method
                this.instance = instance;
                Attributes.Title = "Update the screen with the following matric card number";
            }

            protected override void DoPost(CoapExchange exchange)
            {
                // check the payload's message
                String payload = System.Text.Encoding.Default.GetString(exchange.Request.Payload);
                System.Diagnostics.Debug.WriteLine(payload);

                // respond to the caller
                exchange.Respond("Received payload " + payload);
                
                // call the lookupSeatNumber function using the extracted payload
                instance.lookupSeatNumber(payload);
            }
        }

        public Main()
        {
            InitializeComponent();

            pnlProgress.Location = new System.Drawing.Point(12, 95);
            pnlHallSelection.Location = new System.Drawing.Point(12, 95);
            pnlSeatNumber.Location = new System.Drawing.Point(12, 95);
        }

        public static mifare sm132 = new mifare();
        private static byte BLOCK_NUMBER = 1;
        private ComboBoxItem iSelectedHall;
        private Boolean LOCAL = false;

        private System.Windows.Forms.Timer tmrNfc;
        private System.Windows.Forms.Timer tmrApp;
        private void Main_Load(object sender, EventArgs e)
        {
            pnlProgress.Visible = true;
            pnlHallSelection.Visible = false;
            pnlSeatNumber.Visible = false;

            populateHalls();

            pnlProgress.Visible = false;
            pnlHallSelection.Visible = true;
            pnlSeatNumber.Visible = false;
            setInstruction(INSTR_SELECT_HALL);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            sm132.ClosePort();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            iSelectedHall = (ComboBoxItem)cmbHalls.SelectedItem;
            lblHall.Text = iSelectedHall.Text.ToUpper();

            pnlProgress.Visible = true;
            pnlHallSelection.Visible = false;
            pnlSeatNumber.Visible = false;
            setInstruction(INSTR_SCAN);

            
            CoapServer server = new CoapServer();
            server.Add(new LookupMatricCardResource(this));

            try
            {
                server.Start();

                Console.Write("CoAP server [{0}] is listening on", server.Config.Version);

                foreach (var item in server.EndPoints)
                {
                    Console.Write(" ");
                    Console.Write(item.LocalEndPoint);
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (LOCAL)
            {
                tmrApp = new System.Windows.Forms.Timer();
                tmrApp.Interval = 1000;
                tmrApp.Tick += new EventHandler(_delayTimer2_Elapsed);
                tmrApp.Start();
            }
            else
            {
                sm132.OpenPort("COM3", 19200);
                tmrNfc = new System.Windows.Forms.Timer();
                tmrNfc.Tick += new EventHandler(tmrNfc_Tick);
                tmrNfc.Enabled = true;
                seekTag();
            }
        }

        // LOCAL
        private void _delayTimer2_Elapsed(Object myObject, EventArgs myEventArgs)
        {
            tmrApp.Stop();
            setInstruction(INSTR_LOOKING_UP);

            tmrApp = new System.Windows.Forms.Timer();
            tmrApp.Interval = 1000;
            tmrApp.Tick += new EventHandler(_delayTimer3_Elapsed);
            tmrApp.Start();
        }

        // LOCAL
        private void _delayTimer3_Elapsed(Object myObject, EventArgs myEventArgs)
        {
            tmrApp.Stop();
            lookupSeatNumber("U0000001A");
        }

        public delegate void lookupSNCallback(string matric);

        // nfc
        public void lookupSeatNumber(string strMatricNumber)
        {
            if (this.lblInstruction.InvokeRequired)
            {
                lookupSNCallback d = new lookupSNCallback(lookupSeatNumber);
                this.Invoke(d, new object[] { strMatricNumber });
            }
            else
            {

                setInstruction(INSTR_LOOKING_UP);
                dynamic data = new JObject();
                data.intHallId = iSelectedHall.Value;
                data.strMatricNumber = strMatricNumber;

                string strSeatNumber = fetchData(WebRequestMethods.Http.Post, SvcAddr.SEAT_NUMBER, data.ToString());
                lblInstruction.Text = "Matric Number: " + strMatricNumber;
                lblSeatNumber.Text = (strSeatNumber.Equals("-1") ? "No seat allocated in this hall." : "Seat Number: " + strSeatNumber.PadLeft(4, '0'));

                pnlProgress.Visible = false;
                pnlHallSelection.Visible = false;
                pnlSeatNumber.Visible = true;

                intTimeLeft = 5;
                tmrApp = new System.Windows.Forms.Timer();
                tmrApp.Interval = 1000;
                tmrApp.Tick += new EventHandler(tmrApp_Tick);
                tmrApp.Start();
            }
        }

        private int intTimeLeft;
        private void tmrApp_Tick(Object obj, EventArgs eventArgs)
        {
            if (intTimeLeft > 0)
            {
                intTimeLeft = intTimeLeft - 1;
                lblReturn.Text = "Returning to main screen in " + intTimeLeft + " seconds";
            }
            else
            {
                tmrApp.Stop();
                lblReturn.Text = "";
                pnlProgress.Visible = true;
                pnlHallSelection.Visible = false;
                pnlSeatNumber.Visible = false;
                setInstruction(INSTR_SCAN);
                if (LOCAL)
                {
                    tmrApp = new System.Windows.Forms.Timer();
                    tmrApp.Interval = 1000;
                    tmrApp.Tick += new EventHandler(_delayTimer2_Elapsed);
                    tmrApp.Start();
                }
                else
                {
                    tmrNfc = new System.Windows.Forms.Timer();
                    tmrNfc.Tick += new EventHandler(tmrNfc_Tick);
                    tmrNfc.Enabled = true;
                    seekTag();
                }
            }
        }

        private void populateHalls()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(fetchData(WebRequestMethods.Http.Get, SvcAddr.HALLS, null));
            XmlNodeList xmlNodes = xmlDoc.GetElementsByTagName("Hall");
            foreach (XmlNode xmlNode in xmlNodes)
            {
                string strHallId = xmlNode.ChildNodes[0].InnerText;
                char cHallCode = Convert.ToChar(Convert.ToInt32(xmlNode.ChildNodes[1].InnerText));
                cmbHalls.Items.Add(new ComboBoxItem()
                {
                    Text = "Hall " + cHallCode,
                    Value = strHallId
                });
            }
        }

        private void seekTag()
        {
            byte ReturnCode = 0;
            sm132.CMD_SeekForTag(out ReturnCode);
        }

        private void readTag()
        {
            authBlock();

            byte[] BlockData = new byte[16];
            byte ReturnCode = 0;

            if (sm132.CMD_ReadBlock(BLOCK_NUMBER, out BlockData, out ReturnCode))
            {
                string[] strData = new string[16];
                for (int i = 0; i < 16; i++)
                {
                    strData[i] = BlockData[i].ToString("X2");
                }

                string strMatricNumber = string.Empty;
                for (int i = 0; i < 9; i++)
                {
                    strMatricNumber += Convert.ToChar(Convert.ToUInt32(strData[i], 16));
                }

                String server_ip = "192.168.1.7";
                String port = "5683";

                // new a GET request
                Request request = Request.NewPost();
                request.SetPayload(strMatricNumber);
                request.URI = new Uri("coap://" + server_ip + ":"+ port +"/basic_0");
                //request.Respond();
                request.Send();

                intTimeLeft = 5;
                tmrApp = new System.Windows.Forms.Timer();
                tmrApp.Interval = 1000;
                tmrApp.Tick += new EventHandler(tmrApp_Tick);
                tmrApp.Start();

                // wait for one response (synchronous)
                // Response response = request.WaitForResponse();

                // remove the original search of lookupSeatNumber
                //lookupSeatNumber(strMatricNumber);
            }
        }

        private void authBlock()
        {
            byte AuthSource = 0xFF;
            byte ReturnCode;
            byte[] Key = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                Key[i] = byte.Parse("FF", System.Globalization.NumberStyles.HexNumber);
            }

            AuthSource = (byte)ASource.KeyTypeB;
            sm132.CMD_Authenticate(AuthSource, Key, BLOCK_NUMBER, out ReturnCode);
        }

        private void tmrNfc_Tick(object sender, EventArgs e)
        {
            byte[] TagSerial = new byte[4];
            string Firmware = "";
            byte Response_Type = 0;
            byte TagType = 0;
            int buffercount = 0;

            if (mifare.port.IsOpen)
            {
                buffercount = mifare.port.BytesToRead;
            }

            if (buffercount != 0)
            {
                sm132.ParseIncoming(out Response_Type, out TagType, out TagSerial, out  Firmware);

                if (Response_Type == 2)
                {
                    tmrNfc.Enabled = false;
                    readTag();
                    tmrNfc.Enabled = true;
                }
            }
        }

        private const int INSTR_FETCH_HALLS = 1;
        private const int INSTR_SELECT_HALL = 2;
        private const int INSTR_SCAN = 3;
        private const int INSTR_LOOKING_UP = 4;
        private const int INSTR_NO_RECORD = 5;

        private void setInstruction(int intIstructionId)
        {
            string strInstruction = string.Empty;
            switch (intIstructionId)
            {
                case INSTR_FETCH_HALLS:
                    strInstruction = "Fetching halls...";
                    break;
                case INSTR_SELECT_HALL:
                    strInstruction = "Select hall...";
                    break;
                case INSTR_SCAN:
                    strInstruction = "Scan your matric card to reveal your seat number.";
                    break;
                case INSTR_LOOKING_UP:
                    strInstruction = "Looking up for your seat number...";
                    break;
                case INSTR_NO_RECORD:
                    strInstruction = "You record for this hall is not found in the database.";
                    break;
            }
            lblInstruction.Text = strInstruction;
        }

        private string fetchData(string strReqMethod, string strUri, string strData)
        {
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(strUri);
            httpReq.KeepAlive = false;
            httpReq.Method = strReqMethod;

            if (strReqMethod.Equals(WebRequestMethods.Http.Post))
            {
                httpReq.ContentType = "text/json";

                byte[] buffer = Encoding.ASCII.GetBytes(strData);
                httpReq.ContentLength = buffer.Length;

                Stream sData = httpReq.GetRequestStream();
                sData.Write(buffer, 0, buffer.Length);
                sData.Close();
            }

            HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
            Stream sResp = httpResp.GetResponseStream();
            StreamReader sReader = new StreamReader(sResp);
            string strResult = sReader.ReadToEnd();
            sReader.Close();
            httpResp.Close();

            return strResult;
        }
    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
