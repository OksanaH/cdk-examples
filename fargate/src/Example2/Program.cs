using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;
using static Example2.FargateStack;

namespace Example2
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            var vpcStack = new VpcStack(app, "someTestVpc");
            new FargateStack(app, "containerstack", new FargateStackProps
            {
                Vpc = vpcStack.Vpc
            });

            app.Synth();
        }
    }
}
