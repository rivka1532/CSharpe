
using DO;

namespace Dal
{
    public static class DataSource
    {
        internal static List<Customer?> Customers = new List<Customer?>();
        internal static List<Product?> Products = new List<Product?>();
        internal static List<Sale?> Sales = new List<Sale?>();

        internal class Config
        {
            internal const int NextSaleCode = 1000;
            internal const int NextProductCode = 100;

            private static int SaleIndex = NextSaleCode;
            private static int ProductIndex = NextProductCode;
            public static int ProductCode { 
                get {
                    return ProductIndex++; 
                } 
            }

            public static int SaleCode
            {
                get
                {
                    return SaleIndex+=10;
                }
            }
        }
    }
}
