
namespace Common.Message
{
    public abstract class AbstractCommand : AbstractMessage
    {
        public AbstractCommand(long messageId) : base(messageId) { }
    }
}
