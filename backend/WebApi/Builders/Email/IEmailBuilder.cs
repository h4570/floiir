using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Builders
{
    public interface IEmailBuilder
    {
        /// <summary>
        /// Clean builder data, and prepare for new email
        /// </summary>
        public void Reinitialize();
        public void AddHeader(string text);
        public void AddText(string text);
        public void AddButton(string text, string url);
        public void AddCenteredImage(string url);
        public void AddSpacer();
        public string GetHTML();
    }
}
