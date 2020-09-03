using WebApi.Misc.i18n;

namespace WebApi.Factories.i18n.Languages.en_US
{
    public class I18nENFactory : I18nFactory
    {
        public override Language Language => Language.EN;
        protected override string GetRelativeJsonContentPath() => "Factories/i18n/Languages/en_US/content.json";
    }
}
