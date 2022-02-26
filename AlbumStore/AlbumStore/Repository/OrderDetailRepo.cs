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
    public class OrderDetailRepo : RepositoryBase
    {
        public OrderDetail Add(int orderId)
        {
            OrderDetail orderDetail = null;
            try
            {
                SqlCommand command = new SqlCommand("Sp_OrderDetails", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderId", orderId);
                if (this.Connection.State == ConnectionState.Closed) this.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    orderDetail = Mapping(reader);
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
            return orderDetail;
        }

        private OrderDetail Mapping(SqlDataReader reader)
        {
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.OrderId = Convert.ToInt32(reader["OrderId"]);
            orderDetail.AlbumId = Convert.ToInt32(reader["AlbumId"]);
            orderDetail.TotalPrice = Convert.ToDecimal(reader["TotalPrice"]);
            return orderDetail;
        }
    }
}
