using System.Text;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AIChatbot;

public sealed class Chatbot(IChatClient chatClient, ILogger<Chatbot> logger)
{
    private readonly ILogger _logger = logger;
    private readonly AIAgent _chatAgent = chatClient.AsAIAgent(intialSystemPrompt, "chatbot-agent", "A friendly and helpful assistant."
        //, tools: [
        //    AIFunctionFactory.Create(ChatbotTools.TellJoke),
        //    AIFunctionFactory.Create(ChatbotTools.GetWeather)
        //]
        );

    private const string intialSystemPrompt = "You are a helpful assistant that answers questions.";

    public async Task StartChatSession()
    {
        _logger.LogInformation("Starting chat session with the AI model");

        AgentSession session = await _chatAgent.CreateSessionAsync();

        string userInput;
        while (true)
        {
            Console.Write("You: \n\n");
            userInput = Console.ReadLine() ?? string.Empty;

            Console.Write("\nChatbot: \n\n");

            IAsyncEnumerable<AgentResponseUpdate> updates = _chatAgent.RunStreamingAsync(userInput, session);

            await foreach (var update in updates)
            {
                Console.Write(update.Text);
            }

            Console.Write("\n\n");
        }
    }
}