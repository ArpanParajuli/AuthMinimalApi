using System;
using System.Collections.Generic;
using System.Text;

using MinimalApi.Domain.ValueObjects;

namespace MinimalApi.Domain.Entities
{
    internal class BaseEntity
    {
        public int Id { get; set; }

        public Guid Pid { get; set; } = new Guid();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }

        public DateTime DeletedAt { get; set; }
    }

    internal class User : BaseEntity
    {
        public Password Password { get; private set; } // password pailai hash
        public Email Email { get; private set; }
        public bool IsActive { get; private set; }

        private User()
        {
        }

        public User(Password password, Email email)
        {
            Password = password;
            Email = email;
            IsActive = true;
        }
    }
}