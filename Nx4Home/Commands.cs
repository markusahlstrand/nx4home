using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nx4Home
{
    public static class Constants
    {
        public static class ASCIICommand
        {
            public const string PrimaryKeypadFunctionWithoutPin = "3D";
            public const string PrimaryKeypadFunctionWithoutPinWithAck = "BD";
        }


        public static class Command
        {
            public const byte PrimaryKeypadFunctionWithoutPin = 0x3D;
            public const byte PrimaryKeypadFunctionWithoutPinWithAck = 0xBD;
        }

        public static class FunctionPerformed
        {
            public const byte ArmInAwayMode = 0x02;
        }


        public static class ASCIIFunctionPerformed
        {
            public const string ArmInAwayMode = "02";
        }
    }
}
