using Windows.ApplicationModel.DataTransfer;

namespace HelpCentreUwp.Routine
{
    class General
    {
        public static bool CopyTextToClipboard(string text)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(text);
            return Clipboard.SetContentWithOptions(dataPackage, null);
        }
    }
}
