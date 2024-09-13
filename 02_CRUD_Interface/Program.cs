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
        string name = "Olena";
        private SqlConnection conn;
        private string connectionString;
        public SportShopDb(string connectionString)
        {     
            this.connectionString = connectionString;
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
                              VALUES (@name, 
                                      @type, 
                                      @Quantity, 
                                      @Price, 
                                      @Producer, 
                                      @CostPrice)";

            SqlCommand command = new SqlCommand(cmdText, conn);
            command.Parameters.AddWithValue("name", product.Name);
            command.Parameters.AddWithValue("type", product.Type);
            command.Parameters.AddWithValue("Quantity", product.Quantity);
            command.Parameters.AddWithValue("Price", product.Price);
            command.Parameters.AddWithValue("Producer", product.Producer);
            command.Parameters.AddWithValue("CostPrice", product.CostPrice);
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
            return this.GetProductsByQuery(reader);
        }
        public List<Product> GetByName(string name)
        {
           
            //Stanga';drop table Hack;--
            string cmdText = $@"select * from Products where Name = @name";

            SqlCommand command = new SqlCommand(cmdText, conn);
            //command.Parameters.Add("name",System.Data.SqlDbType.NVarChar).Value = name;
            SqlParameter parameter = new SqlParameter()
            {
                ParameterName = "name",
                SqlDbType = System.Data.SqlDbType.NVarChar,
                Value = name
            };
            command.Parameters.Add(parameter);
            SqlDataReader reader = command.ExecuteReader();
            return this.GetProductsByQuery(reader);
        }
        public Product GetById(int id)
        {
            string cmdText = $@"select * from Products where Id = {id}";

            SqlCommand command = new SqlCommand(cmdText, conn);

            SqlDataReader reader = command.ExecuteReader();

            return this.GetProductsByQuery(reader).FirstOrDefault()!;

        }
        private List<Product> GetProductsByQuery(SqlDataReader reader)
        {

            List<Product> products = new List<Product>();
            ////// відображаємо всі значення кожного рядка
            while (reader.Read())
            {
                products.Add(new Product()
                {
                    Id = (int)reader[0],
                    Name = (string)reader[1],
                    Type = (string)reader[2],
                    Quantity = (int)reader[3],
                    CostPrice = (int)reader[4],
                    Producer = (string)reader[5],
                    Price = (int)reader[6]
                });
            }

            reader.Close();
            return products;

        }
        public void Update(Product product)
        {
            string cmdText = $@"UPDATE Products
                              SET Name =@name, 
                                  TypeProduct =@type, 
                                  Quantity =@Quantity, 
                                  CostPrice =@CostPrice, 
                                  Producer =@Producer, 
                                  Price =@Price
                                  where Id = {product.Id}";

            SqlCommand command = new SqlCommand(cmdText, conn);
            command.Parameters.AddWithValue("name", product.Name);
            command.Parameters.AddWithValue("type", product.Type);
            command.Parameters.AddWithValue("Quantity", product.Quantity);
            command.Parameters.AddWithValue("Price", product.Price);
            command.Parameters.AddWithValue("Producer", product.Producer);
            command.Parameters.AddWithValue("CostPrice", product.CostPrice);
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
