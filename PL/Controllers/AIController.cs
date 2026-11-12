using BL.Model;
using BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly AIOrchestratorService _ai;

        public AIController(AIOrchestratorService ai)
        {
            _ai = ai;
        }

        // =========================
        // יצירת שאלות (גנרי)
        // =========================
        [HttpGet("questions/{type}")]
        public async Task<IActionResult> GenerateQuestions(string type)
        {
            var result = await _ai.GenerateQuestions(type);
            return Ok(result);
        }

        // =========================
        // המלצה (גנרי)
        // =========================
        [HttpPost("recommend/{type}")]
        public async Task<IActionResult> Recommend(
            string type,
            [FromBody] UserAnswersDto answers)
        {
            if (answers == null)
                return BadRequest("Missing answers");

            var result = await _ai.Recommend(type, answers);

            return Ok(result);
        }
    }
}