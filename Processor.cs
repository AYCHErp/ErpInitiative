using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Frapid.Messaging;
using Frapid.Messaging.DTO;
using Newtonsoft.Json.Linq;
using Serilog;

namespace InitiativeNepal
{
    public sealed class Processor : ISmsProcessor
    {
        public bool IsEnabled { get; set; }
        public ISmsConfig Config { get; set; }

        public void InitializeConfig(string database)
        {
            var config = ConfigurationManager.Get(database);
            this.Config = config;

            this.IsEnabled = this.Config.Enabled;

            if (!this.IsEnabled)
                return;

            if (string.IsNullOrWhiteSpace(config.AuthenticationToken))
                this.IsEnabled = false;
        }

        public async Task<bool> SendAsync(SmsMessage sms)
        {
            var config = this.Config as Config;
            if (config == null)
            {
                sms.Status = Status.Cancelled;
                return false;
            }

            try
            {
                string sendTo = sms.SendTo.Split(',').FirstOrDefault();

                if (sendTo == null || !sendTo.StartsWith("9") || sendTo.Length != 10)
                    return false;


                var endpoint = EndpointBuilder.Initialize
                    .WithConfiguration(config)
                    .AddMessage(sms.Message)
                    .SendTo(sendTo);

                var request = GetRequest(config, endpoint);

                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    var stream = response.GetResponseStream();

                    if (stream == null)
                    {
                        sms.Status = Status.Unknown;
                        return false;
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        string data = await reader.ReadToEndAsync().ConfigureAwait(false);
                        dynamic result = JObject.Parse(data);

                        if (result == null)
                        {
                            sms.Status = Status.Failed;
                            return false;
                        }

                        int status = (int) result.response_code;

                        switch (status)
                        {
                            case 200:
                                sms.Status = Status.Completed;
                                break;
                            default:
                                sms.Status = Status.Failed;
                                break;
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                sms.Status = Status.Failed;
                Log.Warning(@"Could not send sms to {To} using InitiativeNepal. {Ex}. ", sms.SendTo, ex);
            }

            return false;
        }

        private static HttpWebRequest GetRequest(Config config, EndpointBuilder endpoint)
        {
            var request = (HttpWebRequest) WebRequest.Create(endpoint.Get());

            request.ContentType = config.ContentType;
            request.Method = config.Method;
            request.Accept = config.AcceptHeader;

            return request;
        }
    }
}