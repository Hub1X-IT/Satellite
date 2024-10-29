using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Folder : DataContainer
{
    public Folder(string name) : base(name)
    {
        ChildDataContainersDictionary = new();
    }

    [SerializeField]
    private Folder[] childFolders;

    public Dictionary<string, DataContainer> ChildDataContainersDictionary { get; private set; }

    public void Initialize()
    {
        ChildDataContainersDictionary = new();

        if (childFolders != null)
        {
            foreach (var folder in childFolders)
            {
                folder.Initialize();
                folder.SetParentFolder(this);
            }
        }
    }

    public void AddChildDataContainer(string name, DataContainer dataContainer)
    {
        // Add a contain check!
        ChildDataContainersDictionary.Add(name, dataContainer);
    }
}
