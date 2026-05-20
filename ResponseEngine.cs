using System;
using System.Collections.Generic;

namespace CyberBotSA_part2
{
    public class ResponseEngine
    {
        private static Random random = new Random();
        private string lastTopic = "";

        // Random responses stored in lists
        private static Dictionary<string, List<string>> topicResponses = new Dictionary<string, List<string>>
        {
            {
                "password", new List<string>
                {
                    "Strong passwords are your first line of defence!\n\n• Use at least 12 characters\n• Mix uppercase, lowercase, numbers and symbols\n• Never reuse passwords across sites\n• Consider using a password manager like Bitwarden",
                    "A strong password is like a strong lock!\n\n• Avoid using your name or birthdate\n• Use a passphrase like 'Coffee@Sunrise2024'\n• Change your passwords regularly\n• Never share your password with anyone",
                    "Password safety is critical in South Africa!\n\n• Use different passwords for every account\n• Enable two-factor authentication alongside your password\n• Avoid obvious passwords like '123456' or 'password'\n• Use a password manager to keep track"
                }
            },
            {
                "phishing", new List<string>
                {
                    "Phishing is one of the most common cyber threats in South Africa!\n\n• Be cautious of emails asking for personal info\n• Check the sender's email address carefully\n• Never click suspicious links\n• Legitimate organisations will never ask for passwords via email",
                    "Scammers are getting smarter with phishing!\n\n• Look for spelling mistakes in emails\n• Hover over links before clicking to see the real URL\n• When in doubt contact the organisation directly\n• Never download attachments from unknown senders",
                    "Phishing attacks often impersonate trusted brands!\n\n• Banks will never ask for your PIN via email\n• Check for HTTPS in the website URL\n• Report suspicious emails to your IT department\n• Trust your instincts — if it feels wrong it probably is"
                }
            },
            {
                "privacy", new List<string>
                {
                    "Protecting your privacy online is very important!\n\n• Review your social media privacy settings regularly\n• Don't share personal information publicly\n• Use a VPN when browsing on public networks\n• Be careful what you post online — it stays there forever",
                    "Your privacy is your right!\n\n• Limit the personal info you share on social media\n• Read privacy policies before signing up for services\n• Use private browsing mode when on shared computers\n• Opt out of data collection where possible",
                    "Privacy starts with awareness!\n\n• Check app permissions on your phone regularly\n• Disable location tracking for apps that don't need it\n• Use encrypted messaging apps like Signal\n• Be cautious of free apps — you may be the product"
                }
            },
            {
                "scam", new List<string>
                {
                    "Online scams are very common in South Africa!\n\n• If it sounds too good to be true it probably is\n• Never send money to someone you haven't met in person\n• Be wary of unsolicited calls claiming to be from your bank\n• Report scams to the SAPS cybercrime unit",
                    "Scammers use many tricks to steal your money!\n\n• Never share your OTP with anyone\n• Banks will never ask for your full password\n• Be cautious of lottery winnings you didn't enter\n• Verify requests for money from friends via a phone call",
                    "Protect yourself from scams!\n\n• Check URLs carefully before entering payment details\n• Use secure payment methods like PayPal\n• Never pay upfront fees for prizes or loans\n• Trust your gut — if something feels off hang up"
                }
            },
            {
                "malware", new List<string>
                {
                    "Malware can seriously damage your device!\n\n• Install reputable antivirus software\n• Never open email attachments from unknown senders\n• Avoid pirated software — it often contains malware\n• Back up your data regularly",
                    "Protect yourself from malware!\n\n• Keep your operating system updated\n• Don't click on pop-up ads\n• Scan USB drives before opening files\n• Use a firewall to block suspicious connections",
                    "Ransomware is a growing threat in South Africa!\n\n• Back up your files to the cloud regularly\n• Never pay the ransom — it doesn't guarantee recovery\n• Keep all software updated\n• Disconnect from the internet if you suspect infection"
                }
            },
            {
                "wifi", new List<string>
                {
                    "Public Wi-Fi can be very risky!\n\n• Avoid accessing banking on public Wi-Fi\n• Use a VPN to encrypt your connection\n• Turn off auto-connect to Wi-Fi networks\n• Verify the network name with staff before connecting",
                    "Stay safe on public networks!\n\n• Attackers can set up fake hotspots\n• Avoid logging into sensitive accounts on public Wi-Fi\n• Use your mobile data for important transactions\n• Always forget public networks after use",
                    "Wi-Fi safety is essential!\n\n• Use WPA3 encryption on your home router\n• Change your router's default password\n• Hide your home network SSID\n• Regularly check connected devices on your network"
                }
            }
        };

        public string GetResponse(string input, string userName, MemoryManager memory)
        {
            input = input.ToLower().Trim();

            // Check sentiment first
            string sentiment = SentimentDetector.DetectSentiment(input);
            string sentimentResponse = SentimentDetector.GetSentimentResponse(sentiment, userName);

            // Handle follow up requests
            if (input.Contains("tell me more") || input.Contains("more info") ||
                input.Contains("explain more") || input.Contains("another tip"))
            {
                if (!string.IsNullOrEmpty(lastTopic) && topicResponses.ContainsKey(lastTopic))
                {
                    string followUp = GetRandomResponse(lastTopic);
                    return (sentimentResponse != null ? sentimentResponse + "\n\n" : "") + followUp;
                }
                return "Could you let me know which topic you'd like more information on?";
            }

            // Store favourite topic in memory
            foreach (var topic in topicResponses.Keys)
            {
                if (input.Contains(topic))
                {
                    lastTopic = topic;
                    memory.Store("favouriteTopic", topic);
                    string response = GetRandomResponse(topic);
                    return (sentimentResponse != null ? sentimentResponse + "\n\n" : "") + response;
                }
            }

            // General conversation
            if (input.Contains("how are you"))
                return $"I'm running smoothly and ready to help you stay safe online, {userName}!";

            if (input.Contains("your purpose") || input.Contains("what do you do"))
                return $"My purpose is to educate South African citizens like yourself, {userName}, on cybersecurity threats and how to avoid them.";

            if (input.Contains("help") || input.Contains("what can you do"))
                return $"You can ask me about:\n\n• Password safety\n• Phishing scams\n• Privacy\n• Scams\n• Malware\n• Wi-Fi safety";

            if (input.Contains("who are you") || input.Contains("your name"))
                return "I'm CyberBot SA, your cybersecurity awareness assistant!";

            // Check if user mentions a remembered topic
            if (memory.Has("favouriteTopic"))
            {
                string favTopic = memory.Retrieve("favouriteTopic");
                if (input.Contains("remember") || input.Contains("what did i say"))
                    return $"You mentioned you're interested in {favTopic}, {userName}. Would you like more tips on that?";
            }

            // Sentiment only response
            if (sentimentResponse != null)
                return sentimentResponse + "\n\nFeel free to ask me about password safety, phishing, privacy, scams, malware, or Wi-Fi safety.";

            // Default fallback
            return $"I'm not sure I understand that, {userName}. Could you rephrase? Type 'help' to see what I can assist with.";
        }

        private string GetRandomResponse(string topic)
        {
            List<string> responses = topicResponses[topic];
            return responses[random.Next(responses.Count)];
        }
    }
}
