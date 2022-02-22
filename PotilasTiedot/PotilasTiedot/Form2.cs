using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PotilasTiedot
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            //Sisään kirjautuminen
            string user = textBox1.Text;
            string passw = textBox2.Text;
            Names.Login(user, passw);
            textBox1.Clear();
            textBox2.Clear();
           
          


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Sulkee ikkunan
            this.Close();
        }


    }
}
