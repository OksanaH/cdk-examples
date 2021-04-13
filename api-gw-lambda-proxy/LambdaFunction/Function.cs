using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace DayOfWeek {
    public class Function {
        public APIGatewayProxyResponse FunctionHandler(Stream stream, ILambdaContext context) {
            using var sr = new StreamReader(stream);

            var input = sr.ReadToEnd();
            var dateFromPath = Convert.ToDateTime(JObject.Parse(input)["path"].ToString().TrimStart('/'));
            return new APIGatewayProxyResponse
             {
                 Body = $"{dateFromPath.ToString("DD-MM-YYY")} was a {dateFromPath.DayOfWeek}",
                 StatusCode = 200
             };
        }
    }
}