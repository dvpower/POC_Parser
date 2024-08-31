using _837ParserPOC.Parsers;
using Microsoft.AspNetCore.Mvc;
using POC837Parser.DataAccess;
using POC837Parser.Models;
using System.Text.Json;
using System;
using TextParserApi.Models;
using System.Net.Http.Json;

namespace TextParserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class _837ParserController : ControllerBase
    {

        private readonly BlobStorageService _blobStorageService;

        public _837ParserController(BlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }


        [HttpGet("{submissionId}")]
        [ProducesResponseType(typeof(EDI837Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDocument(Guid submissionId)
        {
            try
            {
                EDI837Result document = await _blobStorageService.GetDocumentAsync(submissionId);

                if (document == null)
                {
                    return NotFound($"Document with submission ID {submissionId} not found.");
                }

                return Ok(document);
            }
            catch (JsonException ex)
            {
                //_logger.LogError(ex, $"Error deserializing document with submission ID {submissionId}");
                return StatusCode(500, "An error occurred while processing the document.");
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error retrieving document with submission ID {submissionId}");
                return StatusCode(500, "An error occurred while retrieving the document.");
            }
        }


        [HttpGet("submissions")]
        [ProducesResponseType(typeof(List<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListSubmissions()
        {
            try
            {
                List<Guid> submissionIds = await _blobStorageService.ListSubmissionIdsAsync();
                return Ok(submissionIds);
            }
            catch (Exception ex)
            {
              //  _logger.LogError(ex, "Error retrieving submission IDs");
                return StatusCode(500, "An error occurred while retrieving the submission IDs.");
            }
        }

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

        public async Task<IActionResult> ParseText([FromBody] TextSubmission submission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Guid submissionId = Guid.NewGuid();
            string blobName = $"{submissionId}.json";

            var parser = new EDI837Parser();
            EDI837Result result =  parser.ParseEDI837(submission.EDIRaw);
            result.SubmissionID = submissionId;

            string parsedJson = JsonSerializer.Serialize(result);
            await _blobStorageService.UploadJsonBlobAsync(parsedJson, blobName);

            return Ok(result);
        }
    }
}