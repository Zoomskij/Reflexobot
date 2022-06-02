using Reflexobot.Service.Services.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Service.Services
{
    public class ReceiverService : IReceiverService
    {
        public ReceiverService() { }
        public async Task<string[]> GetPhrases()
        {
            string[] phrases =
            {
                "Привет! Я - твой Гуру и буду помогать тебе постигать твой путь обучения и дойти до поставленной тобой цели!",
                "Спасибо, что выбрал меня",
                "Я знаю, что ты поставил себе большую и амбициозную цель- и я уверен, что у тебя все получится. Я буду рядом и не дам тебе остановиться на полпути!"
            };
            return phrases;
        }
    }
}
