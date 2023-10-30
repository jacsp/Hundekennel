using ModernDesign.MVVM.Model;
using ModernDesign.MVVM.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModernDesign.MVVM.ViewModel.Repositories
{
    public class DogsRepository : IDogsRepository
    {
        public void Add(Dog entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Dog> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> Find(Expression<Func<Dog, bool>> prediction)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> GetAll()
        {
            throw new NotImplementedException();
        }

        public Dog GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Dog entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Dog entity)
        {
            throw new NotImplementedException();
        }
    }
}
