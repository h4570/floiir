using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Builders;
using WebApi.Factories.i18n;
using WebApi.Models.Internal;

namespace WebApi.Services.External
{
    public enum EmailType
    {
        BlueGray
    }

    public class EmailService
    {

        private readonly IEmailBuilder _emailBuilder;

        /// <exception cref="EmailTypeNotSupportedException">When there is no builder configured for given email type</exception>
        public EmailService(EmailType type)
        {
            _emailBuilder = type switch
            {
                EmailType.BlueGray => new BlueGrayEmailBuilder(),
                _ => throw new EmailTypeNotSupportedException("No builder found for given email type"),
            };
        }

        public void SendConfirmEmailEmail(User user, I18nFactory i18nFactory)
        {
            var director = new EmailDirector(_emailBuilder, i18nFactory);
            var emailHtml = director.GetConfirmEmailEmailHtml(user);
        }

    }

    public class EmailTypeNotSupportedException : Exception { public EmailTypeNotSupportedException(string text) : base(text) { } }

}
