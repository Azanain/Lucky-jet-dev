using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolText : MonoBehaviour
{
    public Text Prefab { get; private set; }

    private Transform _container;
    private int _minCapacity;
    private int _maxCapacity;
    private bool _autoExpand;

    private List<Text> pool;

    public void Initialize(Text prefab = null, int minPoolSize = 5, int maxPoolSize = 5, bool autoExpand = true, Transform container = null)
    {
        _minCapacity = minPoolSize;
        _maxCapacity = maxPoolSize;
        _autoExpand = autoExpand;
        _container = container;
        Prefab = prefab;
        
        CreatePool();
    }
    private void OnValidate()
    {
        if (_autoExpand)
        {
            _maxCapacity = int.MaxValue;
        }
    }
    private void CreatePool()
    {
        pool = new List<Text>(_minCapacity);

        for (int i = 0; i < _minCapacity; i++)
        {
            CreateAllElement();
        }
    }
    private Text CreateAllElement(bool isActiveByDefault = false)
    {
        Text createdObject = null;
        createdObject = Instantiate(Prefab, _container);
        createdObject.gameObject.SetActive(false);
        pool.Add(createdObject);
         return createdObject;
    }
    public bool TryGetElement(out Text element)
    {
        foreach (var item in pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }
    public Text GetFreeElement(Vector3 position, Quaternion rotation)
    {
        var element = GetFreeElement(position);
        element.transform.rotation = rotation;
        return element;
    }
    public Text GetFreeElement(Vector3 position)
    {
        var element = GetFreeElement();
        element.transform.position = position;
        return element;
    }
    public Text GetFreeElement()
    {
        if (TryGetElement(out var element))
        {
            return element;
        }
        if (_autoExpand)
        {
            return CreateAllElement(true);
        }
        if (pool.Count < _maxCapacity)
        {
            return CreateAllElement(true);
        }
        throw new Exception("Poo; is over!");
    }
}
