using WebApi.BusinessLogic.Factories.i18n;
using WebApi.Extensions;
using WebApi.Models.Internal;
using WebApi.Models.Internal.i18n;

namespace WebApi.BusinessLogic.Builders.Email
{

    public class EmailDirector
    {

        private IEmailBuilder Builder { get; set; }
        private I18nFactory I18nFactory { get; set; }
        private I18nEmail I18n { get => I18nFactory.Content.Email; }

        public EmailDirector(IEmailBuilder builder, I18nFactory i18nFactory)
        {
            Builder = builder;
            I18nFactory = i18nFactory;
        }

        public string GetConfirmEmailEmailHtml(IUser user)
        {
            Builder.Reinitialize();
            var headerText = I18n.HiUser.ReplaceI18nVar("user", user.FirstName);
            Builder.AddHeader(headerText);
            Builder.AddCenteredImage("https://img.icons8.com/clouds/100/000000/handshake.png");
            Builder.AddText(I18n.ConfirmEmailText);
            Builder.AddSpacer();
            Builder.AddButton(I18n.ConfirmEmailButton, "https://www.google.com/");
            Builder.AddSpacer();
            return Builder.GetHTML();
        }

    }
}
