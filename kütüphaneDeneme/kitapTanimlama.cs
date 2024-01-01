using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kütüphaneDeneme
{
    public partial class kitapTanimlama : Form
    {
        SqlConnection connect = new SqlConnection("Data Source=PER_TEKNO;Initial Catalog=kitaplar;Integrated Security=True");
        SqlCommand command = new SqlCommand();
        DataTable dataTable = new DataTable();

        MailMessage eMail = new MailMessage();

        public kitapTanimlama()
        {
            InitializeComponent();
        }

        public void sendMail()
        {
            eMail.From = new MailAddress("emrullah.burma@yandex.com");
            eMail.To.Add(textBox5.Text.ToString());
            eMail.Subject = textBox7.Text.ToString();
            eMail.Body = textBox6.Text.ToString();


            SmtpClient smtp = new SmtpClient();

            smtp.Credentials = new System.Net.NetworkCredential("emrullah.burma@yandex.com", "omptgxdxlqtraver");
            smtp.Host = "smtp.yandex.ru";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.Send(eMail);
            MessageBox.Show("Mail Gönderilmiştir.");
        }


        public void dataList()
        {
            try
            {
                dataTable.Clear();
                connect.Open();
                SqlDataAdapter listele = new SqlDataAdapter("select * from kitapAlma", connect);
                listele.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                connect.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Hata Sistem Mesajı : " + error);
            }

        }

        private void kitapAlma_Load(object sender, EventArgs e)
        {
            dataList();

            try
            {
                SqlDataReader read;
                connect.Open();


                command.Connection = connect;
                command.CommandText = "select kullaniciAd from kullanicilar";
                read = command.ExecuteReader();

                while (read.Read())
                {
                    comboBox1.Items.Add(read[0].ToString());
                }
                connect.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Hata Sistem Mesajı : " + error);
            }


            try
            {
                SqlDataReader read;
                connect.Open();


                command.Connection = connect;
                command.CommandText = "select distinct(kitapIsim) from kitaplarDeneme";
                read = command.ExecuteReader();

                while (read.Read())
                {
                    comboBox2.Items.Add(read[0].ToString());
                }
                connect.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Hata Sistem Mesajı : " + error);
            }







            DataTable dateTable = new DataTable();

            string dateQuery = "SELECT kitapAlanKullanici, kitapAlmaBaslangic, kitapAlmaBitis, aldigiKitap, CASE WHEN GETDATE() = kitapAlmaBitis OR GETDATE() > kitapAlmaBitis THEN 'Bugün içerisinde teslim edilmesi gerekiyor.' ELSE 'Tarih Geçmiştir.' END AS Durum FROM kitapAlma";

            connect.Open();
            SqlDataAdapter listele = new SqlDataAdapter(dateQuery, connect);
            listele.Fill(dateTable);
            dataGridView2.DataSource = dateTable;
            connect.Close();

            //for (int i = 0; dataGridView2.Rows.Count > i; i++)
            //{
            //    string dateStart = dataGridView2.Rows[i].Cells["kitapAlmaBaslangic"].Value.ToString();
            //    string dateFinish = dataGridView2.Rows[i].Cells["kitapAlmaBitis"].Value.ToString();
            //}

            //for (int i = 0; i < dataGridView2.Rows.Count; i++)
            //{
            //    // Hücre değerlerini kontrol et
            //    if (dataGridView2.Rows[i].Cells["kitapAlmaBaslangic"].Value != null &&
            //        dataGridView2.Rows[i].Cells["kitapAlmaBitis"].Value != null)
            //    {
            //        string dateStart = dataGridView2.Rows[i].Cells["kitapAlmaBaslangic"].Value.ToString();
            //        string dateFinish = dataGridView2.Rows[i].Cells["kitapAlmaBitis"].Value.ToString();
            //    }
            //}


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string insertQuery = "insert into kitapAlma (kitapAlanKullanici, kitapAlmaBaslangic, kitapAlmaBitis, aldigiKitap) values ('" + comboBox1.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + comboBox2.Text + "')";

            connect.Open();
            command.Connection = connect;
            command.CommandText = (insertQuery);
            command.ExecuteNonQuery();

            MessageBox.Show("Kitap Alma eklendi");
            connect.Close();
            dataList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kitapEkleme home = new kitapEkleme();
            home.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string selectUser = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = selectUser;

            string query = "select kullaniciEposta from kullanicilar k inner join kitapAlma kA on k.kullaniciAd = kA.kitapAlanKullanici where kA.kitapAlanKullanici = '" + selectUser + "'";



            connect.Open();
            command.Connection = connect;
            command.CommandText = query;
            SqlDataReader read = command.ExecuteReader();
            while (read.Read())
            {
                textBox5.Text = read[0].ToString();
            }
            connect.Close();



            textBox6.Text = ("Sayın " + dataGridView2.CurrentRow.Cells[0].Value.ToString() + " kullanıcımız aldığınız " + dataGridView2.CurrentRow.Cells[3].Value.ToString() + " kitabı lütfen gün içerisinde getiriniz. ");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sendMail();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult verify;

            string deleteSelect = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            string deleteQuery = "delete from kitapAlma where kitapAlmaId = " + deleteSelect + "";

            if (checkBox1.Checked == true)
            {
                verify = MessageBox.Show("Silmek İstiyormusunuz", "Silme", MessageBoxButtons.YesNo);

                if (verify == DialogResult.Yes)
                {
                    connect.Open();
                    command.Connection = connect;
                    command.CommandText = (deleteQuery);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Silme İşelmi tamamlanmıştır");
                    connect.Close();
                    dataList();

                    checkBox1.Checked = false;
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edilmiştir");
                }
            }
            else
            {
                MessageBox.Show("Onay kutusunu lütfen işaretleyiniz");
            }
        }
    }
}
