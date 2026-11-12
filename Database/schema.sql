/* =====================================================================
   Diagnoser — מסד הנתונים
   ---------------------------------------------------------------------
   הסקריפט בונה את מסד הנתונים מאפס:
     1. יצירת מסד הנתונים Diagnoser
     2. מבנה הטבלאות (נוצר מתוך מודל ה-EF Core, ‏DAL/Models/DbManager.cs)
     3. טבלאות קבועות שהקוד תלוי בהן (סטטוסים וסוגי קבוצות)
     4. נתוני דמה להדגמה (שמות ומיילים בדויים)

   הרצה: SQL Server Object Explorer ב-Visual Studio, או SSMS, או:
     sqlcmd -S "(localdb)\MSSQLLocalDB" -i Database\schema.sql
   ===================================================================== */

IF DB_ID(N'Diagnoser') IS NULL
    CREATE DATABASE [Diagnoser];
GO

USE [Diagnoser];
GO

/* =====================================================================
   1. טבלאות
   ===================================================================== */

CREATE TABLE [Status] (
    [Code]        int       NOT NULL IDENTITY,
    [Description] nchar(10) NULL,
    CONSTRAINT [PK_Status] PRIMARY KEY ([Code])
);
GO

CREATE TABLE [TypeGroup] (
    [Code]        int       NOT NULL IDENTITY,
    [Description] nchar(50) NULL,
    CONSTRAINT [PK_TypeGroup] PRIMARY KEY ([Code])
);
GO

CREATE TABLE [Diagnoser] (
    [Code]              int          NOT NULL IDENTITY,
    [Name]              nchar(20)    NOT NULL,
    [Mail]              nvarchar(50) NOT NULL,
    [Password]          int          NOT NULL,
    [Phone]             nchar(10)    NULL,
    [Morphology]        bit          NOT NULL,
    [Graphology]        bit          NOT NULL,
    [Chirology]         bit          NOT NULL,
    [Available]         bit          NOT NULL,
    [precentagePayment] int          NULL DEFAULT 0,   -- יתרת העמלות שהמאבחנת חייבת למנהלת
    CONSTRAINT [PK_Diagnoser] PRIMARY KEY ([Code])
);
GO

CREATE TABLE [Customers] (
    [Code]     int          NOT NULL,
    [Name]     nchar(20)    NOT NULL,
    [Mail]     nvarchar(50) NOT NULL,
    [Password] int          NOT NULL,
    [Phone]    nchar(10)    NULL,
    [CodeType] int          NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Code]),
    CONSTRAINT [FK_Customers_TypeGroup] FOREIGN KEY ([CodeType]) REFERENCES [TypeGroup] ([Code])
);
GO

CREATE TABLE [Leads] (
    [Code]     int          NOT NULL,
    [Name]     nchar(20)    NOT NULL,
    [Mail]     nvarchar(50) NOT NULL,
    [Password] int          NOT NULL,
    [Phone]    nchar(10)    NULL,
    [CodeType] int          NOT NULL,
    CONSTRAINT [PK_Leads] PRIMARY KEY ([Code]),
    CONSTRAINT [FK_Leads_TypeGroup] FOREIGN KEY ([CodeType]) REFERENCES [TypeGroup] ([Code])
);
GO

CREATE TABLE [Workshops] (
    [Code]           int           NOT NULL IDENTITY,
    [CodeDiagnoser]  int           NOT NULL,
    [TypeGroup]      int           NOT NULL,
    [Description]    nvarchar(200) NULL,
    [Morfology]      bit           NOT NULL,
    [Grafology]      bit           NOT NULL,
    [Chirology]      bit           NOT NULL,
    [Price]          int           NOT NULL,
    [AccontOfPeople] int           NULL,
    CONSTRAINT [PK_Workshops] PRIMARY KEY ([Code]),
    CONSTRAINT [FK_Workshops_TypeGroup] FOREIGN KEY ([TypeGroup]) REFERENCES [TypeGroup] ([Code]),
    CONSTRAINT [FK_Workshops_Diagnoser] FOREIGN KEY ([CodeDiagnoser]) REFERENCES [Diagnoser] ([Code])
);
GO

CREATE TABLE [Order_WorkShops] (
    [Code]           int       NOT NULL IDENTITY,
    [Code_Diagnoser] int       NOT NULL,
    [Code_Customer]  int       NOT NULL,
    [TypeWorkShop]   int       NOT NULL,
    [Date]           date      NOT NULL,
    [Adress]         nchar(50) NULL,
    [Price]          int       NOT NULL,
    [paied]          bit       NULL,
    [parsent]        bit       NOT NULL,
    CONSTRAINT [PK_Order_WorkShops] PRIMARY KEY ([Code]),
    CONSTRAINT [FK_Order_WorkShops_Diagnoser] FOREIGN KEY ([Code_Diagnoser]) REFERENCES [Diagnoser] ([Code]),
    CONSTRAINT [FK_Order_WorkShops_Workshops] FOREIGN KEY ([TypeWorkShop]) REFERENCES [Workshops] ([Code]),
    CONSTRAINT [FK_Order_WorkShops_Customers] FOREIGN KEY ([Code_Customer]) REFERENCES [Customers] ([Code])
);
GO

