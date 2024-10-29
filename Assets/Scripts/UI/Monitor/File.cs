using System;

[Serializable]
public class File<T> : DataContainer
{
    public File(string name) : base(name) { }

    public T Content { get; private set; }

    public void SetContent(T content)
    {
        Content = content;
    }
}
