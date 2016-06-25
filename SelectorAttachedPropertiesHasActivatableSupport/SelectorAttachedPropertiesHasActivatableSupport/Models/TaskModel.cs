using SelectorAttachedPropertiesHasActivatableSupport.Utils;

namespace SelectorAttachedPropertiesHasActivatableSupport.Models
{
    public class TaskModel : ViewModel, IActivatable
    {
        private string _name;
        private bool _isDone;
        private string _statusText;

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public string StatusText
        {
            get { return _statusText; }
            set { Set(ref _statusText, value); }
        }

        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                if (Set(ref _isDone, value))
                {
                    StatusText = $"Marked {Name} as {(value ? "" : "not ")}done.";
                }
            }
        }

        public void Activate()
        {
            StatusText = $"Activated {Name}";
        }

        public bool Deactivate()
        {
            if (IsDone)
            {
                StatusText = $"Deactivated {Name}";
            }
            else
            {
                StatusText = $"Cannot deactivate task {Name}, it's not done yet.";
            }
            return IsDone;
        }
    }
}