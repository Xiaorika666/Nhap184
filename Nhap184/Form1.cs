using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace Nhap184
{ 

    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        DataTable tblSach;
        private void Form1_Load(object sender, EventArgs e)
        {
            txtMasach.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            Load_DataGridView();
            DAO.FillCombo("select Manhasanxuat,Tennhasanxuat from Nhasanxuat",cboNhasanxuat,"Manhasanxuat","Tennhasanxuat");
            cboNhasanxuat.SelectedIndex = -1;
            ResetValues();

            // Hello

        }
        private void Load_DataGridView()
        {
            string sql;
            sql = "select Masach,Tensach,Soluong,Gia from Sach";
            tblSach = DAO.GetDataToTable(sql);
            dataGridView1.DataSource = tblSach;
            dataGridView1.Columns[0].HeaderText = "ma sach";
            dataGridView1.Columns[1].HeaderText = "ten sach ";
            dataGridView1.Columns[2].HeaderText = "so luong ";
            dataGridView1.Columns[3].HeaderText = "gia";
            //dataGridView1.Columns[4].HeaderText = " ma nha san xuat ";
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 110;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 130;
            //dataGridView1.Columns[4].Width = 120;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void DataGridView_Click(object sender, EventArgs e)
        {
            if(btnThem.Enabled ==false)
            {
                MessageBox.Show(" dang o che do them moi", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMasach.Focus();
                return;
            }
            if(tblSach.Rows.Count==0)
            {
                MessageBox.Show("khong co du lieu ", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMasach.Text = dataGridView1.CurrentRow.Cells["Masach"].Value.ToString();
            txtTensach.Text = dataGridView1.CurrentRow.Cells["Tensach"].Value.ToString();
            txtSoluong.Text = dataGridView1.CurrentRow.Cells["Soluong"].Value.ToString();
            txtGia.Text = dataGridView1.CurrentRow.Cells["Gia"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMasach.Enabled = true;
            txtMasach.Focus();
              
        }
        private void ResetValues()
        {
            txtMasach.Text = "";
            txtTensach.Text = "";
            txtSoluong.Text = "0";
            txtGia.Text = "0";
            cboNhasanxuat.Text = "";

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblSach.Rows.Count == 0)
            {
                MessageBox.Show("khong con du lieu ", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMasach.Text == "")
            {
                MessageBox.Show("ban chua chon ban ghi nao", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTensach.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap ten sach ", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTensach.Focus();
                return;
            }
            if (txtSoluong.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap so luong  ", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoluong.Focus();
                return;
            }
            if (txtGia.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap gia  ", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGia.Focus();
                return;
            }
            if (cboNhasanxuat.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai chon Nhasanxuat ", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhasanxuat.Focus();
                return;
            }
            sql = " update Sach  set Tensach=N'" + txtTensach.Text.Trim().ToString() + "',Manhasanxuat=N'"+cboNhasanxuat.SelectedValue.ToString()+"'where Masach=N'" + txtMasach.Text + "'";
            DAO.RunSql(sql);
            Load_DataGridView();
            ResetValues();
            btnHuy.Enabled = false;
            
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
                string sql;
                if(tblSach.Rows.Count==0)
                {
                    MessageBox.Show("khong con du lieu", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if(txtMasach.Text=="")
                {
                    MessageBox.Show("banj chuwa chon ban ghi nao ", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("ban co muon xoa khong ?", "thog bao", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) 
                {
                    sql = "Delete Sach where Masach=N'" + txtMasach.Text + "'";
                    DAO.RunSql(sql);
                    Load_DataGridView();
                    ResetValues();
                }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if(txtMasach.Text .Trim().Length==0)
            {
                MessageBox.Show("ban phai nhap ma sach ", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMasach.Focus();
                return;
            }
            if(txtTensach.Text.Trim().Length==0)
            {
                MessageBox.Show("ban phai nhap ten chat lieu", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTensach.Focus();
                return;

            }
            if (txtSoluong.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap so luong", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoluong.Focus();
                return;


            }
            if (txtGia.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap gia", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGia.Focus();
                return;

            }
            if (cboNhasanxuat.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai chon ma nha san xuat", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhasanxuat.Focus();
                return;

            }
            sql = "select Masach from Sach where Masach=N'" + txtMasach.Text.Trim() + "'";
            if(DAO.CheckKey(sql))
            {
                MessageBox.Show("ma sach nay da co, ban hay nhap ma khac", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMasach.Focus();
                txtMasach.Text = "";
                return;
            }
            sql = "insert into Sach(Masach,Tensach,Manhasanxuat,Soluong,Gia) values(N'" + txtMasach.Text + "',N'" + txtTensach.Text + "', N'"+cboNhasanxuat.Text+"',N'" + txtSoluong.Text + "',N'" + txtGia.Text + "')";
            DAO.RunSql(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtMasach.Enabled = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
                ResetValues();
                btnHuy.Enabled = false;
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnLuu.Enabled = false;
                txtMasach.Enabled = false;

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
                this.Close();
        }
    }
}
