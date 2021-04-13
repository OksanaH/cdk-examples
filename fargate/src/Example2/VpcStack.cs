using Amazon.CDK;
using Amazon.CDK.AWS.EC2;

namespace Example2
{
    public class VpcStack : Stack
    {
        public Vpc Vpc;
        public class VpcStackProps : StackProps
        {            
        }
        public VpcStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            this.Vpc = new Vpc(this, "myVpc", new VpcProps
            {   
                MaxAzs = 2
            });
        }
    }
}
