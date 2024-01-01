using DevExpress.XtraBars.Commands.Internal;
using DevExpress.XtraEditors.Filtering.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kütüphaneDeneme
{

    public partial class kitapEkleme : Form
    {
        SqlConnection connect = new SqlConnection("Data Source=PER_TEKNO;Initial Catalog=kitaplar;Integrated Security=True");
        SqlCommand command = new SqlCommand();
        DataTable dataTable = new DataTable();

        public kitapEkleme()
        {
            InitializeComponent();
        }
        public void listData()
        {
            try
            {
                dataTable.Clear();
                connect.Open();
                SqlDataAdapter list = new SqlDataAdapter("select * from kitaplarDeneme", connect);
                list.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connect.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Hata Sistem Mesajı :" + error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string listQuery = "select * from kitaplarDeneme where kitapIsim like '%" + textBox1.Text + "%' or kitapYazar like '%" + textBox1.Text + "%' or kitapYayinEvi like '%" + textBox1.Text + "%' ";

            try
            {
                connect.Open();
                dataTable.Clear();
                SqlDataAdapter list = new SqlDataAdapter(listQuery, connect);
                list.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                MessageBox.Show(textBox1.Text + "' e göre veri sonuçları");
                connect.Close();
            }

            catch (Exception error)
            {
                MessageBox.Show("Sistem Mesajı: " + error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string newJoinQuery = "insert into kitaplarDeneme (kitapIsim, kitapYazar, kitapSayfaSayisi, kitapYayinEvi) values ('" + textBox2.Text + "' ,'" + textBox3.Text + "' , " + textBox4.Text + " , '" + textBox5.Text + "')";
                connect.Open();
                command.Connection = connect;
                command.CommandText = (newJoinQuery);
                command.ExecuteNonQuery();
                MessageBox.Show("Kitap kaydı yapılmıştır");
                connect.Close();
                listData();
            }
            catch (Exception error)
            {
                MessageBox.Show("Sistem Mesajı :" + error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'kitaplarDataSet.kullanicilar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            // this.kullanicilarTableAdapter.Fill(this.kitaplarDataSet.kullanicilar);

            listData();


            // Kullanıcı Adları comboBox

            /*   try
               {
                   SqlDataReader read;
                   connect.Open();


                   command.Connection = connect;
                   command.CommandText = "select kullaniciAd from kullanicilar";
                   read = command.ExecuteReader();

                   while (read.Read())
                   {
                       comboBox1.Items.Add(read[0].ToString());
                       comboBox2.Items.Add(read[0].ToString());
                   }
                   connect.Close();
               }
               catch(Exception error)
               {
                   MessageBox.Show("Hata Sistem Mesajı : " + error);
               }

               */

            //SqlDataAdapter listeDeneme = new SqlDataAdapter("Select kullaniciAd from kullanicilar", baglanti);
            //listeDeneme.Fill(deneme);
            //comboBox1.DataSource = deneme;
            //MessageBox.Show("deneme");
            //baglanti.Close();

            //DateTime bugunTarih = DateTime.Today;
            //textBox6.Text = bugunTarih.ToString("yyyy-MM-dd");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult verify;

            string selectDelete = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string deleteQuery = "delete from kitaplarDeneme where kitapId =" + selectDelete;

            verify = MessageBox.Show("Silmek İstediğinize Emin misiniz ?", "Silme İşlemi", MessageBoxButtons.YesNo);

            try
            {
                if (checkBox1.Checked == true)
                {
                    if (verify == DialogResult.Yes)
                    {
                        connect.Open();
                        command.Connection = connect;
                        command.CommandText = (deleteQuery);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Silme işlemi gerçekleşmiştir.");
                        connect.Close();
                        listData();
                    }
                    else
                    {
                        MessageBox.Show("Silme İşlemi İptal Edildi.");
                    }
                }
                else
                {
                    MessageBox.Show("Onaylamadığınız için işlem gerçekleştirilememiştir");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Hata Sistem Mesajı :" + error);
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            string updateSelect = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string updateQuery = "update kitaplarDeneme set kitapIsim = '" + textBox12.Text + "', kitapYazar = '" + textBox15.Text + "', kitapSayfaSayisi = '" + textBox14.Text + "', kitapYayinEvi = '" + textBox13.Text + "' where kitapId = '" + updateSelect + "' ";

            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = (updateQuery);
                command.ExecuteNonQuery();
                MessageBox.Show("Güncelleme İşlemi yapılmıştır");
                connect.Close();
                listData();
            }
            catch (Exception error)
            {
                MessageBox.Show("Hata Sistem Mesajı: " + error);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox12.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox13.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox14.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox15.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            // comboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            // comboBox2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable deneme = new DataTable();

            //baglanti.Open();
            //SqlDataAdapter listeDeneme = new SqlDataAdapter("Select kullaniciAd from kullanicilar", baglanti);
            //listeDeneme.Fill(deneme);
            //comboBox1.DataSource = deneme;
            //MessageBox.Show("deneme");
            //baglanti.Close();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            kullaniciEkleme kullaniciEkleme = new kullaniciEkleme();
            kullaniciEkleme.Show();
            this.Hide();
        }


        // Kitaba Kullanıcı Tanımlama

        /* private void button9_Click(object sender, EventArgs e)
         {
             string selectTanimla = dataGridView1.CurrentRow.Cells[0].Value.ToString();
             string updateQuery = "update kitaplarDeneme set tanimliKullaniciAdi = '" + comboBox1.Text + "' where kitapId =" + selectTanimla + "";

             try
             {
                 connect.Open();
                 command.Connection = connect;
                 command.CommandText = (updateQuery);
                 command.ExecuteNonQuery();

                 MessageBox.Show(selectTanimla + "Id li kitabınızın tanımlı olduğu kullanıcı" + comboBox1.Text + "olarak düzenlemmiştir.");
                 connect.Close();
                 listData();
             }
             catch(Exception error)
             {
                 MessageBox.Show("Hata Sistem Mesajı : " + error);
             }



         } */

        private void button10_Click(object sender, EventArgs e)
        {
            kitapTanimlama kitapAlma = new kitapTanimlama();
            kitapAlma.Show();
            this.Hide();
        }
    }
}
