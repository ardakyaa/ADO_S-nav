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
    public partial class OrderListForm : Form
    {
        OrderRepo orderRepo;
        public OrderListForm()
        {
            InitializeComponent();

            orderRepo = new OrderRepo();
        }

        private void OrderListForm_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            var orders = orderRepo.Get();
            dataGridView1.DataSource = orders;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            var order = (dataGridView1.DataSource as List<Order>)[e.RowIndex];
            OrderForm form = new OrderForm();
            form.Tag = order.OrderId;
            form.ShowDialog();
            FillGrid();
        }
    }
}
