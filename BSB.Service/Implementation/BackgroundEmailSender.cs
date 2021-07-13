using BSB.Data.Entity;
using BSB.Repository.Interface;
using BSB.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Service.Implementation
{
    public class BackgroundEmailSender : IBackgroundEmailSender

    {
        private readonly IEmailService _emailService;
        private readonly IProductRepository _mailRepository;

        public BackgroundEmailSender(IEmailService emailService, IProductRepository mailRepository)
        {
            _emailService = emailService;
            _mailRepository = mailRepository;
        }

        /*public async Task DoWork()
        {
            *//*await _emailService.SendEmailAsync(_mailRepository.GetAll().Where(z => !z.Status).ToList()); *//*
        }*/
    }
}
