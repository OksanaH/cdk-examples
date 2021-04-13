using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ECS.Patterns;
using System.IO;
using Amazon.CDK.AWS.AutoScaling;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.CloudWatch;
using System.Collections.Generic;
using Amazon.CDK.AWS.ECR;

namespace Example2
{
    public class FargateStack : Stack
    {
        public class FargateStackProps : StackProps
        {
            public Vpc Vpc { get; set; }
        }
        public FargateStack(Construct scope, string id, FargateStackProps props = null) : base(scope, id, props)
        {
            var cluster = new Cluster(this, "WhatDayOfWeekCluster", new ClusterProps
            {
                Vpc = props.Vpc
            });

            var logging = new AwsLogDriver(new AwsLogDriverProps()
            {
                StreamPrefix = "WhatDayOfWeek",
                LogRetention = Amazon.CDK.AWS.Logs.RetentionDays.ONE_DAY
            });
            var taskDef = new FargateTaskDefinition(this, "WhatDayOfWeekTaskDefinition");           

            var repo = Repository.FromRepositoryName(this, "myrepo","MyRepositoryName");
            
            var containerOptions = new ContainerDefinitionOptions
            {
                Image =  ContainerImage.FromEcrRepository(repo)
            };
            /*
                to build the container image from the app in the local folder, replace lines 34-39 with 
                
                var rootDirectory = Directory.GetCurrentDirectory();
                var path = Path.GetFullPath(Path.Combine(rootDirectory, @"App/WhatDayOfWeek"));
                var containerOptions = new ContainerDefinitionOptions
                {
                    Image =  ContainerImage.FromAsset(@"App/WhatDayOfWeek")
                };
            */

            var portMapping = new PortMapping()
            {
                ContainerPort = 80,
                HostPort = 80
            };

            taskDef.AddContainer("WhatDayOfWeekContainer", containerOptions).AddPortMappings(portMapping);

            var serviceProps = new ApplicationLoadBalancedFargateServiceProps()
            {
                ServiceName = "WhatDayOfWeekService",
                MemoryLimitMiB = 512,
                Cpu = 256,
                TaskDefinition = taskDef                
            };

            ApplicationLoadBalancedFargateService service = new ApplicationLoadBalancedFargateService(this, "WhatDayOfWeekService", serviceProps);
        }
    }
}
