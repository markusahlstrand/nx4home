using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nx4Home.Utils
{
    public static class MessageUtils
    {
        private const byte STOPBIT = 0x0D;
        private const byte STARTBIT = 0x0A;

        public static byte[] StuffMessage(byte[] message)
        {
            int numberOfBytesToStuff = message.Count(b => b == 0x7D || b == 0x7E);

            if (numberOfBytesToStuff == 0)
            {
                return message;
            }

            int i = 0;
            byte[] stuffedMessage = new byte[message.Length + numberOfBytesToStuff];

            foreach (byte b in message)
            {
                if (b == 0x7D)
                {
                    stuffedMessage[i++] = 0x7D;
                    stuffedMessage[i++] = 0x5D;
                }
                else if (b == 0x7E)
                {
                    stuffedMessage[i++] = 0x7D;
                    stuffedMessage[i++] = 0x5E;
                }
                else
                {
                    stuffedMessage[i++] = b;
                }
            }

            return stuffedMessage;
        }

        public static byte[] PackageMessage(byte[] message)
        {
            int messageLength = message.Length;
            byte[] packagedMessage = new byte[messageLength + 5];
            packagedMessage[0] = STARTBIT;
            packagedMessage[1] = (byte)messageLength;
            message.CopyTo(packagedMessage, 2);
            ChecksumUtils.CalculateFletcher16Checksum(packagedMessage, 1, messageLength + 1);
            packagedMessage[messageLength + 4] = STOPBIT;

            return packagedMessage;
        }
    }
}
