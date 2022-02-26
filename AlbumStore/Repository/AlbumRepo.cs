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
    public class AlbumRepo : RepositoryBase, IRepository<Album>
    {
        public int Create(Album item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Album_Create_Update", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AlbumName", item.AlbumName);
                command.Parameters.AddWithValue("@SingerId", item.SingerId);
                command.Parameters.AddWithValue("@GenreId", item.GenreId);
                command.Parameters.AddWithValue("@Price", item.Price);
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

        public int Delete(Album item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Album_Delete", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AlbumId", item.AlbumId);
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

        public List<Album> Get()
        {
            List<Album> albums = new List<Album>();
            try
            {
                SqlCommand command = new SqlCommand();
                SqlDataReader reader = GetReader(command, "Sp_Albums", this.Connection);
                while (reader.Read())
                {
                    var album = Mapping(reader);
                    albums.Add(album);
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
            return albums;
        }

        private SqlDataReader GetReader(SqlCommand command, string procedure, SqlConnection con)
        {
            command = new SqlCommand(procedure, con);
            command.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed) con.Open();
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }

        public Album GetById(int id)
        {
            Album album = null;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Albums", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AlbumId", id);
                if (this.Connection.State == ConnectionState.Closed) this.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    album = Mapping(reader);
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
            return album;
        }

        public Album Mapping(SqlDataReader reader)
        {
            Album album = new Album();
            album.AlbumId = Convert.ToInt32(reader["AlbumId"]);
            album.AlbumName = reader["AlbumName"].ToString();
            album.SingerId = Convert.ToInt32(reader["SingerId"]);
            album.GenreId = Convert.ToInt32(reader["GenreId"]);
            album.Price = Convert.ToDecimal(reader["Price"]);
            album.Discount = Convert.ToInt16(reader["Discount"]);
            return album;
        }

        public int Update(Album item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Album_Create_Update", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AlbumId", item.AlbumId);
                command.Parameters.AddWithValue("@AlbumName", item.AlbumName);
                command.Parameters.AddWithValue("@SingerId", item.SingerId);
                command.Parameters.AddWithValue("@GenreId", item.GenreId);
                command.Parameters.AddWithValue("@Price", item.Price);
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
