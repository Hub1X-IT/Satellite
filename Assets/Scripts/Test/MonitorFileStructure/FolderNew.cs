using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FolderNew : DataContainerNew
{
    public FolderNew(string name) : base(name)
    {
        ChildDataContainersDictionary = new();
    }

    public Dictionary<string, DataContainerNew> ChildDataContainersDictionary { get; private set; }

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
