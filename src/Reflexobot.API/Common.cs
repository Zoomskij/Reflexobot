using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Reflexobot.API
{
    public class Common
    {
        private int _currentChoice = 1;
        
        public Common() { }

        public async Task ChooseTeacher(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            Dictionary<int, string> phrases = GetStartPhrases();
            if (phrases != null)
            {
                List<List<InlineKeyboardButton>> inLineList = new List<List<InlineKeyboardButton>>();
                List<InlineKeyboardButton> inLineRow = new List<InlineKeyboardButton>();

                InlineKeyboardButton inLineKeyboard = InlineKeyboardButton.WithCallbackData(text: "Выбрать", callbackData: _currentChoice.ToString());
                InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: "NextReason;2");
                inLineRow.Add(inLineKeyboard);
                inLineRow.Add(inLineKeyboardNext);

                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);

                await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: $"{phrases[1]}",
                replyMarkup: inlineKeyboardMarkup,
                cancellationToken: cancellationToken);
            }
        }

        public async Task ChooseTeacher(ITelegramBotClient botClient, CallbackQuery callbackQuery, int currentTeacher, CancellationToken cancellationToken)
        {
            Dictionary<int, string> phrases = GetStartPhrases();
            if (phrases != null)
            {
                List<List<InlineKeyboardButton>> inLineList = new List<List<InlineKeyboardButton>>();
                List<InlineKeyboardButton> inLineRow = new List<InlineKeyboardButton>();
                if (currentTeacher > 1)
                {
                    InlineKeyboardButton inLineKeyboardPrev = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: $"PrevReason;{currentTeacher-1}");
                    inLineRow.Add(inLineKeyboardPrev);
                }
                InlineKeyboardButton inLineKeyboard = InlineKeyboardButton.WithCallbackData(text: "Выбрать", callbackData: $"{currentTeacher}");
                inLineRow.Add(inLineKeyboard);
                if (currentTeacher < phrases.Count())
                {
                    InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"NextReason;{currentTeacher + 1}");
                    inLineRow.Add(inLineKeyboardNext);
                }
                
                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);
                var chatId = callbackQuery.Message.Chat.Id;
                var messageId = callbackQuery.Message.MessageId;

                await botClient.EditMessageTextAsync(chatId, messageId, $"{phrases[currentTeacher]}", replyMarkup: inlineKeyboardMarkup);
            }
        }

        //Фразы приветствия (самое начало)
        public async Task ChooseHello(ITelegramBotClient botClient, CallbackQuery callbackQuery, int currentHello, CancellationToken cancellationToken)
        {
            Dictionary<int, string> phrases = GetHelloPhrases();
            if (phrases != null)
            {
                List<List<InlineKeyboardButton>> inLineList = new List<List<InlineKeyboardButton>>();
                List<InlineKeyboardButton> inLineRow = new List<InlineKeyboardButton>();
                if (currentHello > 1)
                {
                    InlineKeyboardButton inLineKeyboardPrev = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: $"Hello;{currentHello - 1}");
                    inLineRow.Add(inLineKeyboardPrev);
                }
                if (currentHello < phrases.Count())
                {
                    InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"Hello;{currentHello + 1}");
                    inLineRow.Add(inLineKeyboardNext);
                } 
                else
                {
                    InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"Second");
                    inLineRow.Add(inLineKeyboardNext);
                }

                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);
                var chatId = callbackQuery.Message.Chat.Id;
                var messageId = callbackQuery.Message.MessageId;

                
                await botClient.EditMessageTextAsync(chatId, messageId, $"{phrases[currentHello]}", replyMarkup: inlineKeyboardMarkup, parseMode: ParseMode.Html);
            }
        }

        public async Task Training(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            Dictionary<int, string> phrases = GetTrainingPhrases();
            if (phrases != null)
            {
                List<List<InlineKeyboardButton>> inLineList = new List<List<InlineKeyboardButton>>();
                List<InlineKeyboardButton> inLineRow = new List<InlineKeyboardButton>();

                InlineKeyboardButton inLineKeyboard = InlineKeyboardButton.WithCallbackData(text: "Выбрать", callbackData: _currentChoice.ToString());
                InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: "Training;2");
                inLineRow.Add(inLineKeyboard);
                inLineRow.Add(inLineKeyboardNext);

                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);

                await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: $"{phrases[1]}",
                replyMarkup: inlineKeyboardMarkup,
                cancellationToken: cancellationToken);
            }
        }

        public async Task Training(ITelegramBotClient botClient, CallbackQuery callbackQuery, int currentTraining, CancellationToken cancellationToken)
        {
            Dictionary<int, string> phrases = GetTrainingPhrases();
            if (phrases != null)
            {
                List<List<InlineKeyboardButton>> inLineList = new List<List<InlineKeyboardButton>>();
                List<InlineKeyboardButton> inLineRow = new List<InlineKeyboardButton>();
                if (currentTraining > 1)
                {
                    InlineKeyboardButton inLineKeyboardPrev = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: $"Training;{currentTraining - 1}");
                    inLineRow.Add(inLineKeyboardPrev);
                }
                if (currentTraining < phrases.Count())
                {
                    InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"Training;{currentTraining + 1}");
                    inLineRow.Add(inLineKeyboardNext);
                }

                InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);
                var chatId = callbackQuery.Message.Chat.Id;
                var messageId = callbackQuery.Message.MessageId;


                await botClient.EditMessageTextAsync(chatId, messageId, $"{phrases[currentTraining]}", replyMarkup: inlineKeyboardMarkup, parseMode: ParseMode.Html);
            }
        }

        public Dictionary<int, string> GetTrainingPhrases()
        {
            Dictionary<int, string> phrases = new Dictionary<int, string>();
            phrases.Add(1, "Энергию на достижения мы всегда берем из нашего будущего");
            phrases.Add(2, "Мы научимся переворачивать наши проблемы в идеальные ситуации");
            phrases.Add(3, "А как бы я хотел? Это первая ступенька к формированию ВИДЕНИЯ");
            phrases.Add(4, "Опиши идеальную ситуацию в 2-3 предложениях");
            phrases.Add(5, "Например\n<b>Что меня смущает?\nЯ не могу сосредоточится на обучении, меня все отвлекает, у меня нет времени</b>");
            phrases.Add(6, "<b>Идеальная ситуация: я спокоен, у меня есть время, и все условия для успешного обучения</b>");
            phrases.Add(7, "Назовём эту идеальную ситуацию точкой Б");
            phrases.Add(8, "Каким бы простым не был навык переворачивания проблемы в идеальную ситуацию");
            phrases.Add(9, "Он позволяет концентрировать наш мозг на том в чем состоит решение вопроса");
            phrases.Add(10, "И включает программы подсознания, которые помогают найти решение УДОВЛЕТВОРЯЮЩЕЕ НАШЕЙ ЦЕЛИ");
            phrases.Add(11, "Здесь мы с Вами научимся выявлять некорректные программы которые мешают решать проблемные ситуации в Вашей жизни и заставляют не замечать возможностей. Начнём?");

            return phrases;
        }

        public Dictionary<int, string> GetHelloPhrases()
        {
            Dictionary<int, string> phrases = new Dictionary<int, string>();
            phrases.Add(1, $"<b>Я помогу тебе:</b>" +
                            "\n✅ осознавать получаемый опыт в процессе обучения" +
                            "\n✅ регулярно рефлексировать" +
                            "\n✅ фокусироваться на цели обучения" +
                            "\n✅ возвращаться в ресурсное состояние в моменты спада" +
                            "\n✅ и сокращать прокрастинацию!");

            phrases.Add(2, "<b>Как я работаю:</b> " +
                            "\n1. обращаюсь к тебе в дни, когда ты обучаешься на курсе, чтобы ты закрепил полученные знания сразу же " +
                            "\n2. появляюсь по запросу, когда необходим тебе " +
                            "\n3. иногда предлагаю тебе изучить себя поглубже - ты узнаешь много интересного о себе!");

            phrases.Add(3, $"<b>Что ты получишь в результате общения со мной: </b>" +
                            "\n💎 поймешь, как обучаешься именно ты " +
                            "\n💎осознаешь, что тебе дается легко, а что сложнее " +
                            "\n💎увеличишь эффективность своего обучения и полученных знаний " +
                            "\n💎быстрее достигнешь поставленных целей " +
                            "\n💎закончишь курс с уверенностью в своих силах!");
            return phrases;
        }

        public Dictionary<int, string> GetStartPhrases()
        {
            Dictionary<int, string> phrases = new Dictionary<int, string>();
            phrases.Add(1, "Ответ 1 из 4\n\nЯ люблю осознанно подходить к любому процессу, размышляю и во всем ищу смысл. Медитация и философия помогают мне во всем");
            phrases.Add(2, "Ответ 2 из 4\n\nМне нравится держать ритм и соревноваться, поддерживаю спортивный интерес даже в обучении.Самодисциплина - моя основа");
            phrases.Add(3, "Ответ 3 из 4\n\nЯ всегда просчитываю варианты и выгоды, видение будущего успеха помогает мне. Меня вдохновляют истории успеха");
            phrases.Add(4, "Ответ 4 из 4\n\nГлавное для меня- это побороть свою лень и прокрастинацию. Моя основа и причина успехов- это внутренняя работа с собой");
            return phrases;
        }
    }
}
