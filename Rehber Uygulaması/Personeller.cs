using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Rehber_Uygulaması
{
    public partial class Personeller : Form
    {
        public Personeller()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Rehber;Integrated Security=True");
        SqlCommand com = new SqlCommand();
        SqlDataAdapter adap = new SqlDataAdapter();
        DataSet set = new DataSet();

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || comboBox1.Text == "" || textBox7.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" || textBox8.Text == "" || comboBox4.Text == "" || dateTimePicker1.Text == "" || textBox11.Text == "")
            {
                MessageBox.Show("Bilgilerde eksiklik var. Zorunlu olan yerleri doldurunuz.");
            }
            else
            {
                con.Open();
                com = new SqlCommand("insert into Personel (Ad,Soyad,Tc,Eposta,Telefon,Adres,Departman,Unvan,OgrenimDurumu,CalismaSekli,Maas,Durum,GirisTarih,CikisTarih,Resim) values (@Adi,@Soyadi,@TC,@Eposta,@Telefon,@Adres,@Departman,@Unvani,@OgrenimDurumu,@CalismaSekli,@Maasi,@Durum,@GirisTarihi,@CikisTarihi,@Resim)", con);
                com.Parameters.AddWithValue("@Adi", textBox1.Text);
                com.Parameters.AddWithValue("@Soyadi", textBox2.Text);
                com.Parameters.AddWithValue("@TC", textBox3.Text);
                com.Parameters.AddWithValue("@Eposta", textBox4.Text);
                com.Parameters.AddWithValue("@Telefon", textBox5.Text);
                com.Parameters.AddWithValue("@Adres", textBox6.Text);
                com.Parameters.AddWithValue("@Departman", comboBox1.Text);
                com.Parameters.AddWithValue("@Unvani", textBox7.Text);
                com.Parameters.AddWithValue("@OgrenimDurumu", comboBox2.Text);
                com.Parameters.AddWithValue("@CalismaSekli", comboBox3.Text);
                com.Parameters.AddWithValue("@Maasi", textBox8.Text);
                com.Parameters.AddWithValue("@Durum", comboBox4.Text);
                com.Parameters.AddWithValue("@GirisTarihi", dateTimePicker1.Text);
                com.Parameters.AddWithValue("@CikisTarihi", textBox9.Text);
                com.Parameters.AddWithValue("@Resim", textBox11.Text);
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Personel Başarıyla Eklendi.");
                Temizle();
            }
        }

        void Temizle() 
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            dateTimePicker1.Text = "";
            textBox9.Text = "";
            textBox11.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            pictureBox1.Image = null;
        }

        private void btnRSec_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Yüklemek İstediğiniz Resmi Seçiniz";
            openFileDialog1.Filter = "Resim Dosyası |*.jpeg;*.jpg;*.png";
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            textBox11.Text = openFileDialog1.FileName;
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            con.Open();
            adap = new SqlDataAdapter("select*from Personel", con);
            set = new DataSet();
            adap.Fill(set, "Personel");
            dataGridView1.DataSource = set.Tables["Personel"];
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secili = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[secili].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[secili].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[secili].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[secili].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[secili].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.Rows[secili].Cells[6].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[secili].Cells[7].Value.ToString();
            textBox7.Text = dataGridView1.Rows[secili].Cells[8].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[secili].Cells[9].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[secili].Cells[10].Value.ToString();
            textBox8.Text = dataGridView1.Rows[secili].Cells[11].Value.ToString();
            comboBox4.Text = dataGridView1.Rows[secili].Cells[12].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[secili].Cells[13].Value.ToString();
            textBox9.Text = dataGridView1.Rows[secili].Cells[14].Value.ToString();
            textBox11.Text = dataGridView1.Rows[secili].Cells[15].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.Rows[secili].Cells[15].Value.ToString();
        }
        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            com = new SqlCommand("select * from Personel where Ad like '%" + textBox12.Text + "%' ", con);
            SqlDataAdapter adap = new SqlDataAdapter(com);
            set = new DataSet();
            adap.Fill(set);
            dataGridView1.DataSource = set.Tables[0];
            con.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            con.Open();
            com = new SqlCommand("update Personel set Ad=@Adi, Soyad=@Soyadi, Tc=@TC, Eposta=@Eposta, Telefon=@Telefon, Adres=@Adres, Departman=@Departman, Unvan=@Unvani, OgrenimDurumu=@OgrenimDurumu, CalismaSekli=@CalismaSekli, Maas=@Maasi, Durum=@Durum, GirisTarih=@GirisTarihi, CikisTarih=@CikisTarihi, Resim=@Resim where Tc=@TC", con);
            com.Parameters.AddWithValue("@Adi", textBox1.Text);
            com.Parameters.AddWithValue("@Soyadi", textBox2.Text);
            com.Parameters.AddWithValue("@TC", textBox3.Text);
            com.Parameters.AddWithValue("@Eposta", textBox4.Text);
            com.Parameters.AddWithValue("@Telefon", textBox5.Text);
            com.Parameters.AddWithValue("@Adres", textBox6.Text);
            com.Parameters.AddWithValue("@Departman", comboBox1.Text);
            com.Parameters.AddWithValue("@Unvani", textBox7.Text);
            com.Parameters.AddWithValue("@OgrenimDurumu", comboBox2.Text);
            com.Parameters.AddWithValue("@CalismaSekli", comboBox3.Text);
            com.Parameters.AddWithValue("@Maasi", textBox8.Text);
            com.Parameters.AddWithValue("@Durum", comboBox4.Text);
            com.Parameters.AddWithValue("@GirisTarihi", dateTimePicker1.Text);
            com.Parameters.AddWithValue("@CikisTarihi", textBox9.Text);
            com.Parameters.AddWithValue("@Resim", textBox11.Text);
            com.ExecuteNonQuery();
            con.Close();
            con.Open();
            adap = new SqlDataAdapter("select*from Personel", con);
            set = new DataSet();
            adap.Fill(set, "Personel");
            dataGridView1.DataSource = set.Tables["Personel"];
            con.Close();
            MessageBox.Show("Personel Başarıyla Güncellendi.");
            Temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            con.Open();
            com = new SqlCommand("delete from Personel where Tc=@TC", con);
            com.Parameters.AddWithValue("@TC", textBox3.Text);
            com.ExecuteNonQuery();
            con.Close();
            con.Open();
            adap = new SqlDataAdapter("select*from Personel", con);
            set = new DataSet();
            adap.Fill(set, "Personel");
            dataGridView1.DataSource = set.Tables["Personel"];
            con.Close();
            MessageBox.Show("Personel Başarıyla Silindi.");
            Temizle();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            com = new SqlCommand("select * from Personel where Soyad like '%" + textBox10.Text + "%' ", con);
            SqlDataAdapter adap = new SqlDataAdapter(com);
            set = new DataSet();
            adap.Fill(set);
            dataGridView1.DataSource = set.Tables[0];
            con.Close();
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            com = new SqlCommand("select * from Personel where Telefon like '%" + textBox13.Text + "%' ", con);
            SqlDataAdapter adap = new SqlDataAdapter(com);
            set = new DataSet();
            adap.Fill(set);
            dataGridView1.DataSource = set.Tables[0];
            con.Close();
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            com = new SqlCommand("select * from Personel where Adres like '%" + textBox14.Text + "%' ", con);
            SqlDataAdapter adap = new SqlDataAdapter(com);
            set = new DataSet();
            adap.Fill(set);
            dataGridView1.DataSource = set.Tables[0];
            con.Close();
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            com = new SqlCommand("select * from Personel where Departman like '%" + textBox15.Text + "%' ", con);
            SqlDataAdapter adap = new SqlDataAdapter(com);
            set = new DataSet();
            adap.Fill(set);
            dataGridView1.DataSource = set.Tables[0];
            con.Close();
        }   
    }
}
