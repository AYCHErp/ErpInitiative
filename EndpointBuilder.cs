using System.Text;

namespace InitiativeNepal
{
    public sealed class EndpointBuilder
    {
        private Config Config { get; set; }
        private string Destination { get; set; }
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
            this.Destination = phoneNumber;
            return this;
        }

        public string Get()
        {
            if (string.IsNullOrWhiteSpace(this.Config?.ApiUrl) || string.IsNullOrWhiteSpace(this.Config.Username) || string.IsNullOrWhiteSpace(this.Config.Password))
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            builder.Append(this.Config.ApiUrl);
            builder.Append("?");
            builder.Append($"username={this.Config.Username}");
            builder.Append($"&password={this.Config.Password}");
            builder.Append($"&message={this.Message}");
            builder.Append($"&destination={this.Destination}");
            builder.Append($"&sender={this.Config.SenderId}");

            return builder.ToString();
        }
    }
}