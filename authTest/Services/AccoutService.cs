using authTest.Data.Entities;
using authTest.Dto;
using Microsoft.EntityFrameworkCore;

namespace authTest.Services
{
    public interface IAccoutService
    {
        Task<UserDto> CheckLogin(LoginDto dto);
    }

    public class AccoutService : IAccoutService
    {
        private readonly AppDbContext _dbContext;

        public AccoutService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDto> CheckLogin(LoginDto dto)
        {
            if (await _dbContext.TblUserMasters.AnyAsync(u =>
            u.Email == dto.Email && u.Password == dto.Password && u.IsActive.Value))
            {
                TblUserMaster? user = await _dbContext.TblUserMasters.Include(r => r.Role).FirstOrDefaultAsync(u =>
                 u.Email == dto.Email && u.Password == dto.Password && u.IsActive.Value);
                return new UserDto
                {
                    Email = user.Email,
                    Id = user.Id,
                    Name = user?.Name,
                    RoleName = user?.Role?.RoleName
                };
            }
            else
            {
                return null;
            }
        }
    }
}
