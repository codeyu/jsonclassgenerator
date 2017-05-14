using System;
using System.IO;
using Xamasoft.JsonClassGenerator;
using Xamasoft.JsonClassGenerator.CodeWriters;
using Xunit;

namespace JsonCSharpClassGeneratorLib.Test
{
    public class JsonClassGeneratorTest
    {
        [Fact]
        public void TestGenerator()
        {
            var json = @"{""employees"": [
                        {  ""firstName"":""John"" , ""lastName"":""Doe"" }, 
                        {  ""firstName"":""Anna"" , ""lastName"":""Smith"" }, 
                        { ""firstName"": ""Peter"" ,  ""lastName"": ""Jones "" }
                        ]
                        }".Trim();
            ICodeWriter writer = new CSharpCodeWriter();

            var gen = new JsonClassGenerator();
            gen.Example = json;
            gen.InternalVisibility = false;
            gen.CodeWriter = writer;
            gen.ExplicitDeserialization = false;

            gen.Namespace = "Demo";


            gen.NoHelperClass = false;
            gen.SecondaryNamespace = null;
            gen.UseProperties = true;
            gen.MainClass = "Employee";
            gen.UsePascalCase = true;
            //gen.PropertyAttribute = propertyAttribute;

            gen.UseNestedClasses = true;
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
            Assert.NotEmpty(result);
        }
    }
}
