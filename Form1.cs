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
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MovieContext context = new MovieContext();
        private void btnList_Click(object sender, EventArgs e)
        {
            var values = context.Categories.ToList();
            DGVCategory.DataSource = values;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.CategoryName = txtCatName.Text;
            context.Categories.Add(category);
            context.SaveChanges();
            MessageBox.Show("Mission Successfully");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCatId.Text);
            var value = context.Categories.Find(id); // id ile buldu.
            value.CategoryName = txtCatName.Text; // id sini buldugu degerin adını guncelledi.
            context.SaveChanges();
            MessageBox.Show("Category Updated Successfully!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCatId.Text);
            var value = context.Categories.Find(id);
            context.Categories.Remove(value);
            context.SaveChanges();
            MessageBox.Show("Category Deleted Successfully!");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var values = context.Categories.Where(x=>x.CategoryName==txtCatName.Text).ToList();
            DGVCategory.DataSource = values;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
