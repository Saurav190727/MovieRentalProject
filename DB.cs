using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MoviesRentalsProjeect
{
    public class DB
    {
        //connection string
        public string ConnectionString = @"Data Source=LAPTOP-6F1IP0CI\SQLEXPRESS01; Initial Catalog=VBMOVIESFULLDATA.MDF;Integrated Security=True";
        public SqlConnection Connection;

        public DB()
        {
            Connection = new SqlConnection(ConnectionString);
        }
           // load customers data
        public DataTable LoadCustomers()
        {
            //connection open 
            Connection.Open();

            DataTable CustomersTable = new DataTable();

            CustomersTable.Clear();

            CustomersTable.Columns.Add("CustID");
            CustomersTable.Columns.Add("FirstName");
            CustomersTable.Columns.Add("LastName");
            CustomersTable.Columns.Add("Address");
            CustomersTable.Columns.Add("Phone");
            //query for data selection from customer table
            string query = "SELECT * FROM Customer";

            SqlCommand command = new SqlCommand(query, Connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                CustomersTable.Rows.Add(
                    reader["CustID"],
                    reader["FirstName"],
                    reader["LastName"],
                    reader["Address"],
                    reader["Phone"]
                    );
            }
            //close connection
            Connection.Close();

            return CustomersTable;
        }
        // load movies data
        public DataTable LoadMovies()
        {
            //open connection
            Connection.Open();

            DataTable MoviesTable = new DataTable();

            MoviesTable.Clear();

            MoviesTable.Columns.Add("MovieID");
            MoviesTable.Columns.Add("Rating");
            MoviesTable.Columns.Add("Title");
            MoviesTable.Columns.Add("Year");
            MoviesTable.Columns.Add("Rental_Cost");
            MoviesTable.Columns.Add("Copies");
            MoviesTable.Columns.Add("Plot");
            MoviesTable.Columns.Add("Genre");
            //query for selection Movies
            string query = "SELECT * FROM Movies";

            SqlCommand command = new SqlCommand(query, Connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                MoviesTable.Rows.Add(
                    reader["MovieID"],
                    reader["Rating"],
                    reader["Title"],
                    reader["Year"],
                    reader["Rental_Cost"],
                    reader["Copies"],
                    reader["Plot"],
                    reader["Genre"]
                    );
            }
            // close connection
            Connection.Close();

            return MoviesTable;
        }
        //Add Customers
        public void AddCustomer(string fname, string lname, string phone, string address)
        {
            //Connection Open
            Connection.Open();
            //Query for inserting Customers
            string query = "INSERT INTO Customer (FirstName, LastName, Phone, Address) " +
                    "VALUES(@FirstName, @LastName, @Phone, @Address);";

            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = fname;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lname;
                command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = phone;
                command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = address;

                command.ExecuteNonQuery();
            }
            //Connection Close
            Connection.Close();
        }
        // Update Customer
        public void UpdateCustomer(string id, string fname, string lname, string phone, string address)
        {
            //Connection open
            Connection.Open();
            //Query for updating customer
            string query = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Address = @Address WHERE CustID = " + id + "; ";

            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = fname;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lname;
                command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = phone;
                command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = address;

                command.ExecuteNonQuery();
            }
            //Connection Close
            Connection.Close();
        }
        //Delete Customer
        public void DeleteCustomer(string id)
        {
            //connection Open
            Connection.Open();
            // Query for delete Customer
            string query = "DELETE Customer WHERE CustID = " + id;

            SqlCommand command = new SqlCommand(query, Connection);

            command.ExecuteNonQuery();
            //Connection Close
            Connection.Close();
        }
        //Add Issue Movies
        public void IssueMovie(string movieIDFK, string custIDFK)
        {
            //Connection Open
            Connection.Open();
            //Add query for adding issue movies
            string query = "INSERT INTO RentedMovies (MovieIDFK, CustIDFK, DateRented)" +
                    "VALUES(@MovieIDFK, @CustIDFK, @DateRented)";

            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                command.Parameters.AddWithValue("@MovieIDFK", movieIDFK);
                command.Parameters.AddWithValue("@CustIDFK", custIDFK);
                command.Parameters.AddWithValue("@DateRented", DateTime.Now);

                command.ExecuteNonQuery();
            }
            //Connection close
            Connection.Close();
        }
        //Show Return movies
        public void ReturnMovie(string rmID)
        {
         //Connection open
            Connection.Open();
            //Add query for updating Return Movies
            string query = "UPDATE RentedMovies set DateReturned=@DateReturned Where RMID = @RMID";

            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                command.Parameters.Add("@RMID", SqlDbType.NVarChar).Value = rmID;
                command.Parameters.Add("@DateReturned", SqlDbType.DateTime).Value = DateTime.Now;

                command.ExecuteNonQuery();
            }
            //Connection Close
            Connection.Close();
        }
        //Load Rented Movies
        public DataTable LoadRentedMovies()
        {
            //Connection open
            Connection.Open();

            DataTable CustomersTable = new DataTable();

            CustomersTable.Clear();

            CustomersTable.Columns.Add("RMID");
            CustomersTable.Columns.Add("MovieIDFK");
            CustomersTable.Columns.Add("CustIDFK");
            CustomersTable.Columns.Add("DateRented");
            CustomersTable.Columns.Add("DateReturned");
            //Add Query for selection of rented movies table
            string query = "SELECT * FROM RentedMovies";

            SqlCommand command = new SqlCommand(query, Connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                CustomersTable.Rows.Add(
                    reader["RMID"],
                    reader["MovieIDFK"],
                    reader["CustIDFK"],
                    reader["DateRented"],
                    reader["DateReturned"]
                    );
            }
            //Connection close
            Connection.Close();

            return CustomersTable;
        }
        //Show popular customer
        public string PopularCustomer()
        {
            //Connection open
            Connection.Open();
            //Add query for show popular customer
            string query = "SELECT CustIDFK, COUNT(*) AS Rep FROM RentedMovies GROUP BY CustIDFK ORDER BY Rep DESC";

            SqlCommand command = new SqlCommand(query, Connection);

            var result = command.ExecuteScalar().ToString();
            //Connection Close
            Connection.Close();
            //Connection Open
            Connection.Open();

            Console.WriteLine(result);

            query = "SELECT FirstName, LastName FROM Customer WHERE CustID = " + result;

            command = new SqlCommand(query, Connection);

            SqlDataReader reader = command.ExecuteReader();

            string output = "";

            while (reader.Read())
            {
                output = reader["FirstName"].ToString();
                output += " ";
                output += reader["LastName"].ToString();
            }
            //Connection Close
            Connection.Close();

            return output;
        }
        //show popular movie
        public string PopularMovie()
        {
            //Connection open
            Connection.Open();
            //Add query to show popular movies
            string query = "SELECT MovieIDFK, COUNT(*) AS Rep FROM RentedMovies GROUP BY MovieIDFK ORDER BY Rep DESC";

            SqlCommand command = new SqlCommand(query, Connection);

            var result = command.ExecuteScalar().ToString();
            //Connection close
            Connection.Close();

            return result;
        }
        //Add Connection state
        public ConnectionState DBStatus()
        {
            return Connection.State;
        }
        // Add Movies
        public void AddMovies(string Title, int Year, decimal Rental_Cost, string Copies, string Rating, string Plot, string Genre)
        {
            //Connection Open
            Connection.Open();
            //Add query for add movies
            string query = "INSERT INTO Movies (Title, Year, Rental_Cost, Copies, Rating, Plot, Genre) " +
                    "VALUES(@Title, @Year, @Rental_Cost, @Copies, @Rating, @Plot,@Genre);";

            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Title;
                command.Parameters.Add("@Year", SqlDbType.Int).Value = Year;
                command.Parameters.Add("@Rental_Cost", SqlDbType.Decimal).Value = Rental_Cost;
                command.Parameters.Add("@Rating", SqlDbType.Int).Value = Rating;
                command.Parameters.Add("@Plot", SqlDbType.NVarChar).Value = Plot;

                command.Parameters.Add("@Copies", SqlDbType.NVarChar).Value = Copies;
                command.Parameters.Add("@Genre", SqlDbType.NVarChar).Value = Genre;


                command.ExecuteNonQuery();
            }
            //connection close
            Connection.Close();
            //Update Movies
        }
        public void UpdateMovies(string id, string Title, string Rating, int Year, decimal Rental_Cost, string Copies, string Plot, string Genre)
        {
            //Connection open
            Connection.Open();
            //Add query for updating movies
            string query = "UPDATE Movies SET Title = @Title, Rating = @Rating, Year = @Year, Rental_Cost = @Rental_Cost, Copies = @Copies, Plot=@Plot, Genre=@Genre WHERE MovieID = " + id + "; ";

            using (SqlCommand command = new SqlCommand(query, Connection))
            {
                command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Title;
                command.Parameters.Add("@Rating", SqlDbType.NVarChar).Value = Rating;
                command.Parameters.Add("@Year", SqlDbType.Int).Value = Year;
                command.Parameters.Add("@Rental_Cost", SqlDbType.Decimal).Value = Rental_Cost;
                command.Parameters.Add("@Copies", SqlDbType.NVarChar).Value = Copies;
                command.Parameters.Add("@Plot", SqlDbType.NVarChar).Value = Plot;
                command.Parameters.Add("@Genre", SqlDbType.NVarChar).Value = Genre;

                command.ExecuteNonQuery();
            }
            //Connection close
            Connection.Close();
        }
        // Delete Movies
        public void DeleteMovie(string id)
        {
            //Connection open
            Connection.Open();
            //Add query for delete movies
            string query = "DELETE Movies WHERE MovieID = " + id;

            SqlCommand command = new SqlCommand(query, Connection);

            command.ExecuteNonQuery();
            //Connection close
            Connection.Close();
        }

    }
}
    

