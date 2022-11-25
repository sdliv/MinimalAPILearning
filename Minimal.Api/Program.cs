using Minimal.Api;

var builder = WebApplication.CreateBuilder(args);
//Configure our service
var app = builder.Build();

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

app.Run();