CREATE TABLE [Reference] (
    [Code]          int           NOT NULL IDENTITY,
    [Code_WorkShop] int           NOT NULL,
    [Code_Customer] int           NOT NULL,
    [Date]          date          NOT NULL,
    [Time]          float         NOT NULL,
    [Adress]        nvarchar(50)  NOT NULL,
    [Comments]      nvarchar(200) NULL,
    [Status]        int           NOT NULL,
    CONSTRAINT [PK_Reference] PRIMARY KEY ([Code]),
    CONSTRAINT [FK_Reference_Workshops] FOREIGN KEY ([Code_WorkShop]) REFERENCES [Workshops] ([Code]),
    CONSTRAINT [FK_Reference_Customers] FOREIGN KEY ([Code_Customer]) REFERENCES [Customers] ([Code]),
    CONSTRAINT [FK_Reference_Status] FOREIGN KEY ([Status]) REFERENCES [Status] ([Code])
);
GO

-- טבלאות מגרסה מוקדמת של המערכת (קורסים ותלמידות), עדיין קיימות במודל
CREATE TABLE [Courses] (
    [Code]              int          NOT NULL,
    [Name]              nvarchar(1)  NOT NULL,
    [AccountOfStudent]  int          NOT NULL,
    [Desciption]        nvarchar(50) NULL,
    [AccountOfMeetings] int          NOT NULL,
    [Day]               nchar(10)    NOT NULL,
    [FirstMeeting]      date         NOT NULL,
    [Adress]            nvarchar(50) NULL,
    [Price]             int          NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Code])
);
GO

CREATE TABLE [Students] (
    [Code]        int          NOT NULL,
    [Name]        nvarchar(20) NOT NULL,
    [Mail]        nvarchar(50) NOT NULL,
    [Password]    int          NOT NULL,
    [Phone]       nchar(10)    NULL,
    [Adress]      nvarchar(50) NULL,
    [BirthDate]   date         NULL,
    [CodeCourses] int          NULL,
    CONSTRAINT [FK_Students_Courses] FOREIGN KEY ([CodeCourses]) REFERENCES [Courses] ([Code])
);
GO

/* אינדקסים על המפתחות הזרים */
CREATE INDEX [IX_Customers_CodeType]             ON [Customers]       ([CodeType]);
CREATE INDEX [IX_Leads_CodeType]                 ON [Leads]           ([CodeType]);
CREATE INDEX [IX_Workshops_CodeDiagnoser]        ON [Workshops]       ([CodeDiagnoser]);
CREATE INDEX [IX_Workshops_TypeGroup]            ON [Workshops]       ([TypeGroup]);
CREATE INDEX [IX_Order_WorkShops_Code_Customer]  ON [Order_WorkShops] ([Code_Customer]);
CREATE INDEX [IX_Order_WorkShops_Code_Diagnoser] ON [Order_WorkShops] ([Code_Diagnoser]);
CREATE INDEX [IX_Order_WorkShops_TypeWorkShop]   ON [Order_WorkShops] ([TypeWorkShop]);
CREATE INDEX [IX_Reference_Code_Customer]        ON [Reference]       ([Code_Customer]);
CREATE INDEX [IX_Reference_Code_WorkShop]        ON [Reference]       ([Code_WorkShop]);
CREATE INDEX [IX_Reference_Status]               ON [Reference]       ([Status]);
CREATE INDEX [IX_Students_CodeCourses]           ON [Students]        ([CodeCourses]);
GO

/* =====================================================================
   2. טבלאות קבועות — הקוד תלוי בערכים האלה, אין לשנות אותם
   ===================================================================== */

-- סטטוסי הזמנה.
-- 1 = הזמנה חדשה (הקליינט שולח status: 1 ביצירת הזמנה).
-- "לא שולם", "שולם" ו"בוטל" נמצאים לפי השם ב-BLReferenceServices (CloseOrder, Pay, DeleteOrder).
SET IDENTITY_INSERT [Status] ON;
INSERT INTO [Status] ([Code], [Description]) VALUES
    (1, N'הוזמן'),
    (2, N'לא שולם'),
    (3, N'שולם'),
    (4, N'בוטל');
SET IDENTITY_INSERT [Status] OFF;
GO

-- סוגי קבוצות (קהל היעד של סדנה, וסיווג לקוחות ומתעניינות)
SET IDENTITY_INSERT [TypeGroup] ON;
INSERT INTO [TypeGroup] ([Code], [Description]) VALUES
    (1, N'נשים'),
    (2, N'בנות סמינר'),
    (3, N'צוותי חינוך'),
    (4, N'מפגש אישי');
