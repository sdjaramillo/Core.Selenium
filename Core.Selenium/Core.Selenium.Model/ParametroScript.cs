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
        [JsonProperty("nombrePrueba")]
        public string NombrePrueba { get; set; }

        [JsonProperty("driver")]
        public string Driver { get; set; }

        [JsonProperty("dataTest")]
        public DataTest[] DataTest { get; set; }
    }

    public partial class DataTest
    {
        [JsonProperty("suiteVars")]
        public TestVars SuiteVars { get; set; }

        [JsonProperty("tests")]
        public TestVars[][] TestsVars { get; set; }
    }

    public partial class TestVars
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
