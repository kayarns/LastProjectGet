
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using LastProjectGet;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;
using System.Dynamic;
using Amazon.DynamoDBv2.Model;
using Amazon;
using Amazon.Runtime;

namespace LastProjectGet.Tests
{
    public class FunctionTest
   
    {
        //public static readonly HttpClient client = new HttpClient();
        public static readonly AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        public static string tableName = "LastProject";

        [Fact]
        public async Task TestToGet()
        {


            string id = "95";

            GetItemResponse res = await client.GetItemAsync(tableName, new Dictionary<string, AttributeValue>
            {
                {
                "id" , new AttributeValue { S = id}
                }
            });

            Document myDoc = Document.FromAttributeMap(res.Item);
            Joke joke = JsonConvert.DeserializeObject<Joke>(myDoc.ToJson());

            string type = "general";

            Assert.Equal(type, joke.type);
        }
    }
}
