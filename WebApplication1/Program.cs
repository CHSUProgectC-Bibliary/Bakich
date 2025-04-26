using Microsoft.OpenApi.Models;
using BookReviewAPI.Data;
using BookReviewAPI;
using Microsoft.EntityFrameworkCore;
using BookReviewAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile)); // Для IMapper

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "BooksReviewAPI",
        Version = "v2"
    }
));
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "BooksReviewAPI v2");
    
});



app.UseRouting();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();