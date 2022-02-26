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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void yeniAlbümToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlbumForm form = new AlbumForm();
            form.ShowDialog();
        }

        private void albümListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlbumListForm form = new AlbumListForm();
            form.ShowDialog();
        }

        private void yeniSanatçıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SingerForm form = new SingerForm();
            form.ShowDialog();
        }

        private void sanatçıListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SingerListForm form = new SingerListForm();
            form.ShowDialog();
        }

        private void yeniSiparişToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderForm form = new OrderForm();
            form.ShowDialog();
        }

        private void siparişListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderListForm form = new OrderListForm();
            form.ShowDialog();
        }
    }
}
