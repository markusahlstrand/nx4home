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
        private const byte BINARY_STARTBIT = 0x7E;

        public static byte[] StuffMessage(byte[] message)
        {
            int numberOfBytesToStuff = message.Skip(1).Count(b => b == 0x7D || b == 0x7E);

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
                else if (b == 0x7E && i != 0)
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

        public static byte[] PackageASCIIMessage(byte[] message)
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



        public static byte[] PackageBinaryMessage(byte[] message)
        {
            int messageLength = message.Length;            
            byte[] packagedMessage = new byte[messageLength + 4];
            packagedMessage[0] = BINARY_STARTBIT;
            packagedMessage[1] = (byte)messageLength;
            message.CopyTo(packagedMessage, 2);
            ChecksumUtils.CalculateFletcher16Checksum(packagedMessage, 1, messageLength + 1);

            byte[] stuffedMessage = StuffMessage(packagedMessage);

            return stuffedMessage;
        }


        internal static string ParseASCIIMessage(byte[] message)
        {
            try
            {
                int indexOfStartbit = -1;
                for (int i = 0; i < message.Length; i++)
                {
                    if (message[i] == STARTBIT)
                    {
                        indexOfStartbit = i;
                        break;
                    }
                }

                return ASCIIEncoding.ASCII.GetString(message, indexOfStartbit + 1, message.Length - indexOfStartbit - 1);
            }
            catch (Exception)
            {
                return "Failed";
            }
        }


        internal static byte[] ParseMessage(byte[] message)
        {
            try
            {
                int indexOfStartbit = -1;
                for (int i = 0; i < message.Length; i++)
                {
                    if (message[i] == STARTBIT)
                    {
                        indexOfStartbit = i;
                        break;
                    }
                }

                if (indexOfStartbit == -1)
                {
                    return message;
                }

                int messageLength = message[indexOfStartbit + 1];

                byte[] parsedMessage = new byte[messageLength];
                for (int i = 0; i < messageLength; i++)
                {
                    parsedMessage[i] = message[i + indexOfStartbit + 2];
                }

                return parsedMessage;
            }
            catch (Exception ex)
            {
                return message;
            }
        }
    }
}
