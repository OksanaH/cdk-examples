using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;
using static Example3.FargateStack;

namespace Example3
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();

            var vpcStack = new VpcStack(app, "VpcStack");

            new FargateStack(app, "FargateStack", new FargateStackProps
            {
                Vpc = vpcStack.Vpc
            });

            app.Synth();
        }
    }
}
