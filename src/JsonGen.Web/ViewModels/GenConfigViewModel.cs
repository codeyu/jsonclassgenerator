using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JsonGen.Web.ViewModels
{
    public class GenConfigViewModel
    {
        public string ClassName { get; set; }
        public string Json { get; set; }

        public List<KeyValuePair<string, string>> PropertyAttributeOptions
        {
            get {
                return new List<KeyValuePair<string, string>>() { 
                    new KeyValuePair<string, string>( "None", "None" ),
                    new KeyValuePair<string, string>( "DataMember", "DataMember" ),
                    new KeyValuePair<string, string>( "JsonProperty", "JsonProperty" )
                };
            }
        }

        public string PropertyAttribute { get; set; }

        public int Language { get; set; }

        public string CodeObjects { get; set; }

        public bool Nest { get; set; }
        public bool Pascal { get; set; }
        public bool Properties { get; set; }
        public bool GenFile {get;set;}
        
    }
}