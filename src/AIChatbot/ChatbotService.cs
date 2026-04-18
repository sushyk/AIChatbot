using System.Text;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AIChatbot;

public sealed class Chatbot(
    IChatClient chatClient,
    ILogger<Chatbot> logger
) 
{
    private const string intialSystemPrompt = "You are a helpful assistant that answers questions.";

    public async Task StartChatSession()
    {
        logger.LogInformation("Starting chat session with the language model {model}", "smollm2:135M");

        StringBuilder llmResponseBuffer = new();
        List<ChatMessage> chatHistory =
        [
            new(ChatRole.System, intialSystemPrompt),
        ];

        string userInput;

        while (true)
        {
            Console.Write("You: \n\n");
            userInput = Console.ReadLine() ?? string.Empty;
            chatHistory.Add(new ChatMessage(ChatRole.User, userInput));

            Console.Write("\nChatbot: \n\n");

            IAsyncEnumerable<ChatResponseUpdate> updates = chatClient.GetStreamingResponseAsync(chatHistory);

            await foreach (var update in updates)
            {
                Console.Write(update.Text);

                llmResponseBuffer.Append(update.Text);
                llmResponseBuffer.Append(' ');
            }

            chatHistory.Add(new ChatMessage(ChatRole.Assistant, llmResponseBuffer.ToString()));
            llmResponseBuffer.Clear();
            Console.Write("\n\n");
        }
    }
}