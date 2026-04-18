using Microsoft.Extensions.AI;
using OllamaSharp;
using System.Text;

List<ChatMessage> chatHistory =
[
    new(ChatRole.System, "Answer all my questions remembering my conversation history"),
];

IChatClient chatClient = new OllamaApiClient("http://localhost:11434", "smollm2:135M");

string userInput;

while (true)
{
    Console.Write("You: \n\n");
    userInput = Console.ReadLine() ?? string.Empty;
    chatHistory.Add(new ChatMessage(ChatRole.User, userInput));

    Console.Write("\nChatbot: \n\n");

    StringBuilder llmResponseBuffer = new StringBuilder();
    IAsyncEnumerable<ChatResponseUpdate> updates = chatClient.GetStreamingResponseAsync(chatHistory);

    await foreach (var update in updates)
    {
        Console.Write(update.Text);

        llmResponseBuffer.Append(update.Text);
        llmResponseBuffer.Append(' ');
    }

    chatHistory.Add(new ChatMessage(ChatRole.Assistant, llmResponseBuffer.ToString()));

    Console.Write("\n\n");
}
