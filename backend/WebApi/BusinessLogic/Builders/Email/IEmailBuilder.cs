namespace WebApi.BusinessLogic.Builders.Email
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
