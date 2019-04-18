using RestSharp;
using System;

namespace DiscordBotBase.API
{
    class Utility
    {
        public string ExecuteRequest(Uri baseUri, string apiVersion, string apiQuery, Method method)
        {
            var client = new RestClient(baseUri);
            var theRequest = new RestRequest(apiVersion + apiQuery, method);

            IRestResponse response = client.Execute(theRequest);


            // Whitelist over blacklist
            if(response.StatusCode != System.Net.HttpStatusCode.OK || response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                return Convert.ToString(response.StatusCode);
            }

            return response.Content;
        }
    }
}
