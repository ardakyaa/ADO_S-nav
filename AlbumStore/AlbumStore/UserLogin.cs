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

    public partial class UserLogin : Form
    {
        UserRepo userRepo;

        public UserLogin()
        {
            InitializeComponent();

            userRepo = new UserRepo();
        }

        private void UserLogin_Load(object sender, EventArgs e)
        {

        }

        bool move;
        int mouse_x;
        int mouse_y;

        private void UserLogin_MouseUp_1(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void UserLogin_MouseDown_1(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void UserLogin_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            try
            {
                var users = userRepo.Get();

                foreach (var item in users)
                {
                    if (txtUserName.Text!=item.Email && mtxtPassword.Text!=item.UserPassword)
                    {
                        MessageBox.Show("Kullanıcı adı veya şifreniz hatalıdır.", "Kullanıcı Girişi", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        FormClear();
                    }
                    else
                    {
                        MainForm form = new MainForm();
                        form.ShowDialog();
                        FormClear();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormClear()
        {
            txtUserName.Text = "";
            mtxtPassword.Text = "";
        }

        private void cbShowPassword_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cbShowPassword.CheckState == CheckState.Checked)
            {
                mtxtPassword.PasswordChar = char.Parse("\0");
            }
            else if (cbShowPassword.CheckState == CheckState.Unchecked)
            {
                mtxtPassword.PasswordChar = char.Parse("*");
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            UserSignUp form = new UserSignUp();
            form.ShowDialog();
        }


    }
}
