using RestSharp;

namespace DiscordBotBase.API
{
    class Utility
    {
        public DAL.CodeWarsUser ExecuteRequest(string param, string baseurl, Method method)
        {
            var client = new RestClient(baseurl);
            var theRequest = new RestRequest("{default}", method);
            theRequest.AddUrlSegment("default", param);
            var theResponse = client.Execute<DAL.CodeWarsUser>(theRequest);

            //var asyncResponse = client.ExecuteAsync(theRequest, theResponse => { theContent = theResponse.Content; });

            return theResponse.Data;
        }
    }
}
