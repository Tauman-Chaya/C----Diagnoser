using BL.API;
using BL.Model;
using DAL.Api;
using System.Text;
using System.Text.Json;

namespace BL.Services
{
    public class BLDiagnoserAIService
    {
        private readonly IBLDiagnoser _diagnoserService;
        private readonly IAIService _aiService;

        public BLDiagnoserAIService(BLManager blManager, IAIService aiService)
        {
            _diagnoserService = blManager.diagnoser;
            _aiService = aiService;
        }

        // =========================
        // יצירת 5 שאלות למאבחנות
        // =========================
        public async Task<string> GenerateQuestions()
        {
            var diagnosers = await _diagnoserService.Read();
            var str = JsonSerializer.Serialize(diagnosers);

            var prompt = $"""
תיצור בדיוק 5 שאלות קצרות שיעזרו להבין איזו מאבחנת הכי מתאימה למשתמש.

מטרת השאלות היא לאפיין התאמה למאבחנת לפי הצרכים של המשתמש ולפי תחומי ההתמחות שלה.

תחומי כיוון אפשריים לשאלות (לא חובה להשתמש בכולם):
- סוג האבחון הנדרש
- העדפה לליווי אישי או קבוצתי
- זמינות וגמישות
- ניסיון או רמת עומק מקצועית רצויה
- תחום התמחות רלוונטי

רשימת המאבחנות האפשריות:
{str}

כל מאבחנת מוגדרת כך:
"code": 0,
  "name": "string",
  "mail": "string",
  "password": 0,
  "phone": "string",
  "morphology": true,
  "graphology": true,
  "chirology": true,
  "available": true,
  "precentagePayment": 0


כל שדה רלוונטי לבחירה:
- morphology = מאבחנת בתחום מורפולוגיה
- graphology = מאבחנת בתחום גרפולוגיה
- chirology = מאבחנת בתחום כירולוגיה
- available = זמינות בפועל

הנחיות:
- בכל פעם תבחר בדיוק 5 שאלות שהכי יעזרו להתאים מאבחנת מתאימה
- אין להוסיף הסברים
- אין להחזיר טקסט נוסף מעבר לרשימת השאלות
- תענה בעברית בלבד
"""; 

            return await _aiService.SendAsync(prompt);
        }

        // =========================
        // המלצת מאבחנת
        // =========================
        public async Task<string> RecommendDiagnoser(
            string answer1,
            string answer2,
            string answer3,
            string answer4,
            string answer5)
        {
            var diagnosers = await _diagnoserService.Read();
            var str = JsonSerializer.Serialize(diagnosers);

            var prompt = $"""
            יש רשימת מאבחנות:

            {str}

            תשובות המשתמש:
            1. {answer1}
            2. {answer2}
            3. {answer3}
            4. {answer4}
            5. {answer5}

            תחזיר
            - קוד מאבחנת אחת מתאימה
            - הסיבה לבחירה.

            אל תשים בסיבות את הנתונים האלו
            - code
            - password
            - available
            - precentagePayment
            """;

            return await _aiService.SendAsync(prompt);
        }

        // =========================
        // בניית טקסט למאבחנות
        // =========================
        //private string BuildDiagnosersString(List<BLDiagnoser> list)
        //{
        //    StringBuilder sb = new();

        //    foreach (var d in list)
        //    {
        //        sb.AppendLine($"""
        //        Code: {d.Code}
        //        Name: {d.Name}
        //        Morphology: {d.Morphology}
        //        Graphology: {d.Graphology}
        //        Chirology: {d.Chirology}
        //        Available: {d.Available}
        //        """);
        //    }

        //    return sb.ToString();
        //}
    }
}