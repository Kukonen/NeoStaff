namespace NeoStaffBot
{
    internal class DailyScheduler
    {
        private readonly Action _action;
        private readonly TimeSpan _timeOfDay;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public DailyScheduler(Action action, TimeSpan timeOfDay)
        {
            _action = action;
            _timeOfDay = timeOfDay;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            Task.Run(async () =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    //DateTime now = DateTime.Now;
                    //DateTime nextRun = now.Date.Add(new TimeSpan(10));

                    //if (now > nextRun)
                    //{
                    //    nextRun = nextRun.AddMilliseconds(10);
                    //}

                    //TimeSpan delay = nextRun - now;

                    //await Task.Delay(delay, _cancellationTokenSource.Token);

                    //if (!_cancellationTokenSource.Token.IsCancellationRequested)
                    //{
                    //    _action.Invoke();
                    //}

                    try
                    {
                        _action.Invoke();
                        await Task.Delay(10000, _cancellationTokenSource.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }
            });
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
