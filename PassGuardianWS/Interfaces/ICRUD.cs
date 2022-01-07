using System.Collections.Generic;

namespace PassGuardianWS.Interfaces
{
    public interface ICRUD<T>
    {
        T GetById(int id);
        bool Update(T entity);
        bool Delete(T entity);
        bool Insert(T entity);
        bool Save(T entity);
    }
}
