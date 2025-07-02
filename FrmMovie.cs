using EFCodeFirstMovie4.DAL.Context;
using EFCodeFirstMovie4.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFCodeFirstMovie4
{
    public partial class FrmMovie: Form
    {
        public FrmMovie()
        {
            InitializeComponent();
        }

        MovieContext context = new MovieContext();

        void CategoryList()
        {
            // Film Kategorilerini Çekme
            var values = context.Categories.ToList();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryId";
            cmbCategory.DataSource = values;
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            var values = context.Movies.ToList();
            DGVMovie.DataSource = values;
        }

        private void FrmMovie_Load(object sender, EventArgs e)
        {
            CategoryList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Movie movie = new Movie();
            movie.MovieTitle = txtMovieTitle.Text;
            movie.Duration = int.Parse(txtDuration.Text);
            movie.Description = txtDescription.Text;
            movie.CreatedDate = DateTime.Parse(mskDate.Text);
            movie.CategoryId = int.Parse(cmbCategory.SelectedValue.ToString());
            context.Movies.Add(movie);
            context.SaveChanges();
            MessageBox.Show("Movie Added Successfully!");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var values = context.Movies.Where(x=>x.MovieTitle==txtMovieTitle.Text).ToList();
            DGVMovie.DataSource = values;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtMovieId.Text);
            var value = context.Movies.Find(id);
            value.MovieTitle = txtMovieTitle.Text;
            value.Duration = int.Parse(txtDuration.Text);
            value.Description = txtDescription.Text;
            value.CategoryId = int.Parse(cmbCategory.SelectedValue.ToString());
            value.CreatedDate = DateTime.Parse(mskDate.Text);
            context.SaveChanges();
            MessageBox.Show("Movie Updated Successfully!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtMovieId.Text);
            var value = context.Movies.Find(id);
            context.Movies.Remove(value);
            context.SaveChanges();
            MessageBox.Show("Movie Deleted Successfully!");
        }

        private void btnListCat_Click(object sender, EventArgs e)
        {
            var values = context.Movies
                        .Join(context.Categories,       // 2 tablonun birlestirildigi kisim
                        movie => movie.CategoryId,      // Movies tablosunun degiskenine movie dedik
                        category => category.CategoryId,// Categories tablosunun degskne category dedik
                        (movie, category) => new        // ortak tablonun sütunları
                        {
                            MovieId = movie.MovieId,
                            MovieTitle = movie.MovieTitle,
                            Duration = movie.Duration,
                            Description = movie.Description,
                            CategoryName = category.CategoryName
                        }).ToList();
            DGVMovie.DataSource = values;
        }
    }
}
