using System;
using WebApi.BusinessLogic.Factories.i18n;
using WebApi.Models.Internal.i18n;

namespace WebApi.Extensions
{
    public static class I18nExtensions
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
                _ => throw new NoFactoryForLanguageException("Factory for given language was not found")
            };
        }

        /// <summary>
        /// Replaces i18n variable ({{varName}}) with given value
        /// </summary>
        /// <exception cref="I18nVarWasNotFoundException">When given i18n var was not found in text</exception>
        public static string ReplaceI18nVar(this string text, string varName, string varValue)
        {
            if (text.Contains($"{{{{{varName}}}}}"))
                return text.Replace($"{{{{{varName}}}}}", varValue);
            else throw new I18nVarWasNotFoundException($"Variable with name: \"{varName}\" was not found in given i18n text");
        }

        public class NoFactoryForLanguageException : Exception { public NoFactoryForLanguageException(string text) : base(text) { } }
        public class I18nVarWasNotFoundException : Exception { public I18nVarWasNotFoundException(string text) : base(text) { } }

    }
}
