using System;
using Clipify.Domain.Common;

namespace Clipify.Domain.Entities
{
    public class User : Entity
    {
        public string Username { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;
    }
}