using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ECS.Patterns;
using Amazon.CDK.AWS.ECR;
using System.IO;

namespace Example3
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

            //container
           /*
            var repo = Repository.FromRepositoryName(this, "myrepo","MyRepositoryName");

            var containerOptions = new ContainerDefinitionOptions
            {
                Image =  ContainerImage.FromEcrRepository(repo)
            };
            */
            // to build the container image from the app in the local folder, replace lines 29-35 with 


            //var rootDirectory = Directory.GetCurrentDirectory();
            //var path = Path.GetFullPath(Path.Combine(rootDirectory, @"App/WhatDayOfWeek"));

            var containerOptions = new ContainerDefinitionOptions
            {
                Image = ContainerImage.FromAsset(@"App/WhatDayOfWeek"),
                Logging = logging
            };

            var portMapping = new PortMapping()
            {
                ContainerPort = 80,
                HostPort = 80
            };

            var taskDef = new FargateTaskDefinition(this, "WhatDayOfWeekTaskDefinition");             

            taskDef.AddContainer("WhatDayOfWeekContainer", containerOptions).AddPortMappings(portMapping);

            var serviceProps = new ApplicationLoadBalancedFargateServiceProps()
            {
                ServiceName = "WhatDayOfWeekService",
                MemoryLimitMiB = 512,
                Cpu = 256,
                TaskDefinition = taskDef,
                Cluster = cluster                
            };

            ApplicationLoadBalancedFargateService service = new ApplicationLoadBalancedFargateService(this, "WhatDayOfWeekService", serviceProps);
        }
    }
}
