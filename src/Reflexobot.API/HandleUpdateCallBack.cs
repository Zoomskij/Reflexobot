using Newtonsoft.Json;
using Reflexobot.Entities;
using Reflexobot.Services.Inerfaces;
using Reflexobot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Linq;

namespace Reflexobot.API
{
    public class HandleUpdateCallBack
    {
        private readonly ICourseService _courseService;
        private readonly IReceiverService _receiverService;
        private readonly IStudentService _userService;
        public HandleUpdateCallBack(ICourseService courseService, IReceiverService receiverService, IStudentService userService)
        {
            _courseService = courseService;
            _receiverService = receiverService;
            _userService = userService;
        }
        public async Task HandleUpdateCallBackAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            //return;
            if (callbackQuery == null)
                return;
            var chatId = callbackQuery.Message.Chat.Id;
            var messageId = callbackQuery.Message.MessageId; 
            if (!string.IsNullOrWhiteSpace(callbackQuery.Data) && callbackQuery.Data.Contains("Hello"))
            {
                var splitData = callbackQuery.Data.Split(";");
                var currentHello = Convert.ToInt32(splitData[1]);
                await new Common().ChooseHello(botClient, callbackQuery, currentHello, cancellationToken);
                return;
            }

            if (!string.IsNullOrWhiteSpace(callbackQuery.Data) & callbackQuery.Data.Contains("AnswerSelected"))
            {
                var splitData = callbackQuery.Data.Split(";");
                var selecteTeacherId = Convert.ToInt32(splitData[1]);
                var allTeachers = GetTeacherPhrases();

                var teachers = _receiverService.GetTeachers();

                if (teachers != null)
                {
                    var teacher = teachers.FirstOrDefault(x => x.Id == selecteTeacherId);
                    if (teacher != null)
                    {
                        StudentPersonIds userPersonIds = new StudentPersonIds
                        {
                            PersonId = selecteTeacherId,
                            UserId = callbackQuery.From.Id
                        };
                        await _receiverService.AddOrUpdateUserPersonId(userPersonIds);

                        await botClient.DeleteMessageAsync(chatId, callbackQuery.Message.MessageId, cancellationToken);

                        await botClient.SendStickerAsync(
                            chatId: chatId,
                            sticker: teacher.Img,
                            cancellationToken: cancellationToken);
                    }
                }

                await botClient.SendTextMessageAsync(chatId, $"{allTeachers[selecteTeacherId]}", replyMarkup: null, parseMode: ParseMode.Html);
                return;
            }

            if (!string.IsNullOrWhiteSpace(callbackQuery.Data) && (callbackQuery.Data.Contains("Delay") || callbackQuery.Data.Contains("QuestionAnwer")))
            {
                if (callbackQuery.Data.Contains("Delay"))
                {
                    var splitData = callbackQuery.Data.Split(";");
                    var notifyGuid = Guid.Parse(splitData[1]);
                    StudentNotifyIds userNotifyIds = new StudentNotifyIds
                    {
                        NotifyGuid = notifyGuid,
                        UserId = callbackQuery.From.Id
                    };

                    await _userService.AddOrUpdateUserNotifyId(userNotifyIds);

                    var questionComon = "🤔расскажи, что обычно мотивирует тебя не терять интерес и фокусироваться на своей цели? Какое из описаний ниже совпадает с тобой максимально точно? \n\n👌это поможет мне лучше подстроиться под тебя и общаться с тобой на одной волне!";

                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: questionComon,
                        cancellationToken: cancellationToken);
                }
                var answerId = 1;
                if (callbackQuery.Data.Contains("QuestionAnwer"))
                {
                    var splitData = callbackQuery.Data.Split(";");
                    answerId = Convert.ToInt32(splitData[1]);
                }

                var questions = GetTeacherQuestions();
                if (questions != null)
                {
                    List<List<InlineKeyboardButton>> inLineList = new List<List<InlineKeyboardButton>>();
                    List<InlineKeyboardButton> inLineRow = new List<InlineKeyboardButton>();
                    if (answerId > 1)
                    {
                        InlineKeyboardButton inLineKeyboardPrev = InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: $"QuestionAnwer;{answerId - 1}");
                        inLineRow.Add(inLineKeyboardPrev);
                    }

