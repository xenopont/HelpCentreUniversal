using System;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace HelpCentreUwp.Routine
{
    public class Ip
    {
        public static string ExternalIp => DetectedIp;
        public static bool UpdatesActive => PeriodicalUpdatesToken != NoUpdatesToken;

        public static void AddListener(TextBlock listener)
        {
            Listener = listener;
        }

        public static void StartUpdates()
        {
            if (UpdatesActive)
            {
                return;
            }

            PeriodicalUpdatesToken = DateTime.UtcNow.Ticks;
            _ = UpdatePeriodically(PeriodicalUpdatesToken);
        }

        public static void StopUpdates()
        {
            PeriodicalUpdatesToken = NoUpdatesToken;
        }

        public static async Task UpdateNow()
        {
            string ip = await client.GetStringAsync("https://api.ipify.org");
            if (ip != DetectedIp)
            {
                DetectedIp = ip;
                InformListeners();
            }
        }

        private static string DetectedIp = "";
        private static readonly long NoUpdatesToken = -1;
        private static long PeriodicalUpdatesToken = NoUpdatesToken;
        private static TextBlock Listener = null;
        private static readonly HttpClient client = new HttpClient();

        private static readonly Random rand = new Random(DateTime.Now.Millisecond);

        private static void InformListeners()
        {
            if (Listener != null)
            {
                Listener.Text = ExternalIp;
            }
        }

        private static async Task UpdatePeriodically(long updateToken)
        {
            if (updateToken != PeriodicalUpdatesToken)
            {
                return;
            }
            await UpdateNow();
            await Task.Delay(TimeSpan.FromSeconds(60 * 10));
            _ = UpdatePeriodically(updateToken);
        }
    }
}
