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
    public class SingerRepo : RepositoryBase, IRepository<Singer>
    {
        public int Create(Singer item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Singer_Create_Update", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", item.FirstName);
                command.Parameters.AddWithValue("@LastName", item.LastName);

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

        public int Delete(Singer item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Singer_Delete", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SingerId", item.SingerId);
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

        public List<Singer> Get()
        {
            List<Singer> singers = new List<Singer>();
            try
            {
                SqlCommand command = new SqlCommand();
                SqlDataReader reader = GetReader(command, "Sp_Singers", this.Connection);
                while (reader.Read())
                {
                    var singer = Mapping(reader);
                    singers.Add(singer);
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
            return singers;
        }

        private SqlDataReader GetReader(SqlCommand command, string procedure, SqlConnection con)
        {
            command = new SqlCommand(procedure, con);
            command.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed) con.Open();
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }

        public Singer GetById(int id)
        {
            Singer singer = null;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Singers", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SingerId", id);
                if (this.Connection.State == ConnectionState.Closed) this.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    singer = Mapping(reader);
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
            return singer;
        }

        public Singer Mapping(SqlDataReader reader)
        {
            Singer singer = new Singer();
            singer.SingerId = Convert.ToInt32(reader["SingerId"]);
            singer.FirstName = reader["FirstName"].ToString();
            singer.LastName = reader["LastName"].ToString();
            return singer;
        }

        public int Update(Singer item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Singer_Create_Update", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SingerId", item.SingerId);
                command.Parameters.AddWithValue("@FirstName", item.FirstName);
                command.Parameters.AddWithValue("@LastName", item.LastName);

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
