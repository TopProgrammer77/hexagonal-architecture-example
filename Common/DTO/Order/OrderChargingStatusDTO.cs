
namespace Common.DTO.Order;

public class OrderChargingStatusDTO : AbstractDTO
{
    private readonly bool _successful;
    private readonly string? _failureReason;

    public OrderChargingStatusDTO(bool successful, string failureReason)
    {
        _successful = successful;
        _failureReason = failureReason;
    }

    public OrderChargingStatusDTO()
    {
        _successful = true;
    }

    public bool IsSuccessful()
    {
        return _successful;
    }

    public string? GetFailureReason()
    {
        return _failureReason;
    }

    public override bool Equals(object? obj)
    {
        if (this == obj)
            return true;

        if (obj == null || GetType() != obj.GetType())
            return false;

        OrderChargingStatusDTO other = (OrderChargingStatusDTO)obj;
        return _successful == other._successful && _failureReason == other._failureReason;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_successful, _failureReason);
    }
}
