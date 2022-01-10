using System.IO;
using Microsoft.Extensions.Configuration;

namespace SearchServer.Services
{
    public class ScriptRetriever
    {
        public static string getJSON(string fileName)
        {
            string scriptsDir = ConfigurationFactory.GetConfiguration().GetValue<string>("elastic:scriptsDir");
            var file = Path.Combine(scriptsDir, fileName+".json");
            return File.ReadAllText(file);
        }
    }
}
