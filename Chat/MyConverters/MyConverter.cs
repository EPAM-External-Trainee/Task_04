using Chat.Models.ClientSide;
using Chat.Structs;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace Chat.MyConverters
{
    public static class MyConverter
    {
        public static byte[] GetBytesFromClientMessage(ClientMessage message)
        {
            int size = Marshal.SizeOf(message);
            byte[] result = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(message, ptr, true);
            Marshal.Copy(ptr, result, 0, size);
            Marshal.FreeHGlobal(ptr);
            return result;
        }

        public static void GetClientMessageFromBytes(byte[] bytes, ref ClientMessage message)
        {
            int len = Marshal.SizeOf(message);
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.Copy(bytes, 0, ptr, len);
            message = (ClientMessage)Marshal.PtrToStructure(ptr, typeof(ClientMessage));
            Marshal.FreeHGlobal(ptr);
        }

        public static byte[] GetBytesFromClient(Client client)
        {
            var binaryFormatter = new BinaryFormatter();
            using var memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, client);
            return memoryStream.ToArray();
        }

        public static Client GetClientFromBytes(byte[] bytes)
        {
            var binaryFormatter = new BinaryFormatter();
            using var memoryStream = new MemoryStream(bytes);
            object client = binaryFormatter.Deserialize(memoryStream);
            return client as Client;
        }
    }
}