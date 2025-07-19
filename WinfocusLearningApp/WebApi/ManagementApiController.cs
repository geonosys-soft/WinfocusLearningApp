using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.WebApi
{
    public class ManagementApiController : ApiController
    {
        
        Winfocus_CS winfocus_CS = new Winfocus_CS();
        // GET: api/ManagementApi

        [Route("api/ManagementApi/AddYear")]
        [ResponseType(typeof(TblAccademicYear))]
        public IHttpActionResult AddYear(TblAccademicYear jsonData)
        {
           
               try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the year to the database
                winfocus_CS.TblAccademicYears.Add(jsonData);
                winfocus_CS.SaveChanges();
                return Ok("Year added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
            
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
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
        }
    }
}