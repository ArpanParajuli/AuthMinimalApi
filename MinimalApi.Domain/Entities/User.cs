using System;
using System.Collections.Generic;
using System.Text;

using MinimalApi.Domain.ValueObjects;

namespace MinimalApi.Domain.Entities
{
    internal class BaseEntity
    {
        public int Id { get; set; }

        public Guid Pid { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
    }

    internal class User : BaseEntity
    {
        public Password password { get; private set; } // password pailai hash
        public Email email { get; private set; }
        public bool IsActive { get; private set; }

        public User(Password password, Email email)
        {
            this.password = password;
            this.email = email;
            IsActive = true;
        }
    }
}