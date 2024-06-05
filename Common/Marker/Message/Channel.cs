
using System.ComponentModel;

namespace Common.Marker.Message;

public enum Channel
{
    // Order channels
    CHARGE_ORDER,
    CREATE_ORDER,
    ORDER_CREATED,

    ORDER_CHARGED,
    ORDER_NOT_CHARGED
}

