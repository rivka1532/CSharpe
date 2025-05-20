using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal;

internal class ProductImplementation : IProduct
{
    static string filePath = "../xml/products.xml";
    static XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
    static List<Product> products = new List<Product>();

    public int Create(Product item)
    {
        //    static string file_path = "../xml/customers.xml";
        //    static XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>));
        //    static List<Customer> customers = new List<Customer>();
        //public int Create(Customer item)
        //{
        //    //List<Customer> customers = new List<Customer>();
        //    try
        //    {
        //        customers = DO.Tools.loadDataFromXmlFile(customers, file_path, serializer);
        //        Customer existsCustomer = customers.FirstOrDefault(c => c.Id == item.Id);
        //        if (existsCustomer == null)
        //            customers.Add(item);
        //        DO.Tools.saveDataToXmlFile(customers, file_path, serializer);
        //        return item.Id;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        try
        {
            item = item with { ProductId = Config.NextProductCode };
            products = DO.Tools.loadDataFromXmlFile(products, filePath, serializer);

            Product existProduct = products.FirstOrDefault(p => p.ProductName.Equals(item.ProductName));
            if (existProduct != null)
            {
                throw new DO.DalExceptionIdAllreadyExist("Product already exist");
            }

            products.Add(item);
            DO.Tools.saveDataToXmlFile(products, filePath, serializer);
            return item.ProductId;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public void Delete(int id)
    {
        try
        {
            products = DO.Tools.loadDataFromXmlFile(products, filePath, serializer);

            Product existProduct = products.FirstOrDefault(p => p.ProductId == id);
            if (existProduct == null)
            {
                throw new DO.DalExceptionIdNotExist("Product not exist");
            }
            products.Remove(existProduct);
            DO.Tools.saveDataToXmlFile(products, filePath, serializer);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public Product? Read(int id)
    {
        try
        {
            products = DO.Tools.loadDataFromXmlFile(products, filePath, serializer);
            Product existProduct = products.FirstOrDefault(p => p.ProductId == id);
            if (existProduct != null)
            {
                return existProduct;
            }
            else
            {
                throw new DO.DalExceptionIdNotExist("Product not exist");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Product? Read(Func<Product, bool> filter)
    {
        try
        {
            products = DO.Tools.loadDataFromXmlFile(products, filePath, serializer);
            Product existProduct = products.FirstOrDefault(filter);
            if (existProduct != null)
            {
                return existProduct;
            }
            else
            {
                throw new DO.DalExceptionIdNotExist("Product not exist");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<Product?> ReadAll(Func<Product, bool>? filter = null)
    {
        try
        {
            products = DO.Tools.loadDataFromXmlFile(products, filePath, serializer);

            if (filter == null)
                return products;
            var filterList = from p in products
                             where filter(p)
                             select p;

            return filterList.ToList();
        }
        catch (Exception ex)
        {
            throw ex;
        }   
    }

    public void Update(Product item)
    {
        if (item == null)
        {
            throw new DalExceptionNullReference("item is null");
        }
        try
        {
            products = DO.Tools.loadDataFromXmlFile(products, filePath, serializer);
            Product existProduct = products.FirstOrDefault(p => p.ProductName.Equals(item.ProductName));
            if (existProduct == null)
            {
                throw new DO.DalExceptionIdNotExist("Product not exist");
            }
            products.Remove(existProduct);
            products.Add(item);
            //Delete(item.ProductId);
            //Create(item);
            DO.Tools.saveDataToXmlFile(products, filePath, serializer);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
