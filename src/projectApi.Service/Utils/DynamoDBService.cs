using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace projectApi.Service.Utils
{
    public class DynamoDBService
    {
        private AWSCredentialsService _credentialsService;
        public DynamoDBService(AWSCredentialsService credentialsService)
        {
            _credentialsService = credentialsService;
        }

        public AmazonDynamoDBClient GetDynamoDBClient(string region)
        {
            AWSCredentials creds = _credentialsService.GetAWSCredentials();
            AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(region)
            };
            return new AmazonDynamoDBClient(creds, clientConfig);
        }

        public Table LoadTable(string tableName, string region)
        {
            return Table.LoadTable(GetDynamoDBClient(region), tableName);
        }

        public async Task<List<Document>> GetAllDocumentsFromTable(Table tableName)
        {
            ScanFilter scanFilter = new ScanFilter();
            Search search = tableName.Scan(scanFilter);
            List<Document> results = new List<Document>();
            while (!search.IsDone)
            {
                results.AddRange(await search.GetNextSetAsync());
            }

            return results;
        }
    }
}
