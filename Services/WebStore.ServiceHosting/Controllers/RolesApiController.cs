using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities.Identity;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route(WebAPI.Identity.Roles)]
    [Produces("application/json")]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        private readonly RoleStore<Role, WebStoreContext> _RoleStore;

        public RolesApiController(WebStoreContext db) => _RoleStore = new RoleStore<Role, WebStoreContext>(db);
    }
}