using Newtonsoft.Json.Linq;
using System;
using System.IO;
using Xunit;
using AggregateGDPPopulation;



namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {
        [Fact]
        public static void Test1()
        {
            Class1.Method();
            StreamReader readJSON = new StreamReader(@"../../../../Output/output.json");
            string inputData = JObject.Parse(readJSON.ReadToEnd()).ToString();
            StreamReader readExpectedJSON = new StreamReader(@"../../../expected-output.json");
            string outputData = JObject.Parse(readExpectedJSON.ReadToEnd()).ToString();
            Assert.Equal(outputData, inputData);
        }
    }
}
