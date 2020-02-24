using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;

namespace WebStore.Clients.Identity
{
    public class RolesClient : BaseClient
    {
        public RolesClient(IConfiguration config) : base(config, WebAPI.Identity.Roles) { }

    }
}
