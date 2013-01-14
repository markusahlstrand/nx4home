using Nx4Home.Utils;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nx4Home
{
    public class AlarmSystem
    {
        SerialPort serialPort;

        enum CommunicationMode
        {
            Binary,
            ASCII
        }

        public AlarmSystem()
        {
            serialPort = new SerialPort();
            serialPort.BaudRate = 38400;
            serialPort.PortName = "COM3";
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;

            communicationMode = CommunicationMode.ASCII;

            serialPort.DataReceived += serialPort_DataReceived;
            serialPort.Open();
        }

        ~AlarmSystem()
        {
            serialPort.Close();
            serialPort.Dispose();
        }       


        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string RxString = serialPort.ReadExisting();

            //byte[] message = MessageUtils.ParseMessage(Encoding.ASCII.GetBytes(RxString));
            string message = MessageUtils.ParseASCIIMessage(Encoding.ASCII.GetBytes(RxString));


            MessageReceived.Invoke(this, new AlarmMessageEventArgs() {  StringMessage = message }); 
        }


        public void Arm()
        {
            byte[] message = new byte[] 
            { 
                Constants.Command.PrimaryKeypadFunctionWithoutPinWithAck
                , Constants.FunctionPerformed.ArmInAwayMode
                , 0x01
                , 0x02
            };
            //byte[] packagedMessage = MessageUtils.PackageBinaryMessage(message);
            byte[] packagedMessage = MessageUtils.PackageASCIIMessage(message);

            serialPort.Write(packagedMessage, 0, packagedMessage.Length);  
        }

        public void ReadStatus()
        {
            byte[] message = new byte[] { 0x84, 0x09, 0x7E, 0x10, 0x58, 0x01, 0x00 };
            byte[] packagedMessage = MessageUtils.PackageBinaryMessage(message);

            serialPort.Write(packagedMessage, 0, packagedMessage.Length);


            //byte[] message = new byte[] { 126, 7, 132, 9, 125, 94, 16, 88, 1, 0, 124, 209 };
            serialPort.Write(packagedMessage, 0, packagedMessage.Length);
        }

        public void RejectMessage()
        {
            byte[] message = new byte[] { 126, 1, 31, 32, 33 };
            serialPort.Write(message, 0, message.Length);            
        }

        public void AcknowledgeMessage()
        {
            byte[] message = new byte[] { 0x1D };
            byte[] packagedMessage = MessageUtils.PackageBinaryMessage(message);

            serialPort.Write(packagedMessage, 0, packagedMessage.Length);
        }

        public delegate void MessageReceivedDelegate(object sender, AlarmMessageEventArgs args);
        public event MessageReceivedDelegate MessageReceived;
        private CommunicationMode communicationMode;

    }    
}
