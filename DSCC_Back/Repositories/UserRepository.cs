using DSCC_Back.Data;
using DSCC_Back.Interfaces;
using DSCC_Back.Models;
using Microsoft.EntityFrameworkCore;

namespace DSCC_Back.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Orders).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.Include(u => u.Orders)
                                       .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
