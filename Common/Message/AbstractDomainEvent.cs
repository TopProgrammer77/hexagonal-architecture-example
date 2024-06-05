
namespace Common.Message
{
    public abstract class AbstractDomainEvent : AbstractMessage
    {
        public AbstractDomainEvent(long messageId, long eventId) :
            base(messageId)
        {
            EventId = eventId;
        }

        public long EventId
        {
            get; private set;
        }
    }
}
