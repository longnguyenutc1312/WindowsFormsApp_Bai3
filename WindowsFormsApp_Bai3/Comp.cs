using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Ports; // Thư viện bắt buộc để dùng cổng COM
using System.Windows.Forms;

namespace WindowsFormsApp_Bai3
{
    public partial class FormComPort : Form
    {
        // 1. CẤU HÌNH KẾT NỐI (Dùng chung cấu trúc của toàn bài)
        string strCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        SqlConnection sqlCon = null;

        public FormComPort()
        {
            InitializeComponent();
        }

        // Hàm mở kết nối CSDL an toàn
        private void MoKetNoi()
        {
            if (sqlCon == null) sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
        }

        // SỰ KIỆN: Khi Form vừa mở lên
        private void FormComPort_Load(object sender, EventArgs e)
        {
            // Tự động quét các cổng COM đang có trên máy tính
            string[] ports = SerialPort.GetPortNames();
            cboComPort.Items.AddRange(ports);

            if (ports.Length > 0) cboComPort.SelectedIndex = 0; // Chọn cổng đầu tiên nếu có
        }

        // NÚT: KẾT NỐI / NGẮT KẾT NỐI CỔNG COM
        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1 == null || !serialPort1.IsOpen)
                {
                    // Khởi tạo cổng với các thông số mặc định (Baudrate 9600)
                    serialPort1 = new SerialPort(cboComPort.Text, 9600, Parity.None, 8, StopBits.One);

                    // Đăng ký sự kiện: Khi có dữ liệu gửi đến cổng COM
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                    serialPort1.Open();
                    btnKetNoi.Text = "Ngắt kết nối";
                    btnKetNoi.BackColor = Color.LightCoral;
                    lblTrangThai.Text = "Trạng thái: Đang kết nối cổng " + cboComPort.Text;
                }
                else
                {
                    serialPort1.Close();
                    btnKetNoi.Text = "Kết nối";
                    btnKetNoi.BackColor = Color.LightGreen;
                    lblTrangThai.Text = "Trạng thái: Đã ngắt kết nối";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối cổng COM: " + ex.Message);
            }
        }

        // HÀM XỬ LÝ KHI NHẬN DỮ LIỆU (Chạy ngầm)
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // Đọc mã sinh viên gửi về (giả sử thiết bị gửi kèm ký tự xuống dòng)
                string maSV = serialPort1.ReadLine().Trim();

                // Vì sự kiện này chạy ở luồng phụ (Thread), phải dùng Invoke để hiển thị lên UI
                this.Invoke(new MethodInvoker(delegate {
                    txtMaNhanDuoc.Text = maSV;
                    HienThiThongTin(maSV); // Gọi hàm truy vấn CSDL
                }));
            }
            catch { /* Tránh treo app khi dữ liệu lỗi */ }
        }

        // HÀM TRUY VẤN: Lấy thông tin từ mã sinh viên nhận được
        private void HienThiThongTin(string maSV)
        {
            try
            {
                MoKetNoi();
                // Câu lệnh SQL liên kết bảng để lấy đủ thông tin lớp
                string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.AnhSV, l.TenLop, l.KhoaHoc 
                               FROM SinhVien sv 
                               INNER JOIN Lop l ON sv.MaLop = l.MaLop 
                               WHERE sv.MaSV = @ma";

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.Parameters.AddWithValue("@ma", maSV);

                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                dgvKetQua.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    lblThongBao.Text = "Đã tìm thấy sinh viên: " + maSV;
                    lblThongBao.ForeColor = Color.Blue;
                }
                else
                {
                    lblThongBao.Text = "Mã " + maSV + " không có trong hệ thống!";
                    lblThongBao.ForeColor = Color.Red;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi CSDL: " + ex.Message); }
            finally { sqlCon.Close(); }
        }

        // Đảm bảo đóng cổng COM khi tắt Form để không bị lỗi treo cổng
        private void FormComPort_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1 != null && serialPort1.IsOpen) serialPort1.Close();
        }
    }
}