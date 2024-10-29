using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DiskNew
{
    public DiskNew(string name)
    {
        Name = name;
        ChildDataContainersDictionary = new();
    }
    public string Name { get; private set; }

    public Dictionary<string, DataContainerNew> ChildDataContainersDictionary { get; private set; }

    public void SetName(string name)
    {
        Name = name;
    }

    public void AddChildDataContainer(string name, DataContainerNew dataContainer)
    {
        // Add a contain check!
        ChildDataContainersDictionary.Add(name, dataContainer);
    }

    public void RemoveChildDataContainer(string name)
    {
        ChildDataContainersDictionary.Remove(name);
    }
}
