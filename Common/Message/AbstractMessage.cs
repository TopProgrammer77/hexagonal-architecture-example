

namespace Common.Message;

[Serializable]
public abstract class AbstractMessage
{
    public AbstractMessage(long messageId)
    {
        this.MessageId = messageId;
        CreationDateTime = DateTime.Now;
    }

    public AbstractMessage()
    {
        CreationDateTime = DateTime.Now;
    }

    public abstract string Name
    {
        get;
    }

    public long MessageId
    {
        get; private set;
    }

    public DateTime CreationDateTime
    {
        get; private set;
    }
}