SET IDENTITY_INSERT [TypeGroup] OFF;
GO

/* =====================================================================
   3. נתוני דמה להדגמה — כל השמות, המיילים והטלפונים בדויים
   ===================================================================== */

-- מאבחנות. הראשונה היא המנהלת: לפי BLReferenceServices.CloseOrder,
-- המנהלת (המייל esty@2024) לא משלמת עמלה על סדנאות שהיא עצמה מעבירה.
SET IDENTITY_INSERT [Diagnoser] ON;
INSERT INTO [Diagnoser] ([Code], [Name], [Mail], [Password], [Phone], [Morphology], [Graphology], [Chirology], [Available], [precentagePayment]) VALUES
    (1, N'אסתר לוי',     N'esty@2024',            1234, N'0500000001', 1, 1, 1, 1, 0),
    (2, N'רבקה כהן',     N'rivka@example.com',    1234, N'0500000002', 1, 1, 0, 1, 45),
    (3, N'שרה פרידמן',   N'sara@example.com',     1234, N'0500000003', 0, 1, 0, 1, 0),
    (4, N'לאה גולדברג',  N'leah@example.com',     1234, N'0500000004', 1, 0, 1, 0, 20);
SET IDENTITY_INSERT [Diagnoser] OFF;
GO

-- סדנאות
SET IDENTITY_INSERT [Workshops] ON;
INSERT INTO [Workshops] ([Code], [CodeDiagnoser], [TypeGroup], [Description], [Morfology], [Grafology], [Chirology], [Price], [AccontOfPeople]) VALUES
    (1, 2, 2, N'סדנת היכרות עם מורפולוגיה: מה תווי הפנים מלמדים על אופי וכישרונות', 1, 0, 0, 450, 25),
    (2, 3, 3, N'גרפולוגיה לצוותי חינוך: לזהות קשיים וחוזקות דרך כתב היד',          0, 1, 0, 600, 20),
    (3, 1, 1, N'ערב העשרה לנשים: היכרות עם שלושת תחומי האבחון',                    1, 1, 1, 350, 40),
    (4, 2, 4, N'אבחון אישי מקיף: מורפולוגיה וגרפולוגיה, כולל משוב כתוב',            1, 1, 0, 300, 1),
    (5, 4, 1, N'סדנה מעשית במורפולוגיה, בקבוצה קטנה',                               1, 0, 0, 500, 12);
SET IDENTITY_INSERT [Workshops] OFF;
GO

-- לקוחות
INSERT INTO [Customers] ([Code], [Name], [Mail], [Password], [Phone], [CodeType]) VALUES
    (1001, N'מרים שטרן',    N'miriam@example.com', 1111, N'0520000001', 2),
    (1002, N'חנה וייס',     N'chana@example.com',  1111, N'0520000002', 3),
    (1003, N'יהודית רוזן',  N'yehudit@example.com', 1111, N'0520000003', 1);
GO

-- מתעניינות
INSERT INTO [Leads] ([Code], [Name], [Mail], [Password], [Phone], [CodeType]) VALUES
    (2001, N'דבורה קליין', N'dvora@example.com',  2222, N'0530000001', 1),
    (2002, N'נחמה ברגר',   N'nechama@example.com', 2222, N'0530000002', 2);
GO

-- הזמנות (הפניות) — אחת בכל סטטוס
SET IDENTITY_INSERT [Reference] ON;
INSERT INTO [Reference] ([Code], [Code_WorkShop], [Code_Customer], [Date], [Time], [Adress], [Comments], [Status]) VALUES
    (1, 1, 1001, '2026-12-01', 19.5, N'סמינר בית יעקב, ירושלים', N'כ-25 בנות, יש מקרן באולם', 1),
    (2, 2, 1002, '2026-11-20', 10.0, N'תלמוד תורה, בני ברק',     N'מפגש לצוות המורות',        2),
    (3, 3, 1003, '2026-10-15', 20.0, N'אולם קהילתי, אלעד',       NULL,                       3),
    (4, 5, 1003, '2026-11-05', 18.0, N'בית פרטי, מודיעין עילית', N'בוטל לבקשת הלקוחה',        4);
SET IDENTITY_INSERT [Reference] OFF;
GO

-- היסטוריית סדנאות שהתקיימו
SET IDENTITY_INSERT [Order_WorkShops] ON;
INSERT INTO [Order_WorkShops] ([Code], [Code_Diagnoser], [Code_Customer], [TypeWorkShop], [Date], [Adress], [Price], [paied], [parsent]) VALUES
    (1, 3, 1002, 2, '2026-11-20', N'תלמוד תורה, בני ברק', 600, 0, 1),
    (2, 1, 1003, 3, '2026-10-15', N'אולם קהילתי, אלעד',   350, 1, 1);
SET IDENTITY_INSERT [Order_WorkShops] OFF;
GO

PRINT N'Diagnoser database created successfully.';
GO
