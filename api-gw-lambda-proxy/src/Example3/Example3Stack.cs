using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Events;
using Amazon.CDK.AWS.Events.Targets;
using Amazon.CDK.AWS.APIGateway;
using System.Collections.Generic;

namespace Example3
{
    public class Example3Stack : Stack
    {
        internal Example3Stack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var functionHandler = new Function(this, "LambdaHandler", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("./LambdaFunction/bin/Release/netcoreapp3.1/publish"),
                Handler = "WhatDayFunction::DayOfWeek.Function::FunctionHandler",
            });
            
            var restApi = new LambdaRestApi(this, "TestApi", new LambdaRestApiProps
            {
                Handler = functionHandler,   
                RestApiName = "TestApi"
            });

            new CfnOutput(this, "out", new CfnOutputProps
            {
                Value = restApi.Url
            });
        }
    }
}
