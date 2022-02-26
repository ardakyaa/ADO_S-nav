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
    public class AlbumGenreRepo : RepositoryBase, IRepository<AlbumGenre>
    {
        public int Create(AlbumGenre item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_AlbumGenre_Create_Update", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@GenreName", item.GenreName);

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

        public int Delete(AlbumGenre item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_AlbumGenre_Delete", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@genreId", item.GenreId);
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

        public List<AlbumGenre> Get()
        {
            List<AlbumGenre> albumGenres = new List<AlbumGenre>();
            try
            {
                SqlCommand command = new SqlCommand();
                SqlDataReader reader = GetReader(command, "Sp_AlbumGenre", this.Connection);
                while (reader.Read())
                {
                    var genre = Mapping(reader);
                    albumGenres.Add(genre);
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
            return albumGenres;
        }

        private SqlDataReader GetReader(SqlCommand command, string procedure, SqlConnection con)
        {
            command = new SqlCommand(procedure, con);
            command.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed) con.Open();
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }

        public AlbumGenre GetById(int id)
        {
            AlbumGenre albumGenre = null;
            try
            {
                SqlCommand command = new SqlCommand("Sp_AlbumGenre", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@GenreId", id);
                if (this.Connection.State == ConnectionState.Closed) this.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    albumGenre = Mapping(reader);
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
            return albumGenre;
        }

        public AlbumGenre Mapping(SqlDataReader reader)
        {
            AlbumGenre albumGenre = new AlbumGenre();
            albumGenre.GenreId = Convert.ToInt32(reader["GenreId"]);
            albumGenre.GenreName = reader["GenreName"].ToString();
            return albumGenre;
        }

        public int Update(AlbumGenre item)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_AlbumGenre_Create_Update", this.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@GenreId", item.GenreId);
                command.Parameters.AddWithValue("@GenreName", item.GenreName);

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
