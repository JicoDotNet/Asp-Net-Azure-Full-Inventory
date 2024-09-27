﻿using JicoDotNet.Inventory.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.Core.Common.Auth
{
    public class SessionToken : ISessionToken
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public DateTime? TokenDate { get; set; }

        public SessionToken(ISessionCredential credential)
        {
            this.UserFullName = credential.UserFullName;
            this.UserEmail = credential.UserEmail;
            this.Token = credential.Token;
            this.TokenDate = credential.TokenDate;
        }
    }
}
