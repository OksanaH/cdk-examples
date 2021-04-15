using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LambdaFunction {
        
    public class Function {

        public APIGatewayProxyResponse Handler(JObject request, ILambdaContext context) 
        {
            string responseBody = $"You've hit path {request["path"]}";

            request.TryGetValue("queryStringParameters", out var queryStringParams);
            
            if (queryStringParams.HasValues)
            {
                var name = request["queryStringParameters"]["name"].ToString();
                var surname = request["queryStringParameters"]["surname"].ToString();
                var dob = request["queryStringParameters"]["dob"].ToString();
                
                responseBody = $"{name} {surname} was born on a {Convert.ToDateTime(dob).DayOfWeek}";
            }

            return new APIGatewayProxyResponse
             {
                 Body = responseBody,
                 StatusCode = 200
             };
        }
    }
}