                    InlineKeyboardButton inLineKeyboard = InlineKeyboardButton.WithCallbackData(text: "Выбрать", callbackData: $"AnswerSelected;{answerId}");
                    inLineRow.Add(inLineKeyboard);

                    if (answerId < questions.Count())
                    {
                        InlineKeyboardButton inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"QuestionAnwer;{answerId + 1}");
                        inLineRow.Add(inLineKeyboardNext);
                    }

                    InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);

                    if (callbackQuery.Data.Contains("QuestionAnwer"))
                        await botClient.EditMessageTextAsync(chatId, messageId, $"{questions[answerId]}", replyMarkup: inlineKeyboardMarkup, parseMode: ParseMode.Html);
                    else
                    {
                        botClient.SendTextMessageAsync(chatId, $"{questions[answerId]}", replyMarkup: inlineKeyboardMarkup, parseMode: ParseMode.Html);
                    }
                    return;
                }
            }


            if (!string.IsNullOrWhiteSpace(callbackQuery.Data) && callbackQuery.Data.Contains("Reason"))
            {
                var splitData = callbackQuery.Data.Split(";");
                var currentTeacher = Convert.ToInt32(splitData[1]);
                await new Common().ChooseTeacher(botClient, callbackQuery, currentTeacher, cancellationToken);
                return;
            }

            if (!string.IsNullOrWhiteSpace(callbackQuery.Data) && callbackQuery.Data.Contains("Training"))
            {
                var splitData = callbackQuery.Data.Split(";");
                var currentTraining = Convert.ToInt32(splitData[1]);
                await new Common().Training(botClient, callbackQuery, currentTraining, cancellationToken);
                return;
            }

            if (!string.IsNullOrWhiteSpace(callbackQuery.Data) && callbackQuery.Data.Contains("Second"))
            {
                var delays = _userService.GetNotifies();

                List<List<InlineKeyboardButton>> inLineDelayList = new List<List<InlineKeyboardButton>>();
                foreach (var delay in delays)
                {
                    inLineDelayList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: delay.Description, callbackData: $"Delay;{delay.Guid}") });
                }
                InlineKeyboardMarkup inlineTaskKeyboard = new InlineKeyboardMarkup(inLineDelayList);

                await botClient.EditMessageTextAsync(chatId, messageId, $"☝️<b>как часто ты хотел бы общаться со мной?</b>", replyMarkup: inlineTaskKeyboard, parseMode: ParseMode.Html);

                return;
            }

            if (callbackQuery.Message.Text.Equals("Выберите урок:"))
            {
                var tasks = _courseService.GetTasksByLessonGuid(Guid.Parse(callbackQuery.Data));
                if (tasks == null || !tasks.Any())
                {
                    await botClient.EditMessageTextAsync(chatId, messageId, $"Выберите урок:", replyMarkup: callbackQuery.Message.ReplyMarkup);
                    return;
                }

                List<List<InlineKeyboardButton>> inLineTasksList = new List<List<InlineKeyboardButton>>();

                foreach (var task in tasks)
                {
                    inLineTasksList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: $"{task.Name}", callbackData: task.Guid.ToString()) });
                }

                InlineKeyboardMarkup inlineTaskKeyboard = new InlineKeyboardMarkup(inLineTasksList);

                await botClient.EditMessageTextAsync(chatId, messageId, $"Выберите задачу:", replyMarkup: inlineTaskKeyboard);
            }
            return;


        }

        public Dictionary<int, string> GetTeacherQuestions()
        {
            Dictionary<int, string> phrases = new Dictionary<int, string>();
            //phrases.Add(0, $"🤔расскажи, что обычно мотивирует тебя не терять интерес и фокусироваться на своей цели? Какое из описаний ниже совпадает с тобой максимально точно? \n\n👌это поможет мне лучше подстроиться под тебя и общаться с тобой на одной волне!");
            
            phrases.Add(1, $"🙏 Я - Мыслитель.\n\nЛюблю осознанно подходить к любому процессу, размышляю и во всем ищу смысл.\n\nЯ держу фокус на цели и возвращаюсь к ней время от времени.");

            phrases.Add(2, $"💥Я – Активист.\n\nМне нравится держать ритм и соревноваться, поддерживаю спортивный интерес даже в обучении.\n\nСамодисциплина - моя основа и залог высокой мотивации.");

            phrases.Add(3, $"😎Я – Прагматик. \n\nЯ всегда просчитываю варианты и выгоды, видение будущего успеха помогает мне.\n\nМеня вдохновляют и мотивируют истории успеха.");
           
            phrases.Add(4, $"😂Я – Прокрастинатор.\n\nЯ постоянно нахожу важные дела, чтобы не садиться за домашку.\n\nГлавное для меня — это побороть свою лень и договориться с собой.\n\nМоя основа и причина успехов — это внутренняя работа с собой.");

            return phrases;
        }
        public Dictionary<int, string> GetTeacherPhrases()
        {
            Dictionary<int, string> phrases = new Dictionary<int, string>();
            phrases.Add(1, "😊Здорово, будем знакомы! Я -  Цифровой Гуру." +
                            "\nЯ, так же, как и ты, люблю осознанность и смысл во всем." +
                            "\nЯ знаю, что ты поставил себе большую и амбициозную цель - и я уверен, что у тебя все получится.Я буду рядом и не дам тебе остановиться на полпути!" +
                            "\n\nЯ готов появиться в любой момент, когда буду нужен тебе: " +
                            "\n✅ты чувствуешь, что тебе нужна помощь и поддержка - /help" +
                            "\n✅ты хочешь услышать мой голос и помедитировать - /meditation" +
                            "\n✅ты хочешь узнать о своих успехах - /mysuccess" +
                            "\n✅ты хочешь узнать о себе больше - /tellme" +
                            "\n\n🌟Помни! Цель не в том, чтобы быть лучше, чем кто - либо, а в том, чтобы быть лучше прежнего тебя.");

            phrases.Add(2, "Ух ты!! Я рад, что ты, как и я, любишь движение вперед! 🤩"+
                            "\n\nБудем знакомы - я твой Фитнес - тренер на пути обучения!" +
                            "\n\n💪Мы вместе добьемся лучших результатов, ты прокачаешься новыми знаниями по полной! Я не дам тебе снизить ритм и потерять форму!" +
                            "\n\nЯ готов появиться в любой момент, когда буду нужен тебе: " +
                            "\n✅ты чувствуешь, что тебе нужна помощь и поддержка - /help" +
                            "\n✅ты хочешь услышать мой голос и помедитировать - /meditation" +
                            "\n✅ты хочешь узнать о своих успехах - /mysuccess" +
                            "\n✅ты хочешь узнать о себе больше - /tellme");

            phrases.Add(3, "Коллега, я рад знакомству!" +
                            "\n\nЗдесь, я твой Бизнес - партнер 😎" +
                            "\n\nи мы вместе пойдем к твоей цели, чтобы прийти к твоему успеху максимально быстро🚀!" +
                            "\n\nЯ разделяю твой бизнес - подход, мне нравится достигать успеха, и я также постоянно экспериментирую!" +
                            "\n\nЯ готов появиться в любой момент, когда буду нужен тебе:" +
                            "\n✅ты чувствуешь, что тебе нужна помощь и поддержка - /help" +
                            "\n✅ты хочешь услышать мой голос и помедитировать - /meditation" +
                            "\n✅ты хочешь узнать о своих успехах - /mysuccess" +
                            "\n✅ты хочешь узнать о себе больше - /tellme");

            phrases.Add(4, "🌟Круто! Я - Федор Лежебокин и твое отражение!" +
                            "\n\nЯ буду задавать тебе много вопросов и эмоционально поддерживать тебя. 💪Мы вместе будем бороться с твоей прокрастинацией и идти к твоей цели." +
                            "\nЯ готов появиться в любой момент, когда буду нужен тебе:" +
                            "\n✅ты чувствуешь, что тебе нужна помощь и поддержка - /help" +
                            "\n✅ты хочешь услышать мой голос и помедитировать - /meditation" +
                            "\n✅ты хочешь узнать о своих успехах - /mysuccess" +
                            "\n✅ты хочешь узнать о себе больше - /tellme");
            return phrases;
        }
    }
}
