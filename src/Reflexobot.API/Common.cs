using Reflexobot.API.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Reflexobot.API
{
    public class Common
    {
        public Common() { }


        public async Task ChooseTeacher(ITelegramBotClient botClient, CallbackQuery callbackQuery, int currentTeacher, CancellationToken cancellationToken)
        {
            var phrases = GetStarStringtPhrases();
            var chatId = callbackQuery.Message.Chat.Id;
            var messageId = callbackQuery.Message.MessageId;
            await PaginationHelper.Pagination(botClient, phrases, chatId, messageId, "NextReason", "SelectReason", string.Empty);
        }

        //Фразы приветствия (самое начало)
        public async Task ChooseHello(ITelegramBotClient botClient, CallbackQuery callbackQuery, int currentHello, CancellationToken cancellationToken)
        {
            var phrases = GetHelloPhrases();
            var chatId = callbackQuery.Message.Chat.Id;
            var messageId = callbackQuery.Message.MessageId;
            await PaginationHelper.Pagination(botClient, phrases, chatId, messageId, "Hello", string.Empty, "Second", currentHello);
        }

        public async Task Training(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var phrases = GetTrainingPhrases();
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            await PaginationHelper.Pagination(botClient, phrases, chatId, messageId, "Training", string.Empty, string.Empty, isNew: true);
        }

        public async Task Training(ITelegramBotClient botClient, CallbackQuery callbackQuery, int currentTraining, CancellationToken cancellationToken)
        {
            var phrases = GetTrainingPhrases();
            var chatId = callbackQuery.Message.Chat.Id;
            var messageId = callbackQuery.Message.MessageId;
            await PaginationHelper.Pagination(botClient, phrases, chatId, messageId, "Training", string.Empty, string.Empty, currentTraining);
        }

        public string[] GetTrainingPhrases()
        {
            string[] phrases = {
                "Энергию на достижения мы всегда берем из нашего будущего",
                "Мы научимся переворачивать наши проблемы в идеальные ситуации",
                 "А как бы я хотел? Это первая ступенька к формированию ВИДЕНИЯ",
                "Опиши идеальную ситуацию в 2-3 предложениях",
                "Например\n<b>Что меня смущает?\nЯ не могу сосредоточится на обучении, меня все отвлекает, у меня нет времени</b>",
                "<b>Идеальная ситуация: я спокоен, у меня есть время, и все условия для успешного обучения</b>",
                "Назовём эту идеальную ситуацию точкой Б",
                "Каким бы простым не был навык переворачивания проблемы в идеальную ситуацию",
                "Он позволяет концентрировать наш мозг на том в чем состоит решение вопроса",
                "И включает программы подсознания, которые помогают найти решение УДОВЛЕТВОРЯЮЩЕЕ НАШЕЙ ЦЕЛИ",
                "Здесь мы с Вами научимся выявлять некорректные программы которые мешают решать проблемные ситуации в Вашей жизни и заставляют не замечать возможностей. Начнём?"
            };
            return phrases;
        }

        public string[] GetHelloPhrases()
        {
            string[] phrases = {
                $"<b>Я помогу тебе:</b>" +
                                "\n✅ осознавать получаемый опыт в процессе обучения" +
                                "\n✅ регулярно рефлексировать" +
                                "\n✅ фокусироваться на цели обучения" +
                                "\n✅ возвращаться в ресурсное состояние в моменты спада" +
                                "\n✅ и сокращать прокрастинацию!",

                "<b>Как я работаю:</b> " +
                                "\n1. обращаюсь к тебе в дни, когда ты обучаешься на курсе, чтобы ты закрепил полученные знания сразу же " +
                                "\n2. появляюсь по запросу, когда необходим тебе " +
                                "\n3. иногда предлагаю тебе изучить себя поглубже - ты узнаешь много интересного о себе!",

                $"<b>Что ты получишь в результате общения со мной: </b>" +
                                "\n💎 поймешь, как обучаешься именно ты " +
                                "\n💎осознаешь, что тебе дается легко, а что сложнее " +
                                "\n💎увеличишь эффективность своего обучения и полученных знаний " +
                                "\n💎быстрее достигнешь поставленных целей " +
                                "\n💎закончишь курс с уверенностью в своих силах!"
            };
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

        public string[] GetStarStringtPhrases()
        {
            string[] phrases = {
                "Ответ 1 из 4\n\nЯ люблю осознанно подходить к любому процессу, размышляю и во всем ищу смысл. Медитация и философия помогают мне во всем",
                "Ответ 2 из 4\n\nМне нравится держать ритм и соревноваться, поддерживаю спортивный интерес даже в обучении.Самодисциплина - моя основа",
                "Ответ 3 из 4\n\nЯ всегда просчитываю варианты и выгоды, видение будущего успеха помогает мне. Меня вдохновляют истории успеха",
                "Ответ 4 из 4\n\nГлавное для меня- это побороть свою лень и прокрастинацию. Моя основа и причина успехов- это внутренняя работа с собой"
            };
            return phrases;

        }
    }
}
