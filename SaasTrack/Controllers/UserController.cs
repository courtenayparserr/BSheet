using Saastrack.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaasTrack.Controllers
{
    public class UserController : ApiController
    {
        // GET api/<controller>/5
        public string Get(string id)
        {
            if(!string.IsNullOrEmpty(id))
            {
                string email = UserDetailOperations.GetUserByEmail(id);
                return email;
            }
            else
            {
                return string.Empty;
            }

        }
    }
}
