using System;
using WebApi.BusinessLogic.Builders.Email;
using WebApi.BusinessLogic.Factories.i18n;
using WebApi.Models.Internal;

namespace WebApi.BusinessLogic.Services.Internal
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

        public void SendConfirmEmailEmail(IUser user, I18nFactory i18nFactory)
        {
            var director = new EmailDirector(_emailBuilder, i18nFactory);
            var emailHtml = director.GetConfirmEmailEmailHtml(user);
            // TODO, send email through Azure SendGrid
        }

    }

    public class EmailTypeNotSupportedException : Exception { public EmailTypeNotSupportedException(string text) : base(text) { } }

}
