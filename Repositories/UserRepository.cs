
using Microsoft.EntityFrameworkCore;

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

    public async Task<User?> GetUserById(int id)
    {
       return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
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

    public async Task<bool> DeleteUser(int id)
    {
        var user = await GetUserById(id);
        if (user != null){
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        } else {
            return false;
        }
    }
}