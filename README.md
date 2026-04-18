# AI Chatbot

A console-based chatbot using Ollama and Microsoft.Extensions.AI.

## Prerequisites

1. **Install Ollama**
   - Download from [ollama.com](https://ollama.com)
   - Start the Ollama service

2. **Pull the model**
   ```bash
   ollama pull smollm2:135M
   ```

## Build

```bash
dotnet build
```

## Run

```bash
dotnet run --project src/AIChatbot/AIChatbot.csproj
```

## Run Unit Tests

No unit tests are currently configured for this project.