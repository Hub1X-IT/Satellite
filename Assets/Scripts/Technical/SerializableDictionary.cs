using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that allows to make a serializable dictionary.
/// To get the dictionary, use property Dictionary.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
[Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [SerializeField]
    private DictionaryItem[] items;

    public Dictionary<TKey, TValue> Dictionary
    {
        get
        {
            Dictionary<TKey, TValue> tempDictionary = new();
            foreach (DictionaryItem item in items)
            {
                tempDictionary.Add(item.key, item.value);
            }
            return tempDictionary;
        }
    }

    [Serializable]
    private class DictionaryItem
    {
        public TKey key;
        public TValue value;
    }
}