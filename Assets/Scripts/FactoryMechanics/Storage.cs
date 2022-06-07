using Resource;

namespace FactoryMechanics
{
    public class Storage
    {
        private int _cur;
        public int CurrentAmount
        {
            get
            {
                return _cur;
            }
            set
            {
                _cur = value;
            }
        }

        public ResourceType Resource { get; }

        public int Capacity { get; }

        public Storage(int capacity, ResourceType type)
        {
            Resource = type;
            Capacity = capacity;
            CurrentAmount = 0;
        }
        
    }
}
