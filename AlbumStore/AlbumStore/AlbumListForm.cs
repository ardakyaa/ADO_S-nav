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

    public partial class AlbumListForm : Form
    {
        AlbumRepo albumRepo;
        public AlbumListForm()
        {
            InitializeComponent();
            albumRepo = new AlbumRepo();
        }

        private void AlbumListForm_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            var albums = albumRepo.Get();
            dataGridView1.DataSource = albums;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            var album = (dataGridView1.DataSource as List<Album>)[e.RowIndex];
            AlbumForm form = new AlbumForm();
            form.Tag = album.AlbumId;
            form.ShowDialog();
            FillGrid();
        }
    }
}
