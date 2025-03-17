namespace ffTicketingCsharp
{
    internal class Looper
    {
        private readonly IActor _actor;
        private readonly CancellationToken _token;
        private bool _inloop;
        private object _lock = new object();

        public Looper(IActor actor, CancellationToken token)
        {
            _actor = actor;
            _token = token;
        }

        public async Task Loop()
        {
            lock (_lock)
            {
                if (_inloop) return;
                _inloop = true;
            }

            while (!_token.IsCancellationRequested)
            {
                var result = _actor.Loop();
                if (result)
                {
                    return;
                }
            }
        }


    }
}
