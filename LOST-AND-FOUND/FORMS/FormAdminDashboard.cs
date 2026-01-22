using System;
using System.Windows.Forms;
using LostAndFound;
using System.Data.SQLite;

namespace LOST_AND_FOUND
{
    public partial class FormAdminDashboard : Form
    {
        private User AdminUser;

        public FormAdminDashboard(User admin)
        {
            AdminUser = admin;
            InitializeComponent();
            this.Text = $"Admin Dashboard - {AdminUser.Username}";

            // Events
            btnAddStudent.Click += BtnAddStudent_Click;
            btnViewLost.Click += BtnViewLost_Click;
            btnViewFound.Click += BtnViewFound_Click;
            btnMatch.Click += BtnOpenMatchForm_Click;
            btnLogout.Click += BtnLogout_Click;
            btnSubmitStudent.Click += BtnSubmitStudent_Click;
            btnCancelStudent.Click += BtnCancelStudent_Click;

            RefreshCounts();
        }

        // ===================== STUDENT MANAGEMENT =====================
        private void BtnAddStudent_Click(object sender, EventArgs e) => pnlAddStudent.Visible = true;

        private void BtnSubmitStudent_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string studentID = txtStudentID.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(studentID))
            {
                MessageBox.Show("All fields are required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ok = UserRepository.AddStudent(username, password, studentID);
            MessageBox.Show(ok ? "Student created successfully." : "Failed to create student (maybe username exists).");

            if (ok)
            {
                pnlAddStudent.Visible = false;
                txtUsername.Clear();
                txtPassword.Clear();
                txtStudentID.Clear();
                RefreshCounts();
            }
        }

        private void BtnCancelStudent_Click(object sender, EventArgs e)
        {
            pnlAddStudent.Visible = false;
            txtUsername.Clear();
            txtPassword.Clear();
            txtStudentID.Clear();
        }

        // ===================== VIEW LOST / FOUND ITEMS =====================
        private void BtnViewLost_Click(object sender, EventArgs e) => new FormViewLost().ShowDialog();
        private void BtnViewFound_Click(object sender, EventArgs e) => new FormViewFound().ShowDialog();

        // ===================== MANUAL MATCH =====================
        private void BtnOpenMatchForm_Click(object sender, EventArgs e)
        {
            using (var form = new FormViewMatched())
            {
                form.ShowDialog();
                RefreshCounts();
            }
        }

        // ===================== LOGOUT =====================
        private void BtnLogout_Click(object sender, EventArgs e) => Application.Exit();

        // ===================== REFRESH ITEM COUNTS =====================
        private void RefreshCounts()
        {
            if (lblLostCount != null) lblLostCount.Text = LostItemRepository.GetAll().Count.ToString();
            if (lblFoundCount != null) lblFoundCount.Text = FoundItemRepository.GetAll().Count.ToString();

            if (lblMatchedCount != null)
            {
                int matchedCount = 0;
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM MatchedItems;", conn))
                        matchedCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                lblMatchedCount.Text = matchedCount.ToString();
            }
        }
    }
}
