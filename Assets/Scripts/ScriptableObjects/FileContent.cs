using System;
using UnityEngine;

[Serializable]
public class FileContent<T>
{
    [SerializeField]
    private T content;

    public T Content => content;
}
