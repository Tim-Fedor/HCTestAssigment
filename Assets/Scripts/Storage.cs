public class Storage
{
    private ResourceType _resource;
    private int _capacity;
    private int _currentAmount;

    public ResourceType Resource
    {
        get
        {
            return _resource;
        }
    }
    
    public int Capacity
    {
        get
        {
            return _capacity;
        }
    }

    public Storage(ResourceType type, int capacity)
    {
        _resource = type;
        _capacity = capacity;
        _currentAmount = 0;
    }

    public void GetItemFromStorage()
    {
        
    }
    
    public void GiveItemToStorage()
    {
        
    }
}
