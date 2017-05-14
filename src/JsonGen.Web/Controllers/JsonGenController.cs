using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xamasoft.JsonClassGenerator;
using Xamasoft.JsonClassGenerator.CodeWriters;
using System.IO;
using JsonGen.Web.ViewModels;
namespace JsonGen.Web.Controllers
{
    [Route("api/[controller]")]
    public class JsonGenController : Controller
    {
        // GET api/jsongen
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "version", "1.0" };
        }


        // POST api/jsongen
        [HttpPost]
        public string Post([FromBody]GenConfigViewModel genConfig)
        {
            var json = @"{""employees"": [
                        {  ""firstName"":""John"" , ""lastName"":""Doe"" }, 
                        {  ""firstName"":""Anna"" , ""lastName"":""Smith"" }, 
                        { ""firstName"": ""Peter"" ,  ""lastName"": ""Jones "" }
                        ]
                        }".Trim();
            ICodeWriter writer = new CSharpCodeWriter();

            var gen = new JsonClassGenerator();
            gen.Example = genConfig.Json;
            gen.InternalVisibility = false;
            gen.CodeWriter = writer;
            gen.ExplicitDeserialization = false;
            gen.Namespace = "Demo";
            gen.NoHelperClass = true;
            gen.SecondaryNamespace = null;
            gen.UseProperties = true;
            gen.UsePascalCase = true;
            gen.PropertyAttribute = "DataMember";
            gen.UseNestedClasses = false;
            gen.ApplyObfuscationAttributes = false;
            gen.SingleFile = true;
            gen.ExamplesInDocumentation = false;
            gen.TargetFolder = null;
            gen.SingleFile = true;
            var result = "";
            using (var sw = new StringWriter())
            {
                gen.OutputStream = sw;
                gen.GenerateClasses();
                sw.Flush();

                result = sw.ToString();
            }
            return result;
        }

        
    }
}
