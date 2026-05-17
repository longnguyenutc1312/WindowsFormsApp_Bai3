using System;
using System.Windows.Forms;

namespace WindowsFormsApp_Bai3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // ==========================================
        // CÔNG THỨC MỞ FORM: Ẩn Form chính -> Mở Form con -> Hiện lại khi đóng
        // ==========================================
        private void MoForm(Form fCon)
        {
            this.Hide();                // Ẩn Form1 (Menu chính)
            fCon.ShowDialog();          // Hiện Form con (Dạng hội thoại)
            this.Show();                // Khi Form con đóng, hiện lại Form1
        }

        // --- MỞ FORM KHOA ---
        private void menuKhoa_Click(object sender, EventArgs e)
        {
            Khoa f = new Khoa();        // Tạo mới Form Khoa
            MoForm(f);                  // Gọi hàm mở
        }

        // --- MỞ FORM LỚP ---
        private void menuLop_Click(object sender, EventArgs e)
        {
            Lop f = new Lop();          // Tạo mới Form Lớp
            MoForm(f);
        }

        // --- MỞ FORM SINH VIÊN ---
        private void menuSinhVien_Click(object sender, EventArgs e)
        {
            Sinhvien f = new Sinhvien(); // Tạo mới Form Sinh viên
            MoForm(f);
        }

        // --- THOÁT CHƯƠNG TRÌNH ---
        private void menuThoat_Click(object sender, EventArgs e)
        {
            // Hiển thị thông báo xác nhận trước khi thoát
            if (MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void sinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Timkiemsinhvien f = new Timkiemsinhvien(); // Tạo mới Form Tìm kiếm sinh viên
            MoForm(f);
        }
    }
}