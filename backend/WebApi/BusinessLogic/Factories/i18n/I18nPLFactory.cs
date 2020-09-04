using WebApi.Models.Internal.i18n;

namespace WebApi.BusinessLogic.Factories.i18n
{
    public class I18nPLFactory : I18nFactory
    {
        public override Language Language => Language.PL;
        protected override string GetRelativeJsonContentPath() => "Resources/i18n/pl_PL.json";
    }
}
