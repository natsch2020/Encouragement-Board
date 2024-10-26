using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Encouragement_Board
{
    public static class XMLSerialization
    {
        public static void Write<T>(string filepath, T obj, bool append = false) where T : new()
        {
            TextWriter writer = null;

            var cerealer = new XmlSerializer(typeof(T));
            writer = new StreamWriter(filepath, append);
            cerealer.Serialize(writer, obj);

            if(writer != null)
            {
                writer.Close();
            }
        }

        public static T Read<T>(string filepath) where T : new()
        {
            TextReader reader = null;

            try
            {
                var cerealer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filepath);
                return (T)cerealer.Deserialize(reader);
            }
            finally
            {
                if(reader != null)
                {
                    reader.Close();
                }
            }
        }
    }
}
