<div align="center">

# 🧠 Diagnoser — Server

### מערכת חכמה לניהול מאבחנות, סדנאות והזמנות, עם מנוע המלצות מבוסס AI

**RESTful Web API** · **N-Tier Architecture** · **LLM Integration**

<br/>

![.NET](https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core_Web_API-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core_8-6DB33F?style=for-the-badge&logo=nuget&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![OpenAI](https://img.shields.io/badge/GPT--4o--mini-412991?style=for-the-badge&logo=openai&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

<br/>

[🎨 צד הלקוח (React)](https://github.com/Tauman-Chaya/React---Diagnoser) · [🏗️ ארכיטקטורה](#-ארכיטקטורה) · [🤖 מנוע ה-AI](#-מנוע-ההמלצות-מבוסס-ai) · [📡 API](#-api-reference) · [🚀 הרצה](#-התקנה-והרצה)

</div>

<br/>

<div dir="rtl">

## 📌 על הפרויקט

**Diagnoser** היא מערכת **Full-Stack** לניהול עסק של אבחונים וסדנאות בתחומי **מורפולוגיה**, **גרפולוגיה** ו**כירולוגיה**.
הריפו הזה הוא צד השרת: **Web API** ב-**ASP.NET Core 8**, שבנוי בארכיטקטורת שכבות נקייה ומשרת את [צד הלקוח ב-React](https://github.com/Tauman-Chaya/React---Diagnoser).

המערכת מלווה את כל התהליך: רישום מתעניינות ולקוחות, התאמת סדנה ומאבחנת בעזרת **בינה מלאכותית**, ועד ההזמנה, סגירת ההזמנה, התשלום וחישוב העמלות.

### ✨ יכולות מרכזיות

| | יכולת | תיאור |
|:---:|---|---|
| 🤖 | **המלצות AI** | שאלון חכם שה-**LLM** מייצר, והמלצה מותאמת אישית על סדנה או מאבחנת, על סמך הנתונים האמיתיים במסד |
| 👩‍⚕️ | **ניהול מאבחנות** | פרופיל, תחומי התמחות, זמינות, סינון רב-ממדי לפי תחום, ומעקב יתרות |
| 🎓 | **ניהול סדנאות** | קטלוג סדנאות לפי סוג קבוצה, תחום, מחיר וכמות משתתפות, וחיפוש מתקדם |
| 🧾 | **הזמנות והפניות** | יצירה, עדכון, סגירה וביטול, עם **ניהול סטטוסים** לאורך כל התהליך |
| 💰 | **תשלומים ועמלות** | חישוב אוטומטי של **10% עמלה** בסגירת הזמנה, ומעקב תשלומים למנהלת |
| 👥 | **לקוחות ולידים** | הרשמה עם ולידציה, סיווג לפי סוג קבוצה, ומעקב מתעניינות |
| 📧 | **שליחת מיילים** | שירות **SMTP** לשליחת הודעות HTML ללקוחות |
| 🔐 | **אבטחה** | מפתחות מחוץ לקוד (**User Secrets**), ו-**CORS Policy** שמאפשרת גישה רק לצד הלקוח |

<br/>

## 🏗️ ארכיטקטורה

המערכת בנויה ב-**N-Tier (Layered) Architecture**, עם **Separation of Concerns** מלא. כל שכבה מכירה רק את השכבה שמתחתיה, ומדברת איתה רק דרך **ממשקים (Interfaces)**.

</div>

```mermaid
flowchart TB
    Client["🎨 React Client<br/><i>Redux Toolkit · React Router</i>"]

    subgraph Server["⚙️ ASP.NET Core 8 Web API"]
        direction TB
        PL["<b>PL — Presentation Layer</b><br/>Controllers · Swagger · CORS · DI Container"]
        BL["<b>BL — Business Logic Layer</b><br/>Services · Domain Models · Validation · AI Orchestrator"]
        DAL["<b>DAL — Data Access Layer</b><br/>EF Core DbContext · Data Services · AI Gateway"]
        PL --> BL --> DAL
    end

    DB[("🗄️ SQL Server<br/>LocalDB")]
    AI["🤖 LLM API<br/>GPT-4o-mini"]
    SMTP["📧 SMTP Server"]

    Client -- "HTTPS · JSON · REST" --> PL
    DAL -- "Entity Framework Core" --> DB
    DAL -- "Typed HttpClient" --> AI
    PL -. "System.Net.Mail" .-> SMTP

    classDef client fill:#61DAFB,stroke:#20232A,color:#20232A
    classDef pl fill:#512BD4,stroke:#2E1A80,color:#fff
    classDef bl fill:#7B5CE5,stroke:#2E1A80,color:#fff
    classDef dal fill:#A28BF0,stroke:#2E1A80,color:#fff
    classDef ext fill:#F3F0FF,stroke:#512BD4,color:#2E1A80
    class Client client
    class PL pl
    class BL bl
    class DAL dal
    class DB,AI,SMTP ext
```

<div dir="rtl">

### 🧩 השכבות

| שכבה | פרויקט | אחריות | דוגמאות מהקוד |
|---|---|---|---|
| **Presentation** | `PL` | קבלת בקשות HTTP, ניתוב, החזרת תשובות JSON, תיעוד **OpenAPI** | `WorkShopsController`, `AIController`, `Program.cs` |
| **Business Logic** | `BL` | כללים עסקיים, ולידציה, **מיפוי בין Entities ל-Domain Models**, תזמור ה-AI | `BLReferenceServices.CloseOrder`, `AIOrchestratorService` |
| **Data Access** | `DAL` | גישה למסד דרך **EF Core**, ושליחת בקשות לשירות ה-LLM | `DbManager`, `WorkshopServices`, `AIService` |

### 🎯 עקרונות ותבניות עיצוב

- **Generic CRUD Interfaces:** ‏`IcrudBL<T>` ו-`Icrud<T>` מגדירים חוזה אחיד (`Create`, `Read`, `Update`, `Delete`) לכל הישויות, כולל `Read(Func<T,bool>)` לשאילתות דינמיות.
- **Dependency Injection:** כל השירותים נרשמים ב-DI Container ומוזרקים דרך ה-Constructor, וכך אין תלות ישירה במימוש.
- **Manager / Facade Pattern:** ‏`BLManager` ו-`DalManager` מרכזים את כל השירותים של השכבה ומציגים נקודת גישה אחת.
- **Orchestrator Pattern:** ‏`AIOrchestratorService` מנתב בקשות AI לשירות המתאים (סדנה או מאבחנת), בעזרת `switch` expression.
- **DTO & Domain Models:** השכבות העליונות לא נחשפות לישויות של EF. כל שירות ב-BL ממפה `Entity ↔ BLModel`.
- **Typed HttpClient** דרך `IHttpClientFactory`, לתקשורת יעילה ובטוחה מול שירות ה-AI.
- **Custom Domain Exceptions:** ‏`SignInException`, ‏`ReferenceException` ו-`SigninWorkShopExeption` מבטאות שגיאות עסקיות בצורה ברורה.
- **Asynchronous Programming:** כל הקוד, מה-Controller ועד המסד, הוא `async/await`.
- **Configuration over Code:** מחרוזת החיבור, מפתח ה-AI ופרטי המייל נקראים מ-`appsettings.json` ומ-**User Secrets**, ולא נכתבים בקוד.

<br/>

## 🤖 מנוע ההמלצות מבוסס AI

הלב החדשני של המערכת. במקום שהלקוחה תחפש בעצמה בין עשרות סדנאות ומאבחנות, המערכת מנהלת איתה **שיחה קצרה** וממליצה לה על ההתאמה הטובה ביותר.

**איך זה עובד:**
1. **Dynamic Question Generation:** ה-LLM מייצר חמש שאלות אבחון, לפי סוג ההמלצה.
2. **Context Injection:** בזמן ההמלצה, השרת שולף את **הנתונים העדכניים מהמסד** (סדנאות או מאבחנות) ומשלב אותם ב-Prompt. כך ה-AI ממליץ רק על מה שבאמת קיים במערכת.
3. **Prompt Engineering:** הנחיות מדויקות לפורמט התשובה (קוד + נימוק), לשפה (עברית), וסינון שדות רגישים מהנימוק.
4. **Structured Response:** הקליינט מקבל קוד של סדנה או מאבחנת ונימוק אישי, ומציג אותם.

</div>

```mermaid
sequenceDiagram
    autonumber
    actor U as 👩 לקוחה
    participant C as 🎨 React Client
    participant API as ⚙️ AIController
    participant O as 🧭 AIOrchestrator
    participant S as 🧠 Workshop / Diagnoser AI Service
    participant DB as 🗄️ SQL Server
    participant LLM as 🤖 GPT-4o-mini

    U->>C: פתיחת הבוט ובחירת "סדנה" או "מאבחנת"
    C->>API: GET /api/AI/questions/{type}
    API->>O: GenerateQuestions(type)
    O->>S: GenerateQuestions()
    S->>LLM: Prompt: צור 5 שאלות אבחון
    LLM-->>C: 5 שאלות מותאמות

    U->>C: עונה על השאלות
    C->>API: POST /api/AI/recommend/{type}
    API->>O: Recommend(type, answers)
    O->>S: Recommend(answers)
    S->>DB: שליפת סדנאות או מאבחנות עדכניות
    DB-->>S: נתונים
    S->>LLM: Prompt: נתונים + תשובות + הנחיות
    LLM-->>S: קוד + נימוק בעברית
    S-->>C: ההמלצה
    C-->>U: ✨ הסדנה או המאבחנת שהכי מתאימה לך
```

<div dir="rtl">

<br/>

## 🗄️ מבנה מסד הנתונים

מסד נתונים רלציוני ב-**SQL Server**, שממופה ל-C# בגישת **EF Core Database-First**, עם **Navigation Properties** בין הישויות. הסקריפט המלא, כולל נתוני דמה: [`Database/schema.sql`](Database/schema.sql).

</div>

```mermaid
erDiagram
    DIAGNOSER ||--o{ WORKSHOP : "מעבירה"
    DIAGNOSER ||--o{ ORDER_WORKSHOP : "מבצעת"
    CUSTOMER ||--o{ ORDER_WORKSHOP : "מזמינה"
    CUSTOMER ||--o{ REFERENCE : "פונה"
    WORKSHOP ||--o{ ORDER_WORKSHOP : "נכללת ב"
    WORKSHOP ||--o{ REFERENCE : "מבוקשת ב"
    STATUS ||--o{ REFERENCE : "מגדיר מצב"
    TYPE_GROUP ||--o{ WORKSHOP : "מסווג"
    TYPE_GROUP ||--o{ CUSTOMER : "מסווג"
    TYPE_GROUP ||--o{ LEAD : "מסווג"

    DIAGNOSER {
        int Code PK
        string Name
        string Mail
        string Phone
        bool Morphology
        bool Graphology
        bool Chirology
        bool Available
        int PrecentagePayment "יתרת עמלות"
    }
    WORKSHOP {
        int Code PK
        int CodeDiagnoser FK
        int TypeGroup FK
        string Description
        bool Morfology
        bool Grafology
        bool Chirology
        int Price
        int AccontOfPeople
    }
    CUSTOMER {
        int Code PK
        string Name
        string Mail
        string Phone
        int CodeType FK
    }
    REFERENCE {
        int Code PK
        int CodeWorkShop FK
        int CodeCustomer FK
        date Date
        double Time
        string Adress
        string Comments
        int Status FK
    }
    ORDER_WORKSHOP {
        int Code PK
        int CodeDiagnoser FK
        int CodeCustomer FK
        int TypeWorkShop FK
        date Date
        int Price
        bool Paied
    }
    STATUS {
        int Code PK
        string Description
    }
    TYPE_GROUP {
        int Code PK
        string Description
    }
    LEAD {
        int Code PK
        string Name
        string Mail
        string Phone
        int CodeType FK
    }
```

<div dir="rtl">

<br/>

## 🔄 מחזור החיים של הזמנה

כל הזמנה עוברת בין סטטוסים שמוגדרים בטבלת `Statuses`. כשסוגרים הזמנה, המערכת **מחשבת אוטומטית עמלה של 10%** ממחיר הסדנה ומוסיפה אותה ליתרה של המאבחנת. כשהמאבחנת משלמת, היתרה מתעדכנת (`PayToManager`).

</div>

```mermaid
stateDiagram-v2
    direction LR
    Ordered: 🧾 הוזמנה
    Unpaid: ⏳ לא שולם
    Paid: ✅ שולם
    Cancelled: ❌ בוטל

    [*] --> Ordered: Add (לקוחה מזמינה סדנה)
    Ordered --> Unpaid: CloseOrder (+10% עמלה למאבחנת)
    Ordered --> Cancelled: DeleteOrder
    Unpaid --> Paid: Pay
    Paid --> [*]
    Cancelled --> [*]
```

<div dir="rtl">

<br/>

## 📡 API Reference

תיעוד אינטראקטיבי מלא זמין ב-**Swagger UI** בכתובת `/swagger` כשהשרת רץ.

<details open>
<summary><b>🤖 AI</b> — <code>/api/AI</code></summary>

| Method | Endpoint | תיאור |
|:---:|---|---|
| `GET` | `/api/AI/questions/{type}` | יצירת שאלון חכם (`workshop` / `diagnoser`) |
| `POST` | `/api/AI/recommend/{type}` | המלצה מותאמת אישית לפי התשובות |

</details>

<details>
<summary><b>🎓 סדנאות</b> — <code>/api/WorkShops</code></summary>

| Method | Endpoint | תיאור |
|:---:|---|---|
| `GET` | `/api/WorkShops/ReadAll` | כל הסדנאות |
| `GET` | `/api/WorkShops/Read/{code}` | סדנה לפי קוד |
| `GET` | `/api/WorkShops/Get/{mail}` | הסדנאות של מאבחנת |
| `GET` | `/api/WorkShops/GetWorkShops/{m}/{g}/{c}/{typeGroup}` | חיפוש לפי תחום וסוג קבוצה |
| `GET` | `/api/WorkShops/GetLDiagnosers/{m}/{g}/{c}/{typeGroup}` | המאבחנות שמתאימות לחיפוש |
| `POST` | `/api/WorkShops/Add` | הוספת סדנה |
| `PUT` | `/api/WorkShops/Update` | עדכון סדנה |
| `DELETE` | `/api/WorkShops/Delete` | מחיקת סדנה |

</details>

<details>
<summary><b>👩‍⚕️ מאבחנות</b> — <code>/Diagnosers</code></summary>

| Method | Endpoint | תיאור |
|:---:|---|---|
| `GET` | `/Diagnosers/ReadAll` | כל המאבחנות |
| `GET` | `/Diagnosers/Read` | מאבחנת לפי קוד |
| `GET` | `/Diagnosers/GetDiagnosers/{c}/{g}/{m}` | סינון רב-ממדי לפי תחומי התמחות |
| `GET` | `/Diagnosers/AllMorfology` · `AllGrafology` · `AllChirology` | מאבחנות לפי תחום |
| `GET` | `/Diagnosers/AllAvailable` | מאבחנות זמינות |
| `GET` | `/Diagnosers/IsMorfology` · `IsGrafology` · `IsChirology` · `IsAvailable` | בדיקת התמחות וזמינות |
| `POST` | `/Diagnosers/Add` | הוספת מאבחנת |
| `POST` | `/Diagnosers/Update` | עדכון פרטים |
| `POST` | `/Diagnosers/PayToManager/{num}/{code}` | תשלום עמלה למנהלת |
| `DELETE` | `/Diagnosers/Delete` | מחיקה |

</details>

<details>
<summary><b>🧾 הזמנות והפניות</b> — <code>/References</code></summary>

| Method | Endpoint | תיאור |
|:---:|---|---|
| `GET` | `/References/ReadAll` | כל ההזמנות |
| `GET` | `/References/Read` | הזמנה לפי קוד |
| `GET` | `/References/GetWorkShop` · `GetDiagnoser` | הסדנה והמאבחנת של ההזמנה |
| `POST` | `/References/Add` | יצירת הזמנה |
| `PUT` | `/References/Update` | עדכון הזמנה |
| `PUT` | `/References/CloseOrder/{code}` | סגירת הזמנה וחישוב עמלה |
| `PUT` | `/References/Pay/{code}` | סימון כשולם |
| `PUT` | `/References/DeleteOrder` | ביטול הזמנה |
| `DELETE` | `/References/Delete` | מחיקה |

</details>

<details>
<summary><b>👥 לקוחות, לידים ונתוני מערכת</b></summary>

| Method | Endpoint | תיאור |
|:---:|---|---|
| `GET` | `/Customers/ReadAll` | כל הלקוחות |
| `POST` | `/Customers/Add` · `Update` · `Type` | הרשמה עם ולידציה, עדכון וזיהוי סוג משתמש |
| `DELETE` | `/Customers/Delete` | מחיקה |
| `GET` | `/Leads/ReadAll` | כל המתעניינות |
| `POST` | `/Leads/Add` · `Update` · `Type` | הוספה ועדכון |
| `DELETE` | `/Leads/Delete` | מחיקה |
| `GET` | `/Statuses/ReadAll` | סטטוסי הזמנה |
| `GET` | `/ReadAll` · `/GetCode/{name}` · `/GetName/{code}` | סוגי קבוצות |
| `POST` / `PUT` / `DELETE` | `/Create` · `/Update` · `/Delete` | ניהול סוגי קבוצות |
| `POST` | `/api/Email/SendEmail` | שליחת מייל HTML |

</details>

<br/>

## 🛠️ טכנולוגיות

| תחום | טכנולוגיה |
|---|---|
| **Framework** | ASP.NET Core 8 Web API, ‏C# 12 |
| **ORM** | Entity Framework Core 8 (Database-First, LINQ) |
| **Database** | Microsoft SQL Server (LocalDB) |
| **AI** | Large Language Model: ‏GPT-4o-mini דרך Chat Completions API |
| **Documentation** | Swagger / OpenAPI (Swashbuckle) |
| **Security** | .NET User Secrets, ‏CORS Policy, ‏HTTPS Redirection |
| **Communication** | REST, ‏JSON, ‏`IHttpClientFactory`, ‏SMTP |
| **Tools** | Visual Studio 2022, ‏Git, ‏GitHub |

<br/>

## 📂 מבנה הפרויקט

</div>

```text
Diagnoser-Server/
├── 📄 Morfology.sln
│
├── 🌐 PL/                         # Presentation Layer
│   ├── Controllers/               # AI, WorkShops, Diagnosers, References,
│   │                              # Customers, Leads, Statuses, TypeGroups, Email
│   ├── Program.cs                 # DI, CORS, Swagger, connection string
│   └── appsettings.json           # הגדרות (בלי סודות)
│
├── 🧠 BL/                         # Business Logic Layer
│   ├── API/                       # IcrudBL<T> + ממשקי השירותים
│   ├── Model/                     # Domain Models / DTOs
│   ├── Services/                  # לוגיקה עסקית + AI Orchestrator
│   ├── Exeptions/                 # Custom Domain Exceptions
│   └── BLManager.cs               # Facade לשכבה
│
├── 🗃️ Database/
│   └── schema.sql                 # טבלאות + נתונים קבועים + נתוני דמה
│
└── 🗄️ DAL/                        # Data Access Layer
    ├── Api/                       # Icrud<T> + IAIService
    ├── Models/                    # EF Core Entities + DbManager (DbContext)
    ├── Services/                  # גישה לנתונים + AIService (LLM Gateway)
    └── DalManager.cs              # Facade לשכבה
```

<div dir="rtl">

<br/>

## 🚀 התקנה והרצה

### דרישות מקדימות
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server LocalDB (מגיע עם Visual Studio)
- מפתח API לשירות AI תואם OpenAI

### 1. שכפול הריפו

</div>

```bash
git clone https://github.com/Tauman-Chaya/C----Diagnoser.git
cd C----Diagnoser
```

<div dir="rtl">

### 2. מסד הנתונים

הסקריפט [`Database/schema.sql`](Database/schema.sql) בונה את מסד הנתונים מאפס: את כל הטבלאות והקשרים, את הטבלאות הקבועות שהקוד תלוי בהן (סטטוסי הזמנה וסוגי קבוצות), ו**נתוני דמה** להדגמה (מאבחנות, סדנאות, לקוחות והזמנות בכל הסטטוסים).

</div>

```bash
sqlcmd -S "(localdb)\MSSQLLocalDB" -i Database\schema.sql
```

<div dir="rtl">

> 💡 אפשר גם בלי שורת פקודה: ב-Visual Studio פותחים **View ← SQL Server Object Explorer**, מתחברים ל-`(localdb)\MSSQLLocalDB`, פותחים את הקובץ ולוחצים **Execute**.

אחר כך מכוונים את השרת למסד החדש:

</div>

```bash
cd PL
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\MSSQLLocalDB;Database=Diagnoser;Trusted_Connection=True;TrustServerCertificate=True"
```

<div dir="rtl">

> 📁 יש לך כבר קובץ `MyData.mdf`? אפשר לוותר על השלבים האלה ולהניח אותו בתיקייה `Data` שליד `PL`. מחרוזת החיבור שב-`appsettings.json` משתמשת ב-`|DataDirectory|`, והשרת מוצא את התיקייה לבד.

### 3. הגדרת סודות (User Secrets)

המפתחות **לא נשמרים בקוד**. מגדירים אותם פעם אחת במחשב:

</div>

```bash
cd PL
dotnet user-secrets set "AI:ApiKey" "<your-api-key>"

# אופציונלי – לשליחת מיילים
dotnet user-secrets set "Email:UserName" "<your-email>"
dotnet user-secrets set "Email:Password" "<your-app-password>"
```

<div dir="rtl">

### 4. הרצה

</div>

```bash
dotnet run --project PL
```

<div dir="rtl">

פותחים את Swagger בכתובת **https://localhost:7082/swagger**.

כדי להפעיל את המערכת המלאה, מריצים גם את [צד הלקוח](https://github.com/Tauman-Chaya/React---Diagnoser) (`http://localhost:3000`). השרת מוגדר לקבל ממנו בקשות (CORS).

<br/>

## 🔗 פרויקטים קשורים

| | ריפו | תיאור |
|:---:|---|---|
| ⚙️ | **[C----Diagnoser](https://github.com/Tauman-Chaya/C----Diagnoser)** | צד השרת: ASP.NET Core Web API *(אתם כאן)* |
| 🎨 | **[React---Diagnoser](https://github.com/Tauman-Chaya/React---Diagnoser)** | צד הלקוח: React 18, ‏Redux Toolkit, יומן עברי, בוט AI |

<br/>

<div align="center">

---

### 👩‍💻 פותח על ידי **חיה טאומן** ו**מלי אורלינסקי**

[![Chaya Tauman](https://img.shields.io/badge/GitHub-Tauman--Chaya-181717?style=for-the-badge&logo=github)](https://github.com/Tauman-Chaya)
[![Maly Orlinsky](https://img.shields.io/badge/GitHub-Maly--Orlinsky-181717?style=for-the-badge&logo=github)](https://github.com/Maly-Orlinsky)

⭐ אם הפרויקט מצא חן בעינייך, אשמח לכוכב!

</div>

</div>
