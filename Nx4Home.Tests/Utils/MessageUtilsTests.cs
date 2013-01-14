using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nx4Home.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nx4Home.Tests.Utils
{
    [TestClass]
    public class MessageUtilsTests
    {
        [TestMethod]
        public void MessageWithoutStopbitDoesNotGetStuffed()
        {
            byte[] expectedMessage = new byte[] { 0x01 };

            byte[] stuffedMessage = MessageUtils.StuffMessage( new byte[]{0x01});

            CollectionAssert.AreEqual(expectedMessage, stuffedMessage);
        }

        [TestMethod]
        public void MessageStopbitDoesGetStuffed()
        {
            byte[] expectedMessage = new byte[] { 0x01, 0x7D, 0x5D, 0x7D, 0x5E };

            byte[] stuffedMessage = MessageUtils.StuffMessage(new byte[] { 0x01, 0x7D, 0x7E });

            CollectionAssert.AreEqual(expectedMessage, stuffedMessage);
        }

        [TestMethod]
        public void PackageASCIIMessage()
        {
            byte[] message = new byte[] { 0x84, 0x09, 0x7E, 0x10, 0x58, 0x01, 0x00 };
            //byte[] expectedMessage = new byte[] { 0x0A, 0x07, 0x84, 0x09, 0x7E, 0x10, 0x58, 0x01, 0x00, 0x7C, 0xD1, 0x0D };
            byte[] expectedMessage = new byte[] { 0x0A, 0x30, 0x37, 0x38, 0x34, 0x30, 0x39, 0x37, 0x45, 0x31, 0x30, 0x35, 0x38, 0x30, 0x31, 0x30, 0x30, 0x37, 0x43, 0x44, 0x31, 0x0D };

            byte[] packagedMessage = MessageUtils.PackageASCIIMessage(message);

            CollectionAssert.AreEqual(expectedMessage, packagedMessage);
        }

        [TestMethod]
        public void PackageBinaryMessage()
        {
            byte[] message = new byte[] { 0x84, 0x09, 0x7E, 0x10, 0x58, 0x01, 0x00 };
            byte[] expectedMessage = new byte[] { 0x7E, 0x07, 0x84, 0x09, 0x7D, 0x5E, 0x10, 0x58, 0x01, 0x00, 0x7C, 0xD1 };


            byte[] packagedMessage = MessageUtils.PackageBinaryMessage(message);

            CollectionAssert.AreEqual(expectedMessage, packagedMessage);
        }
    }
}
