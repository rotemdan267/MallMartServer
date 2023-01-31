using MallMartAPI.Repos;
using MallMartDB;
using MallMartDB.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.AllowAnyOrigin();
                              policy.AllowAnyHeader();
                              policy.AllowAnyMethod();
                          });
});


builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MallMartDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MallMartConnectionString")));
builder.Services.AddScoped<IDBManager,DBManager>();

builder.Services.AddScoped<IGenericRepository<AcquisitionOrder>, GenericRepository<AcquisitionOrder>>();
builder.Services.AddScoped<IGenericRepository<AcquisitionOrderLine>, GenericRepository<AcquisitionOrderLine>>();
builder.Services.AddScoped<IGenericRepository<Address>, GenericRepository<Address>>();
builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
builder.Services.AddScoped<IGenericRepository<Customer>, GenericRepository<Customer>>();
builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
builder.Services.AddScoped<IGenericRepository<EmployeeRegion>, GenericRepository<EmployeeRegion>>();
builder.Services.AddScoped<IGenericRepository<Order>, GenericRepository<Order>>();
builder.Services.AddScoped<IGenericRepository<OrderLine>, GenericRepository<OrderLine>>();
builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
builder.Services.AddScoped<IGenericRepository<Region>, GenericRepository<Region>>();
builder.Services.AddScoped<IGenericRepository<Supplier>, GenericRepository<Supplier>>();
builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<IGenericRepository<UserImage>, GenericRepository<UserImage>>();

builder.Services.AddSingleton<RsaSecurityKey>(provider => {
    // It's required to register the RSA key with depedency injection.
    // If you don't do this, the RSA instance will be prematurely disposed.

    RSA rsa = RSA.Create();
    rsa.ImportRSAPublicKey(
        source: Convert.FromBase64String(builder.Configuration["Jwt:Asymmetric:PublicKey"]),
        bytesRead: out int _
    );

    return new RsaSecurityKey(rsa);
});

builder.Services.AddAuthentication().AddJwtBearer("Asymmetric", options =>
{
    SecurityKey rsa = builder.Services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();

    options.IncludeErrorDetails = true; // <- great for debugging

    // Configure the actual Bearer validation
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = rsa,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        RequireSignedTokens = true,
        RequireExpirationTime = true, // <- JWTs are required to have "exp" property set
        ValidateLifetime = true, // <- the "exp" will be validated
        ValidateAudience = true,
        ValidateIssuer = true,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();




//      5.0... צריך לתרגם ל- 6.0


//public void ConfigureServices(IServiceCollection services)
//{



//// הגדרות שאומרות שהתוכנה תזהה משתמשים בעזרת ספריית Identity
//// שהתקנו
//services.AddIdentity<ApplicationUser, IdentityRole>() // אמרנו לה מי המשתמש שלנו (אפליקיישן-יוזר, שיורש בעצמו מיוזר שמגיע עם הספריה)
//                                                      // Role - מה ה"תפקיד" (מנהל/משתמש רגיל) שאנחנו דורשים, במקרה זה 
//                                                      // לא בנינו תפקיד לכן שמנו תפקיד ברירת מחדל שהספריה נתנה לנו

//    .AddEntityFrameworkStores<BooksContext>()    // קישור לדאטאבייס - אמרנו לו שצריך דרך אנטיטי פריימוורק ונתנו לו את הקונטקסט שלנו

//    .AddDefaultTokenProviders();  // מוסיף קלאס שיודע לעשות Hash
//                                  // לסיסמאות וכו', אפשר
//                                  // גם לבחור אחד אחר שיעשה הצפנות אחרות אבל זה הברירת מחדל שמגיע עם הספריה


//services.AddAuthentication(options =>   // לוקחים מספריית JWT 
//                                        // הגדרות ברירת מחדל בנוגע להזדהות (אאל"ט ההגדרות האלה ספיציפית קשורות לסיסמא)
//{   // סכמה (Scheme) - סט של אפשרוית
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  // פרטים ספיציפיים בסיסמא (חובה אות גדולה וכו')
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.SaveToken = true; // "זכור אותי"

//    options.RequireHttpsMetadata = false; // האם אני דורש שיגיעו אליי דרך https
//                                          // אמיתי, כמובן שבעולם האמיתי זה צריך להיות
//                                          // true

//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
//    {   // הפרמטרים של ה-JWT
//        // עצמו

//        // הערת אגב לגבי Configuration
//        // זה אובייקט שמשתמשים בו הרבה, מקבלים אותו "בחינם" בקונסטרקטור, ויש דרכו גישה לארגומנטים של שורת פקודה, מה-appsettings
//        // וכו'. במקרה הזה הבאנו באמת מה-appsettings
//        // אובייקט בשם JWT
//        // שהוא בעצמו מכיל פרופרטי בשם ValidIssuer,
//        // שממנו לקחנו את התוכן
//        ValidIssuer = Configuration["JWT:ValidIssuer"],          // מי המאמת
//        ValidAudience = Configuration["JWT:ValidAudience"],      // מי הקהל
//        ValidateIssuer = true,                                   // האם לאמת את המאמת
//        ValidateAudience = true,                                 // האם לאמת את הקהל
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])) // מה ה-Secret 
//                                                                                                         // שנשתמש בו כדי לחתום (המפתח)
//                                                                                                         // צריך לשים לב שיצרנו פה מפתח סימטרי, והקונספט של מפתח פרטי וציבורי עובד
//                                                                                                         // בצורה א-סימטרית. שאלו את אייל בשיעור והוא אמר שהוא לא יודע לענות, לא מכיר
//                                                                                                         // את זה מספיק טוב, לא יצא לו לממש בעצמו, כך שיכול להיות שהמימוש הזה לא מדויק
//    };
//});

//services.AddControllers().AddNewtonsoftJson();


//}