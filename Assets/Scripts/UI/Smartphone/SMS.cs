public class SMS
{
    public SMS(string title, string content)
    {
        Title = title;
        Content = content;
    }


    public string Title { get; private set; }
    public string Content { get; private set; }
}
