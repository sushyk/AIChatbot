using System.ComponentModel;

namespace AIChatbot
{
    public class ChatbotTools
    {
        [Description("Tells a random joke.")]
        public static void TellJoke()
        {
            List<string> jokes =
            [
                "Why don't scientists trust atoms? Because they make up everything!",
                "Why did the scarecrow win an award? Because he was outstanding in his field!",
                "Why did the bicycle fall over? Because it was two-tired!",
                "Why don't skeletons fight each other? They don't have the guts!",
                "Why did the math book look sad? Because it had too many problems!",
                "Why did the tomato turn red? Because it saw the salad dressing!",
                "Why did the coffee file a police report? It got mugged!",
                "Why does the Java developer wear glasses? Because they don't C#!",
            ];
            Random random = new();
            int index = random.Next(jokes.Count);
            Console.WriteLine(jokes[index]);
        }

        [Description("Get the weather for a given location.")]
        public static string GetWeather([Description("The location to get the weather for.")] string location)
            => $"The weather in {location} is cloudy with a high of 15°C.";
    }
}
