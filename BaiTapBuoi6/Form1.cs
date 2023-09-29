using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapBuoi6
{
    public partial class frmDSNV : Form
    {
        public frmDSNV()
        {
            InitializeComponent();
        }

        QLNhanSu_DB context = new QLNhanSu_DB();

        private void frmDSNV_Load(object sender, EventArgs e)
        {
            try
            {
                // QuanLySinhVien_EF context = new QuanLySinhVien_EF();
                List<PhongBan> listKhoas = context.PhongBans.ToList(); //lấy các khoa
                List<NhanVien> listNhanVien = context.NhanViens.ToList(); //lấy sinh viên
                FillFalcultyCombobox(listKhoas);
                BindGrid(listNhanVien);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillFalcultyCombobox(List<PhongBan> listKhoas)
        {
            this.cmbKhoa.DataSource = listKhoas;
            this.cmbKhoa.DisplayMember = "Tên_phòng_ban";
            this.cmbKhoa.ValueMember = "Mã_phòng_ban";
        }
       
        private void BindGrid(List<NhanVien> listNhanVien)
        {
            dgvNhanVien.Rows.Clear();
            foreach (var item in listNhanVien)
            {
                int index = dgvNhanVien.Rows.Add();
                dgvNhanVien.Rows[index].Cells[0].Value = item.Mã_nhân_viên;
                dgvNhanVien.Rows[index].Cells[1].Value = item.Tên_nhân_viên;
                dgvNhanVien.Rows[index].Cells[2].Value = item.Ngày_sinh;
                dgvNhanVien.Rows[index].Cells[3].Value = item.PhongBan.Tên_phòng_ban;
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNhanVien.Rows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvNhanVien.SelectedRows[0];
                if (selectedRow.Cells[0].Value != null)
                {
                    txtMaNhanVien.Text = selectedRow.Cells[0].Value.ToString();
                    txtTenNhanVien.Text = selectedRow.Cells[1].Value.ToString();
                    dtpNgaySinh.Text = selectedRow.Cells[2].Value.ToString();
                    cmbKhoa.Text = selectedRow.Cells[3].Value.ToString();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn thoát????", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            { 
                    List<NhanVien> ds = context.NhanViens.ToList();
                        NhanVien a = new NhanVien();
                        a.Mã_nhân_viên = txtMaNhanVien.Text;
                        a.Tên_nhân_viên = txtTenNhanVien.Text;
                        a.Ngày_sinh = dtpNgaySinh.Value;
                        a.Mã_phòng_ban = cmbKhoa.SelectedValue.ToString();
                        context.NhanViens.Add(a);
                        context.SaveChanges();
                        MessageBox.Show("Thêm thành công", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmDSNV_Load(sender, e);
                    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool timNV(List<NhanVien> ds, string ms)
        {
            foreach (var i in ds)
                if (i.Mã_nhân_viên== ms)
                {
                    return true;
                    MessageBox.Show("Không tìm thấy sinh viên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            return false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            NhanVien a = context.NhanViens.FirstOrDefault(p => p.Mã_nhân_viên == txtMaNhanVien.Text);
            if (a != null)
            {
                DialogResult dr;
                a.Tên_nhân_viên = txtTenNhanVien.Text;
                a.Ngày_sinh = dtpNgaySinh.Value;
                a.Mã_phòng_ban = cmbKhoa.SelectedValue.ToString();

                dr = MessageBox.Show("Bạn có chắc chắc xóa ", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    context.NhanViens.Remove(a);
                    context.SaveChanges();
                    MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    frmDSNV_Load(sender, e);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                
                    List<NhanVien> ds = context.NhanViens.ToList();
                    if (timNV(ds, txtMaNhanVien.Text))
                    {
                        NhanVien a = context.NhanViens.FirstOrDefault(p => p.Mã_nhân_viên == txtMaNhanVien.Text);
                        if (a != null)
                        {

                            a.Tên_nhân_viên = txtTenNhanVien.Text;
                            a.Ngày_sinh = dtpNgaySinh.Value;
                         a.Mã_phòng_ban = cmbKhoa.SelectedValue.ToString();

                        context.SaveChanges();
                            MessageBox.Show("Sửa thành công", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmDSNV_Load(sender, e);
                        }
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
