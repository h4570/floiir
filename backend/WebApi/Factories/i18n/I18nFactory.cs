using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using WebApi.Factories.i18n.Models;
using WebApi.Misc.i18n;

namespace WebApi.Factories.i18n
{

    public abstract class I18nFactory
    {

        protected I18nFactory() { LoadContent(); }

        public abstract Language Language { get; }
        public I18nModel Content { get; private set; }
        protected abstract string GetRelativeJsonContentPath();

        private void LoadContent()
        {
            string path =
              Path.Combine(
                  Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                  GetRelativeJsonContentPath()
              );
            var json = File.ReadAllText(path);
            Content = JsonConvert.DeserializeObject<I18nModel>(json);
        }

    }

}
