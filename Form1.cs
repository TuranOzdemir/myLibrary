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
namespace myLibrary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection("Data Source=DESKTOP-INQ1LMR\\SQLEXPRESS;Initial Catalog=Kutuphane;Integrated Security=True");

        private void showInfo()
        {
            connect.Open();
            SqlCommand komut = new SqlCommand("select *from kitaplar", connect);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem add = new ListViewItem();
                add.Text = oku["Id"].ToString();
                add.SubItems.Add(oku["kitapAd"].ToString());
                add.SubItems.Add(oku["yazar"].ToString());
                add.SubItems.Add(oku["yayınEvi"].ToString());
                add.SubItems.Add(oku["sayfa"].ToString());
                listView1.Items.Add(add);
            }
            connect.Close();
        }

        

        private void ekle_btn_Click(object sender, EventArgs e)
        {
            connect.Open();
            
            SqlCommand komut = new SqlCommand("insert into kitaplar (Id,kitapAd,yazar,yayınEvi,sayfa) values('" + sira_tbx.Text.ToString() + "','" + ad_tbx.Text.ToString() + "','" + yazar_tbx.Text.ToString() + "','" + yayin_tbx.Text.ToString() + "','" + sayfa_tbx.Text.ToString() + "')", connect);
            komut.ExecuteNonQuery();
            connect.Close();
            sira_tbx.Clear();
            ad_tbx.Clear();
            yazar_tbx.Clear();
            yayin_tbx.Clear();
            sayfa_tbx.Clear();
            listView1.Items.Clear();
            showInfo();
        }
      
        int id = 0;
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            sira_tbx.Text = listView1.SelectedItems[0].SubItems[0].Text;
            ad_tbx.Text = listView1.SelectedItems[0].SubItems[1].Text;
            yazar_tbx.Text = listView1.SelectedItems[0].SubItems[2].Text;
            yayin_tbx.Text = listView1.SelectedItems[0].SubItems[3].Text;
            sayfa_tbx.Text = listView1.SelectedItems[0].SubItems[4].Text;
        }

        private void sil_btn_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            connect.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM kitaplar where Id =(" + id + ")", connect);
            komut.ExecuteNonQuery();
            
            connect.Close();
            showInfo();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            showInfo();
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            sira_tbx.Clear();
            ad_tbx.Clear();
            yazar_tbx.Clear();
            yayin_tbx.Clear();
            sayfa_tbx.Clear();

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            connect.Open();
            SqlCommand komut = new SqlCommand("UPDATE kitaplar set Id='"+sira_tbx.Text.ToString()+"', kitapAd='"+ad_tbx.Text.ToString()+ "', yazar='"+yazar_tbx.Text.ToString()+ "', yayınEvi='"+yayin_tbx.Text.ToString()+ "', sayfa='"+sayfa_tbx.Text.ToString()+"'WHERE Id=(" +id+")",connect);
            komut.ExecuteNonQuery();
            connect.Close();
            showInfo();
        }
    }
}
