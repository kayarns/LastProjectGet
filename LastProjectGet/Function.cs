using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Amazon.Lambda.APIGatewayEvents;



// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LastProjectGet
{
    public class Joke
    {
        public string id;
        public string type;
        public string setup;
        public string punchline;
    }
    public class Function
    {

        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private static string tableName = "LastProject";
        public async Task<Joke> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            string id = "";
            Dictionary<string, string> dict = (Dictionary<string, string>)input.QueryStringParameters;
            dict.TryGetValue("id", out id);
            GetItemResponse res = await client.GetItemAsync(tableName, new Dictionary<string, AttributeValue>
            {
                {"id", new AttributeValue {S = id } }
            }
            );

            Document myDoc = Document.FromAttributeMap(res.Item);
            Joke myJoke = JsonConvert.DeserializeObject<Joke>(myDoc.ToJson());

            return myJoke;
        }
    }
}