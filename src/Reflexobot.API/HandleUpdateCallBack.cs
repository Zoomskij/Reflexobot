using Newtonsoft.Json;
using Reflexobot.Entities;
using Reflexobot.Services.Inerfaces;
using Reflexobot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Linq;
using Reflexobot.API.Helpers;
using Reflexobot.Common;

namespace Reflexobot.API
{
    public class HandleUpdateCallBack
    {
        private readonly ICourseService _courseService;
        private readonly IReceiverService _receiverService;
        private readonly IStudentService _studentService;
        private readonly IScenarioService _scenarioService;
        public HandleUpdateCallBack(ICourseService courseService, IReceiverService receiverService, IStudentService studentService, IScenarioService scenarioService)
        {
            _courseService = courseService;
            _receiverService = receiverService;
            _studentService = studentService;
            _scenarioService = scenarioService;
        }
        public async Task HandleUpdateCallBackAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            //return;
            if (callbackQuery == null)
                return;

            var chatId = callbackQuery.Message.Chat.Id;
            var messageId = callbackQuery.Message.MessageId;
            var student = await _studentService.GetStudentByChatIdAsync(chatId);

            Guid.TryParse(callbackQuery.Data, out Guid callbackGuid);
            if (callbackGuid != Guid.Empty)
            {
                var allScenarios = _scenarioService.Get().ToList();
                var scenarios = allScenarios.Where(x => x.ParrentGuid == callbackGuid);
                var parrentGuid = allScenarios.First(x => x.Guid == callbackGuid).ParrentGuid;
                //parrentGuid = allScenarios.First(x => x.ParrentGuid == callbackGuid).ParrentGuid;
                foreach (var scenario in scenarios)
                {
                    // Send text with buttons
                    if (!string.IsNullOrWhiteSpace(scenario.Command))
                    {
                        List<InlineKeyboardButton> inLineRow = new();
                        InlineKeyboardButton inLineKeyboardNext;
                        InlineKeyboardMarkup inlineKeyboardMarkup;
                        List<List<InlineKeyboardButton>> inLineList = new();
                        switch (scenario.Command)
                        {
                            case "Next":
                                if (parrentGuid.HasValue && parrentGuid != Guid.Empty)
                                    inLineRow.Add(InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: $"{parrentGuid}"));
                                inLineRow.Add(inLineKeyboardNext = InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"{scenario.Guid}"));
                                inlineKeyboardMarkup = new InlineKeyboardMarkup(inLineRow);

                                await botClient.EditMessageTextAsync(
                                    messageId: messageId,
                                    chatId: chatId,
                                    text: scenario.Text ?? "empty message",
                                    replyMarkup: inlineKeyboardMarkup,
                                    parseMode: ParseMode.Html,
                                    cancellationToken: cancellationToken);
                                continue;

                            case "Choose":
                                var chooses = allScenarios.Where(x => x.ParrentGuid == scenario.Guid);
                                var answerGuid = chooses.FirstOrDefault(x => x.Type == 3)?.Guid;


                                if (parrentGuid.HasValue && parrentGuid != Guid.Empty)
                                    inLineList.Add(new List<InlineKeyboardButton>() {
                                        { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: $"{parrentGuid}") },
                                        { InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"{callbackGuid}") }
                                    });
                                foreach (var choose in chooses)
                                {
                                    inLineList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: choose.Text, callbackData: $"{answerGuid}") });
                                }
                                InlineKeyboardMarkup inlineTaskKeyboard = new InlineKeyboardMarkup(inLineList);

                                await botClient.EditMessageTextAsync(chatId, messageId, text: scenario.Text ?? "empty message", replyMarkup: inlineTaskKeyboard, parseMode: ParseMode.Html);
                                continue;

                            case "List":
                                var lists = allScenarios.Where(x => x.ParrentGuid == scenario.Guid).ToList();
                                Scenario currentScenario = new();
                                Scenario nextScenario = new();
                                if (!lists.Any(x => x.Guid == callbackGuid))
                                {
                                    currentScenario = lists.First();
                                    int index = lists.Select((elem, index) => new { elem, index }).First(p => p.elem == currentScenario).index;
                                    nextScenario = lists.Skip(++index).Take(1).First();
                                }
                                if (parrentGuid.HasValue && parrentGuid != Guid.Empty)
                                    inLineList.Add(new List<InlineKeyboardButton>() {
                                        { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: $"{parrentGuid}") },
                                        { InlineKeyboardButton.WithCallbackData(text: "Дальше", callbackData: $"{nextScenario.Guid}") }
                                    });
                                inLineList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: "Выбрать", callbackData: $"{currentScenario.Guid}") });

                                inlineTaskKeyboard = new InlineKeyboardMarkup(inLineList);

                                await botClient.EditMessageTextAsync(chatId, messageId, text: currentScenario.Text ?? "empty message", replyMarkup: inlineTaskKeyboard, parseMode: ParseMode.Html);
                                continue;

                            default:
                                continue;
                        }
                    }

                    // Send just text
                    await botClient.EditMessageTextAsync(
                        messageId: messageId,
                        chatId: chatId,
                        text: scenario.Text ?? "empty message",
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken);
                    await Task.Delay(100);
                }
                return;
            }

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
                selecteTeacherId--; //TODO; временно до изменения айдишников персонажей
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
                            StudentGuid = student.Guid
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
                        StudentGuid = student.Guid
                    };

                    await _studentService.AddOrUpdateUserNotifyId(userNotifyIds);

                    var questionComon = "🤔расскажи, что обычно мотивирует тебя не терять интерес и фокусироваться на своей цели? Какое из описаний ниже совпадает с тобой максимально точно? \n\n👌это поможет мне лучше подстроиться под тебя и общаться с тобой на одной волне!";

                    await botClient.EditMessageTextAsync(
                        chatId: chatId,
                        messageId: messageId,
                        text: questionComon,
                        cancellationToken: cancellationToken);
                }
                var answerId = 0;
                var isNew = true;
                if (callbackQuery.Data.Contains("QuestionAnwer"))
                {
                    var splitData = callbackQuery.Data.Split(";");
                    answerId = Convert.ToInt32(splitData[1]);
                    isNew = false;
                }

                var questions = GetTeacherQuestions();
                NavigationModel model = new NavigationModel
                {
                    Items = questions,
                    ChatId = callbackQuery.Message.Chat.Id,
                    MessageId = callbackQuery.Message.MessageId,
                    NavigationCommand = "QuestionAnwer",
                    SelectCommand = "AnswerSelected",
                    NextStepCommand = string.Empty,
                    CurrentPosition = answerId,
                    IsNew = isNew
                };

                await NavigationHelper.Navigation(botClient, model);
            }

            if (!string.IsNullOrWhiteSpace(callbackQuery.Data) && callbackQuery.Data.Contains("NavigationCourse;"))
            {
                var courses = _courseService.GetCourses();
                var images = courses.Select(x => x.Img).ToArray();

                var splitData = callbackQuery.Data.Split(";");
                var currentCourse = Convert.ToInt32(splitData[1]);
                NavigationModel model = new NavigationModel
                {
                    Images = images,
                    ChatId = callbackQuery.Message.Chat.Id,
                    MessageId = callbackQuery.Message.MessageId,
                    NavigationCommand = "NavigationCourse",
                    SelectCommand = "SelectedCourse",
                    NextStepCommand = string.Empty,
                    CurrentPosition = currentCourse
                };

                await NavigationHelper.Navigation(botClient, model);
                return;
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
                var delays = _studentService.GetNotifies();

                List<List<InlineKeyboardButton>> inLineDelayList = new List<List<InlineKeyboardButton>>();
                foreach (var delay in delays)
                {
                    inLineDelayList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: delay.Description, callbackData: $"Delay;{delay.Guid}") });
                }
                InlineKeyboardMarkup inlineTaskKeyboard = new InlineKeyboardMarkup(inLineDelayList);

                await botClient.EditMessageTextAsync(chatId, messageId, $"☝️<b>как часто ты хотел бы общаться со мной?</b>", replyMarkup: inlineTaskKeyboard, parseMode: ParseMode.Html);

                return;
            }

            if (callbackQuery.Data.Contains("SelectedCourse;"))
            {
                var splitData = callbackQuery.Data.Split(";");
                var currentTeacher = Convert.ToInt32(splitData[1]);

                //TODO: брать из параметра
                var courseGuid = Guid.Parse("8B7AFF9B-E2D0-494F-85BD-D29F96C6AB65");
                var lessons = _courseService.GetLessonEntitiesByCourseGuid(courseGuid);

                if (lessons == null || !lessons.Any())
                {
                    await botClient.EditMessageTextAsync(chatId, messageId, $"Выберите урок:", replyMarkup: callbackQuery.Message.ReplyMarkup);
                    return;
                }

                List<List<InlineKeyboardButton>> inLineLessonList = new List<List<InlineKeyboardButton>>();

                foreach (var lesson in lessons)
                {
                    inLineLessonList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: $"{lesson.Name}", callbackData: lesson.Guid.ToString()) });
                }

                InlineKeyboardMarkup inlineLessonKeyboard = new InlineKeyboardMarkup(inLineLessonList);
                await botClient.EditMessageReplyMarkupAsync(chatId, messageId);

                var curse = await _courseService.GetCourse(courseGuid);
                await botClient.SendTextMessageAsync(chatId, $"<b>Цель курса:</b>{curse?.Goal?.Text}", parseMode: ParseMode.Html);

                await botClient.SendTextMessageAsync(chatId,"<b>Выберите урок:</b>", replyMarkup: inlineLessonKeyboard, parseMode: ParseMode.Html);
            }

            if (callbackQuery.Data.Equals("Выберите урок:"))
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

        public string[] GetTeacherQuestions()
        {
            string[] phrases = { 
            //phrases.Add(0, $"🤔расскажи, что обычно мотивирует тебя не терять интерес и фокусироваться на своей цели? Какое из описаний ниже совпадает с тобой максимально точно? \n\n👌это поможет мне лучше подстроиться под тебя и общаться с тобой на одной волне!");
                $"🙏 Я - Мыслитель.\n\nЛюблю осознанно подходить к любому процессу, размышляю и во всем ищу смысл.\n\nЯ держу фокус на цели и возвращаюсь к ней время от времени.",
                $"💥Я – Активист.\n\nМне нравится держать ритм и соревноваться, поддерживаю спортивный интерес даже в обучении.\n\nСамодисциплина - моя основа и залог высокой мотивации.",
                $"😎Я – Прагматик. \n\nЯ всегда просчитываю варианты и выгоды, видение будущего успеха помогает мне.\n\nМеня вдохновляют и мотивируют истории успеха.",
                $"😂Я – Прокрастинатор.\n\nЯ постоянно нахожу важные дела, чтобы не садиться за домашку.\n\nГлавное для меня — это побороть свою лень и договориться с собой.\n\nМоя основа и причина успехов — это внутренняя работа с собой."
            };
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
