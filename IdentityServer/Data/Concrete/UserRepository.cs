using IdentityServer.Data.Abstract;
using IdentityServer.Exceptions;
using IdentityServer.Models;
using IdentityServer.Protos;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data.Concrete;

public class UserRepository : IUserRepository
{
    private readonly DbSet<ApplicationUser> _table;

    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = dbContext.Set<ApplicationUser>();
    }

    public async Task<ApplicationUser> GetUserById(int id)
    {
        return await _table.FindAsync(id) 
               ?? throw new EntityNotFoundException(nameof(User), id);
    }

    public async Task<IEnumerable<ApplicationUser>> GetUsersByIds(IEnumerable<int> ids)
    {
        // var query = from id in ids
        //     join item in _table
        //         on id equals item.Id
        //     select item;
        
        // var query = from item in _table
        //     join id in ids
        //         on item.Id equals id
        //     select item;

        var users = ids.Join(_table, id => id, user => user.Id, (id, user) => user).ToList();
        return users;
    }
}