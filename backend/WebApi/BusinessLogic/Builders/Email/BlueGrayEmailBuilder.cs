using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace WebApi.BusinessLogic.Builders.Email
{

    public class BlueGrayEmailBuilder : IEmailBuilder
    {

        private List<string> HtmlParts { get; set; }

        private const string BLUE = "#4a69bd";
        private const string GRAY = "#f4f4f4";
        private const string RELATIVE_PATH = @"Resources/EmailTemplates/BlueGray";

        public BlueGrayEmailBuilder() { HtmlParts = new List<string>(); }

        public void Reinitialize()
        {
            HtmlParts = new List<string>();
            AddBeginningHtml();
        }

        private void AddBeginningHtml() { HtmlParts.Add(GetTemplateFileContent("beginning.html")); }

        public void AddHeader(string text)
        {
            var part = GetTemplateFileContent("header.html");
            part = part.Replace("{{text}}", text);
            HtmlParts.Add(part);
        }

        public void AddText(string text)
        {
            var part = GetTemplateFileContent("text.html");
            part = part.Replace("{{text}}", text);
            HtmlParts.Add(part);
        }

        public void AddButton(string text, string url)
        {
            var part = GetTemplateFileContent("button.html");
            part = part.Replace("{{text}}", text);
            part = part.Replace("{{url}}", url);
            HtmlParts.Add(part);
        }

        public void AddCenteredImage(string url)
        {
            var part = GetTemplateFileContent("centered-img.html");
            part = part.Replace("{{url}}", url);
            HtmlParts.Add(part);
        }

        public void AddSpacer() { HtmlParts.Add(GetTemplateFileContent("spacer.html")); }

        public string GetHTML()
        {
            var result = new List<string>();
            result.AddRange(HtmlParts);
            var ending = GetEndingHtml();
            ending = ending.Replace("{{bgColor}}", GRAY);
            result.Add(ending);
            SetPartsColors(result);
            return string.Join(string.Empty, result);
        }

        private string GetEndingHtml() { return GetTemplateFileContent("ending.html"); }

        private void SetPartsColors(List<string> htmlParts)
        {
            var middleOfArr = htmlParts.Count / 3;
            for (int i = 0; i < htmlParts.Count; i++)
            {
                var currentColor = i <= middleOfArr ? BLUE : GRAY;
                htmlParts[i] = htmlParts[i].Replace("{{bgColor}}", currentColor);
            }
        }

        private string GetTemplateFileContent(string filename)
        {
            string path =
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    Path.Join(RELATIVE_PATH, filename)
                );
            return File.ReadAllText(path);
        }

    }
}
