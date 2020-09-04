using WebApi.Misc.i18n;

namespace WebApi.Factories.i18n
{
    public class I18nPLFactory : I18nFactory
    {
        public override Language Language => Language.PL;
        protected override string GetRelativeJsonContentPath() => "Resources/i18n/pl_PL.json";
    }
}
