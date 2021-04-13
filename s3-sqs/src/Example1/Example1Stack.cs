using Amazon.CDK;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.SQS;
using Amazon.CDK.AWS.S3.Notifications;
namespace Example1
{
    public class Example1Stack : Stack
    {
        internal Example1Stack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var bucket = new Bucket(this, "AwsVlcBucket", new BucketProps
            {
                BucketName = "aws-vlc-12204",
                Versioned = true
            });

            var queue =  new Queue(this, "AwsVlcQueue", new QueueProps
            {
                QueueName = "aws-vlc-122204"
            });

            bucket.AddEventNotification(EventType.OBJECT_CREATED, new SqsDestination(queue));

            new CfnOutput(this, "QueueUrl", new CfnOutputProps
            {
                Value = queue.QueueUrl
            });

        }
    }
}
