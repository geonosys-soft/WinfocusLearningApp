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
        //Accademic Year Management APIs
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
                TblAccademicYear inf=new TblAccademicYear();
                inf.AccademicYear = jsonData.AccademicYear;
                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;
               
                winfocus_CS.TblAccademicYears.Add(inf);
                winfocus_CS.SaveChanges();
                return Ok("Year added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/UpdateAcYear")]
        [ResponseType(typeof(TblAccademicYear))]
        public IHttpActionResult UpdateAcYear(TblAccademicYear jsonData)
        {

            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the year to the database
                TblAccademicYear inf = new TblAccademicYear();
                inf = winfocus_CS.TblAccademicYears.Find(jsonData.Id);
                inf.AccademicYear = jsonData.AccademicYear;
                inf.IsDeleted = 0;
                inf.ModifiedBy= 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime= DateTime.UtcNow;
               winfocus_CS.TblAccademicYears.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Year updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        [Route("api/ManagementApi/DltAcYear")]
        [ResponseType(typeof(TblAccademicYear))]
        public IHttpActionResult DltAcYear(TblAccademicYear jsonData)
        {

            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the year to the database
                TblAccademicYear inf = new TblAccademicYear();
                inf = winfocus_CS.TblAccademicYears.Find(jsonData.Id);
                inf.IsDeleted = 1;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblAccademicYears.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Year updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }



        }
        [HttpGet]
        [Route("api/ManagementApi/FetchAccademicYear")]
        public IHttpActionResult FetchAccademicYear()
        {
            List<TblAccademicYear> list = new List<TblAccademicYear>();

            try
            {
               var ayList= winfocus_CS.TblAccademicYears.Where(x=>x.IsDeleted==0).ToList();
                return Ok(ayList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }
       
        [HttpPost]
        [Route("api/ManagementApi/ListAccademicYear")]
        public IHttpActionResult ListAccademicYear()
        {
            List<TblAccademicYear> list = new List<TblAccademicYear>();

            try
            {
                var ayList = winfocus_CS.TblAccademicYears.ToList();
                return Ok(ayList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        //Grade Management APIs
        [Route("api/ManagementApi/AddGrade")]
        [ResponseType(typeof(TblGrade))]
        public IHttpActionResult AddGrade(TblGrade jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the grade to the database
                TblGrade inf = new TblGrade();
                inf.Name = jsonData.Name;
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;
                winfocus_CS.TblGrades.Add(inf);
                winfocus_CS.SaveChanges();
                return Ok("Grade added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/UpdateGrade")]
        [ResponseType(typeof(TblGrade))]
        public IHttpActionResult UpdateGrade(TblGrade jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to update the grade in the database
                TblGrade inf = winfocus_CS.TblGrades.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.Name = jsonData.Name;
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.IsDeleted = 0;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblGrades.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Grade updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/DltGrade")]
        [ResponseType(typeof(TblGrade))]
        public IHttpActionResult DltGrade(TblGrade jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to delete the grade in the database
                TblGrade inf = winfocus_CS.TblGrades.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.IsDeleted = 1;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblGrades.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Grade deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/FetchGrade")]
        [HttpGet]
        public IHttpActionResult FetchGrade()
        {
            try
            {
                var gradeList = winfocus_CS.TblGrades.Where(x => x.IsDeleted == 0).ToList();
                return Ok(gradeList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Stream Management APIs
        [Route("api/ManagementApi/AddStream")]
        [ResponseType(typeof(TblStream))]
        public IHttpActionResult AddStream(TblStream jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the stream to the database
                TblStream inf = new TblStream();
                inf.Name = jsonData.Name;
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.SyllabusID = jsonData.SyllabusID; // Assuming SyllabusID is the ID of the syllabus
                inf.GradeID = jsonData.GradeID; // Assuming GradeID is the ID of the grade
                inf.Description = jsonData.Description;
                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;
                winfocus_CS.TblStreams.Add(inf);
                winfocus_CS.SaveChanges();
                return Ok("Stream added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/UpdateStream")]
        [ResponseType(typeof(TblStream))]
        public IHttpActionResult UpdateStream(TblStream jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to update the stream in the database
                TblStream inf = winfocus_CS.TblStreams.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.Name = jsonData.Name;
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.SyllabusID = jsonData.SyllabusID; // Assuming SyllabusID is the ID of the syllabus
                inf.GradeID = jsonData.GradeID; // Assuming GradeID is the ID of the grade
                inf.Description = jsonData.Description;
                inf.IsDeleted = 0;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblStreams.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Stream updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/DltStream")]
        [ResponseType(typeof(TblStream))]
        public IHttpActionResult DltStream(TblStream jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to delete the stream in the database
                TblStream inf = winfocus_CS.TblStreams.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.IsDeleted = 1;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblStreams.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Stream deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/FetchStream")]
        [HttpGet]
        public IHttpActionResult FetchStream()
        {
            try
            {
                var streamList = winfocus_CS.TblStreams.Where(x => x.IsDeleted == 0).ToList();
                return Ok(streamList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //Course Management APIs
        [Route("api/ManagementApi/AddCourse")]
        [ResponseType(typeof(TblCourse))]
        public IHttpActionResult AddCourse(TblCourse jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the course to the database
                TblCourse inf = new TblCourse();
                inf.Name = jsonData.Name;
                inf.StreamID = jsonData.StreamID; // Assuming StreamID is the ID of the stream
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.SyllabusID = jsonData.SyllabusID; // Assuming SyllabusID is the ID of the syllabus
                inf.StreamID = jsonData.StreamID; // Assuming StreamID is the ID of the stream
                inf.GradeID= jsonData.GradeID; // Assuming GradeID is the ID of the grade
              inf.Description = jsonData.Description;
                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;
                winfocus_CS.TblCourses.Add(inf);
                winfocus_CS.SaveChanges();
                return Ok("Course added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/UpdateCourse")]
        [ResponseType(typeof(TblCourse))]
        public IHttpActionResult UpdateCourse(TblCourse jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to update the course in the database
                TblCourse inf = winfocus_CS.TblCourses.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.Name = jsonData.Name;
                inf.StreamID = jsonData.StreamID; // Assuming StreamID is the ID of the stream
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.SyllabusID = jsonData.SyllabusID; // Assuming SyllabusID is the ID of the syllabus
                inf.StreamID = jsonData.StreamID; // Assuming StreamID is the ID of the stream
                inf.GradeID = jsonData.GradeID; // Assuming GradeID is the ID of the grade
                inf.Description = jsonData.Description;
                inf.IsDeleted = 0;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblCourses.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Course updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/DltCourse")]
        [ResponseType(typeof(TblCourse))]
        public IHttpActionResult DltCourse(TblCourse jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to delete the course in the database
                TblCourse inf = winfocus_CS.TblCourses.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.IsDeleted = 1;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblCourses.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Course deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/FetchCourse")]
        [HttpGet]
        public IHttpActionResult FetchCourse()
        {
            try
            {
                var courseList = winfocus_CS.TblCourses.Where(x => x.IsDeleted == 0).ToList();
                return Ok(courseList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Subject Management APIs
        [Route("api/ManagementApi/AddSubject")]
        [ResponseType(typeof(TblSubject))]
        
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