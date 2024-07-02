using _837ParserPOC.Parsers;
using Microsoft.AspNetCore.Mvc;
using TextParserApi.Models;

namespace TextParserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class _837ParserController : ControllerBase
    {
        /// <summary>
        /// Parses the submitted text and returns a foramtted response.
        /// </summary>
        /// <param name="submission">The text submission to be parsed.</param>
        /// <returns>A parsed response containing formatted text and analysis.</returns>
        /// <response code="200">Returns the parsed 837 document</response>
        /// <response code="400">If the submission is null or invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(EDI837Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EDI837Result> ParseText([FromBody] TextSubmission submission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parser = new EDI837Parser();
            var result =  parser.ParseEDI837(submission.Text);

            return Ok(result);
        }
    }
}