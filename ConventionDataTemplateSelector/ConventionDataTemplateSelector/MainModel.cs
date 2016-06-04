using ConventionDataTemplateSelectorDemo.Pills;

namespace ConventionDataTemplateSelectorDemo
{
    public class MainModel : ViewModel
    {
        private ViewModel _pill;

        public void SetRedPill()
        {
            Pill = new RedPillModel();
        }

        public void SetBluePill()
        {
            Pill = new BluePillModel();
        }

        public ViewModel Pill
        {
            get { return _pill; }
            set
            {
                if (_pill == value) return;
                _pill = value;
                OnPropertyChanged();
            }
        }
    }
}