using System;
using System.Media;
using System.IO;

namespace CyberBotSA_part2
{
    public class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                if (File.Exists(audioPath))
                {
                    SoundPlayer player = new SoundPlayer(audioPath);
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Could not play audio: {ex.Message}]");
            }
        }
    }
}

