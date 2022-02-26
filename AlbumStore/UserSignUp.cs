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

    public partial class UserSignUp : Form
    {
        UserRepo userRepo;

        public UserSignUp()
        {
            InitializeComponent();

            userRepo = new UserRepo();
        }

        private void UserSignUp_Load(object sender, EventArgs e)
        {

        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            FormSave();
            MessageBox.Show("Kayıt Başarılı");
        }
        User user=null;
        private void FormSave()
        {
            
            if (this.user==null)
            {
                this.user = new User();
            }

            this.user.FirstName = txtFirstName.Text;
            this.user.LastName = txtLastName.Text;
            this.user.Email = txtEmail.Text;
            this.user.Phone = txtPhone.Text;
            this.user.UserPassword = txtPassword.Text;

            if (Convert.ToInt32(this.Tag)==0)
            {
                this.user.UserId=userRepo.Create(this.user);
                this.Tag = this.user.UserId;
            }
            else
            {
                userRepo.Update(this.user);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
