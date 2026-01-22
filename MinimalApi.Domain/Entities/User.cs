using System;
using System.Collections.Generic;
using System.Text;

using MinimalApi.Domain.ValueObjects;

namespace MinimalApi.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public Guid Pid { get; set; } = Guid.NewGuid();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }

        public DateTime DeletedAt { get; set; }
    }

    public class User : BaseEntity
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

        public void ChangePassword(Password password)
        {
            Password = password;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeEmail(Email email)
        {
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}