﻿using System;
using System.Collections.Generic;
using System.Text;

namespace websocketschat.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordHashingKey { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}