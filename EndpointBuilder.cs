using System.Text;

namespace InitiativeNepal
{
    public sealed class EndpointBuilder
    {
        private Config Config { get; set; }
        private string To { get; set; }
        private string Message { get; set; }
        public static EndpointBuilder Initialize => new EndpointBuilder();

        public EndpointBuilder WithConfiguration(Config config)
        {
            this.Config = config;
            return this;
        }

        public EndpointBuilder AddMessage(string message)
        {
            this.Message = message;
            return this;
        }

        public EndpointBuilder SendTo(string phoneNumber)
        {
            this.To = phoneNumber;
            return this;
        }

        public string Get()
        {
            if (string.IsNullOrWhiteSpace(this.Config?.ApiUrl) || string.IsNullOrWhiteSpace(this.Config.AuthenticationToken))
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            builder.Append(this.Config.ApiUrl);
            builder.Append("?");
            builder.Append($"token={this.Config.AuthenticationToken}");
            builder.Append($"&sender={this.Config.SenderId}");
            builder.Append($"&to={this.To}");
            builder.Append($"&message={this.Message}");

            return builder.ToString();
        }
    }
}