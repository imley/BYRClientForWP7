using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using RestSharp;

namespace BYRClient.Models
{
    public class ByrApi
    {
        //const string BaseUrl = "http://nforum.byr.edu.cn/byr/api";
        const string BaseUrl = "http://api.byr.cn";

        private string _accountSid;
        private string _secretKey;
        private const string _appKey = "";        

        public ByrApi(string accountSid, string secretKey)
        {
            _accountSid = accountSid;
            _secretKey = secretKey;
        }

        public string getApiKey()
        {
            return _appKey;
        }

        public string getUserId()
        {
            return _accountSid;
        }

        public string getPassword()
        {
            return _secretKey;
        }

        public void SetAuthinfo(string accountSid, string secretKey)
        {
            _accountSid = accountSid;
            _secretKey = secretKey;
        }

        public void Execute<T>(RestRequest request, Action<T> success, Action<string> failure) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = BaseUrl;
            client.Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey);
            //request.AddUrlSegment()
            request.AddParameter("appkey", _appKey); // used on every request
            client.ExecuteAsync<T>(request, (response) =>
            {
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    response.ErrorException.ToString();
                    failure(response.ErrorMessage);                    
                }
                else
                {
                    if (response.Data == null)
                    {
                        failure("Some Thing Goes Wrong!");
                        return;
                    }
                    success(response.Data);                    
                }
            });
        }
    }
}
