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

    public partial class SingerListForm : Form
    {
        SingerRepo singerRepo;

        public SingerListForm()
        {
            InitializeComponent();

            singerRepo = new SingerRepo();
        }

        private void SingerListForm_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            var singers = singerRepo.Get();
            dataGridView1.DataSource = singers;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            var singer = (dataGridView1.DataSource as List<Singer>)[e.RowIndex];
            SingerForm form = new SingerForm();
            form.Tag = singer.SingerId;
            form.ShowDialog();
            FillGrid();
        }
    }
}
