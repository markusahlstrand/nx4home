using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nx4Home.Utils
{
    public static class ChecksumUtils
    {
        public static byte[] CalculateFletcher16Checksum(byte[] message)
        {
            int fletcher1 = 0;
            int fletcher2 = 0;

            foreach (byte b in message)
            {
                fletcher1 = (fletcher1 + b) % 0xFF;
                fletcher2 = (fletcher2 + fletcher1) % 0xFF;                
            }

            return new byte[] { (byte)fletcher1, (byte)fletcher2 };
        }

        public static void CalculateFletcher16Checksum(byte[] message, int startByte, int endByte)
        {
            int fletcher1 = 0;
            int fletcher2 = 0;

            for(int i = startByte; i <= endByte ; i++)
            {
                fletcher1 = (fletcher1 + message[i]) % 0xFF;
                fletcher2 = (fletcher2 + fletcher1) % 0xFF;
            }

            message[endByte + 1] =  (byte)fletcher1;
            message[endByte + 2] = (byte)fletcher2;
        }
   }
}
