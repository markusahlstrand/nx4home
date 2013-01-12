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

        public AlarmSystem()
        {
            serialPort = new SerialPort();
            serialPort.BaudRate = 38400;
            serialPort.PortName = "COM3";
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;


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

            MessageReceived.Invoke(this, new AlarmMessageEventArgs() {  ByteMessage = Encoding.ASCII.GetBytes(RxString) }); 
        }


        public void ReadStatus()
        {
            byte[] message = new byte[] { 126, 7, 132, 9, 125, 94, 16, 88, 1, 0, 124, 209 };
            serialPort.Write(message, 0, message.Length);            
        }

        public void RejectMessage()
        {
            byte[] message = new byte[] { 126, 1, 31, 32, 33 };
            serialPort.Write(message, 0, message.Length);            
        }

        public void AcknowledgeMessage()
        {
            byte[] message = new byte[] { 126, 1, 29, 30, 31 };
            serialPort.Write(message, 0, message.Length);
        }

        public delegate void MessageReceivedDelegate(object sender, AlarmMessageEventArgs args);
        public event MessageReceivedDelegate MessageReceived;

    }    
}
