using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<MAUser> GetAll();
        MAUser Get(string id);

        void Insert(MAUser entity);

        void Update(MAUser entity);

        void Delete(MAUser entity);

    }
}
