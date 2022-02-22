using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace PotilasTiedot
{
    class Names
    {
        public static SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Henkilötiedot;Integrated Security=True;" + "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

      



        public static void AddNames(string t1,string t2, string t3,string t4,string t5,string t6,string t7)
        {
            //Lisää nimi tietokantaan

            try
            {
          
            connection.Open();
            SqlCommand command = new SqlCommand("Insert into Potilasrekisteri Values (@Nimi,@Syntymäaika,@Osoite,@Postinumero,@Kaupunki,@Puhelinnumero,@Sähköposti)", connection);

            command.Parameters.AddWithValue("@Nimi", t1);
            command.Parameters.AddWithValue("@Syntymäaika", t2);
            command.Parameters.AddWithValue("@Osoite", t3);
            command.Parameters.AddWithValue("@Postinumero", t4);
            command.Parameters.AddWithValue("@Kaupunki", t5);
            command.Parameters.AddWithValue("@Puhelinnumero", t6);
            command.Parameters.AddWithValue("@Sähköposti", t7);
            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("Tallennettu!");

              
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        public static void Updating(string sql)
        {
            //Tietokannan tietojen päivitys


          
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.UpdateCommand = connection.CreateCommand();
                adapter.UpdateCommand.CommandText = sql;


                if (adapter.UpdateCommand.ExecuteNonQuery() > 0)
                {

                    MessageBox.Show("Päivitetty!");
                }

                connection.Close();

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }



       



        public static void RemoveName(int id)
        {
            //Tiedon poistaminen tietokannasta
          
            connection.Open();

            try
            {
                DialogResult dialogResult = MessageBox.Show("Poistetaanko?", "Varoitus!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommand command1 = new SqlCommand($"DELETE FROM Potilaskertomus WHERE Asiakasnumero={id}", connection);
                    SqlCommand command = new SqlCommand($"DELETE FROM Potilasrekisteri WHERE Asiakasnumero={id}", connection);
                    command1.ExecuteNonQuery();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                     connection.Close();
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public static void Login(string user, string passw)
        {
            try
            {
                
                SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Henkilötiedot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                SqlDataAdapter adapter = new SqlDataAdapter("Select Count (*) from Kirjautuminen where Käyttäjätunnus='" + user + "' And Salasana='" + passw + "'", connection);
                connection.Open();
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                if (dataTable.Rows[0][0].ToString() == "1")
                {

                    connection.Close();
                    Form1 form1 = new Form1();
                    form1.Show();

                }

                else
                {
                    MessageBox.Show("Käyttäjätunnus tai salasana väärä!");                 
                }
            

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

    

    }
}
