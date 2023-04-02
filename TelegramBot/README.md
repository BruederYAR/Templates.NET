# Telegram Bot Template

This extension allows you to quickly create telegram bots.

For the application to work, you need to add the Telegram token to the telegramsetting.json:

```json
{
  "Token": "your_token"
}
```

## Definitions

Allows you to conveniently add services. To create your own definition:

1. Create a folder in "Definitions" folder
2. Create a .cs file in a new folder
3. Inherit the "Definition" class and override the "ConfigureService" method
4. Inside the method, add the necessary services

It should look something like this:

```C#
public class MyDefinition : Definition
{
    public override void ConfigureService(IServiceCollection services, IConfiguration configuration)
    {
        Object myService = new();
        services.AddSingleton(myService);
    }
}
```

## Handlers

Allows processing messages by "UpdateType"

To create your own Handler:

1. Create .cs file in "Handlers" folder
2. Add the "UpdateHandler" attribute and specify the type of update
3. Implement the "IApplicationUpdateHandler" interface

It should look something like this:

```C#
[UpdateHandler(UpdateType.Message)]
public class MessageHandler : IApplicationUpdateHandler
{
    public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        if(message is not null)
        {
            await botClient.SendTextMessageAsync(update.Message.Chat.Id, message);
        }
    }
}
``` 

## Commands

Allows you to create your own commands executed by the user

To create your own Handler:
1. Create a folder in "Commands" folder
2. Create a .cs file in a new folder
3. Add the "Command" attribute and specify the command name
4. Implement the "ICommandHandler" interface

It should look something like this:

```C#
[Command("/mycommand")]
public class MyCommand : ICommandHandler
{
    public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, UserCommand command)
    {
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "my command executed!");
    }
}
```