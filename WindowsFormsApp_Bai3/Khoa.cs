using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp_Bai3
{
    public partial class Khoa : Form
    {
        // 1. Cấu hình kết nối (Dùng chung cho toàn bài)
        string strCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        SqlConnection sqlCon = null;

        public Khoa()
        {
            InitializeComponent();
        }

        // Hàm mở kết nối
        private void MoKetNoi()
        {
            if (sqlCon == null) sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        }

        // Hàm nạp dữ liệu lên bảng
        private void LoadData()
        {
            try
            {
                MoKetNoi();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Khoa", sqlCon); 
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvKhoa.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            finally { sqlCon.Close(); }
        }

        // Sự kiện khi mở Form
        private void Khoa_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // --- NÚT THÊM ---
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MoKetNoi();
                // Bảng Khoa chỉ có 2 cột: MaKhoa, TenKhoa 
                SqlCommand cmd = new SqlCommand("INSERT INTO Khoa VALUES(@ma, @ten)", sqlCon);
                 cmd.Parameters.AddWithValue("@ma", txtMaKhoa.Text); 
                 cmd.Parameters.AddWithValue("@ten", txtTenKhoa.Text); 

                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm khoa thành công!");
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thêm: " + ex.Message); }
            finally { sqlCon.Close(); }
        }

        // --- NÚT SỬA ---
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                MoKetNoi();
                // Sửa tên khoa dựa theo mã khoa
                SqlCommand cmd = new SqlCommand("UPDATE Khoa SET TenKhoa=@ten WHERE MaKhoa=@ma", sqlCon);
                cmd.Parameters.AddWithValue("@ma", txtMaKhoa.Text);
                cmd.Parameters.AddWithValue("@ten", txtTenKhoa.Text);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Sửa thành công!");
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi sửa: " + ex.Message); }
            finally { sqlCon.Close(); }
        }

        // --- NÚT XÓA ---
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa khoa này có thể làm mất dữ liệu Lớp và Sinh viên liên quan. Bạn chắc chứ?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    MoKetNoi();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Khoa WHERE MaKhoa=@ma", sqlCon);
                    cmd.Parameters.AddWithValue("@ma", txtMaKhoa.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa khoa!");
                    LoadData();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa: " + ex.Message); }
                finally { sqlCon.Close(); }
            }
        }

        // --- NÚT LÀM MỚI ---
        private void button5_Click(object sender, EventArgs e)
        {
            txtMaKhoa.Text = "";
            txtTenKhoa.Text = "";
            LoadData();
        }

        // --- CLICK VÀO BẢNG HIỆN LÊN Ô NHẬP ---
        private void dgvKhoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhoa.Rows[e.RowIndex];
                txtMaKhoa.Text = row.Cells[0].Value.ToString();
                txtTenKhoa.Text = row.Cells[1].Value.ToString();
            }
        }
    }
}