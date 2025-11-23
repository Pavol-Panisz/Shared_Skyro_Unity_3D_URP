using System.Collections.Generic;

namespace Game.Items
{
    public class ItemsService : IItemsService
    {
        private readonly Dictionary<string, float> _items = new();

        private readonly System.Action<string, float> _onAdd;
        private readonly System.Action<string, float> _onChange;
        private readonly System.Action<string> _onRemove;

        public ItemsService(
            System.Action<string, float> onAdd,
            System.Action<string, float> onChange,
            System.Action<string> onRemove)
        {
            _onAdd = onAdd;
            _onChange = onChange;
            _onRemove = onRemove;
        }

        public bool Exists(string name) => _items.ContainsKey(name);

        public void Add(string name, float value)
        {
            if(_items.ContainsKey(name)) return;
            _items[name] = value;
            _onAdd?.Invoke(name, value);
        }

        public float Get(string name)
        {
            return _items.TryGetValue(name, out var value) ? value : float.NaN;
        }

        public void Change(string name, float newValue)
        {
            if(!_items.ContainsKey(name)) return;

            _items[name] = newValue;
            _onChange?.Invoke(name, newValue);
        }

        public void Remove(string name)
        {
            if(!_items.ContainsKey(name)) return;

            _items.Remove(name);
            _onRemove?.Invoke(name);
        }
    }
}