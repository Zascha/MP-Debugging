using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace MP.Debugging.CodeGen
{
    internal static class CodeGererator
    {
        private static string KeyDelimiter = "-";

        public static string GetCrackKey()
        {
            var networkBytes = GetNetworkInterfaceBytes();
            var dateBytes = GetCurrentDateBytes();

            var keyItems = networkBytes.Zip(dateBytes, (i, b) => i ^ b).Select(b => b * 10);

            return GetFormatedKey(keyItems);
        }

        #region Private methods

        private static int[] GetNetworkInterfaceBytes()
        {
            var networkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault();
            if (networkInterface == null)
            {
                throw new ApplicationException("Network interface required");
            }

            var networkInterfaceBytes = networkInterface.GetPhysicalAddress().GetAddressBytes();

            return ToIntArray(networkInterfaceBytes);
        }

        private static int[] GetCurrentDateBytes()
        {
            return ToIntArray(BitConverter.GetBytes(DateTime.Now.Date.ToBinary()));
        }

        private static int[] ToIntArray(byte[] byteArray)
        {
            return byteArray.Select(byteItem => (int)byteItem).ToArray();
        }

        private static string GetFormatedKey(IEnumerable<int> keyItems)
        {
            return string.Join(KeyDelimiter, keyItems);
        }

        #endregion
    }
}
