using System;
using System.Data;
using System.Data.SqlClient; // Bắt buộc cho SQL
using System.Drawing;
using System.IO;             // Bắt buộc cho Xử lý ảnh
using System.Windows.Forms;

namespace WindowsFormsApp_Bai3
{
    public partial class Sinhvien : Form
    {
        // ==========================================
        // 1. CẤU HÌNH KẾT NỐI (Chỉ cần sửa tên Database ở đây)
        // ==========================================
        string strCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        SqlConnection sqlCon = null;

        public Sinhvien()
        {
            InitializeComponent();
        }

        // ==========================================
        // 2. CÁC HÀM DÙNG CHUNG (Để gọi lại nhiều lần)
        // ==========================================

        // Hàm mở kết nối CSDL (Gọi trước mỗi lần Thêm/Sửa/Xóa/Hiển thị)
        private void MoKetNoi()
        {
            if (sqlCon == null) sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        }

        // Hàm lấy dữ liệu lên bảng (Gọi khi mở Form hoặc sau khi Thêm/Sửa/Xóa)
        private void LoadData()
        {
            try
            {
                MoKetNoi();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM SinhVien", sqlCon);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvSinhVien.DataSource = dt; // Đổ dữ liệu vào DataGridView
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            finally { sqlCon.Close(); }
        }

        // Hàm chuyển ảnh thành Byte để lưu vào CSDL
        private byte[] ImageToByteArray(PictureBox pb)
        {
            if (pb.Image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                pb.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        // ==========================================
        // 3. XỬ LÝ CÁC NÚT BẤM (Nhấp đúp vào nút trên giao diện rồi dán code)
        // ==========================================

        // SỰ KIỆN: Nhấp đúp vào Form (Khi vừa mở phần mềm lên)
        private void Sinhvien_Load(object sender, EventArgs e)
        {
            LoadData(); // Tự động load bảng
        }

        // NÚT THÊM
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MoKetNoi();
                SqlCommand cmd = new SqlCommand("INSERT INTO SinhVien VALUES(@ma, @ten, @ngay, @anh, @malop)", sqlCon);

                cmd.Parameters.AddWithValue("@ma", txtMaSV.Text);
                cmd.Parameters.AddWithValue("@ten", txtHoTen.Text);
                cmd.Parameters.AddWithValue("@ngay", dtpNgaySinh.Value);
                cmd.Parameters.AddWithValue("@anh", ImageToByteArray(picAnhSV));
                cmd.Parameters.AddWithValue("@malop", txtMaLop.Text);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Thêm thành công!");
                    LoadData(); // Load lại bảng
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thêm: " + ex.Message); }
            finally { sqlCon.Close(); }
        }

        // NÚT SỬA (Sửa theo Mã Sinh Viên)
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                MoKetNoi();
                SqlCommand cmd = new SqlCommand("UPDATE SinhVien SET HoTen=@ten, NgaySinh=@ngay, AnhSV=@anh, MaLop=@malop WHERE MaSV=@ma", sqlCon);

                cmd.Parameters.AddWithValue("@ma", txtMaSV.Text);
                cmd.Parameters.AddWithValue("@ten", txtHoTen.Text);
                cmd.Parameters.AddWithValue("@ngay", dtpNgaySinh.Value);
                cmd.Parameters.AddWithValue("@anh", ImageToByteArray(picAnhSV));
                cmd.Parameters.AddWithValue("@malop", txtMaLop.Text);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Sửa thành công!");
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi sửa: " + ex.Message); }
            finally { sqlCon.Close(); }
        }

        // NÚT XÓA (Chỉ cần Mã Sinh Viên)
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    MoKetNoi();
                    SqlCommand cmd = new SqlCommand("DELETE FROM SinhVien WHERE MaSV=@ma", sqlCon);
                    cmd.Parameters.AddWithValue("@ma", txtMaSV.Text);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Đã xóa!");
                        LoadData();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa: " + ex.Message); }
                finally { sqlCon.Close(); }
            }
        }

        // NÚT CHỌN ẢNH
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                picAnhSV.Image = Image.FromFile(open.FileName); // Hiển thị ảnh lên ô PictureBox
            }
        }

        // NÚT LÀM MỚI (Xóa trắng các ô để nhập người mới)
        private void button5_Click(object sender, EventArgs e)
        {
            txtMaSV.Text = "";
            txtHoTen.Text = "";
            txtMaLop.Text = "";
            picAnhSV.Image = null;
            dtpNgaySinh.Value = DateTime.Now;
            LoadData();
        }

        // ==========================================
        // 4. SỰ KIỆN CLICK VÀO BẢNG ĐỂ HIỆN LÊN Ô NHẬP (CellClick)
        // ==========================================
        // Trên giao diện -> Chọn dgvSinhVien -> Bảng Properties (biểu tượng tia sét) -> Nhấp đúp CellClick
        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

                // Lấy Text
                txtMaSV.Text = row.Cells[0].Value.ToString();
                txtHoTen.Text = row.Cells[1].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells[2].Value);
                txtMaLop.Text = row.Cells[4].Value.ToString();

                // Lấy Ảnh
                if (row.Cells[3].Value != DBNull.Value)
                {
                    byte[] imgData = (byte[])row.Cells[3].Value;
                    using (MemoryStream ms = new MemoryStream(imgData))
                    {
                        picAnhSV.Image = Image.FromStream(ms);
                    }
                }
                else { picAnhSV.Image = null; }
            }
        }

        private void Sinhvien_Load_1(object sender, EventArgs e)
        {

        }
    }
}