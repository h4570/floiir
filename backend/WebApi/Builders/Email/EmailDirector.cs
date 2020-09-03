using WebApi.Misc.Language;
using WebApi.Models.Internal;

namespace WebApi.Builders
{

    public class EmailDirector
    {

        private IEmailBuilder Builder { get; set; }

        public EmailDirector(IEmailBuilder builder) { Builder = builder; }

        public string GetConfirmEmailEmailHtml(Language language, User user)
        {
            Builder.Reinitialize();
            Builder.AddHeader("Witamy!");
            Builder.AddCenteredImage("https://img.icons8.com/clouds/100/000000/handshake.png");
            Builder.AddText($"Cześć {user.FirstName} przykładowy tekst...");
            Builder.AddSpacer();
            Builder.AddButton("Potwierdź email", "https://www.google.com/");
            Builder.AddSpacer();
            return Builder.GetHTML();
        }

    }
}
