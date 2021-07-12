using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoviesRentalsProjeect
{
    public partial class RentalForm : Form
    {
        DB Database = new DB();
        string WhichButtonClicked = "";
        string RMID = "";
        public RentalForm()
        {
            //Disabled CustomerID and MovieID
            InitializeComponent();
            CustIDTBox.Enabled = false;
            MovieIDTBox.Enabled = false;
          
              
                    IssueMovieBtn.Enabled = false;
       



        }

        private void btnLoadCust_Click(object sender, EventArgs e)
        {
            //Load data of Customer table
            MainGrid.DataSource = Database.LoadCustomers();
            WhichButtonClicked = "Customer";
            AddMovieBtn.Enabled = false;
            UpdateMovieBtn.Enabled = false;
            DeleteMovieBtn.Enabled = false;


            AddCustBtn.Enabled = true;
            UpdCustBtn.Enabled = true;
            DltCustBtn.Enabled = true;
            TitleTBox.Clear();
            RatingTBox.Clear();
            //YearTBox.Clear();
            RatingCTBox.Clear();
            PlotTBox.Clear();
            GenreTBox.Clear();
            CpyTBox.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Delete Movies
            Database.DeleteMovie(MovieIDTBox.Text);
            LoadMovies_Click(null, null);
            MessageBox.Show("Movie Deleted Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Load Movies
        private void LoadMovies_Click(object sender, EventArgs e)
        {
            MainGrid.DataSource = Database.LoadMovies();
            WhichButtonClicked = "Movies";
            if (MovieIDTBox.Text == (Convert.ToString(MovieIDTBox.Text)) && CustIDTBox.Text==(Convert.ToString(CustIDTBox.Text)))
            {
                IssueMovieBtn.Enabled = true;
            }
           


            AddMovieBtn.Enabled = true;
            UpdateMovieBtn.Enabled = true;
            DeleteMovieBtn.Enabled = true;

            AddCustBtn.Enabled = false;
            UpdCustBtn.Enabled = false;
            DltCustBtn.Enabled = false;
            TitleTBox.Undo();
            RatingTBox.Undo();
            //YearTBox.Undo();
            RatingCTBox.Undo();
            PlotTBox.Undo();
            GenreTBox.Undo();
            CpyTBox.Undo();
        }
        //Load Rented Movies

        private void LoadRentedMovies_Click(object sender, EventArgs e)
        {
            MainGrid.DataSource = Database.LoadRentedMovies();
            WhichButtonClicked = "Rented";
        }
        //Show Popular Movie
        private void PopMovBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Database.PopularMovie());
        }
        //Show Popular Customer
        private void PopCustBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Database.PopularCustomer());
        }
        //Show Issue Movies
        private void IssueMovieBtn_Click(object sender, EventArgs e)
        {
            Database.IssueMovie(MovieIDTBox.Text, CustIDTBox.Text);
            LoadRentedMovies_Click(null, null);
            if (CustIDTBox.Text == "" && MovieIDTBox.Text == "")
            {
                MessageBox.Show("Please select Movie first");
            }


         
        }
        //Show Return Movies
        private void ReturnMovieBtn_Click(object sender, EventArgs e)
        {
            Database.ReturnMovie(RMID);
            LoadRentedMovies_Click(null, null);
        }
        //Fill Customer,Movies and Rented movies
        private void MainGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow row = MainGrid.Rows[index];

            if (WhichButtonClicked == "Customer")
            {
               CustIDTBox.Text = row.Cells[0].Value.ToString();
                FNTBox.Text = row.Cells[1].Value.ToString();
                LNTBox.Text = row.Cells[2].Value.ToString();
                AdTBox.Text = row.Cells[3].Value.ToString();
                PhTBox.Text = row.Cells[4].Value.ToString();
            }
            else if (WhichButtonClicked == "Movies")
            {
                MovieIDTBox.Text = row.Cells[0].Value.ToString();
                RatingTBox.Text = row.Cells[1].Value.ToString();
                TitleTBox.Text = row.Cells[2].Value.ToString();
                //YearTBox.Text = row.Cells[3].Value.ToString();
                RatingCTBox.Text = row.Cells[4].Value.ToString();
                CpyTBox.Text = row.Cells[5].Value.ToString();
                PlotTBox.Text = row.Cells[6].Value.ToString();
                GenreTBox.Text = row.Cells[7].Value.ToString();
            }
            else if (WhichButtonClicked == "Rented")
            {
                RMID = row.Cells[0].Value.ToString();
                Console.WriteLine(RMID);
            }
        }

        private void AddCustBtn_Click(object sender, EventArgs e)
        {
            //Add Customer
            Database.AddCustomer(FNTBox.Text, LNTBox.Text, PhTBox.Text, AdTBox.Text);
            //button1_Click(null, null);
            MainGrid.DataSource = Database.LoadCustomers();
            CustIDTBox.Enabled = true;
            MessageBox.Show("Customer Added Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        //Update Customer
        private void UpdCustBtn_Click(object sender, EventArgs e)
        {
            Database.UpdateCustomer(CustIDTBox.Text, FNTBox.Text, LNTBox.Text, PhTBox.Text, AdTBox.Text);
           btnLoadCust_Click(null, null);
            MessageBox.Show("Customer Updated Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Delete Customer
        private void DltCustBtn_Click(object sender, EventArgs e)
        {
            Database.DeleteCustomer(CustIDTBox.Text);
            btnLoadCust_Click(null, null);
            MessageBox.Show("Customer Deleted Successfully","Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Add Movies
        private void AddMovieBtn_Click(object sender, EventArgs e)
        {
            
            Database.AddMovies (TitleTBox.Text, Convert.ToInt32(YearTBox.Value.Year),
               Convert.ToDecimal( RatingCTBox.Text), CpyTBox.Text,RatingTBox.Text,PlotTBox.Text, GenreTBox.Text);
            LoadMovies_Click(null, null);
            MessageBox.Show("Movie Added Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //Update Movies
        private void UpdateMovieBtn_Click(object sender, EventArgs e)
        {
            Database.UpdateMovies(MovieIDTBox.Text, TitleTBox.Text, RatingTBox.Text, Convert.ToInt32(YearTBox.Value.Year), Convert.ToDecimal(RatingCTBox.Text), CpyTBox.Text, PlotTBox.Text, GenreTBox.Text);
            LoadMovies_Click(null, null);
            MessageBox.Show("Movie Updated Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CustIDTBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (CustIDTBox.Text == "" && MovieIDTBox.Text == "")
            {
                MessageBox.Show("Please selet Movie first");
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RentalForm_Load(object sender, EventArgs e)
        {

        }

        private void YearTBox_ValueChanged(object sender, EventArgs e)
        {
            // if videos are older than 5 years (Release Date) then they cost $2 otherwise they cost $5
            YearTBox.Format = DateTimePickerFormat.Custom;
            YearTBox.CustomFormat = "yyyy";

            if (YearTBox.Value.Date <= DateTime.Now.Date.AddYears(-5))
            {
                RatingCTBox.Text = "2";
            }
            else
            {
                RatingCTBox.Text = "5";
            }
        }

       //Refresh button
        private void btnRefresh_Click(object sender, EventArgs e)
        {
           Application.Restart();
        }

        private void MovieIDTBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void MovieIDLabel_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {

        }

        private void RatingCTBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

