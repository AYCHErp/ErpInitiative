using System.ComponentModel.DataAnnotations;
using Frapid.Messaging;

namespace InitiativeNepal
{
    public sealed class Config : ISmsConfig
    {
        [Required]
        public string AcceptHeader { get; set; } = "application/json";

        [Required]
        public string ContentType { get; set; } = "application/x-www-form-urlencoded;";

        [Required]
        public string Method { get; set; } = "POST";

        [Required]
        public string ApiUrl { get; set; } = "http://api.ininepal.com/api/index";

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string FromName { get; set; }
        public string FromNumber { get; set; }

        [Required]
        public bool Enabled { get; set; }
    }
}