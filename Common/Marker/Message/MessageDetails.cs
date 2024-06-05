namespace Common.Marker.Message;

public class MessageDetailsAttribute : Attribute
{
    public required Service Publisher { get; set; }

    public required Service[] Subscribers { get; set; }

    public required Channel Channel { get; set; }
}
