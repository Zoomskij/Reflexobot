using Reflexobot.Entities;
using Reflexobot.Services.Inerfaces;
using Reflexobot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Reflexobot.API
{
    public class HandleUpdateCallBack
    {
        private readonly ICourseService _courseService;
        private readonly IReceiverService _receiverService;
        public HandleUpdateCallBack(ICourseService courseService, IReceiverService receiverService)
        {
            _courseService = courseService;
            _receiverService = receiverService;
        }
        public async Task HandleUpdateCallBackAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            //return;
            if (callbackQuery == null)
                return;
            
            if (!string.IsNullOrWhiteSpace(callbackQuery.Data) && callbackQuery.Data.Contains("Delay"))
            {
                var splitData = callbackQuery.Data.Split(";");
                var currentDelay = Convert.ToInt32(splitData[1]);

                //STEP 3
                await botClient.SendTextMessageAsync(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: $"Вопрос 3 из 3\n\nКакие курсы ты проходишь в Нетологии?",
                    cancellationToken: cancellationToken);

                return;
            }

            if (!string.IsNullOrWhiteSpace(callbackQuery.Data) && callbackQuery.Data.Contains("Reason"))
            {
                var splitData = callbackQuery.Data.Split(";");
                var currentTeacher = Convert.ToInt32(splitData[1]);
                await new Common().ChooseTeacher(botClient, callbackQuery, currentTeacher, cancellationToken);
                return;
            }

            var chatId = callbackQuery.Message.Chat.Id;
            if (callbackQuery.Message.Text.Equals("Выберите курс:"))
            {

                var lessons = _courseService.GetLessonEntitiesByCourseGuid(Guid.Parse(callbackQuery.Data));

                List<List<InlineKeyboardButton>> inLineLessonsList = new List<List<InlineKeyboardButton>>();
                foreach (var lesson in lessons)
                {
                    inLineLessonsList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: lesson.Name, callbackData: lesson.Guid.ToString()) });
                }

                InlineKeyboardMarkup inlineLessonKeyboard = new InlineKeyboardMarkup(inLineLessonsList);

                await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Выберите урок:",
                replyMarkup: inlineLessonKeyboard,
                cancellationToken: cancellationToken);
                return;
            }

            if (callbackQuery.Message.Text.Equals("Выберите урок:"))
            {

                var tasks = _courseService.GetTasksByLessonGuid(Guid.Parse(callbackQuery.Data));

                List<List<InlineKeyboardButton>> inLineTasksList = new List<List<InlineKeyboardButton>>();
                foreach (var task in tasks)
                {
                    inLineTasksList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: task.Name, callbackData: task.Guid.ToString()) });
                }

                InlineKeyboardMarkup inlineTaskKeyboard = new InlineKeyboardMarkup(inLineTasksList);

                await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Выберите задачу:",
                replyMarkup: inlineTaskKeyboard,
                cancellationToken: cancellationToken);
                return;
            }

            EventHandlerCallBack data;

            // Account account = JsonConvert.DeserializeObject<Account>(json); 
            var teacherId = int.Parse(callbackQuery.Data);
            var teachers = _receiverService.GetTeachers();

            if (teacherId == 99)
            {
                var currentTeacher = await _receiverService.GetPersonByUserId(callbackQuery.From.Id);
                if (currentTeacher != null)
                {
                    await botClient.SendStickerAsync(
                        chatId: chatId,
                        sticker: currentTeacher.Img,
                        cancellationToken: cancellationToken);
                }
                return;
            }

            if (teachers != null)
            {

                var teacher = teachers.FirstOrDefault(x => x.Id == teacherId);
                if (teacher != null)
                {
                    UserPersonIds userPersonIds = new UserPersonIds
                    {
                        PersonId = teacherId,
                        UserId = callbackQuery.From.Id
                    };
                    await _receiverService.AddOrUpdateUserPersonId(userPersonIds);
                    await botClient.SendStickerAsync(
                        chatId: chatId,
                        sticker: teacher.Img,
                        cancellationToken: cancellationToken);

                    var teacherPhrases = GetTeacherPhrases();

                    await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: teacherPhrases[teacherId],
                            cancellationToken: cancellationToken);


                    ///STEP 2
                    await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"Вопрос 2 из 3\n\n {callbackQuery.Message.Chat.FirstName}, как часто ты хотел бы общаться со мной?",
                            cancellationToken: cancellationToken);

                    List<List<InlineKeyboardButton>> inLineDelayList = new List<List<InlineKeyboardButton>>();
                    inLineDelayList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: "1 раз в день", callbackData: "Delay;1") });
                    inLineDelayList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: "1 раз в неделю", callbackData: "Delay;2") });
                    inLineDelayList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: "1 раз в 2 недели", callbackData: "Delay;3") });
                    inLineDelayList.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(text: "только в выходной", callbackData: "Delay;4") });

                    InlineKeyboardMarkup inlineTaskKeyboard = new InlineKeyboardMarkup(inLineDelayList);
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выберите ответ:",
                        replyMarkup: inlineTaskKeyboard,
                        cancellationToken: cancellationToken);

                    ///STEP 3
                    //await botClient.SendTextMessageAsync(
                    //    chatId: chatId,
                    //    text: $"Вопрос 3 из 3\n\nКакие курсы ты проходишь в Нетологии?",
                    //    cancellationToken: cancellationToken);
                    //return;
                }
            }
        }
        public Dictionary<int, string> GetTeacherPhrases()
        {
            Dictionary<int, string> phrases = new Dictionary<int, string>();
            phrases.Add(1, "Здорово! Будем знакомы! Я -  твой Гуру,  я, также как и ты, люблю осознанность и смысл во всем." +
                "\nЯ знаю, что ты поставил себе большую и амбициозную цель - и я уверен, что у тебя все получится.Я буду рядом и не дам тебе остановиться на полпути!" +
                "\nЯ готов появиться в любой момент, когда буду нужен тебе:\n✓  ты чувствуешь, что теряешь мотивацию и тебе нужна поддержка - /guruhelp" +
                "\n✓ ты хочешь услышать мой голос и помедитировать - /meditation" +
                "\n✓ ты хочешь сфокусироваться на цели - /mygoal" +
                "\nПомни! Цель не в том, чтобы быть лучше, чем кто - либо, а в том, чтобы быть лучше прежнего тебя.Сегодня ты на верном пути!");

            phrases.Add(2, "Ух ты!! Я рад, что ты, как и я,  любишь движение вперед! Будем знакомы- я твой Фитнес-тренер на пути обучения!" +
                            "\nМы вместе добьемся лучших результатов, ты прокачаешься новыми знаниями по полной!" +
                            "\nЯ готов появиться в любой момент, когда буду нужен тебе: " +
                            "\n✓  ты чувствуешь, что теряешь мотивацию и тебе нужна поддержка - /trenerhelp" +
                            "\n✓ ты хочешь услышать мой голос и взбодриться - /trenerdvizh" +
                            "\n✓ ты хочешь вспомнить как звучит твоя цель - /mygoal");

            phrases.Add(3, "Коллега, я рад знакомству! Здесь я твой Бизнес-партнер и мы вместе пойдем к твоей цели, чтобы прийти к твоему успеху максимально быстро!" +
                            "\nЯ разделяю твой бизнес - подход, мне нравится достигать успеха и я также постоянно совершенствуюсь!" +
                            "\nЯ готов появиться в любой момент, когда буду нужен тебе:" +
                            "\n✓  ты чувствуешь, что теряешь мотивацию и тебе нужна поддержка - /guruhelp" +
                            "\n✓ ты хочешь услышать мой голос и удержать фокус на цели -/businessgoal" +
                            "\n✓ ты хочешь узнать историю успеха - /success");

            phrases.Add(4, "Круто! Я, Федор лежебокин, и я - твое отражение! Я буду задавать тебе много вопросов и эмоционально поддерживать тебя. Мы вместе будем бороться с твоей прокрастинацией и идти к твоей цели." +
                            "\nЯ готов появиться в любой момент, когда буду нужен тебе: ✓  ты чувствуешь, что теряешь мотивацию и тебе нужна поддержка - /fedorhelp " +
                            "\n✓ ты хочешь порефлексировать и поразбираться в себе -/fedorref" +
                            "\n✓ ты хочешь вспомнить как звучит твоя цель - /mygoal");
            return phrases;
        }
    }
}
