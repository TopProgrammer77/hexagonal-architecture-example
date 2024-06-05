
namespace Common.DTO.Order;

public class PaymentDTO : AbstractDTO 
{
    private readonly string name;
    private readonly string email;

    public PaymentDTO(string name, string email) {
        this.name = name;
        this.email = email;
    }

    public string GetName() {
        return name;
    }

    public string GetEmail() {
        return email;
    }

    public override bool Equals(object? o) {
        if (this == o) return true;
        if (o == null || GetType() != o.GetType()) return false;
        PaymentDTO that = (PaymentDTO) o;
        return Equals(name, that.name) &&
                Equals(email, that.email);
    }

    public override int GetHashCode() {
        return HashCode.Combine(name, email);
    }
}