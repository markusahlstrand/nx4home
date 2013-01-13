using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nx4Home.Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            AlarmSystem alarmSystem = new AlarmSystem();

            alarmSystem.MessageReceived += (sender, messageArgs) =>
                {
                    Console.WriteLine(messageArgs.StringMessage);
                    //Console.WriteLine("New message");
                    //foreach (byte b in messageArgs.ByteMessage)
                    //{
                    //    Console.Write(b.ToString());
                    //    Console.Write(',');
                    //}
                    //Console.WriteLine();
                };

            //alarmSystem.ReadStatus();
            alarmSystem.Arm();
            while (true)
            {
                Thread.Sleep(3000);
                alarmSystem.AcknowledgeMessage();
                Console.WriteLine("Ack");
            }
            
            Console.ReadKey();
        }
    }
}
