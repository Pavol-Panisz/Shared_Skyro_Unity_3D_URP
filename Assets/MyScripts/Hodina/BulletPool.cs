using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    private readonly Bullet _prefab;
    private readonly int _capacity;
    private readonly Transform _parent;
    private readonly List<Bullet> _items;
    private readonly Queue<Bullet> _activeQueue;

    public BulletPool(Bullet prefab, int capacity, Transform parent = null)
    {
        _prefab = prefab;
        _capacity = Mathf.Max(1, capacity);
        _parent = parent;
        _items = new List<Bullet>(_capacity);
        _activeQueue = new Queue<Bullet>(_capacity);

        for (int i = 0; i < _capacity; i++)
        {
            var b = CreateNew();
            b.gameObject.SetActive(false);
            _items.Add(b);
        }
    }

    private Bullet CreateNew()
    {
        if (_prefab == null)
        {
            Debug.LogError("BulletPool: Prefab is null");
            return null;
        }

        Bullet b = Object.Instantiate(_prefab, _parent);
        return b;
    }

    public Bullet Get()
    {
        // 1) Prefer an inactive bullet
        for (int i = 0; i < _items.Count; i++)
        {
            if (!_items[i].gameObject.activeInHierarchy)
            {
                _activeQueue.Enqueue(_items[i]);
                return _items[i];
            }
        }

        // 2) No inactive available: recycle the oldest active
        // Cleanup any inactive at the front (in case of external deactivation)
        while (_activeQueue.Count > 0 && !_activeQueue.Peek().gameObject.activeInHierarchy)
        {
            _activeQueue.Dequeue();
        }

        if (_activeQueue.Count > 0)
        {
            var oldest = _activeQueue.Dequeue();
            oldest.gameObject.SetActive(false); // ensure reset
            _activeQueue.Enqueue(oldest); // re-enqueue as "newest" once reused
            return oldest;
        }

        // Shouldn't happen, but in case: return first item
        return _items.Count > 0 ? _items[0] : null;
    }
}
