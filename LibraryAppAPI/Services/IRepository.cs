using Library.App.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.App.API.Services
{
    public interface IRepository<T> where T : class, IEntity
    {
        List<T> GetAll();
        Task<T> Get(int _id);
        Task<T> Update(T _entity);
        Task<T> Delete(int _id);
    }
}
