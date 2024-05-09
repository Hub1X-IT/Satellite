using System;
using System.Collections.Generic;

/// <summary>
/// A class that allows to make a serializable dictionary.
/// To get the dictionary, use function GetDictionary().
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
[Serializable]
public class SerializableDictionary<TKey, TValue> {


    public DictionaryItem[] items;
    

    [Serializable]
    public class DictionaryItem {
        public TKey key;
        public TValue value;
    }


    public Dictionary<TKey, TValue> GetDictionary() {
        Dictionary<TKey, TValue> tempDictionary = new Dictionary<TKey, TValue>();

        foreach (DictionaryItem item in items) {
            tempDictionary.Add(item.key, item.value);
        }

        return tempDictionary;
    }
}