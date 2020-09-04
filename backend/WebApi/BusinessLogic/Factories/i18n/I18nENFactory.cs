using WebApi.Models.Internal.i18n;

namespace WebApi.BusinessLogic.Factories.i18n
{
    public class I18nENFactory : I18nFactory
    {
        public override Language Language => Language.EN;
        protected override string GetRelativeJsonContentPath() => "Resources/i18n/en_US.json";
    }
}
