using Resource;

namespace FactoryMechanics
{
    public class Storage
    {
        private int _currentAmount;
        public delegate void OnCurrentAmountChange();
        public event OnCurrentAmountChange CurrentAmountChanged;
        public Storage(int capacity, ResourceType type)
        {
            Resource = type;
            Capacity = capacity;
            CurrentAmount = 0;
            CurrentAmountChanged?.Invoke();
        }

        public int CurrentAmount
        {
            get
            {
                return _currentAmount;
            }
            set
            {
                _currentAmount = value;
                CurrentAmountChanged?.Invoke();
            }
        }

        public ResourceType Resource { get; }

        public int Capacity { get; }
        
    }
}