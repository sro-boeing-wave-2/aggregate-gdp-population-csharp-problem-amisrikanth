using Newtonsoft.Json.Linq;
using System;
using System.IO;
using Xunit;
using AggregateGDPPopulation;
using System.Threading.Tasks;

namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async static void Test1()
        {
            await Class1.MethodAsync();
            StreamReader readJSON = new StreamReader(@"../../../../Output/output.json");
            string inputData = JObject.Parse(readJSON.ReadToEnd()).ToString();
            StreamReader readExpectedJSON = new StreamReader(@"../../../expected-output.json");
            string outputData = JObject.Parse(readExpectedJSON.ReadToEnd()).ToString();
            Assert.Equal(outputData, inputData);
        }
    }
}
