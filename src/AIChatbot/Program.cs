using Microsoft.Extensions.AI;
using OllamaSharp;

List<ChatMessage> chatHistory =
[
    new(ChatRole.System, "Answer all my questions remembering my conversation history"),
];

IChatClient chatClient = new OllamaApiClient("http://localhost:11434", "smollm2:135M");

string userInput = string.Empty;

while (true)
{
    Console.Write("You: \n\n");
    userInput = Console.ReadLine() ?? string.Empty;
    chatHistory.Add(new ChatMessage(ChatRole.User, userInput));

    ChatResponse response = await chatClient.GetResponseAsync(chatHistory);

    Console.Write("\nChatbot: \n\n");
    foreach (var message in response.Messages)
    {
        Console.WriteLine(message.Text);
        Console.Write("\n");
    }

    var chatResponses = response.Messages.Select(m => new ChatMessage(ChatRole.Assistant, m.Text)).ToList();
    chatHistory.AddRange(chatResponses);

    Console.Write("\n");
}
