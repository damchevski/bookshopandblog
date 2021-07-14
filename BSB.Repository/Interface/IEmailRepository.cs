using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Repository.Interface
{
    public interface IEmailRepository
    {
        Task<ICollection<EmailMessage>> GetAll();
        Task<EmailMessage> Add(EmailMessage mes);
    }
}
