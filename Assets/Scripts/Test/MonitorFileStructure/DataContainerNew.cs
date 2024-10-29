using System;
using UnityEngine;

[Serializable]
public abstract class DataContainerNew
{
    public DataContainerNew(string name)
    {
        Name = name;
    }

    public virtual string Name { get; protected set; }


    public virtual FolderNew ParentFolder { get; protected set; }

    public virtual DiskNew RootDisk { get; protected set; }

    public virtual void SetName(string name)
    {
        Name = name;
    }

    public virtual void SetParentFolder(FolderNew parentFolder)
    {
        ParentFolder = parentFolder;
        RootDisk = parentFolder.RootDisk;
        parentFolder.AddChildDataContainer(Name, this);
    }

    public virtual void SetParentDisk(DiskNew parentDisk)
    {
        ParentFolder = null;
        RootDisk = parentDisk;
        parentDisk.AddChildDataContainer(Name, this);
    }
}
