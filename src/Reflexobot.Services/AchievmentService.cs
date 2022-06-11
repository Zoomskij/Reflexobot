using Reflexobot.Entities;
using Reflexobot.Repositories.Interfaces;
using Reflexobot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Services
{
    public class AchievmentService : IAchievmentService
    {
        private readonly IAchievmentRepository _achievmentRepository;
        public AchievmentService(IAchievmentRepository achievmentRepository)
        {
            _achievmentRepository = achievmentRepository;
        }

        public IEnumerable<Achievment> GetAchievments()
        {
            return _achievmentRepository.GetAchievments();
        }
    }
}
