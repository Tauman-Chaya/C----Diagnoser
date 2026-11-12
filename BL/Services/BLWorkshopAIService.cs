using BL.API;
using BL.Model;
using DAL.Api;
using System.Text;

namespace BL.Services
{
    public class BLWorkshopAIService
    {
        private readonly IBLWorkshop _workshopService;
        private readonly IBLTypeGroup _typeGroupService;
        private readonly IAIService _aiService;

        public BLWorkshopAIService(
            BLManager blManager,
            IAIService aiService)
        {
            _workshopService = blManager.workshop;
            _typeGroupService = blManager.typeGroup;
            _aiService = aiService;
        }

        // =========================
        // יצירת 5 שאלות
        // =========================
        public async Task<string> GenerateQuestions()
        {
            var workshops = await _workshopService.Read();
            var str = await BuildWorkshopsString(workshops);

            var prompt = $"""
        תיצור בדיוק 5 שאלות קצרות שיעזרו להבין איזו סדנה הכי מתאימה למשתמש.
        
        תחומי כיוון אפשריים לשאלות (לא חובה להשתמש בכולם):
        - מטרת הסדנה
        - תחומי עניין
        - העדפת חוויה פרקטית או עיונית
        - פתיחות לתהליכים אישיים
        - עבודה בקבוצה מול פרטני
        

        הסדנאות האפשריות:
        {str}

        הסדנאות עוסקות באחד או יותר מהתחומים הבאים:
        - גרפולוגיה
        - מורפולוגיה
        - כירולוגיה
        
        אין חובה להשתמש דווקא ב-5 התחומים הראשונים.

        בכל פעם תבחר את 5 השאלות שהכי יעזרו להתאים סדנה.

        תחזיר רק רשימת שאלות.

        תענה בעברית בלבד.
        """;

            return await _aiService.SendAsync(prompt);
        }

        // =========================
        // המלצת סדנה
        // =========================
        public async Task<string> RecommendWorkshop(UserAnswersDto answers)
        {
            var workshops = await _workshopService.Read();
            var str = await BuildWorkshopsString(workshops);

            var prompt = $"""
            יש רשימת סדנאות:

            {str}

            תשובות המשתמש:
            1. {answers.Answer1}
            2. {answers.Answer2}
            3. {answers.Answer3}
            4. {answers.Answer4}
            5. {answers.Answer5}

            תחזיר:
            - קוד סדנא
            - למה היא מתאימה
            תחזיר רק את הסדנה הכי מתאימה

            בעברית בלבד.
            """;

            return await _aiService.SendAsync(prompt);
        }

        // =========================
        // בניית טקסט לסדנאות
        // =========================
        private async Task<string> BuildWorkshopsString(List<BLWorkshop> list)
        {
            StringBuilder sb = new();

            foreach (var w in list)
            {
                var typeName = await _typeGroupService.Name(w.TypeGroup);

                sb.AppendLine($"""
                Code: {w.Code}
                Type: {typeName}
                Description: {w.Description}
                Grafology: {w.Grafology}
                Morphology: {w.Morfology}
                Chirology: {w.Chirology}
                Price: {w.Price}
                People: {w.AccontOfPeople}
                """);
            }

            return sb.ToString();
        }
    }
}