using AlbumStore.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbumStore.Repository
{
    public class OrderRepo : RepositoryBase, IRepository<Order>
    {
        public int Create(Order item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Order_Create_Update", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmployeeId", item.EmployeeId);
                command.Parameters.AddWithValue("@OrderDate", item.OrderDate);
                command.Parameters.AddWithValue("@Country", item.Country);
                command.Parameters.AddWithValue("@City", item.City);
                command.Parameters.AddWithValue("@County", item.County);
                command.Parameters.AddWithValue("@Address", item.Address);
                command.Parameters.AddWithValue("@Quantity", item.Quantity);
                command.Parameters.AddWithValue("@Discount", item.Discount);

                if (this.Connection.State == ConnectionState.Closed) this.Connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (this.Connection.State == ConnectionState.Open) this.Connection.Close();
            }
            return id;
        }

        public int Delete(Order item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Order_Delete", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderId", item.OrderId);
                if (this.Connection.State == ConnectionState.Closed) this.Connection.Open();
                command.ExecuteNonQuery();
                id = Convert.ToInt32(command.ExecuteNonQuery());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (this.Connection.State == ConnectionState.Open) this.Connection.Close();
            }
            return id;
        }

        public List<Order> Get()
        {
            List<Order> orders = new List<Order>();
            try
            {
                SqlCommand command = new SqlCommand();
                SqlDataReader reader = GetReader(command, "Sp_Orders", this.Connection);
                while (reader.Read())
                {
                    var order = Mapping(reader);
                    orders.Add(order);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (this.Connection.State == ConnectionState.Open) this.Connection.Close();
            }
            return orders;
        }

        private SqlDataReader GetReader(SqlCommand command, string procedure, SqlConnection con)
        {
            command = new SqlCommand(procedure, con);
            command.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed) con.Open();
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }

        public Order GetById(int id)
        {
            Order order = null;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Orders", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderId", id);
                if (this.Connection.State == ConnectionState.Closed) this.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    order = Mapping(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (this.Connection.State == ConnectionState.Open) this.Connection.Close();
            }
            return order;
        }

        public Order Mapping(SqlDataReader reader)
        {
            Order order = new Order();
            order.OrderId = Convert.ToInt32(reader["OrderId"]);
            order.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
            order.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
            order.Country = reader["Country"].ToString();
            order.City = reader["City"].ToString();
            order.County = reader["County"].ToString();
            order.Quantity = Convert.ToInt16(reader["Quantity"]);
            order.Discount = Convert.ToInt16(reader["Discount"]);
            return order;
        }

        public int Update(Order item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Order_Create_Update", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@orderId", item.OrderId);
                command.Parameters.AddWithValue("@EmployeeId", item.EmployeeId);
                command.Parameters.AddWithValue("@OrderDate", item.OrderDate);
                command.Parameters.AddWithValue("@Country", item.Country);
                command.Parameters.AddWithValue("@City", item.City);
                command.Parameters.AddWithValue("@County", item.County);
                command.Parameters.AddWithValue("@Address", item.Address);
                command.Parameters.AddWithValue("@Quantity", item.Quantity);
                command.Parameters.AddWithValue("@Discount", item.Discount);

                if (this.Connection.State == ConnectionState.Closed) this.Connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (this.Connection.State == ConnectionState.Open) this.Connection.Close();
            }
            return id;
        }
    }
}
