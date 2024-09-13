using System.Text;

namespace _02_CRUD_Interface
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            string connctionString = @"Data Source=DESKTOP-1LCG8OH\SQLEXPRESS;
                                        Initial Catalog=SportShop;
                                        Integrated Security=True;
                                        Connect Timeout=2;
                                        Encrypt=False;";
            SportShopDb db = new SportShopDb(connctionString);
           
        


            Product pr = new Product()
            {
                Name = "Stanga",
                Type = "Equipment",
                Quantity = 33,
                Price = 3333,
                Producer = "China",
                CostPrice = 4444
            };
            // db.Create(pr);
            //var products =  db.GetAll();
            Console.WriteLine("Enter name of product to search : ");
            string name = Console.ReadLine()!;
            var products =  db.GetByName(name);
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
            db.Delete(39);

            Product forUpdate = db.GetById(3);
            forUpdate.Price += 2000;
            forUpdate.CostPrice += 5000;

            db.Update(forUpdate);





        }
    }
}
