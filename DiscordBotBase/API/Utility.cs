﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            return response.Content;
        }
    }
}
