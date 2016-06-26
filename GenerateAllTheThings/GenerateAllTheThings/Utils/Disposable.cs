using System;
using System.Threading;

namespace GenerateAllTheThings.Utils
{
    public class Disposable : IDisposable
    {
        public static readonly IDisposable Empty = new Disposable(null);

        private Action _action;

        public static IDisposable Create(Action action)
        {
            if (action == null) return Empty;
            return new Disposable(action);
        }

        private Disposable(Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            if (_action == null) return;
            Action action = Interlocked.Exchange(ref _action, null);
            if (action == null) return;
            action();
        }
    }
}