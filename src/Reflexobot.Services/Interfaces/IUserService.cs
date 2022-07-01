

using Reflexobot.Entities;
using Reflexobot.Models;

namespace Reflexobot.Services.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
