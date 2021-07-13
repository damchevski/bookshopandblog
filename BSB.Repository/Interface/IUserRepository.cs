using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<BSBUser> GetAll();
        BSBUser Get(string id);

        void Insert(BSBUser entity);

        void Update(BSBUser entity);

        void Delete(BSBUser entity);

    }
}
