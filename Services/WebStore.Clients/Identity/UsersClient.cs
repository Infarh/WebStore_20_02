using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;

namespace WebStore.Clients.Identity
{
    public class UsersClient : BaseClient
    {
        public UsersClient(IConfiguration config) : base(config, WebAPI.Identity.Users) { }
    }
}