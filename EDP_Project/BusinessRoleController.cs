using DBService.Models;
using EDP_Project.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EDP_Project
{
    public class PostBR
    {
        public string Name { get; set; }
        public string BusinessId { get; set; }
    }

    public class PutBR
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BusinessId { get; set; }
    }

    public class DeleteBR
    {
        public string Id { get; set; }
    }

    public class BusinessRoleController : ApiController
    {
        // GET api/<controller>
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }*/

        [HttpGet]
        public BusinessRole GetRole([FromUri] string id)
        {
            try
            {
                Service1Client client = new Service1Client();
                return client.GetBusinessRole(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public List<BusinessRole> GetRoles([FromUri] string businessId)
        {
            try
            {
                Service1Client client = new Service1Client();
                return client.GetBusinessRoles(businessId).ToList();
            }
            catch (Exception ex)
            {
                // log?
                return new List<BusinessRole>();
            }
        }

        [HttpPost]
        public BusinessRole CreateRole(PostBR br)
        {
            if (br != null)
            {
                Service1Client client = new Service1Client();
                return client.CreateBusinessRole(br.Name.Trim(), br.BusinessId.Trim());
            }
            else
                return null;
        }

        [HttpPut]
        public bool UpdateRole(PutBR br)
        {
            if (br != null)
            {
                Service1Client client = new Service1Client();
                return client.UpdateBusinessRole(br.Id.Trim(), br.Name.Trim(), br.BusinessId.Trim());
            }
            else
                return false;
        }

        [HttpDelete]
        public bool DeleteRole(DeleteBR br)
        {
            if (br != null)
            {
                Service1Client client = new Service1Client();
                return client.DeleteBusinessRole(br.Id.Trim());
            }
            else
                return false;
        }
    }
}