using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Common
{
    public class NavigationModel
    {
        /// <summary>
        /// Список для навигации
        /// </summary>
        public string[] Items { get; set; }
        /// <summary>
        /// Список для навигации по картинкам
        /// </summary>
        public string[] Images { get; set; }
        /// <summary>
        /// Id чата
        /// </summary>
        public long ChatId { get; set; }
        /// <summary>
        /// Id предыдущего сообщения для обновления
        /// </summary>
        public int MessageId { get; set; }
        /// <summary>
        /// Для чтения в callback функции 
        /// </summary>
        public string NavigationCommand { get; set; }
        /// <summary>
        /// Для чтения в callback функции (подтверждение выбора)
        /// </summary>
        public string SelectCommand { get; set; }
        /// <summary>
        /// Для чтения в callback функции (далее в конце списка)
        /// </summary>
        public string NextStepCommand { get; set; }
        /// <summary>
        /// Текущая позиция от 0
        /// </summary>
        public int CurrentPosition { get; set; } = 0;
        /// <summary>
        /// если true отправляем новое сообщение, иначе перерисовываем
        /// </summary>
        public bool IsNew { get; set; } = false;
    }
}
