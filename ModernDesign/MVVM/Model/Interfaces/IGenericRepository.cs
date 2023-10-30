using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModernDesign.MVVM.Model.Interfaces
{
    internal interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        IEnumerable<T> Find(Expression<Func<T, bool>> prediction);
        void AddRange(IEnumerable<T> entities);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        void Save();
    }
}