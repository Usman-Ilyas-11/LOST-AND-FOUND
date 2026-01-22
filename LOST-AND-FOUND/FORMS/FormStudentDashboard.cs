using System;
using System.Windows.Forms;
using LostAndFound;

namespace LOST_AND_FOUND
{
    public partial class FormStudentDashboard : Form
    {
        private User CurrentUser;

        public FormStudentDashboard(User user)
        {
            CurrentUser = user;
            InitializeComponent(); // Load designer layout
            lblTitle.Text = $"STUDENT DASHBOARD - {user.Username}"; // Show username
        }

        // ===================== NAVBAR BUTTON LOGIC =====================
        private void BtnAddLost_Click(object sender, EventArgs e)
        {
            if (CurrentUser == null) return;
            var form = new FormAddLost(CurrentUser);
            form.ShowDialog();
        }

        private void BtnAddFound_Click(object sender, EventArgs e)
        {
            if (CurrentUser == null) return;
            var form = new FormAddFound(CurrentUser);
            form.ShowDialog();
        }

        private void BtnViewMatches_Click(object sender, EventArgs e)
        {
            ShowMyMatches();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Close(); // Close dashboard
        }

        // ===================== STUDENT MATCH LOGIC =====================
        private void ShowMyMatches()
        {
            if (CurrentUser == null || string.IsNullOrEmpty(CurrentUser.StudentId))
            {
                MessageBox.Show(
                    "No StudentId assigned to your account. Contact admin.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var form = new FormViewMatchedForStudent(CurrentUser.StudentId);
            form.ShowDialog();
        }
    }
}
