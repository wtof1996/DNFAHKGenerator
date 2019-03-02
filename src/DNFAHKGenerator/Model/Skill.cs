using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
// ReSharper disable IdentifierTypo

namespace DNFAHKGenerator.Model
{
    [DataContract]
    public class Skill
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string SkillName { get; set; }

        [DataMember]
        public string HotKeys { get; set; }

        
    }
}
