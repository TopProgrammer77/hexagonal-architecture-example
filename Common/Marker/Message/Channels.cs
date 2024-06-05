namespace Common.Marker.Message;

public sealed class Channels
{

    // commands
    public static class Commands
    {
        public static readonly string CHARGE_ORDER = "charge_order";
        public static readonly string CREATE_ORDER = "create_order";
    }

    // events
    public static class Events
    {
        public static readonly string ORDER_CREATED = "order_created";
        public static readonly string ORDER_CHARGED = "order_charged";
        public static readonly string ORDER_NOT_CHARGED = "order_not_charged";
    }

    // queries
    public static class Queries
    {
        public static readonly string FIND_ORDER = "find_order";
    }

    // deny instantiation
    private Channels() { }
}
