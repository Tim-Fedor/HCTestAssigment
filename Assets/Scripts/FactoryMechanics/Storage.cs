using Resource;

namespace FactoryMechanics
{
    public class Storage
    {
        public Storage(int capacity, ResourceType type)
        {
            Resource = type;
            Capacity = capacity;
            CurrentAmount = 0;
        }

        public int CurrentAmount { get; set; }

        public ResourceType Resource { get; }

        public int Capacity { get; }
    }
}