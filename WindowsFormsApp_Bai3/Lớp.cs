using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp_Bai3
{
    public partial class Lop : Form
    {
        // 1. CHUỖI KẾT NỐI (Luôn dùng chung một kiểu để không bị lẫn)
        string strCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        SqlConnection sqlCon = null;

        public Lop() { InitializeComponent(); }

        // HÀM: Mở kết nối an toàn
        private void MoKetNoi()
        {
            if (sqlCon == null) sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        }

        // HÀM: Nạp dữ liệu lên bảng (GridView)
        private void LoadData()
        {
            try
            {
                MoKetNoi();
                SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM Lop", sqlCon);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                dgvLop.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi hiển thị: " + ex.Message); }
            finally { sqlCon.Close(); }
        }

        // HÀM: Đổ danh sách Khoa vào ComboBox (Để tránh lỗi khóa ngoại)
        private void LoadComboKhoa()
        {
            try
            {
                MoKetNoi();
                SqlDataAdapter ad = new SqlDataAdapter("SELECT MaKhoa FROM Khoa", sqlCon);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                cboMaKhoa.DataSource = dt;
                cboMaKhoa.DisplayMember = "MaKhoa"; // Hiển thị mã khoa
                cboMaKhoa.ValueMember = "MaKhoa";
                cboMaKhoa.SelectedIndex = -1;       // Để trống lúc đầu
            }
            catch { /* Bỏ qua nếu chưa có dữ liệu khoa */ }
            finally { sqlCon.Close(); }
        }

        // SỰ KIỆN: Khi mở Form Lớp lên
        private void Lop_Load(object sender, EventArgs e)
        {
            LoadData();      // Hiện bảng lớp
            LoadComboKhoa(); // Hiện danh sách khoa trong ô chọn
        }

        // NÚT: THÊM
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MoKetNoi();
                // Lệnh SQL: Thêm vào 4 cột tương ứng
                string sql = "INSERT INTO Lop VALUES(@ma, @ten, @khoa, @makhoa)";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.Parameters.AddWithValue("@ma", txtMaLop.Text);
                cmd.Parameters.AddWithValue("@ten", txtTenLop.Text);
                cmd.Parameters.AddWithValue("@khoa", txtKhoaHoc.Text);
                cmd.Parameters.AddWithValue("@makhoa", cboMaKhoa.Text); // Lấy giá trị từ ComboBox

                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm lớp thành công!");
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: Kiểm tra xem đã chọn Mã khoa chưa?\n" + ex.Message); }
            finally { sqlCon.Close(); }
        }

        // NÚT: SỬA
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                MoKetNoi();
                string sql = "UPDATE Lop SET TenLop=@ten, KhoaHoc=@khoa, MaKhoa=@makhoa WHERE MaLop=@ma";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.Parameters.AddWithValue("@ma", txtMaLop.Text);
                cmd.Parameters.AddWithValue("@ten", txtTenLop.Text);
                cmd.Parameters.AddWithValue("@khoa", txtKhoaHoc.Text);
                cmd.Parameters.AddWithValue("@makhoa", cboMaKhoa.Text);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Sửa thành công!");
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi sửa: " + ex.Message); }
            finally { sqlCon.Close(); }
        }

        // NÚT: XÓA
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa lớp sẽ mất sinh viên trong lớp. Tiếp tục?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    MoKetNoi();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Lop WHERE MaLop=@ma", sqlCon);
                    cmd.Parameters.AddWithValue("@ma", txtMaLop.Text);
                    cmd.ExecuteNonQuery();
                    LoadData();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa: " + ex.Message); }
                finally { sqlCon.Close(); }
            }
        }

        // NÚT: LÀM MỚI (Reset ô nhập)
        private void button5_Click(object sender, EventArgs e)
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtKhoaHoc.Clear();
            cboMaKhoa.SelectedIndex = -1;
            LoadData();
        }

        // SỰ KIỆN: Click vào bảng hiện dữ liệu lên ô nhập
        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = dgvLop.Rows[e.RowIndex];
                txtMaLop.Text = r.Cells[0].Value.ToString();
                txtTenLop.Text = r.Cells[1].Value.ToString();
                txtKhoaHoc.Text = r.Cells[2].Value.ToString();
                cboMaKhoa.Text = r.Cells[3].Value.ToString();
            }
        }

        private void Lop_Load_1(object sender, EventArgs e)
        {

        }
    }
}