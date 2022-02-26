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

    public partial class AlbumForm : Form
    {
        SingerRepo singerRepo;
        AlbumGenreRepo albumGenreRepo;
        AlbumRepo albumRepo;
        public AlbumForm()
        {
            InitializeComponent();

            singerRepo = new SingerRepo();
            albumRepo = new AlbumRepo();
            albumGenreRepo = new AlbumGenreRepo();
        }

        private void AlbumForm_Load(object sender, EventArgs e)
        {
            FillControls();
            FillData();
        }

        private void FillControls()
        {
            FillGenre();
            FillSinger();
        }

        private void FillSinger()
        {
            var singers = singerRepo.Get();
            cmbSingers.DataSource = singers;
            cmbSingers.DisplayMember = "NameAndSurname";
            cmbSingers.ValueMember = "SingerId";
            cmbSingers.SelectedIndex = -1;
        }

        private void FillGenre()
        {
            var genres = albumGenreRepo.Get();
            cmbGenres.DataSource = genres;
            cmbGenres.DisplayMember = "GenreName";
            cmbGenres.ValueMember = "GenreId";
            cmbGenres.SelectedIndex = -1;
        }

        private void btnAddGenre_Click(object sender, EventArgs e)
        {
            AlbumGenreForm form = new AlbumGenreForm();
            form.ShowDialog();

        }

        Album selectedAlbum = null;
        private void FillData()
        {
            try
            {
                int albumId = Convert.ToInt32(this.Tag);
                if (albumId>0)
                {
                    var album = albumRepo.GetById(albumId);
                    if (album!=null)
                    {
                        selectedAlbum = album;

                        txtAlbumName.Text = album.AlbumName;
                        cmbGenres.SelectedValue = album.GenreId;
                        cmbSingers.SelectedValue = album.SingerId;
                        nuPrice.Value = album.Price;
                        nuDiscount.Value = Convert.ToDecimal(album.Discount);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormSave();
            this.Close();
        }

        private void FormSave()
        {
            try
            {
                if (this.selectedAlbum == null)
                {
                    this.selectedAlbum = new Album();
                }
                this.selectedAlbum.AlbumName = txtAlbumName.Text;
                this.selectedAlbum.SingerId = Convert.ToInt32(cmbSingers.SelectedValue);
                this.selectedAlbum.GenreId = Convert.ToInt32(cmbGenres.SelectedValue);
                this.selectedAlbum.Price = nuPrice.Value;
                this.selectedAlbum.Discount = Convert.ToInt16(nuDiscount.Value);


                if (Convert.ToInt32(this.Tag) == 0)
                {
                    //insert işlemi
                    this.selectedAlbum.AlbumId = albumRepo.Create(this.selectedAlbum);
                    this.Tag = this.selectedAlbum.AlbumId;
                }
                else
                {
                    //update işlemi
                    albumRepo.Update(this.selectedAlbum);
                }
                MessageBox.Show("Kayıt başarılı");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Tag) == 0)
            {
                MessageBox.Show("Seçili albüm yoktur");
            }
            else
            {
                DialogResult result = MessageBox.Show("Seçili albümü silmek istiyor musunuz?", "Albüm Ekranı", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    FormDelete();
                    MessageBox.Show("Kayıt silme başarılı!");
                    this.Close();
                }
            }

        }

        private void FormDelete()
        {
            albumRepo.Delete(selectedAlbum);
        }
    }
}
