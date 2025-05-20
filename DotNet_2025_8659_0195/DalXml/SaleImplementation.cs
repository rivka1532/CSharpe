using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using DalApi;
using DO;

namespace Dal;

internal class SaleImplementation : ISale
{
    static string filePath = @"../xml/sales.xml";
    static XElement saleXml = XElement.Load(filePath);

    private const string SALE = "sale";
    private const string SALE_ID = "SaleId";
    private const string PRODUCT_ID = "ProductId";
    private const string MIN_AMOUNT = "MinAmount";
    private const string SALE_PRICE = "SalePrice";
    private const string CLUB_SALE = "ClubSale";
    private const string START_SALE = "StartSale";
    private const string FINISH_SALE = "FinishSale";



    public int Create(Sale item)
    {
        try
        {
            if (File.Exists(filePath))
            {
                item = item with { SaleId = Config.NextSaleCode };
                saleXml.Add(new XElement(SALE,
                    new XElement(SALE_ID, item.SaleId),
                    new XElement(PRODUCT_ID, item.ProductId),
                    new XElement(MIN_AMOUNT, item.MinAmount),
                    new XElement(SALE_PRICE, item.SalePrice),
                    new XElement(CLUB_SALE, item.ClubSale),
                    new XElement(START_SALE, item.StartSale),
                    new XElement(FINISH_SALE, item.FinishSale)));
                saleXml.Save(filePath);
            }
            return item.SaleId;
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
            if (!File.Exists(filePath))
            {
                if (Read(id) != null)
                {
                    saleXml.Elements().Single(s => s.Element(SALE_ID).Value.Equals(id)).Remove();
                }
                saleXml.Save(filePath);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Sale? Read(int id)
    {
        try
        {
            if (File.Exists(filePath))
            {
                XElement saleFound = saleXml.Descendants(SALE).Single(s => s.Element(SALE_ID).Value == id.ToString());
                if (saleFound != null)
                {
                    return new Sale((int)saleFound.Element(SALE_ID),
                        (int)saleFound.Element(PRODUCT_ID),
                        (int)saleFound.Element(MIN_AMOUNT),
                        (int)saleFound.Element(SALE_PRICE),
                        (bool)saleFound.Element(CLUB_SALE),
                        (DateTime)saleFound.Element(START_SALE),
                        (DateTime)saleFound.Element(FINISH_SALE));
                }
                else
                {
                    throw new DO.DalExceptionIdNotExist("Id not exist");
                }
            }
            return null;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public Sale? Read(Func<Sale, bool> filter)
    {
        try
        {
            if (File.Exists(filePath))
            {
                List<Sale> listFoundSales = saleXml.Elements(SALE).Select(saleFound =>
                new Sale((int)saleFound.Element(SALE_ID),
                        (int)saleFound.Element(PRODUCT_ID),
                        (int)saleFound.Element(MIN_AMOUNT),
                        (int)saleFound.Element(SALE_PRICE),
                        (bool)saleFound.Element(CLUB_SALE),
                        (DateTime)saleFound.Element(START_SALE),
                        (DateTime)saleFound.Element(FINISH_SALE))).ToList();

                return listFoundSales.FirstOrDefault(filter);
            }
            return null;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public List<Sale?> ReadAll(Func<Sale, bool>? filter = null)
    {
        try
        {
            if (File.Exists(filePath))
            {
                List<Sale> listFoundSales = saleXml.Elements(SALE).Select(saleFound =>
                new Sale((int)saleFound.Element(SALE_ID),
                        (int)saleFound.Element(PRODUCT_ID),
                        (int)saleFound.Element(MIN_AMOUNT),
                        (int)saleFound.Element(SALE_PRICE),
                        (bool)saleFound.Element(CLUB_SALE),
                        (DateTime)saleFound.Element(START_SALE),
                        (DateTime)saleFound.Element(FINISH_SALE))).ToList();
                if (filter != null)
                {
                    return (from s in listFoundSales
                            where filter(s)
                            select s).ToList();
                }
                else
                {
                    return listFoundSales;
                }
            }
            return null;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public void Update(Sale item)
    {
        try
        {
            if (File.Exists(filePath))
            {
                XElement sale = saleXml.Elements().SingleOrDefault(s => s.Element(SALE_ID).Value == item.SaleId.ToString());
                if (sale != null)
                {
                    sale.Element(PRODUCT_ID).SetValue(item.ProductId);
                    sale.Element(MIN_AMOUNT).SetValue(item.MinAmount);
                    sale.Element(SALE_PRICE).SetValue(item.SalePrice);
                    sale.Element(CLUB_SALE).SetValue(item.ClubSale);
                    sale.Element(START_SALE).SetValue(item.StartSale);
                    sale.Element(FINISH_SALE).SetValue(item.FinishSale);
                    saleXml.Save(filePath);
                }
                else
                {
                    throw new DO.DalExceptionIdNotExist("Id not exist");
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
