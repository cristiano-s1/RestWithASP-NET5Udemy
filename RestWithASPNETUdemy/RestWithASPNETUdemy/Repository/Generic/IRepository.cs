using System.Collections.Generic;
using RestWithASPNETUdemy.Model.Base;

namespace RestWithASPNETUdemy.Repository
{
    public interface IRepository <T> where T : BaseEntity
    {
        T Create(T Item);
        T FindById(int id);
        List<T> FindAll();
        T Update(T Item);
        void Delete(int id);
        bool Exists(int id);
    }
}
