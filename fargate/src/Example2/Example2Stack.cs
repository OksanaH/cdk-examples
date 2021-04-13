/*using Amazon.CDK;
using System.IO;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ECS.Patterns;

namespace Example2
{
    public class Example2Stack : Stack
    {
        internal Example2Stack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var vpcStack = new VpcStack(this, "someTestVpc");

            var cluster = new Cluster(this, "WhatDayOfWeekCluster", new ClusterProps
            {
                Vpc = vpcStack.Vpc
            }) ;

            var taskDef = new FargateTaskDefinition(this, "WhatDayOfWeekTaskDefinition");

            var rootDirectory = Directory.GetCurrentDirectory();

            var path = Path.GetFullPath(Path.Combine(rootDirectory, @"App/WhatDayOfWeek"));

            var containerOptions = new ContainerDefinitionOptions
            {
                Image = ContainerImage.FromAsset("App/WhatDayOfWeek")
            };

            var portMapping = new PortMapping()
            {
                ContainerPort = 80,
                HostPort = 80
            };

            taskDef.AddContainer("WhatDayOfWeekContainer", containerOptions).AddPortMappings(portMapping);

            var serviceProps = new ApplicationLoadBalancedFargateServiceProps()
            {
                MemoryLimitMiB = 512,
                Cpu = 256,
                TaskDefinition = taskDef
            };

            ApplicationLoadBalancedFargateService service = new ApplicationLoadBalancedFargateService(this, "WhatDayOfWeekApp", serviceProps);
        }
    }
}*/
