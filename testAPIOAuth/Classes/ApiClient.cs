using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using Newtonsoft.Json;

namespace testAPIOAuth.Classes
{
    // Based in http://blog.techcle.com/2010/03/20/simple-oauth-integration-for-twitter-in-asp-net-mvc/
    public class ApiClient
    {

        // api
        static readonly InMemoryTokenManager TokenManager = new InMemoryTokenManager("consumerKey", "consumerSecret");

        public static WebConsumer GetConsumer()
        {
            return new WebConsumer(ApiServiceDescription, TokenManager);
        }

        public static readonly ServiceProviderDescription ApiServiceDescription
    = new ServiceProviderDescription
    {
        RequestTokenEndpoint = new MessageReceivingEndpoint("http://212.0.111.116/oauth/request_token", HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
        UserAuthorizationEndpoint = new MessageReceivingEndpoint("http://212.0.111.116/oauth/authorize", HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
        AccessTokenEndpoint = new MessageReceivingEndpoint("http://212.0.111.116/oauth/access_token", HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
        TamperProtectionElements = new ITamperProtectionChannelBindingElement[] { new HmacSha1SigningBindingElement() },
    };

        public static readonly MessageReceivingEndpoint GetChatsEndpoint = new MessageReceivingEndpoint("http://212.0.111.116/v1/chats.json", HttpDeliveryMethods.GetRequest);

        public static MessageReceivingEndpoint GetChatDetailsEndpoint(string id)
        {
            return new MessageReceivingEndpoint(string.Format("http://212.0.111.116/v1/chats/{0}.json",id), HttpDeliveryMethods.GetRequest);
        }

        public static T Call<T>(MessageReceivingEndpoint endpoint, ConsumerBase api, string accessToken)
        {
            var response = api.PrepareAuthorizedRequestAndSend(endpoint, accessToken);
            var contents = response.GetResponseReader().ReadToEnd();
            return JsonConvert.DeserializeObject<T>(contents);
        }
    }
}
