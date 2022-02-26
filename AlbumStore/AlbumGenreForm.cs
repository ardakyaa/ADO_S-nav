using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbumStore
{
    using Entities;
    using Repository;

    public partial class AlbumGenreForm : Form
    {
        AlbumGenreRepo albumGenreRepo;
        AlbumRepo albumRepo;

        public AlbumGenreForm()
        {
            InitializeComponent();
            albumRepo = new AlbumRepo();
            albumGenreRepo = new AlbumGenreRepo();
        }

        AlbumGenre albumGenre;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.albumGenre == null)
            {
                this.albumGenre = new AlbumGenre();
            }

            this.albumGenre.GenreName = txtGenreName.Text;


            if (Convert.ToInt32(this.Tag) == 0)
            {
                this.albumGenre.GenreId = albumGenreRepo.Create(this.albumGenre);
                this.Tag = this.albumGenre.GenreId;
            }
            else
            {
                albumGenreRepo.Update(this.albumGenre);
            }
        }

    }
}
