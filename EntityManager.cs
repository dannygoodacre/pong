using System;
using System.Collections.Generic;
using System.Linq;

namespace Pong;

public class EntityManager
{
    private readonly Dictionary<Type, Dictionary<int, object>> _componentStores = [];

    private int _nextEntityId = 0;

    public int CreateEntity() => _nextEntityId++;

    public void AddComponent<T>(int entity, T component)
    {
        var store = GetOrCreateComponentStore<T>();
        
        store[entity] = component;
    }
    
    public T GetComponent<T>(int entity)
    {
        var store = GetOrCreateComponentStore<T>();

        return (T)store[entity];
    }

    public void SetComponent<T>(int entity, T component)
    {
        var store = GetOrCreateComponentStore<T>();

        store[entity] = component;
    }

    public IEnumerable<int> WithComponent<T>() => _componentStores[typeof(T)].Keys;

    public IEnumerable<int> WithComponents<T1, T2>() => WithComponent<T1>().Intersect(WithComponent<T2>());

    public void RemoveEntity(int entity)
    {
        foreach (var store in _componentStores)
        {
            store.Value.Remove(entity);
        }
    }

    private Dictionary<int, object> GetOrCreateComponentStore<T>()
    {
        var type = typeof(T);

        if (!_componentStores.TryGetValue(type, out var store))
        {
            store = [];

            _componentStores[type] = store;
        }

        return store;
    }
}
