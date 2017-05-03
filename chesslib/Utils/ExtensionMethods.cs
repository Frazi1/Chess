using System.Runtime.Serialization.Formatters.Binary;

namespace chesslib.Utils
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T input)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, input);
                ms.Seek(0, System.IO.SeekOrigin.Begin);

                return (T) formatter.Deserialize(ms);
            }
        }
    }
}
