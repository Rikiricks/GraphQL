using DataAccess;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL.GraphQLSchema;
using GraphQLDemo.GraphQlClient;
using GraphQLService.GraphQLQueries;
using Microsoft.EntityFrameworkCore;
using Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IGraphQLClient>(s => new GraphQLHttpClient(builder.Configuration["GraphQLURI"], new NewtonsoftJsonSerializer()));
builder.Services.AddScoped<CustomerConsumerService>();
builder.Services.AddControllers();

builder.Services.AddDbContext<CRUDContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<AppSchema>();



builder.Services.AddGraphQL(b => b
.AddAutoSchema<AppQuery>()
.AddGraphTypes(typeof(AppSchema).Assembly) // schema
    .AddSystemTextJson());



//builder.Services.AddGraphQL().AddGraphTypes(typeof(AppSchema), ServiceLifetime.Scoped);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseGraphQL<AppSchema>();
app.UseGraphQLPlayground(options: new GraphQL.Server.Ui.Playground.PlaygroundOptions());

app.MapControllers();

app.Run();
