using PassGuardianWS.Models;

namespace PassGuardianWS.Interfaces
{
    public interface IPassword : ICRUD<Password>, IListable<Password>
    {
        string GeneratePassword(int userId);
    }
}
