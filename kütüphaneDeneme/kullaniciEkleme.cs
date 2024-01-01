using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kütüphaneDeneme
{
    public partial class kullaniciEkleme : Form
    {
        SqlConnection connect = new SqlConnection("Data Source=PER_TEKNO;Initial Catalog=kitaplar;Integrated Security=True");
        SqlCommand command = new SqlCommand();
        DataTable dataTable = new DataTable();

        public kullaniciEkleme()
        {
            InitializeComponent();
        }

        public void dataList()
        {
            try
            {
                dataTable.Clear();
                connect.Open();
                SqlDataAdapter listele = new SqlDataAdapter("select * from kullanicilar", connect);
                listele.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connect.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Hata Sistem Mesajı : " + error);
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string joinQuery = "insert into kullanicilar (kullaniciAd, kullaniciSoyad, kullaniciTel, kullaniciEposta, kullaniciAdres) values ('" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "')";

            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = (joinQuery);
                command.ExecuteNonQuery();
                MessageBox.Show("Kullanıcı Ekleme İşlemi Başarıyla Gerçekleşti");
                connect.Close();
                dataList();
            }
            catch (Exception error)
            {
                MessageBox.Show("Hata Sistem Mesajı : " + error);
            }

        }

        private void kullaniciEkleme_Load(object sender, EventArgs e)
        {
            dataList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selectDelete = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            string deleteQuery = "delete from kullanicilar where kullaniciId = " + selectDelete + "";

            try
            {
                if (checkBox1.Checked == true)
                {
                    connect.Open();
                    command.Connection = connect;
                    command.CommandText = (deleteQuery);
                    command.ExecuteNonQuery();
                    MessageBox.Show(selectDelete + "İd' li kullanıcı silindi");
                    connect.Close();
                    dataList();

                    checkBox1.Checked = false;
                }
                else
                {
                    MessageBox.Show("Silme işlemi yapabilmek için onay kutusu işaretlenmelidir.");
                }
            }
            catch(Exception error)
            {
                MessageBox.Show("Hata Sistem Mesajı : " + error);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string listQuery = "select * from kullanicilar where kullaniciAd like '%" + textBox1.Text + "%' or kullaniciSoyad like '%" + textBox1.Text + "%' ";

            try
            {
                connect.Open();
                dataTable.Clear();
                SqlDataAdapter listele = new SqlDataAdapter(listQuery, connect);
                listele.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                MessageBox.Show(textBox1.Text + "' e göre veri sonuçları");
                connect.Close();
            }

            catch (Exception error)
            {
                MessageBox.Show("Sistem Mesajı: " + error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string updateSelect = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string updateQuery = "update kullanicilar set kullaniciAd = '" + textBox11.Text + "', kullaniciSoyad = '" + textBox10.Text + "', kullaniciTel = '" + textBox9.Text + "', kullaniciEposta = '" + textBox8.Text + "', kullaniciAdres = '" + textBox7.Text + "' where kullaniciId = '" + updateSelect + "' ";

            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = (updateQuery);
                command.ExecuteNonQuery();
                MessageBox.Show("Güncelleme İşlemi yapılmıştır");
                connect.Close();
                dataList();
            }
            catch (Exception error)
            {
                MessageBox.Show("Hata Sistem Mesajı: " + error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox11.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox11.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox10.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        //private void button8_Click(object sender, EventArgs e)
        //{
        //    string selectBookNum = dataGridView1.CurrentRow.Cells[1].Value.ToString();

        //    string bookNumQuery = "SELECT kD.kitapIsim as 'Kitap İsmi' FROM kitaplarDeneme kD INNER JOIN kullanicilar kll ON kD.tanimliKullaniciAdi = kll.kullaniciAd WHERE kD.tanimliKullaniciAdi = '" + selectBookNum + "'";


        //    baglanti.Open();
        //    SqlDataAdapter listele = new SqlDataAdapter(bookNumQuery, baglanti);
        //    listele.Fill(veriTablosu);
        //    dataGridView2.DataSource = veriTablosu;
        //    baglanti.Close();
        //}

        private void button8_Click_1(object sender, EventArgs e)
        {
            // yeni bir DataTable oluşturuldu sebebi dataGridView1 deki veriTablosunu kontrol ediyordu.
            DataTable dataTable = new DataTable();


            string selectBookNum = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            string bookNumQuery = "SELECT kD.kitapIsim as 'Kitap İsmi' FROM kitaplarDeneme kD INNER JOIN kullanicilar kll ON kD.tanimliKullaniciAdi = kll.kullaniciAd WHERE kD.tanimliKullaniciAdi = '" + selectBookNum + "'";

            try
            {
                connect.Open();
                dataTable.Clear();
                SqlDataAdapter listele = new SqlDataAdapter(bookNumQuery, connect);
                listele.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
                connect.Close();
            }
            catch(Exception error)
            {
                MessageBox.Show("Hata Sistem Mesajı : " + error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            kitapEkleme kitapEkle = new kitapEkleme();
            kitapEkle.Show();
            this.Hide();
        }
    }
}
