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

namespace Car_Project
{
    public partial class Form3 : Form
    {
        SqlConnection connection = new("Server=Murad; Database=Car_Db; Trusted_Connection=True;");
        string id;
        public Form3(string modelId)
        {
            id = modelId;
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new($"SELECT ModelName FROM Models WHERE ModelId = {id}", connection);

            connection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                txtModelName.Text = reader["ModelName"].ToString();
            }

            connection.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand sqlCommand = new($"UPDATE Models SET ModelName=N'{txtModelName.Text}' WHERE ModelId = {id}", connection);
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            Form1 form1 = new();
            this.Hide();
            form1.Show();
        }
    }
}
