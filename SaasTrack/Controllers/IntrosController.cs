using Newtonsoft.Json;
using Saastrack.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaasTrack.Controllers
{
    public class IntrosController : ApiController
    {
        // GET: api/Intros
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Intros/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Intros
        public void Post([FromBody]string value)
        {
            dynamic newItem = JsonConvert.DeserializeObject(value);
            string email = newItem.email.ToString();
            string user = newItem.user.ToString();
            if(newItem.service == "services")
            {
                CompanyUserOperations.UpdateHasSeenIntro(user, email);
            }
            if (newItem.service == "employee")
            {
                CompanyUserOperations.UpdateHasSeenIntroEmployee(newItem.user, newItem.email);
            }
        }

        // PUT: api/Intros/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Intros/5
        public void Delete(int id)
        {
        }
    }
}
