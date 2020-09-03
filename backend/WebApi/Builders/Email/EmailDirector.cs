using WebApi.Factories.i18n;
using WebApi.Factories.i18n.Models;
using WebApi.Models.Internal;

namespace WebApi.Builders
{

    public class EmailDirector
    {

        private IEmailBuilder Builder { get; set; }
        private I18nFactory I18nFactory { get; set; }
        private I18nEmailModel I18n { get => I18nFactory.Content.Email; }

        public EmailDirector(IEmailBuilder builder, I18nFactory i18nFactory)
        {
            Builder = builder;
            I18nFactory = i18nFactory;
        }

        public string GetConfirmEmailEmailHtml(User user)
        {
            Builder.Reinitialize();
            Builder.AddHeader(I18n.Welcome);
            Builder.AddCenteredImage("https://img.icons8.com/clouds/100/000000/handshake.png");
            Builder.AddText($"Cześć {user.FirstName} przykładowy tekst...");
            Builder.AddSpacer();
            Builder.AddButton("Potwierdź email", "https://www.google.com/");
            Builder.AddSpacer();
            return Builder.GetHTML();
        }

    }
}
