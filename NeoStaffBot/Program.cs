using NeoStaffBot;

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

internal class Program
{
    static List<long> chatIds = new List<long>();

    private static void Main(string[] args)
    {
        // Получаем телеграм-бота
        var botClient = new TelegramBotClient("6583175351:AAGyn8gWgygq4dZ_4lGf20JsvOzguD6C3gg");
        
        Console.WriteLine("bot init!");


        // Начинаем получать сообщения от бота
        botClient.StartReceiving(HandleUpdateAsync, Error);
        Console.WriteLine("bot starts recieve!");


        // Запуск планировщика для отправки сообщений каждый день в 9 утра
        var scheduler = new DailyScheduler(() => SendDailyMessage(botClient, chatIds), new TimeSpan(9, 0, 0));
        scheduler.Start();

        Console.WriteLine("Press any key to stop...");
        Console.ReadLine();

        scheduler.Stop();

        async static void SendDailyMessage(ITelegramBotClient botClient, List<long> chatIds)
        {
            Console.WriteLine("Отправка предупреждений!");

            List<string> employees = ComparatorSpecifications.FindOldCertificated(Server.GetLastSpecificationAsync().Result,
                                                                                DateOnly.FromDateTime(DateTime.Today));
            string msg = "Внимание! ";

            if (employees.Count > 0)
            {
                msg += "Сегодня желательно провести аттестацию по причине её месячного отсутствия у:\n";

                foreach (var employee in employees)
                {
                    msg += employee;
                    msg += "\n";
                }
            } else
            {
                msg += "Сегодня нет сотрудников, у которых необходимо провести аттестацию:\n";
            }

            foreach (var chatId in chatIds)
            {   
                await botClient.SendTextMessageAsync(chatId, msg);
            }
        }

        async static Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            if (message.Text.Equals("/start"))
            {
                Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Привет!\nДанный бот предназначен для предупреждения HR о необходимости проведения " +
                "аттестации у сотрудников. Вам будут высылаться табельные номера и ФИО сотрудников, которые " +
                "месяц не проходили аттестацию или набрали необходимое количество баллов для повышения!" + messageText,
                null);

                chatIds.Add(message.Chat.Id);
            }
            else
            {
                Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Возможно, я Вас неправильно понял! Напишите:\n" +
                "/start - чтобы начать получать оповещения об аттестациях!" + messageText,
                null);
            }


        }

        static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}