using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Model
{
    public class ParametroScript
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("driver")]
        public string Driver { get; set; }

        [JsonProperty("dataTest")]
        public DataTest[] DataTest { get; set; }
    }

    public partial class DataTest
    {
        [JsonProperty("nombrePrueba")]
        public string NombrePrueba { get; set; }

        [JsonProperty("suiteVars")]
        public Dictionary<string, string> SuiteVars { get; set; }

        [JsonProperty("testsVars")]
        public List<Dictionary<string, string>> TestsVars { get; set; }
    }

    public partial class TestVars
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
