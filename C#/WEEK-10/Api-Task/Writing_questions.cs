//! 1. What are the fundamental differences between the original ASP.NET and ASP.NET Core?
//? ASP.NET (old): Windows only + IIS
//? ASP.NET Core: Cross-platform + faster + lighter (بيشتغل على Linux وMac كمان)

//! 2. What does it mean for an API to be "Stateless"?
//? Stateless: كل Request لوحده (السيرفر مش بيخزن أي بيانات عن الريكوستات اللي فاتت)

//! 3. Break down the anatomy of an HTTP Request URL?
//? URL = Protocol (http/https) + Domain + Path + Query String (parameters)

//! 4. What are the primary HTTP Methods (Verbs) and their standard uses?
//? GET: Retrieve data (يجيب داتا)
//? POST: Create data (يضيف داتا)
//? PUT: Update full data (يعدل داتا كاملة)
//? PATCH: Update partial data (يعدل جزء)
//? DELETE: Remove data (يمسح داتا)

//! 5. What is the role of Program.cs in an ASP.NET Core application?
//? Program.cs: App startup & configuration (تشغيل الأبلكيشن وإعداد الخدمات)

//! 6. Why is Swagger/OpenAPI typically enabled only in the "Development" environment?
//? Development only: for security (عشان بيعرض تفاصيل ممكن تتستغل في الإنتاج)

//! 7. What is the core concept of "Dependency Injection" (DI)?
//? DI: Inject dependencies بدل ما تنشئها (فصل بين الكلاسات)

//! 8. Explain the three Service Lifetimes in ASP.NET Core DI?
//? Transient: new every time (كل مرة)
//? Scoped: per request (مرة لكل Request)
//? Singleton: once per app (مرة واحدة طول عمر الأبلكيشن)

//! 9. Why is it a "Best Practice" to register services against an Interface rather than a concrete Class?
//? Interface: flexibility + easy testing (مرونة وسهولة تغيير)

//! 10. What are the "Launch Profiles" found in launchSettings.json ?
//? Launch Profiles: different run configs (إعدادات تشغيل مختلفة زي البورت والـ Environment)

//! 11. How does JSON facilitate data exchange in APIs?
//? JSON: lightweight + readable (خفيف وسهل الفهم بين الأنظمة)