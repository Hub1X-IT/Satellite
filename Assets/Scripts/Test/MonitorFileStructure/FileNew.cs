using System;

[Serializable]
public class FileNew<T> : DataContainerNew
{
    public FileNew(string name) : base(name) { }

    public T Content { get; private set; }

    public void SetContent(T content)
    {
        Content = content;
    }
}
