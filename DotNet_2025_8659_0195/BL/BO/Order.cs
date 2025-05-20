
namespace BO;

public class Order
{
    public bool IsCustomerMemberClub;
    public List<ProductInOrder> ProductsInOrder;
    public double Price;

    public Order(bool isCustomerMemberClub, List<ProductInOrder> productsInOrder, double price)
    {
        IsCustomerMemberClub = isCustomerMemberClub;
        ProductsInOrder = productsInOrder;
        Price = price;
    }

    public override string ToString() => this.ToStringProperty();
}
