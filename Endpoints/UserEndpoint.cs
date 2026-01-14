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

    static async Task<IResult> CreateUser([FromBody] CreateUserRequest request, UserService userService)
    {
        Console.WriteLine("Creating user...");
       
        var user = await userService.AddUser(request);
        return Results.Created($"/create/{user.Id}", user);
    }

    static async Task<IResult> GetUserById(int id, UserService userService)
    {
        var user = await userService.GetUserById(id);
        // Console.WriteLine($"Fetching user with ID: {user}");
        if (user == null)
        {
            return Results.NotFound("Data not found");
        }
        return Results.Ok(user);
    }

    static async Task<IResult> GetAllUsers(UserService userService)
    {
        var users = userService.GetAllUsers();
        return Results.Ok(users);
    }

    static async Task<IResult> UpdateUser(int id, [FromBody]CreateUserRequest request, UserService userService)
    {
        var user = await userService.GetUserById(id);
        if (user == null)
        {
            return Results.NotFound("Data not found");
        }

        user.Username = request.Username;
        user.Email = request.Email;

        await userService.UpdateUser(user);

        return Results.Ok(new { before = user, after = request });
    }
    static async Task<IResult> DeleteUser(int id, UserService userService)
    {
        var user = await userService.GetUserById(id);
        if (user == null)
        {
            return Results.NotFound("User not found");
        }

        await userService.DeleteUser(id);

        return Results.Ok("User deleted successfully");
    }
}