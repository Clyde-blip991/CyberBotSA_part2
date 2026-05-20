namespace CyberBotSA_part2
{
    public class SentimentDetector
    {
        public static string DetectSentiment(string input)
        {
            input = input.ToLower();

            if (input.Contains("worried") || input.Contains("scared") ||
                input.Contains("afraid") || input.Contains("nervous"))
                return "worried";

            if (input.Contains("curious") || input.Contains("interested") ||
                input.Contains("want to know") || input.Contains("tell me more"))
                return "curious";

            if (input.Contains("frustrated") || input.Contains("angry") ||
                input.Contains("annoyed") || input.Contains("confused"))
                return "frustrated";

            if (input.Contains("happy") || input.Contains("great") ||
                input.Contains("thanks") || input.Contains("thank you"))
                return "positive";

            return "neutral";
        }

        public static string GetSentimentResponse(string sentiment, string userName)
        {
            switch (sentiment)
            {
                case "worried":
                    return $"I understand you're feeling worried, {userName}. That's completly normal. Cybersecurity can feel overwhelming, but I'm here to help you stay safe step by step";
                case "curious":
                    return $"I love your curiosity, {userName}! Wanting to learn more about Cybersecurity is the first step to staying safe online.";
                case "frustrated":
                    return $"I'm sorry you feeling frustrated, {userName}. Let me try to explain things more clearly. Cybersecurity can be complex but i will make it as simple as possible";
                case "positive":
                    return $"Thats great to hear, {userName}! Keep up the positive attitude towards cybersecurity awareness!";
                default:
                    return null;
            }
        }
    }
}
