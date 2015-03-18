using System.Collections.Generic;
using System.Web.Http;
using Tema2.Models;

namespace Tema2.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return new[]
            {
                new MyClass
                {
                    PropA = 1,
                    ProbB = new int[] {1, 2, 3},
                    PropC = true
                }
            };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
