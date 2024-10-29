using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Disk
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private Folder[] childFolders;

    private string name;

    public string Name
    {
        get
        {
            name ??= _name;
            return name;
        }
        private set => name = value;
    }

    public Dictionary<string, DataContainer> ChildDataContainersDictionary { get; private set; }

    public void Initialize()
    {
        ChildDataContainersDictionary = new();

        if (childFolders != null)
        {
            foreach (var folder in childFolders)
            {
                folder.Initialize();
                folder.SetParentDisk(this);
            }
        }
    }

    public void AddChildDataContainer(string name, DataContainer dataContainer)
    {
        // Add a contain check!
        ChildDataContainersDictionary.Add(name, dataContainer);
    }
}
