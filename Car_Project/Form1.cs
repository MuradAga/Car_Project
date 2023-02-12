using System.Data;
using System.Data.SqlClient;

namespace Car_Project
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new("Server=Murad; Database=Car_Db; Trusted_Connection=True;");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlDataAdapter sqlDataAdapter = new("SELECT ModelId, BrandName, ModelName FROM Models, Brands WHERE ModelBrandId = BrandId", connection);
            connection.Close();

            DataTable dt = new();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form2 form2 = new();
            this.Hide();
            form2.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string modelId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            Form3 form3 = new(modelId);
            this.Hide();
            form3.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connection.Open();

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string modelId = row.Cells[0].Value.ToString();
                SqlCommand sqlCommand = new($"DELETE FROM Models WHERE ModelId = {modelId}", connection);
                sqlCommand.ExecuteNonQuery();
            }

            SqlDataAdapter sqlDataAdapter = new("SELECT ModelId, BrandName, ModelName FROM Models, Brands WHERE ModelBrandId = BrandId", connection);

            DataTable dt = new();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            connection.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            connection.Open();

            SqlDataAdapter sqlDataAdapter = new($"SELECT ModelId, BrandName, ModelName FROM Models, Brands WHERE ModelBrandId = BrandId AND ModelName LIKE '%{txtSearch.Text}%'", connection);

            DataTable dt = new();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            connection.Close();
        }
    }
}