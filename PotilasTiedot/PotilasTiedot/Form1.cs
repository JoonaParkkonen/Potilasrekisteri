using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace PotilasTiedot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        public static string asiakasnumero;

        public static SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Henkilötiedot;Integrated Security=True;" +
            "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    
       

        private void button1_Click(object sender, EventArgs e)
        {
            //Tietojen lisääminen tietokantaan!


           
            if (textBox1.Text.Length > 1)
            {
                Names.AddNames(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
            }
            else if (textBox1.Text.Length < 1)
            {
                MessageBox.Show("Nimikenttä ei voi olla tyhjä!");
            }
            Clear();
            Retrieve();
           
        }
    
        private void button2_Click(object sender, EventArgs e)
        {
            //Tietojen päivitys
           
            if (textBox1.Text.Length > 1)
            {

                string sql = " Update Potilasrekisteri Set Nimi = '" + textBox1.Text + "', Syntymäaika = '" + textBox2.Text + "', Osoite = '" + textBox3.Text + "', Postinumero = '" + textBox4.Text + "', Kaupunki = '" + textBox5.Text + "', Puhelinnumero = '" + textBox6.Text + "', Sähköposti = '" + textBox7.Text + "' where Asiakasnumero = '" + textBox8.Text + "'";

                Names.Updating(sql);
                Clear();
                Retrieve();
            }
            else if (textBox1.Text.Length < 1)
            {
                MessageBox.Show("Nimikenttä ei voi olla tyhjä!");
            }
        }

         


        private void button3_Click(object sender, EventArgs e)
        {
            //Sulje sovellus
            Application.Exit();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            //Textboxien tyhjennys
            Clear();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            //Tietojen poistaminen
            var cells = dataGridView1.CurrentRow.Cells;
            string value = cells[0].Value.ToString();
            int index = int.Parse(value);
            Names.RemoveName(index);
            Retrieve();
            Clear();

        }

       
        private void button6_Click(object sender, EventArgs e)
        {
            //Uuden ikkunan aukaiseminen (Form3)
            if (textBox8.TextLength < 1)
            {
                MessageBox.Show("Valitse Henkilötieto!");
            }
            else
            {
                asiakasnumero = textBox8.Text;
                Form3 form3 = new Form3();
                form3.ShowDialog();
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {  
            Retrieve();
        }




        public void Retrieve()
        {
            //Tietojen päivitys datagridviewiin
            try
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Henkilötiedot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

                string sql1 = "SELECT * FROM Potilasrekisteri";
                SqlCommand cmd = new SqlCommand(sql1, connection);
                DataTable dt = new DataTable();

                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                connection.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                connection.Close();
            }

        }


        public void Clear()
        {
            //Textboxien tyhjennys
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
        }


          

       
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            //Tietojen hakeminen datagridviewiin, textboxista

            SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Henkilötiedot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            SqlDataAdapter adapter = new SqlDataAdapter("Select Asiakasnumero, Nimi,Syntymäaika,Osoite,Postinumero,Kaupunki,Puhelinnumero,Sähköposti From Potilasrekisteri Where Nimi Like'"+textBox9.Text+"%'",connection);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            
        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Tietojen "Siirto" textboxehin, datagridviewistä
            try
            {
                textBox8.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                textBox9.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
