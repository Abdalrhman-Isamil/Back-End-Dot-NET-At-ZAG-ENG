// What is Dependency?
// Dependency: أي كلاس بيعتمد على كلاس تاني عشان يشتغل.

// OrderService هنا معتمد على
// - SqlConnection (للداتابيز)
// - EmailSender (للإيميل)
// - FileLogger (لوج)

// المشكلة
// الكلاس مربوط مباشرة بالكلاسات دي (Tight Coupling)
// يعني صعب تغيرهم أو تعمل Testing بسهولة

// احسن حل
// نستخدم Dependency Injection عشان نفصل بينهم ونخلي الكود مرن أكتر