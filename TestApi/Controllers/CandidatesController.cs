using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Models;
using TestApi.Services;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _service;
        private readonly ILogger<CandidatesController> _logger;

        public CandidatesController(ICandidateService service, ILogger<CandidatesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] CandidateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid request", errors = ModelState });
            }

            if (request.Resume == null || request.Resume.Length == 0)
            {
                return BadRequest(new { success = false, message = "Resume file is required" });
            }

            try
            {
                await _service.SaveCandidateAsync(request);
                return Ok(new { success = true, message = "Application submitted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving candidate");
                return StatusCode(500, new { success = false, message = "An error occurred while processing the request" });
            }
        }
    }
}