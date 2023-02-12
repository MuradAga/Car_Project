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
    public partial class Form2 : Form
    {
        SqlConnection connection = new("Server=Murad; Database=Car_Db; Trusted_Connection=True;");
        public Form2()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string modelName = txtModel.Text;
            string brandId = cmbBrands.SelectedValue.ToString();

            connection.Open();
            SqlCommand sqlCommand = new($"INSERT INTO Models VALUES (N'{modelName}', {brandId})", connection);
            sqlCommand.ExecuteNonQuery();

            connection.Close();

            Form1 form1 = new();
            this.Hide();
            form1.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            connection.Open();

            SqlDataAdapter adapter = new("SELECT * FROM Brands ORDER BY BrandName", connection);

            DataSet ds = new();
            adapter.Fill(ds);

            cmbBrands.DisplayMember = "BrandName";
            cmbBrands.ValueMember = "BrandId";
            cmbBrands.DataSource = ds.Tables[0];

            connection.Close();
        }
    }
}
