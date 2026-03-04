namespace Productos.Cache;

public class CacheLru<TKey, TValue> : ICached<TKey, TValue>
    where TKey : notnull {
    
    private readonly int _capacity;
    private readonly Dictionary<TKey, TValue> _data = new();
    private readonly LinkedList<TKey> _usageOrder = new();
    
    public CacheLru(int capacity) {
        if (capacity <= 0)
            throw new ArgumentException("La capacidad debe ser mayor que 0.", nameof(capacity));
        _capacity = capacity;
    }
    public void Add(TKey key, TValue value) {
        if (_data.TryGetValue(key, out var existingValue)) {
            _data[key] = value;
            RefreshUsage(key);
            return;
        }
        if (_data.Count >= _capacity) {
            var oldestKey = _usageOrder.First!.Value;
            var oldestValue = _data[oldestKey];
            _usageOrder.RemoveFirst();
            _data.Remove(oldestKey);
        }
        _data.Add(key, value);
        _usageOrder.AddLast(key);
    }
    public TValue? Get(TKey key) {
        if (!_data.TryGetValue(key, out var value)) {
            return default;
        }
        RefreshUsage(key);
        return value;
    }
    public bool Remove(TKey key) {
        if (!_data.Remove(key)) {
            return false;
        }
        _usageOrder.Remove(key);
        return true;
    }
    private void RefreshUsage(TKey key) {
        _usageOrder.Remove(key);
        _usageOrder.AddLast(key);
    }
}