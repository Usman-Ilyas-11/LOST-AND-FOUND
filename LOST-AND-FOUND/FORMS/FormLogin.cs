using System;
using System.Windows.Forms;
using LostAndFound;

namespace LOST_AND_FOUND
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (UserRepository.ValidateLogin(txtUser.Text.Trim(), txtPass.Text.Trim(), out User user))
            {
                if (user.Role == "Admin")
                {
                    var admin = new FormAdminDashboard(user);
                    admin.Show();
                    this.Hide();
                }
                else
                {
                    var studentForm = new FormStudentDashboard(user);
                    studentForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
