namespace Game.Items
{
    public interface IItemsService
    {
        bool Exists(string name);
        void Add(string name, float value);
        float Get(string name);
        void Change(string name, float newValue);
        void Remove(string name);
    }

    public interface IItemsView
    {
        void AddUI(string name, float value);
        void ChangeUI(string name, float newValue);
        void RemoveUI(string name);
    }
}