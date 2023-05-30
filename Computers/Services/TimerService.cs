using Computers.Repository;
using System.Timers;

namespace Computers.Services
{
    public class TimerService : ITimerService
    {
        private readonly System.Timers.Timer _timer;
        //30 минут, 60сек * 30мин * 1000млсек
        private readonly int _time = 1800000; 
        private readonly IAuthorizationRepository _authorizationRepository;
        public TimerService(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
            _timer = new System.Timers.Timer(_time)
            {
                AutoReset = true,
                Enabled = true
            };
            _timer.Elapsed += OnTimedEvent;
            StartTimer();
        }
        public void StartTimer()
        {
            _timer.Start();
        }

        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            _authorizationRepository.DeleteAutorizationUsers();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }
    }
}
