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

    public partial class OrderForm : Form
    {
        AlbumRepo albumRepo;
        UserRepo userRepo;
        OrderRepo orderRepo;
        OrderDetailRepo orderDetailRepo;
        public OrderForm()
        {
            InitializeComponent();

            albumRepo = new AlbumRepo();
            userRepo = new UserRepo();
            orderRepo = new OrderRepo();
            orderDetailRepo = new OrderDetailRepo();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            FillControls();
            FillData();
        }

        private void FillControls()
        {
            FillEmployees();
            FillAlbums();
        }

        private void FillAlbums()
        {
            var albums = albumRepo.Get();
            cmbAlbums.DataSource = albums;
            cmbAlbums.DisplayMember = "AlbumName";
            cmbAlbums.ValueMember = "AlbumId";
            cmbAlbums.SelectedIndex = -1;
        }

        private void FillEmployees()
        {
            var employees = userRepo.Get();
            cmbEmployees.DataSource = employees;
            cmbEmployees.DisplayMember = "NameAndSurname";
            cmbEmployees.ValueMember = "UserId";
            cmbEmployees.SelectedIndex = -1;
        }

        Order selectedOrder;
        private void FillData()
        {
            try
            {
                int orderId = Convert.ToInt32(this.Tag);
                if (orderId > 0)
                {
                    var order = orderRepo.GetById(orderId);
                    if (order != null)
                    {
                        selectedOrder = order;

                        cmbEmployees.SelectedValue = order.EmployeeId;
                        nuDiscount.Value = Convert.ToDecimal(order.Discount);
                        nuQuantity.Value = Convert.ToDecimal(order.Quantity);
                        dtOrderDate.Value = order.OrderDate.Value;
                        txtCountry.Text = order.Country;
                        txtCity.Text = order.City;
                        txtCounty.Text = order.County;
                        txtAddress.Text = order.Address;
                    }
                }
                FillGridOrderDetail(orderId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void FillGridOrderDetail(int orderId)
        {
            if (orderId>0)
            {

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormSave();
            this.Close();
        }

        private void FormSave()
        {
            if (this.selectedOrder == null)
            {
                this.selectedOrder = new Order();
            }
            this.selectedOrder.EmployeeId = Convert.ToInt32(cmbEmployees.SelectedValue);
            this.selectedOrder.Discount = Convert.ToInt16(nuDiscount.Value);
            this.selectedOrder.Quantity = Convert.ToInt16(nuQuantity.Value);
            this.selectedOrder.OrderDate = dtOrderDate.Value;
            this.selectedOrder.Country = txtCountry.Text;
            this.selectedOrder.City = txtCity.Text;
            this.selectedOrder.County = txtCounty.Text;
            this.selectedOrder.Address = txtAddress.Text;


            if (Convert.ToInt32(this.Tag) == 0)
            {
                //insert işlemi
                this.selectedOrder.OrderId = orderRepo.Create(this.selectedOrder);
                this.Tag = this.selectedOrder.OrderId;
            }
            else
            {
                //update işlemi
                orderRepo.Update(this.selectedOrder);
            }
            MessageBox.Show("Kayıt Başarılı");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Tag) == 0)
            {
                MessageBox.Show("Seçili sipariş yoktur");
            }
            else
            {
                DialogResult result = MessageBox.Show("Seçili siparişi silmek istiyor musunuz?", "Albüm Ekranı", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
            orderRepo.Delete(selectedOrder);
        }

    }
}
