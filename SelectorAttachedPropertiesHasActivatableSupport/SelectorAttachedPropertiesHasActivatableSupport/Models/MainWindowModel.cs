using System.Linq;

namespace SelectorAttachedPropertiesHasActivatableSupport.Models
{
    public class MainWindowModel : ViewModel
    {
        private string _title;
        private TaskModel _currentTask;
        private string _statusText;

        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        public string StatusText
        {
            get { return _statusText; }
            set { Set(ref _statusText, value); }
        }

        public TaskModel CurrentTask
        {
            get { return _currentTask; }
            set { Set(ref _currentTask, value); }
        }

        public TaskModelCollection Tasks { get; }

        public MainWindowModel()
        {
            Title = "Tasks";
            Tasks = new TaskModelCollection()
            {
                new TaskModel {Name = "Task 1"},
                new TaskModel {Name = "Task 2"},
                new TaskModel {Name = "Task 3"},
            };

            AddTaskStatusTextChangedHandlers();

            CurrentTask = Tasks.First();
        }

        private void AddTaskStatusTextChangedHandlers()
        {
            foreach (var task in Tasks)
            {
                task.PropertyChanged += (s, e) =>
                {
                    var t = (TaskModel) s;
                    if (e.PropertyName == nameof(t.StatusText))
                    {
                        StatusText = t.StatusText;
                    }
                };
            }
        }
    }
}