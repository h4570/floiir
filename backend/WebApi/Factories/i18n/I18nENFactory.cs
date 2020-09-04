using WebApi.Misc.i18n;

namespace WebApi.Factories.i18n
{
    public class I18nENFactory : I18nFactory
    {
        public override Language Language => Language.EN;
        protected override string GetRelativeJsonContentPath() => "Resources/i18n/en_US.json";
    }
}
