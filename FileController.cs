using BL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Hadasim_Test_Server.Controllers
{   
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    [RoutePrefix("api/File")]
    public class FileController : ApiController
    {
        [HttpGet]
        [Route("GetFileStatistic")]
        public IHttpActionResult GetFileStatistic([FromUri] string filePath)
        {
            FileBL file = new FileBL();
            file.LinesCnt = FileBL.CountLines(filePath);
            file.WordsCnt = FileBL.CountWords(filePath);
            file.DistinctWordsCnt = FileBL.CountWordsDistinct(filePath);
            file.AvgSentenceLength = FileBL.AvgLengthOfSentenceVers2(filePath);
            file.MaxSentenceLength = FileBL.MaxLengthOfSentenceVers2(filePath);
            file.LongestSeqWithoutK = FileBL.LongestSequenceWithoutK(filePath);
            file.Colors = FileBL.CountColors(filePath);
            return Ok(file);
        }
    }
}