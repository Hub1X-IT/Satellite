using System;
using UnityEngine;

[Serializable]
public abstract class DataContainer
{
    public DataContainer(string name)
    {
        Name = name;
    }

    [SerializeField]
    protected string _name;


    protected string name;

    public virtual string Name
    {
        get
        {
            // If null then set.
            name ??= _name;
            return name;
        }
        protected set => name = value;
    }

    public virtual Folder ParentFolder { get; protected set; }

    public virtual Disk RootDisk { get; protected set; }

    public virtual void SetName(string name)
    {
        Name = name;
    }

    public virtual void SetParentFolder(Folder parentFolder)
    {
        ParentFolder = parentFolder;
        RootDisk = parentFolder.RootDisk;
        parentFolder.AddChildDataContainer(Name, this);
    }

    public virtual void SetParentDisk(Disk parentDisk)
    {
        ParentFolder = null;
        RootDisk = parentDisk;
        parentDisk.AddChildDataContainer(Name, this);
    }
}
