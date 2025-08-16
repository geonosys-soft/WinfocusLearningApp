using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.WebApi
{
    public class QAController : ApiController
    {
        Winfocus_CS db = new Winfocus_CS();
        [Route("api/QA/AddQuestion")]
        [HttpPost]
        public IHttpActionResult AddQuestion()
        {
            try
            {
                TblQuestionAnswer answer = new TblQuestionAnswer();
                var httpRequest = HttpContext.Current.Request;

                // 1. Get the uploaded file
                var file = httpRequest.Files["file"];
                if (file != null && file.ContentLength > 0)
                {
                    answer.QImage = convertFile(file);
                   // string filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + file.FileName);
                   // file.SaveAs(filePath);
                }

                // 2. Get the JSON data from form-data
                var jsonData = httpRequest.Form["jsonData"];
                if (string.IsNullOrEmpty(jsonData))
                {
                    return BadRequest("No JSON data received.");
                }

                var questionData = JsonConvert.DeserializeObject<TblQuestionAnswer>(jsonData);

                answer.Question = questionData.Question;
                answer.Option1 = questionData.Option1;
                answer.Option2 = questionData.Option2;
                answer.Option3 = questionData.Option3;
                answer.Option4 = questionData.Option4;
                answer.Answer = questionData.Answer;
                answer.Mark = questionData.Mark;
                answer.SubjectID = questionData.SubjectID;
                answer.ChapterID = questionData.ChapterID;
                answer.QuestionType = questionData.QuestionType;
                answer.AccademicYearId = questionData.AccademicYearId;
                answer.SyllabusID = questionData.SyllabusID;
                answer.ClassID = questionData.ClassID;
                answer.StreamID = questionData.StreamID;
                answer.Month = questionData.Month;
                answer.Year = questionData.Year;
                answer.Week = questionData.Week;
                answer.CompletionStatus = questionData.CompletionStatus;
                answer.Completiondate = questionData.Completiondate;
                answer.IsActive = questionData.IsActive ?? 1;
                answer.IsDeleted = 0;
                answer.CreatedBy = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).Id; ;
                answer.CreatedDt = DateTime.Now;

                // 4. Save to database

                db.TblQuestionAnswers.Add(answer);
                db.SaveChanges();
                // 3. Map properties




                return Ok("Question added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("api/QA/UpdateQuestion")]
        [HttpPost]
        public IHttpActionResult UpdateQuestion()
        {
            try
            {
                TblQuestionAnswer answer = new TblQuestionAnswer();
                var httpRequest = HttpContext.Current.Request;

                // 1. Get the uploaded file
                var file = httpRequest.Files["file"];
                if (file != null && file.ContentLength > 0)
                {
                    answer.QImage = convertFile(file);
                    // string filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + file.FileName);
                    // file.SaveAs(filePath);
                }

                // 2. Get the JSON data from form-data
                var jsonData = httpRequest.Form["jsonData"];
                if (string.IsNullOrEmpty(jsonData))
                {
                    return BadRequest("No JSON data received.");
                }

                var questionData = JsonConvert.DeserializeObject<TblQuestionAnswer>(jsonData);

                if (questionData.ID != 0)
                {
                    var findQuestion = db.TblQuestionAnswers.Find(questionData.ID);
                    if (findQuestion != null)
                    {
                        findQuestion.Question = questionData.Question;
                        findQuestion.Option1 = questionData.Option1;
                        findQuestion.Option2 = questionData.Option2;
                        findQuestion.Option3 = questionData.Option3;
                        findQuestion.Option4 = questionData.Option4;
                        findQuestion.Answer = questionData.Answer;
                        findQuestion.Mark = questionData.Mark;
                        findQuestion.SubjectID = questionData.SubjectID;
                        findQuestion.ChapterID = questionData.ChapterID;
                        findQuestion.QuestionType = questionData.QuestionType;
                        findQuestion.AccademicYearId = questionData.AccademicYearId;
                        findQuestion.SyllabusID = questionData.SyllabusID;
                        findQuestion.ClassID = questionData.ClassID;
                        findQuestion.StreamID = questionData.StreamID;
                        findQuestion.Month = questionData.Month;
                        findQuestion.Year = questionData.Year;
                        findQuestion.Week = questionData.Week;
                        findQuestion.QImage = answer.QImage ?? questionData.QImage;
                        db.Entry(findQuestion).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                
                // 3. Map properties




                return Ok("Question added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public byte[] convertFile(HttpPostedFile file)
        {
            byte[] fileContent = new byte[file.ContentLength];
            var FileExt = Path.GetExtension(file.FileName);
            //fileContent = ConvertToByteArrayChunked(file.FileName);
            file.InputStream.Read(fileContent, 0, file.ContentLength);
            return fileContent;
        }
    }
}