
public class UserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public List<User> GetAllUsers()
    {
        return _db.Users.ToList();
    }

    public async Task<User> GetUserById(int id)
    {
        var findUser = _db.Users.FirstOrDefault(u => u.Id == id);
        if (findUser != null) {
            return findUser;
        } else{   
            throw new Exception("User not found");
        }
        
    }

    public async Task<User> AddUser(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUser(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        var user = _db.Users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
        else
        {
            throw new Exception("User not found");
        }
    }
}