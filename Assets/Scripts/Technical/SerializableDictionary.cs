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

    private Dictionary<TKey, TValue> dictionary;

    public Dictionary<TKey, TValue> Dictionary
    {
        get
        {
            if (dictionary == null)
            {
                Dictionary<TKey, TValue> dictionary = new();
                foreach (DictionaryItem item in items)
                {
                    dictionary.Add(item.key, item.value);
                }
                this.dictionary = dictionary;
            }
            return dictionary;
        }
    }

    [Serializable]
    private class DictionaryItem
    {
        public TKey key;
        public TValue value;
    }
}