using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.OperationNameGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ClientGenerator
{
    class Program
    {
        static string baseUrl = "http://localhost:55823/";

        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            tasks.Add(GeneratePaymentSdk());

            Task.WaitAll(tasks.ToArray());
            Console.ReadLine();
        }

        private static async Task GeneratePaymentSdk()
        {
            if (!baseUrl.EndsWith("/"))
                baseUrl += "/";
            var url = baseUrl + "swagger/v1/swagger.json";
            var document = await SwaggerDocument.FromUrlAsync(url);

            var settings = new SwaggerToCSharpClientGeneratorSettings
            {
                ClassName = "ApiClient",
                GenerateClientInterfaces = true,
                OperationNameGenerator = new SingleClientFromOperationIdOperationNameGenerator(),
                ExceptionClass = "ApiClientException",
                InjectHttpClient = true,
                CSharpGeneratorSettings =
                {
                    Namespace = "ServerlessTemplate",
                    ClassStyle = NJsonSchema.CodeGeneration.CSharp.CSharpClassStyle.Poco,
                    ArrayType= "System.Collections.Generic.List"
                },

            };

            var generator = new SwaggerToCSharpClientGenerator(document, settings);
            var code = generator.GenerateFile();
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\Client\\ApiClient.cs"));
            File.WriteAllText(path, code);
            Console.WriteLine("Api client generated");
        }
    }
}
