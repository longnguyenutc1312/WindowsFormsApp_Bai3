using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // Thư viện kết nối SQL
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_Bai3
{
    public partial class Timkiemsinhvien : Form
    {
        // ==========================================
        // BỔ SUNG MODULE: THIẾT LẬP KẾT NỐI
        // ==========================================
        string strCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        SqlConnection sqlCon = null;

        // Hàm mở kết nối an toàn
        private void MoKetNoi()
        {
            if (sqlCon == null) sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        }

        public Timkiemsinhvien()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                MoKetNoi(); // Bây giờ hàm này đã tồn tại trong Form này

                string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.AnhSV, l.TenLop, l.KhoaHoc 
                FROM SinhVien sv 
                INNER JOIN Lop l ON sv.MaLop = l.MaLop 
                WHERE sv.MaSV LIKE @tk OR sv.HoTen LIKE @tk";

                SqlCommand cmd = new SqlCommand(sql, sqlCon);

                // Truyền tham số tìm kiếm (Lưu ý: dùng @tk cho khớp với biến ở trên)
                string tuKhoa = "%" + txtTimKiem.Text + "%";
                cmd.Parameters.AddWithValue("@tk", tuKhoa);

                // 3. Đổ dữ liệu vào DataGridView
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                dgvSinhVien.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy sinh viên nào phù hợp!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }
        }
    }
}