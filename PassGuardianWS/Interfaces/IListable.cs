using System.Collections.Generic;

namespace PassGuardianWS.Interfaces
{
    public interface IListable<T>
    {
        List<T> ListById(int id);
        List<T> List();
    }
}
