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
    public class UserRepo : RepositoryBase, IRepository<User>
    {

        public int Create(User item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_User_Create_Update", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", item.FirstName);
                command.Parameters.AddWithValue("@LastName", item.LastName);
                command.Parameters.AddWithValue("@Email", item.Email);
                command.Parameters.AddWithValue("@Phone", item.Phone);
                command.Parameters.AddWithValue("@UserPassword", item.UserPassword);

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

        public int Delete(User item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_User_Delete", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", item.UserId);
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

        public List<User> Get()
        {
            List<User> users = new List<User>();
            try
            {
                SqlCommand command = new SqlCommand();
                SqlDataReader reader = GetReader(command, "Sp_Users", this.Connection);
                while (reader.Read())
                {
                    var user = Mapping(reader);
                    users.Add(user);
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
            return users;
        }

        public SqlDataReader GetReader(SqlCommand command, string procedure, SqlConnection con)
        {
            command = new SqlCommand(procedure, con);
            command.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed) con.Open();
            SqlDataReader reader = command.ExecuteReader();
            return reader;

        }

        public User GetById(int id)
        {
            User user = null;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Users", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", id);
                if (this.Connection.State == ConnectionState.Closed) this.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user = Mapping(reader);
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
            return user;
        }


        public User Mapping(SqlDataReader reader)
        {
            User user = new User();
            user.UserId = Convert.ToInt32(reader["UserId"]);
            user.FirstName = reader["FirstName"].ToString();
            user.LastName = reader["LastName"].ToString();
            user.Email = reader["Email"].ToString();
            user.Phone = reader["Phone"].ToString();
            return user;
        }

        public int Update(User item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_User_Create_Update", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", item.UserId);
                command.Parameters.AddWithValue("@FirstName", item.FirstName);
                command.Parameters.AddWithValue("@LastName", item.LastName);
                command.Parameters.AddWithValue("@Email", item.Email);
                command.Parameters.AddWithValue("@Phone", item.Phone);
                command.Parameters.AddWithValue("@UserPassword", item.UserPassword);

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
