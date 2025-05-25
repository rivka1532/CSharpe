
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace BO;
internal static class Tools
{

    public static string ToStringProperty<T>(this T obj, string prefix = "")
    {
        //StringBuilder sb = new StringBuilder();
        //Type t = obj?.GetType() ?? throw new Exception("Obj is NULL");
        //foreach (PropertyInfo prop in t.GetProperties())
        //{
        //    if (prop.PropertyType.IsPrimitive
        //        || prop.PropertyType == typeof(string)
        //        || prop.PropertyType == typeof(DateTime))
        //        sb.AppendLine($"{prefix}{prop.Name} = {prop.GetValue(obj)}");
        //    else
        //        sb.Append($"{prefix}{prop.Name} =\n{prop.GetValue(obj).ToStringProperty(prefix + "\t")}");
        //}
        //return sb.ToString();

        //******************

        //if (obj == null)
        //    return $"{prefix}null";

        //Type type = obj.GetType();
        //var sb = new StringBuilder();

        //foreach (FieldInfo field in type.GetFields())
        //{
        //    object? value = field.GetValue(obj);
        //    sb.AppendLine($"{prefix}{field.Name} = {value}");
        //}

        //return sb.ToString();
        if (obj == null)
            return $"{prefix}null";

        Type type = obj.GetType();
        var sb = new StringBuilder();

        foreach (FieldInfo field in type.GetFields())
        {
            object? value = field.GetValue(obj);
            if (value is System.Collections.IEnumerable enumerable && !(value is string))
            {
                sb.AppendLine($"{prefix}{field.Name} = [");
                foreach (var item in enumerable)
                {
                    sb.AppendLine($"{prefix}  {item?.ToString()}");
                }
                sb.AppendLine($"{prefix}]");
            }
            else
            {
                sb.AppendLine($"{prefix}{field.Name} = {value}");
            }
        }

        return sb.ToString();
    }

    public static DO.Customer Convert(this BO.Customer customer)
    {
        return new DO.Customer(customer.Id, customer.Name, customer.Address, customer.PhoneNumber);
    }
    public static BO.Customer Convert(this DO.Customer customer)
    {
        return new BO.Customer(customer.Id, customer.Name, customer.Address, customer.PhoneNumber);
    }
    public static DO.Product Convert(this BO.Product product)
    {
        return new DO.Product(product.ProductId, product.ProductName, (DO.Category?)product.category, product.Price, product.QuantityInStock);
    }
    public static BO.Product Convert(this DO.Product product)
    {
        return new BO.Product(product.ProductId, product.ProductName, (BO.Category?)product.category, product.Price, product.QuantityInStock);
    }
    public static DO.Sale Convert(this BO.Sale sale) 
    { 
        return new DO.Sale(sale.SaleId,sale.ProductId,sale.MinAmount,sale.SalePrice,sale.ClubSale,sale.StartSale,sale.FinishSale);
    }
    public static BO.Sale Convert(this DO.Sale sale)
    {
        return new BO.Sale(sale.SaleId, sale.ProductId, sale.MinAmount, (double)sale.SalePrice, sale.ClubSale, sale.StartSale, sale.FinishSale);
    }

    public static DO.Product ChangeQuantity(this DO.Product product, int quantity) => new DO.Product { ProductId = product.ProductId, ProductName = product.ProductName, QuantityInStock =product.QuantityInStock-quantity, Price = product.Price, category = product.category };
}
