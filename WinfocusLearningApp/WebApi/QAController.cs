using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        [Route("api/QA/AddQuestion")]
        [HttpPost]
        public IHttpActionResult AddQuestion()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                // 1. Get the uploaded file
                var file = httpRequest.Files["file"];
                if (file != null && file.ContentLength > 0)
                {
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

                // 3. Save questionData to database
                // db.TblQuestionAnswers.Add(questionData);
                // db.SaveChanges();

                return Ok("Question added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}