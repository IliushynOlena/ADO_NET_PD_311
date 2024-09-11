using System.Data.SqlClient;
using System.Text;

namespace _02_CRUD_Interface
{
    public class SportShopDb
    {
        //CRUD Interface
        //[C]reate
        //[R]ead
        //[U]pdate
        //[D]elete
        private SqlConnection conn;
        private string connectionString = @"Data Source=DESKTOP-1LCG8OH\SQLEXPRESS;
                                        Initial Catalog=SportShop;
                                        Integrated Security=True;
                                        Connect Timeout=2;
                                        Encrypt=False;";
        public SportShopDb()
        {           
            conn = new SqlConnection(connectionString);
            conn.Open();    
        }
        ~SportShopDb()
        {
            conn.Close();   
        }
        public void Create(Product product)
        {
            string cmdText = $@"INSERT INTO Products
                              VALUES ('{product.Name}', 
                                      '{product.Type}', 
                                       {product.Quantity}, 
                                       {product.Price}, 
                                      '{product.Producer}', 
                                       {product.CostPrice})";

            SqlCommand command = new SqlCommand(cmdText, conn);
            command.CommandTimeout = 5; // default - 30sec
            
            //// ExecuteNonQuery - виконує команду яка не повертає результат 
            ///(insert, update, delete...),
            ////але метод повертає кількітсь рядків, які були задіяні
            int rows = command.ExecuteNonQuery();

            Console.WriteLine(rows + " rows affected!");
        }
        public List<Product> GetAll()
        {
            string cmdText = @"select * from Products";

            SqlCommand command = new SqlCommand(cmdText, conn);

            SqlDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();   
          
            ////// відображаємо всі значення кожного рядка
            while (reader.Read())
            {
               products.Add(new Product()
               {
                   Id =(int) reader[0],
                   Name =(string) reader[1],    
                   Type =(string) reader[2],
                   Quantity =(int) reader[3],
                   CostPrice =(int) reader[4],  
                   Producer = (string) reader[5],   
                   Price =(int) reader[6]
               });
            }

            reader.Close();
            return products;

        }
        public Product GetById(int id)
        {
            #region Execute Reader
            string cmdText = $@"select * from Products where Id = {id}";

            SqlCommand command = new SqlCommand(cmdText, conn);

            SqlDataReader reader = command.ExecuteReader();

            Product product = new Product();

            while (reader.Read())
            {

                product.Id = (int)reader[0];
                product.Name = (string)reader[1];
                product.Type = (string)reader[2];
                product.Quantity = (int)reader[3];
                product.CostPrice = (int)reader[4];
                product.Producer = (string)reader[5];
                product.Price = (int)reader[6];

            }
            reader.Close();
            return product;
            #endregion

        }
        public void Update(Product product)
        {
            string cmdText = $@"UPDATE Products
                              SET Name ='{product.Name}', 
                                  TypeProduct ='{product.Type}', 
                                  Quantity ={product.Quantity}, 
                                  CostPrice ={product.CostPrice}, 
                                  Producer ='{product.Producer}', 
                                  Price ={product.Price}
                                  where Id = {product.Id}";

            SqlCommand command = new SqlCommand(cmdText, conn);
            command.CommandTimeout = 5; // default - 30sec

            command.ExecuteNonQuery();

        }
        public void Delete(int id)
        {
            string cmdText = $@"delete Products where Id = {id}";
            SqlCommand command = new SqlCommand(cmdText, conn);
            command.ExecuteNonQuery();
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
           
            SportShopDb db = new SportShopDb();
           
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
            var products =  db.GetAll();
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
            db.Delete(39);

            Product forUpdate = db.GetById(1);
            forUpdate.Price = 2000;
            forUpdate.CostPrice = 5000;

            db.Update(forUpdate);





        }
    }
}
