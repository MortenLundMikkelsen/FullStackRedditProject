using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;

using miniprojektsem3.Service;
using System.Linq;
using shared.Models;
using Microsoft.AspNetCore.Builder;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Data;

var builder = WebApplication.CreateBuilder(args);

// S�tter CORS s� API'en kan bruges fra andre dom�ner
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Tilf�j DbContext factory som service.
builder.Services.AddDbContext<RedditContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Tilf�j DataService s� den kan bruges i endpoints
builder.Services.AddScoped<RedditService>();

builder.Services.Configure<JsonOptions>(options =>
{
    // Her kan man fjerne fejl der opstår, når man returnerer JSON med objekter,
    // der refererer til hinanden i en cykel.
    // (altså dobbelrettede associeringer)
    options.SerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});



var app = builder.Build();

// Seed data hvis n�dvendigt.
using (var scope = app.Services.CreateScope())
{
    var redditService = scope.ServiceProvider.GetRequiredService<RedditService>();
    redditService.SeedData(); // Fylder data p�, hvis databasen er tom. Ellers ikke.
}

app.UseHttpsRedirection();
app.UseCors(AllowSomeStuff);

// Middlware der k�rer f�r hver request. S�tter ContentType for alle responses til "JSON".
app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await next(context);
});


// DataService f�s via "Dependency Injection" (DI)
app.MapGet("/", (RedditService service) =>
{
    return new { message = "Hello World!" };
});


// Get alle Post
app.MapGet("/api/posts", (RedditService service) =>
{
    return service.GetAllPosts();
});

// Get specifik post
app.MapGet("/api/posts/{id}", (RedditService service, int id) =>
{
    return service.GetPost(id);
    
});


// Create post

app.MapPost("/api/posts", (RedditService service, Post post ) =>
{

    var newPost = service.CreatePost(post.Title, post.Body, post.UserName);
    return new { message = newPost };

});


// Create Comment

app.MapPost("/api/comments/{postId}", (RedditService service, Comment comment, long postId) =>
{
    var newComment = service.CreateComment(comment.Title, comment.Body, comment.UserName, postId);
    return new { message = newComment };

});


// Get comments på specifik post
app.MapGet("/api/comments/{postId}", (RedditService service, long postId) =>
{
    return service.GetComments(postId);

});


// Upvote og downvote - Post og Comments
app.MapPut("/api/posts/upvote/{postId}", (RedditService service, long postId) =>
{
    return service.UpVotePost(postId);

});


app.MapPut("/api/posts/downvote/{postId}", (RedditService service, long postId) =>
{
    return service.DownVotePost(postId);

});

app.MapPut("/api/comments/upvote/{commentId}", (RedditService service, long commentId) =>
{
    return service.UpVoteComment(commentId);

});

app.MapPut("/api/comments/downvote/{commentId}", (RedditService service, long commentId) =>
{
    return service.DownVoteComment(commentId);

});




app.Run();

//record NewPostData(string Titel, int AuthorId);
