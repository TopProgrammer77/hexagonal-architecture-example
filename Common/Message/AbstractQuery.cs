
namespace Common.Message;

public abstract class AbstractQuery : AbstractMessage
{
    public AbstractQuery(long messageId) : base(messageId)
    {
    }
}
