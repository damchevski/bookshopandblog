using BSB.Data;
using BSB.Data.Entity;
using BSB.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Repository.Implementation
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDbContext _context;

        public EmailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<EmailMessage> Add(EmailMessage mes)
        {
            this._context.Add(mes);
            this._context.SaveChanges();

            return Task.FromResult(mes);
        }

        public Task<ICollection<EmailMessage>> GetAll()
        {
            ICollection<EmailMessage> messages = new List<EmailMessage>();
            foreach (var item in _context.EmailMessages)
            {
                messages.Add(item);
            }
            return Task.FromResult(messages);
        }
    }
}
