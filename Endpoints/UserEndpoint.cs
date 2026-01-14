public static class UserEndpoint
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/create", CreateUser);
        app.MapGet("/get-user/{id}", GetUserById);
        app.MapGet("/get-users", GetAllUsers);
        app.MapPut("/update/{id}", UpdateUser);
        app.MapDelete("/delete/{id}", DeleteUser);
    }

    static async Task<IResult> CreateUser(HttpRequest request, UserService userService)
    {
        Console.WriteLine("Creating user...");
        Console.WriteLine($"Username: '{request.ContentType}'");
        // Console.WriteLine($"Email: '{request.Email}'");
        // var user = await userService.AddUser(request);
        // return Results.Created($"/create/{user.Id}", user);
        return Results.Ok("Create User Endpoint");
    }

    static async Task<IResult> GetUserById(int id, UserService userService)
    {
        var user = await userService.GetUserById(id);
        if (user == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(user);
    }

    static async Task<IResult> GetAllUsers(UserService userService)
    {
        var users = userService.GetAllUsers();
        return Results.Ok(users);
    }

    static async Task<IResult> UpdateUser(int id, CreateUserRequest request, UserService userService)
    {
        var user = await userService.GetUserById(id);
        if (user == null)
        {
            return Results.NotFound();
        }

        user.Username = request.Username;
        user.Email = request.Email;

        await userService.UpdateUser(user);

        return Results.Ok(user);
    }
    static async Task<IResult> DeleteUser(int id, UserService userService)
    {
        var user = await userService.GetUserById(id);
        if (user == null)
        {
            return Results.NotFound();
        }

        await userService.DeleteUser(id);

        return Results.NoContent();
    }
}