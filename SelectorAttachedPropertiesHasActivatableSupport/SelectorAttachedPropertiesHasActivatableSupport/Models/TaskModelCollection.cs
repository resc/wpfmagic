using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SelectorAttachedPropertiesHasActivatableSupport.Models
{
    public class TaskModelCollection : ObservableCollection<TaskModel>
    {
        public TaskModelCollection()
        {
        }

        public TaskModelCollection(List<TaskModel> list) : base(list)
        {
        }

        public TaskModelCollection(IEnumerable<TaskModel> collection) : base(collection)
        {
        }
    }
}