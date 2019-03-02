using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DNFAHKGenerator.Model;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace DNFAHKGenerator.Parser
{
    public class FileParser
    {
        public static void SaveSkillFile(string path, IEnumerable<Skill> skillList)
        {
            FileStream file = new FileStream(path, FileMode.Create);

            try
            {
                var jsonStr = JsonConvert.SerializeObject(skillList, Formatting.Indented);

                using (StreamWriter writer = new StreamWriter(file))
                {
                    writer.WriteLine(jsonStr);
                }
            }
            finally
            {
                file.Close();
            }
            
        }

        public static List<Skill> LoadSkillFile(string path)
        {
            List<Skill> resList = new List<Skill>();
            FileStream file = new FileStream(path, FileMode.Open);

            try
            {
                string jsonStr;
                using (StreamReader reader = new StreamReader(file))
                {
                    jsonStr = reader.ReadToEnd();
                }

                resList = JsonConvert.DeserializeObject<List<Skill>>(jsonStr);
            }
            finally
            {
                file.Close();
            }

            return resList;
        }
    }
}
