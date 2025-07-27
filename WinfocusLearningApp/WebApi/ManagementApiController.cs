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
        //Syllabus Management APIs
        [Route("api/ManagementApi/AddSyllabus")]
        [ResponseType(typeof(TblSyllabu))]
        public IHttpActionResult AddSyllabus(TblSyllabu jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the syllabus to the database
                TblSyllabu inf = new TblSyllabu();
                inf.Name = jsonData.Name;
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;
                winfocus_CS.TblSyllabus.Add(inf);
                winfocus_CS.SaveChanges();
                return Ok("Syllabus added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/UpdateSyllabus")]
        [ResponseType(typeof(TblSyllabu))]
        public IHttpActionResult UpdateSyllabus(TblSyllabu jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to update the syllabus in the database
                TblSyllabu inf = winfocus_CS.TblSyllabus.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.Name = jsonData.Name;
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.IsDeleted = 0;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblSyllabus.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Syllabus updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/DltSyllabus")]
        [ResponseType(typeof(TblSyllabu))]
        public IHttpActionResult DltSyllabus(TblSyllabu jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to delete the syllabus in the database
                TblSyllabu inf = winfocus_CS.TblSyllabus.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.IsDeleted = 1;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblSyllabus.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Syllabus deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/FetchSyllabus/{Id}")]
        [HttpGet]
        public IHttpActionResult FetchSyllabus(int Id)
        {
            try
            {
                var syllabusList = winfocus_CS.TblSyllabus.Where(x => x.IsDeleted == 0 && x.ACID== Id).ToList();
                return Ok(syllabusList);
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
                inf.SyllabusID = jsonData.SyllabusID;
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
        [Route("api/ManagementApi/FetchGrade/{Id}")]
        [HttpGet]
        public IHttpActionResult FetchGrade(int Id)
        {
            try
            {
                var gradeList = winfocus_CS.TblGrades.Where(x => x.IsDeleted == 0 && x.SyllabusID==Id).ToList();
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
               /*  inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
               inf.SyllabusID = jsonData.SyllabusID; // Assuming SyllabusID is the ID of the syllabus
                inf.GradeID = jsonData.GradeID; // Assuming GradeID is the ID of the grade
                inf.Description = jsonData.Description;
                inf.IsDeleted = 0;*/
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

        [Route("api/ManagementApi/FetchStream/{Id}")]
        [HttpGet]
        public IHttpActionResult FetchStream(int Id)
        {
            try
            {
                var streamList = winfocus_CS.TblStreams.Where(x => x.IsDeleted == 0&& x.GradeID==Id).ToList();
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
              /*  inf.StreamID = jsonData.StreamID; // Assuming StreamID is the ID of the stream
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.SyllabusID = jsonData.SyllabusID; // Assuming SyllabusID is the ID of the syllabus
                inf.StreamID = jsonData.StreamID; // Assuming StreamID is the ID of the stream
                inf.GradeID = jsonData.GradeID; // Assuming GradeID is the ID of the grade
                inf.Description = jsonData.Description;
                inf.IsDeleted = 0;*/
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
        [Route("api/ManagementApi/FetchCourse/{Id}")]
        [HttpGet]
        public IHttpActionResult FetchCourse(int Id)
        {
            try
            {
                var courseList = winfocus_CS.TblCourses.Where(x => x.IsDeleted == 0&& x.StreamID==Id).ToList();
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
        public IHttpActionResult AddSubject(TblSubject jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the subject to the database
                TblSubject inf = new TblSubject();
                inf.Name = jsonData.Name;
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.SyllabusID = jsonData.SyllabusID; // Assuming SyllabusID is the ID of the syllabus
                inf.GradeID = jsonData.GradeID; // Assuming GradeID is the ID of the grade
                inf.StreamID = jsonData.StreamID; // Assuming StreamID is the ID of the stream
                inf.CourseID = jsonData.CourseID; // Assuming CourseID is the ID of the course
                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;
                winfocus_CS.TblSubjects.Add(inf);
                winfocus_CS.SaveChanges();
                return Ok("Subject added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/UpdateSubject")]
        [ResponseType(typeof(TblSubject))]
        public IHttpActionResult UpdateSubject(TblSubject jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to update the subject in the database
                TblSubject inf = winfocus_CS.TblSubjects.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.Name = jsonData.Name;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblSubjects.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Subject updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/RemoveSubject")]
        [ResponseType(typeof(TblSubject))]
        public IHttpActionResult RemoveSubject(TblSubject jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to delete the subject in the database
                TblSubject inf = winfocus_CS.TblSubjects.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.IsDeleted = 1;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblSubjects.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Subject deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/FetchSubject/{Id}")]
        [HttpGet]
        public IHttpActionResult FetchSubject(int Id)
        {
            try
            {
                var subjectList = winfocus_CS.TblSubjects.Where(x => x.IsDeleted == 0 && x.CourseID==Id).ToList();
                return Ok(subjectList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //Chapter Management APIs
        [Route("api/ManagementApi/AddChapter")]
        [ResponseType(typeof(TblChapter))]
        public IHttpActionResult AddChapter(TblChapter jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the chapter to the database
                TblChapter inf = new TblChapter();
                inf.ChapterName = jsonData.ChapterName;
                inf.SubjectID = jsonData.SubjectID; // Assuming SubjectID is the ID of the subject
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.SyllabusID = jsonData.SyllabusID; // Assuming SyllabusID is the ID of the syllabus
                inf.GradeID = jsonData.GradeID; // Assuming GradeID is the ID of the grade
                inf.StreamID = jsonData.StreamID; // Assuming StreamID is the ID of the stream
                inf.CourseID = jsonData.CourseID; // Assuming CourseID is the ID of the course
                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;
                winfocus_CS.TblChapters.Add(inf);
                winfocus_CS.SaveChanges();
                return Ok("Chapter added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/UpdateChapter")]
        [ResponseType(typeof(TblChapter))]
        public IHttpActionResult UpdateChapter(TblChapter jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to update the chapter in the database
                TblChapter inf = winfocus_CS.TblChapters.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.ChapterName = jsonData.ChapterName;            
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblChapters.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Chapter updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/DltChapter")]
        [ResponseType(typeof(TblChapter))]
        public IHttpActionResult DltChapter(TblChapter jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to delete the chapter in the database
                TblChapter inf = winfocus_CS.TblChapters.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.IsDeleted = 1;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblChapters.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Chapter deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/FetchChapter/{Id}")]
        [HttpGet]
        public IHttpActionResult FetchChapter(int Id)
        {
            try
            {
                var chapterList = winfocus_CS.TblChapters.Where(x => x.IsDeleted == 0 && x.SubjectID==Id).ToList();
                return Ok(chapterList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //SUB CHAPTER Management APIs
        [Route("api/ManagementApi/AddSubChapter")]
        [ResponseType(typeof(TblSubChapter))]
        public IHttpActionResult AddSubChapter(TblSubChapter jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the sub-chapter to the database
                TblSubChapter inf = new TblSubChapter();
                inf.SubChapterName = jsonData.SubChapterName;
                inf.ChapterID = jsonData.ChapterID; // Assuming ChapterID is the ID of the chapter
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.SyllabusID = jsonData.SyllabusID; // Assuming SyllabusID is the ID of the syllabus
                inf.GradeID = jsonData.GradeID; // Assuming GradeID is the ID of the grade
                inf.StreamID = jsonData.StreamID; // Assuming StreamID is the ID of the stream
                inf.CourseID = jsonData.CourseID; // Assuming CourseID is the ID of the course
                inf.SubjectID = jsonData.SubjectID; // Assuming SubjectID is the ID of the subject
                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;
                winfocus_CS.TblSubChapters.Add(inf);
                winfocus_CS.SaveChanges();
                return Ok("Sub-Chapter added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/UpdateSubChapter")]
        [ResponseType(typeof(TblSubChapter))]
        public IHttpActionResult UpdateSubChapter(TblSubChapter jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to update the sub-chapter in the database
                TblSubChapter inf = winfocus_CS.TblSubChapters.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.SubChapterName = jsonData.SubChapterName;            
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblSubChapters.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Sub-Chapter updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/DltSubChapter")]
        [ResponseType(typeof(TblSubChapter))]
        public IHttpActionResult DltSubChapter(TblSubChapter jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to delete the sub-chapter in the database
                TblSubChapter inf = winfocus_CS.TblSubChapters.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.IsDeleted = 1;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblSubChapters.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Sub-Chapter deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/FetchSubChapter/{Id}")]
        [HttpGet]
        public IHttpActionResult FetchSubChapter(int Id)
        {
            try
            {
                var subChapterList = winfocus_CS.TblSubChapters.Where(x => x.IsDeleted == 0 && x.ChapterID == Id).ToList();
                return Ok(subChapterList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/AddModule")]
        [ResponseType(typeof(TblModule))]
        public IHttpActionResult AddModule(TblModule jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the module to the database
                TblModule inf = new TblModule();
                inf.ModuleName = jsonData.ModuleName;
                inf.SubChapterID = jsonData.SubChapterID; // Assuming SubChapterID is the ID of the sub-chapter
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.SyllabusID = jsonData.SyllabusID; // Assuming SyllabusID is the ID of the syllabus
                inf.GradeID = jsonData.GradeID; // Assuming GradeID is the ID of the grade
                inf.StreamID = jsonData.StreamID; // Assuming StreamID is the ID of the stream
                inf.CourseID = jsonData.CourseID; // Assuming CourseID is the ID of the course
                inf.SubjectID = jsonData.SubjectID; // Assuming SubjectID is the ID of the subject
                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;
                winfocus_CS.TblModules.Add(inf);
                winfocus_CS.SaveChanges();
                return Ok("Module added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/UpdateModule")]
        [ResponseType(typeof(TblModule))]
        public IHttpActionResult UpdateModule(TblModule jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to update the module in the database
                TblModule inf = winfocus_CS.TblModules.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.ModuleName = jsonData.ModuleName;            
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblModules.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Module updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/RemoveModule")]
        [ResponseType(typeof(TblModule))]
        public IHttpActionResult RemoveModule(TblModule jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to delete the module in the database
                TblModule inf = winfocus_CS.TblModules.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.IsDeleted = 1;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblModules.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Module deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/FetchModule/{Id}")]
        [HttpGet]
        public IHttpActionResult FetchModule(int Id)
        {
            try
            {
                var moduleList = winfocus_CS.TblModules.Where(x => x.IsDeleted == 0 && x.SubChapterID == Id).ToList();
                return Ok(moduleList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/AddNoteType")]
        [ResponseType(typeof(TblNoteType))]
        public IHttpActionResult AddNoteType(TblNoteType jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the note type to the database
                TblNoteType inf = new TblNoteType();
                inf.Name = jsonData.Name;
                inf.ACID = jsonData.ACID; // Assuming ACID is the ID of the academic year
                inf.SyllabusID = jsonData.SyllabusID; // Assuming SyllabusID is the ID of the syllabus
                inf.GradeID = jsonData.GradeID; // Assuming GradeID is the ID of the grade
                inf.StreamID = jsonData.StreamID; // Assuming StreamID is the ID of the stream
                inf.CourseID = jsonData.CourseID; // Assuming CourseID is the ID of the course
                inf.SubjectID = jsonData.SubjectID; // Assuming SubjectID is the ID of the subject
              
                inf.Description = jsonData.Description; // Assuming Description is a field in TblNoteType
                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;
                winfocus_CS.TblNoteTypes.Add(inf);
                winfocus_CS.SaveChanges();
                return Ok("Note Type added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/UpdateNoteType")]
        [ResponseType(typeof(TblNoteType))]
        public IHttpActionResult UpdateNoteType(TblNoteType jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to update the note type in the database
                TblNoteType inf = winfocus_CS.TblNoteTypes.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.Name = jsonData.Name;            
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblNoteTypes.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Note Type updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/RemoveNoteType")]
        [ResponseType(typeof(TblNoteType))]
        public IHttpActionResult RemoveNoteType(TblNoteType jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to delete the note type in the database
                TblNoteType inf = winfocus_CS.TblNoteTypes.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.IsDeleted = 1;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblNoteTypes.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Note Type deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/FetchNoteType/{Id}")]
        [HttpGet]
        public IHttpActionResult FetchNoteType(int Id)
        {
            try
            {
                var noteTypeList = winfocus_CS.TblNoteTypes.Where(x => x.IsDeleted == 0 && x.SubjectID== Id).ToList();
                return Ok(noteTypeList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/ManagementApi/AddMaterial")]
        [ResponseType(typeof(TblMaterial))]
        public IHttpActionResult AddMaterial(TblMaterial jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the material to the database
                TblMaterial inf = new TblMaterial();
                inf.Name = jsonData.Name;
                inf.CourseID = jsonData.CourseID;
                inf.SubjectId = jsonData.SubjectId;
                inf.ACID = jsonData.ACID;
                inf.GradeID = jsonData.GradeID;
                inf.StreamID = jsonData.StreamID;// Assuming FilePath is the path to the material file
                inf.SubjectId = jsonData.SubjectId;
                inf.SyllabusID = inf.SyllabusID;// Assuming SubjectID is the ID of the subject
                inf.NoteTypeID = jsonData.NoteTypeID;
                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;
                winfocus_CS.TblMaterials.Add(inf);
                winfocus_CS.SaveChanges();
                return Ok("Material added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/UpdateMaterial")]
        [ResponseType(typeof(TblMaterial))]
        public IHttpActionResult UpdateMaterial(TblMaterial jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to update the material in the database
                TblMaterial inf = winfocus_CS.TblMaterials.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.Name = jsonData.Name;
               
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblMaterials.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Material updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/RemoveMaterial")]
        [ResponseType(typeof(TblMaterial))]
        public IHttpActionResult RemoveMaterial(TblMaterial jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to delete the material in the database
                TblMaterial inf = winfocus_CS.TblMaterials.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.IsDeleted = 1;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.TblMaterials.Attach(inf);
                winfocus_CS.Entry(inf).State = System.Data.Entity.EntityState.Modified;
                winfocus_CS.SaveChanges();
                return Ok("Material deleted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/ManagementApi/FetchMaterial/{Id}")]
        [HttpGet]
        public IHttpActionResult FetchMaterial(int Id)
        {
            try
            {
                var materialList = winfocus_CS.TblMaterials.Where(x => x.IsDeleted == 0 && x.NoteTypeID == Id).ToList();
                return Ok(materialList);
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