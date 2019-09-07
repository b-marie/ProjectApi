using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace projectApi.Service.Utils
{
    public class AWSCredentialsService
    {
        private string accessKey = Environment.GetEnvironmentVariable("AccessKey");
        private string privateAccessKey = Environment.GetEnvironmentVariable("PrivateAccessKey");


        public AWSCredentialsService()
        {

        }

        public AWSCredentials GetAWSCredentials()
        {
            return new BasicAWSCredentials(accessKey, privateAccessKey);
        }
    }
}
