using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IMedXModels;
using IMedXModels.Input;
using IMedXUtilities;
using IMedXUtilities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMedXHealthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientHealthController : ControllerBase
    {
        IPatientData patientData;
        // GET: api/PatientHealth
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PatientHealth/5
        [HttpGet(Name = "GetPatientDataCurrent")]
        public IActionResult GetPatientDataCurrent([FromBody]InputRequestData reqData)
        {
            List<IMedXPatientData> patientdata = new List<IMedXPatientData>();
            try
            {
                List<InputPatientICD> inputPatientICD = IMedXUtility.PrepareICDEntries(reqData.icdFeedData, reqData.icdColumnNames);
                List<InputPatientNDC> inputPatientNDC = IMedXUtility.PrepareNDCEntries(reqData.ndcFeedData, reqData.ndcColumnNames);
                patientdata = IMedXUtility.MergePatientData(inputPatientICD, inputPatientNDC);

                return Ok(patientdata);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet(Name ="GetPatientData")]
        public IActionResult GetPatientData()
        {
            try
            {
                return Ok(patientData.GetPatientData());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet(Name = "GetPatientDataRange")]
        public IActionResult GetPatientDataRange(DateTime startDate,DateTime endDate)
        {
            try
            {
                return Ok(patientData.GetPatientDataRange(startDate,endDate));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //[HttpPost, DisableRequestSizeLimit]
        //public IActionResult Upload()
        //{
        //    try
        //    {
        //        var file = Request.Form.Files[0];
        //        var folderName = Path.Combine("StaticFiles", "InputFiles");
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //        if (file.Length > 0)
        //        {
        //            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            var fullPath = Path.Combine(pathToSave, fileName);
        //            var dbPath = Path.Combine(folderName, fileName);

        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }

        //            return Ok(new { dbPath });
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex}");
        //    }
        //}

        //public IActionResult Upload()
        //{
        //    try
        //    {
        //        var files = Request.Form.Files;
        //        var folderName = Path.Combine("StaticFiles", "InputFiles");
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //        if (files.Any(f => f.Length == 0))
        //        {
        //            return BadRequest();
        //        }

        //        foreach (var file in files)
        //        {
        //            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            var fullPath = Path.Combine(pathToSave, fileName);
        //            var dbPath = Path.Combine(folderName, fileName);

        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }
        //        }

        //        return Ok("All the files are successfully uploaded.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Internal server error");
        //    }
        //}
        /// <summary>
        /// The Method that Takes the FeedName, FeedDataLines from File,Feed Column Names and Moves to the Database table.
        /// </summary>
        /// <param name="feedDetails"></param>
        /// <returns></returns>
        //[HttpPost(Name ="AddFileFeed",Order =1)]
        //public IActionResult AddFileFeed([FromBody] FeedDetails feedDetails)
        //{
        //    try
        //    {
        //        DataTable tmpDataTab = DataUtility.BuildDataTableForDelimitedData(feedDetails.FeedDataLines, feedDetails.FeedName, feedDetails.FeedColumnNames);
        //        DBConnectify.InsertBulk(tmpDataTab, DBConnectify.DbConnectionString, "tmp" + feedDetails.FeedName, feedDetails.FeedColumnNames, feedDetails.FeedColumnNames);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        
    }
}
