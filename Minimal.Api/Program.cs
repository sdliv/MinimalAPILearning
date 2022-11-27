using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Minimal.Api;

var builder = WebApplication.CreateBuilder(args);
// Service Registration Starts Here
builder.Services.AddSingleton<PeopleService>();
builder.Services.AddSingleton<GuidGenerator>();

// Service Registration Stops Here

// Configurating Middleware, order matters!
var app = builder.Build();
// Middleware registration starts here.

// Synchronous examples
app.MapGet("get-example", () => "Hello from GET");
app.MapPost("post-example", () => "Hello from POST");
app.MapGet("ok-object", () => Results.Ok( new
{
    Name = "Sean Livingston"
}));

// Async examples
app.MapGet("slow-request", async () =>
{
    await Task.Delay(1000);
    return Results.Ok(new
    {
        Name = "Sean Livingston"
    });
});

app.MapGet("get", () => "This is the GET");
app.MapPost("post", () => "This is a POST");
app.MapPut("put", () => "This is a PUT");
app.MapDelete("delete", () => "This is a DELETE");

// If you need other methods, such Options or Head
app.MapMethods("options-or-head", new[] { "HEAD", "OPTIONS" },
    () => "Hello from either options or head");

// Creating request using handler, both in variable in a class.
var handler = () => "This is coming from a var";
app.MapGet("handler", handler);
app.MapGet("fromclass", ExampleHandler.SomeMethod);

// Route Parameters

app.MapGet("get-params/{age:int}", (int age) =>
{
    return $"Age provided was {age}";
});

// Matches with Regex as parameters.
app.MapGet("cars/{carId:regex(^[a-z0-9]+$)}", (string carId) =>
{
    return $"Car id provied was: {carId}";
});

// Matches with required length in parameters
app.MapGet("books/{isbn:length(13)}", (string isbn) =>
{
    return $"ISBN was: {isbn}";
});

app.MapGet("people/search", (string? searchTerm, PeopleService peopleService) =>
{
    if (searchTerm is null)
    {
        return Results.NotFound();
    }

    var results = peopleService.Search(searchTerm);
    return Results.Ok(results);
});

app.MapGet("mix/{routeParam}", (
    // Implicit and Explicit approach mixed
    string routeParam,
    [FromQuery(Name = "query")] int queryParam,
    GuidGenerator guidGenerator,
    [FromHeader(Name = "Accept-Encoding")] string encoding) =>
{
    return $"{routeParam} {queryParam} {guidGenerator.NewGuild} {encoding}";
});


// Get item from the post request body.

app.MapPost("people", (Person person) =>
{
    return Results.Ok(person);
});


app.MapGet("httpcontext-1", async context =>
{
    await context.Response.WriteAsync("Hello from HttpContext 1");
});

app.MapGet("httpcontext-2", async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello from HttpContext 2");
});

app.MapGet("http", async (HttpRequest request, HttpResponse response) =>
{
    var queries = request.QueryString.Value;
    await response.WriteAsync($"Hello from httpcontext. Queries were: {queries}");
});


// Access to ClaimsPrinicpal

app.MapGet("claims", (ClaimsPrincipal user) =>
{
    
});



app.MapGet("cancel", (CancellationToken cancel) =>
{
    return Results.Ok();
});


app.MapGet("map-point", (MapPoint point) =>
{
    return Results.Ok(point);
});

app.MapPost("map-point", (MapPoint point) =>
{
    return Results.Ok(point);
});
// Registration of middleware stops here.
app.Run();
