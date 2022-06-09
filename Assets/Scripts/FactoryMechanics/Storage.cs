using System;
using Resource;

namespace FactoryMechanics
{
    public class Storage
    {
        public event Action CurrentAmountChanged;
        private int _currentAmount;
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