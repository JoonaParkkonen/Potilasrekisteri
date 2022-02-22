using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PotilasTiedot
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
           
        }
        SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Henkilötiedot;Integrated Security=True;" + "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        private void Form3_Load(object sender, EventArgs e)
        {
            //Tietojen lisäys tietokannasta textboxiin ja richtextboxiin

            textBox1.Text = Form1.asiakasnumero;
            string sqlquery = "SELECT Kertomus FROM Potilaskertomus WHERE Asiakasnumero = " + textBox1.Text;

            SqlCommand command = new SqlCommand(sqlquery, connection);
            connection.Open();

            SqlDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
             richTextBox1.Text = sdr["Kertomus"].ToString();
            }
            connection.Close();

           
        }




        private void button1_Click(object sender, EventArgs e)
        {
            //Richtextboxin sisällön lisääminen ja päivittämien tietokantaan

            SqlCommand cmd = new SqlCommand("select * from Potilaskertomus where Asiakasnumero = @Asiakasnumero", connection);
            connection.Open();
            SqlParameter param = new SqlParameter();
            param.ParameterName = "Asiakasnumero";
            param.Value = textBox1.Text;
            cmd.Parameters.Add(param);
            SqlDataReader reader = cmd.ExecuteReader();
           
            if (reader.HasRows)
            {
                
              
                try
                {
                    reader.Close();
                    string sql1 = " Update Potilaskertomus Set Kertomus = '" + richTextBox1.Text + "' where Asiakasnumero = '" + textBox1.Text + "'";
                    SqlCommand command = new SqlCommand(sql1, connection);
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    adapter.UpdateCommand = connection.CreateCommand();
                    adapter.UpdateCommand.CommandText = sql1;

                    if (adapter.UpdateCommand.ExecuteNonQuery() > 0)
                    {

                        MessageBox.Show("Tallennettu!");
                        connection.Close();                   
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                    reader.Close();
                }
                
            }
            else
            {
                reader.Close();
                try
                {
                        reader.Close();
                   
                        SqlCommand command = new SqlCommand("Insert into Potilaskertomus Values (@Kertomus,@Asiakasnumero)", connection);                     
                        command.Parameters.AddWithValue("@Kertomus", richTextBox1.Text);
                        command.Parameters.AddWithValue("@Asiakasnumero", textBox1.Text);

                        command.ExecuteNonQuery();
                        connection.Close();

                        MessageBox.Show("Tallennettu!");                          
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
            }
                
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //Richtextboxin tyhjennys
            richTextBox1.Clear();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            //sulkee ikkunan
            this.Close();
        }
    }
}
