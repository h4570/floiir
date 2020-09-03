using WebApi.Misc.i18n;

namespace WebApi.Factories.i18n.Languages.pl_PL
{
    public class I18nPLFactory : I18nFactory
    {
        public override Language Language => Language.PL;
        protected override string GetRelativeJsonContentPath() => "Factories/i18n/Languages/pl_PL/content.json";
    }
}
