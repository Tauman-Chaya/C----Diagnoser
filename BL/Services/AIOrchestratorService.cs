using BL.Model;

namespace BL.Services
{
    public class AIOrchestratorService
    {
        private readonly BLWorkshopAIService _workshopAI;
        private readonly BLDiagnoserAIService _diagnoserAI;

        public AIOrchestratorService(
            BLWorkshopAIService workshopAI,
            BLDiagnoserAIService diagnoserAI)
        {
            _workshopAI = workshopAI;
            _diagnoserAI = diagnoserAI;
        }

        // =========================
        // שאלות גנריות
        // =========================
        public async Task<string> GenerateQuestions(string type)
        {
            return type switch
            {
                "workshop" => await _workshopAI.GenerateQuestions(),
                "diagnoser" => await _diagnoserAI.GenerateQuestions(),
                _ => throw new Exception("Unknown type")
            };
        }

        // =========================
        // המלצה גנרית
        // =========================
        public async Task<string> Recommend(string type, UserAnswersDto answers)
        {
            return type switch
            {
                "workshop" => await _workshopAI.RecommendWorkshop(answers),

                "diagnoser" => await _diagnoserAI.RecommendDiagnoser(
                    answers.Answer1, answers.Answer2, answers.Answer3, answers.Answer4, answers.Answer5),

                _ => throw new Exception("Unknown type")
            };
        }
    }
}