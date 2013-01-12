using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nx4Home.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nx4Home.Tests
{
    [TestClass]
    public class FletcherSumTests
    {
        [TestMethod]
        public void CalculateFletcherSumForOneByteMessage()
        {
            byte[] message = new byte[] { 01 };
            byte[] expectedChecksum = new byte[] { 01, 01 };


            byte[] fletcherSum = ChecksumUtils.CalculateFletcher16Checksum(message);

            CollectionAssert.AreEqual(expectedChecksum, fletcherSum);
        }

        [TestMethod]
        public void CalculateFletcherSumForTwoByteMessage()
        {
            byte[] message = new byte[] { 0x01, 0x02 };
            byte[] expectedChecksum = new byte[] { 0x03, 0x04 };


            byte[] fletcherSum = ChecksumUtils.CalculateFletcher16Checksum(message);

            CollectionAssert.AreEqual(expectedChecksum, fletcherSum);
        }

        [TestMethod]
        public void CalculateFletcherSumForTwoByteMessageWithOverflow()
        {
            byte[] message = new byte[] { 0xF1, 0xF2 };
            byte[] expectedChecksum = new byte[] { 0xE4, 0xD6 };


            byte[] fletcherSum = ChecksumUtils.CalculateFletcher16Checksum(message);

            CollectionAssert.AreEqual(expectedChecksum, fletcherSum);
        }

        [TestMethod]
        public void CalculateFletcherSumForSampleMessage()
        {
            byte[] message = new byte[] { 0x07, 0x84, 0x09, 0x7E, 0x10, 0x58, 0x01, 0x00 };
            byte[] expectedChecksum = new byte[] { 0x7C, 0xD1 };


            byte[] fletcherSum = ChecksumUtils.CalculateFletcher16Checksum(message);

            CollectionAssert.AreEqual(expectedChecksum, fletcherSum);
        }
    }
}
