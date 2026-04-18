using AIChatbot;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OllamaSharp;
using System.Text;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Services.AddChatClient(new OllamaApiClient("http://localhost:11434", "smollm2:135M"));

builder.Services.AddSingleton<Chatbot>();

IHost app = builder.Build();

var chatBot = app.Services.GetRequiredService<Chatbot>();

await chatBot.StartChatSession();

