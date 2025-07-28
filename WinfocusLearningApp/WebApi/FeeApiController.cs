using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.WebApi
{
    public class FeeApiController : ApiController
    {
        Winfocus_CS winfocus_CS = new Winfocus_CS();

        [Route("api/fee/AddFeeDetails")]
        [ResponseType(typeof(TblFeeDetail))]
        public IHttpActionResult AddFeeDetails(TblFeeDetail jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                var data = winfocus_CS.TblFeeDetails.FirstOrDefault(x => x.ACID == jsonData.ACID && x.SyllabusID == jsonData.SyllabusID && x.GradeId == jsonData.GradeId && x.StreamID == jsonData.StreamID && x.CourseID == jsonData.CourseID && x.IsDeleted == 0);
                if(data != null)
                {
                    return BadRequest("Fee details for this combination already exists.");
                }
                // Assuming you have a method to add the year to the database
                TblFeeDetail inf = new TblFeeDetail();
                inf.TotalFee = jsonData.TotalFee;
                inf.FeeAmount = jsonData.FeeAmount;
                inf.RegistrationFee = jsonData.RegistrationFee;
                inf.DiscountPers = jsonData.DiscountPers;
                inf.ACID = jsonData.ACID;
                inf.SyllabusID = jsonData.SyllabusID;
                inf.GradeId = jsonData.GradeId;
                inf.StreamID = jsonData.StreamID;
                inf.CourseID = jsonData.CourseID;
                inf.Term1 = jsonData.Term1;
                inf.Term2 = jsonData.Term2;
                inf.Term3 = jsonData.Term3;
                inf.Term4 = jsonData.Term4;
                inf.Term5 = jsonData.Term5;
                inf.Description = jsonData.Description;
                //  var userId = winfocus_CS.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).Id;
                inf.CreatedBy = 1; // Assuming 1 is the ID of the admin user
                inf.CreatedDate = DateTime.UtcNow;
                inf.IsDeleted = 0;

                winfocus_CS.TblFeeDetails.Add(inf);
                winfocus_CS.SaveChanges();
                return Ok("Fee added successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        [Route("api/fee/UpdateFeeDetails")]
        [ResponseType(typeof(TblFeeDetail))]
        public IHttpActionResult UpdateFeeDetails(TblFeeDetail jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the year to the database
                TblFeeDetail inf = winfocus_CS.TblFeeDetails.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
                inf.TotalFee = jsonData.TotalFee;
                inf.FeeAmount = jsonData.FeeAmount;
                inf.RegistrationFee = jsonData.RegistrationFee;
                inf.DiscountPers = jsonData.DiscountPers;
                inf.Term1 = jsonData.Term1;
                inf.Term2 = jsonData.Term2;
                inf.Term3 = jsonData.Term3;
                inf.Term4 = jsonData.Term4;
                inf.Term5 = jsonData.Term5;
                inf.Description = jsonData.Description;
                //  var userId = winfocus_CS.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).Id;
                inf.ModifiedBy = 1; // Assuming 1 is the ID of the admin user
                inf.ModifiedTime = DateTime.UtcNow;
                winfocus_CS.SaveChanges();
                return Ok("Fee updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        [Route("api/fee/RemoveFeeDetails")]
        [ResponseType(typeof(TblFeeDetail))]
        public IHttpActionResult RemoveFeeDetails(TblFeeDetail jsonData)
        {
            try
            {
                if (jsonData == null)
                {
                    return BadRequest("Invalid data format.");
                }
                // Assuming you have a method to add the year to the database
                TblFeeDetail inf = winfocus_CS.TblFeeDetails.Find(jsonData.Id);
                if (inf == null)
                {
                    return NotFound();
                }
               inf.IsDeleted = 1; // Mark as deleted
                inf.DeletedDate = DateTime.UtcNow;
                winfocus_CS.SaveChanges();
                return Ok("Fee updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/fee/GetFeeDetails/{Id}")]
        [ResponseType(typeof(TblFeeDetail))]
        public IHttpActionResult GetFeeDetails(int Id)
        {
            try
            {
                var feeDetail = winfocus_CS.TblFeeDetails.FirstOrDefault(x => x.Id == Id && x.IsDeleted == 0);
                if (feeDetail == null)
                {
                    return NotFound();
                }
                return Ok(feeDetail);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}