using Microsoft.EntityFrameworkCore;
using Tools.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToolsDataContext>();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(option =>
{
    option.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

app.MapControllers();

app.Run();
