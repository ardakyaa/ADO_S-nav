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

    public partial class SingerForm : Form
    {
        SingerRepo singerRepo;
        AlbumRepo albumRepo;

        public SingerForm()
        {
            InitializeComponent();

            singerRepo = new SingerRepo();
            albumRepo = new AlbumRepo();
        }

        Singer selectedSinger = null;
        private void SingerForm_Load(object sender, EventArgs e)
        {
            try
            {
                int singerId = Convert.ToInt32(this.Tag);
                if (singerId > 0)
                {
                    var singer = singerRepo.GetById(singerId);
                    if (singer != null)
                    {
                        selectedSinger = singer;

                        txtFirstName.Text = singer.FirstName;
                        txtLastName.Text = singer.LastName;
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
                if (this.selectedSinger == null)
                {
                    this.selectedSinger = new Singer();
                }

                this.selectedSinger.FirstName = txtFirstName.Text;
                this.selectedSinger.LastName = txtLastName.Text;

                if (Convert.ToInt32(this.Tag) == 0)
                {
                    this.selectedSinger.SingerId = singerRepo.Create(this.selectedSinger);
                    this.Tag = this.selectedSinger.SingerId;
                }
                else
                {
                    singerRepo.Update(this.selectedSinger);
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
                MessageBox.Show("Seçili şarkıcı yoktur");
            }
            else
            {
                DialogResult result = MessageBox.Show("Seçili şarkıcı silmek istiyor musunuz?", "Albüm Ekranı", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
            singerRepo.Delete(selectedSinger);
        }
    }
}
