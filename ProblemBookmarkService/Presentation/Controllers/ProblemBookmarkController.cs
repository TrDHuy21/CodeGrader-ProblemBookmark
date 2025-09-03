using Application.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProblemBookmarkController : ControllerBase
    {
        private IProblemBookmarkService _problemBookmarkService;

        public ProblemBookmarkController(IProblemBookmarkService problemBookmarkService)
        {
            _problemBookmarkService = problemBookmarkService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBookMakr([FromQuery]string problemId)
        {
            var result = await _problemBookmarkService.Add(problemId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProblems()
        {
            var result = await _problemBookmarkService.GetProblemsByUser();
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> UnBookmark([FromQuery] string problemId)
        {
            var result = await _problemBookmarkService.Remove(problemId);
            return Ok(result);
        }
    }
}
