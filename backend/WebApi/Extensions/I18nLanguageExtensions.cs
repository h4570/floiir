using System;
using WebApi.Factories.i18n;
using WebApi.Factories.i18n.Languages.en_US;
using WebApi.Factories.i18n.Languages.pl_PL;
using WebApi.Misc.i18n;

namespace WebApi.Extensions
{
    public static class I18nLanguageExtensions
    {

        /// <summary>
        /// Returns new i18n factory of given language
        /// </summary>
        /// <exception cref="NoFactoryForLanguageException">When there is no i18n factory for given language</exception>
        public static I18nFactory CreateFactory(this Language lang)
        {
            return lang switch
            {
                Language.PL => new I18nPLFactory(),
                Language.EN => new I18nENFactory(),
                _ => throw new NoFactoryForLanguageException("Factory for this language was not found")
            };
        }

        public class NoFactoryForLanguageException : Exception { public NoFactoryForLanguageException(string text) : base(text) { } }

    }
}
