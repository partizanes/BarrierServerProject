using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization
{
    [Serializable]
    public class MSG
    {
        public string group;
        public int type;
        public string message;

        public MSG(string i, int s, string s2)
        {
            group = i;
            type = s;
            message = s2;
        }
    }
    public class Util
    {
        public static byte[] Serialization(MSG obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            byte[] msg = stream.ToArray();
            return msg;
        }

        public static MSG DeSerialization(byte[] serializedAsBytes)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Write(serializedAsBytes, 0, serializedAsBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return (MSG)formatter.Deserialize(stream);
        }
    }
}
