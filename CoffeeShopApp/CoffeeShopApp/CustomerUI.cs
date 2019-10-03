using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeShopApp.Manager;
using CoffeeShopApp.Model;

namespace CoffeeShopApp
{
    public partial class CustomerUI : Form
    {
        private Customer _customer = new Customer();
        CustomerManager _customerManager = new CustomerManager();
        private int rowAffected;
        public CustomerUI()
        {
            InitializeComponent();
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ValidCustomer())
                return;
            _customer.Name = customerNameTextBox.Text;
            _customer.Id = -1;
            if (_customerManager.ExistCustomer(_customer))
            {
                MessageBox.Show("This name already exists");
                return;
            }
            _customer.Contact = contactTextBox.Text;
            _customer.Address = addressTextBox.Text;
            MessageBox.Show(_customerManager.InsertCustomer(_customer));
            ClearInput();
        }
        private bool ValidCustomer()
        {
            if (String.IsNullOrEmpty(customerNameTextBox.Text))
            {
                MessageBox.Show("Please enter Customer name");
                return true;
            }
            if (String.IsNullOrEmpty(contactTextBox.Text))
            {
                MessageBox.Show("Please enter a contact number");
                return true;
            }
            if (String.IsNullOrEmpty(addressTextBox.Text))
            {
                MessageBox.Show("Please enter address");
                return true;
            }
            return false;
        }
        private void showButton_Click(object sender, EventArgs e)
        {
            ClearInput();
            if (_customerManager.ShowCustomer().Rows.Count < 1)
                MessageBox.Show("No Data Found");
            customerDataGridView.DataSource = _customerManager.ShowCustomer();
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (ValidCustomer())
                return;
            _customer.Name = customerNameTextBox.Text;
            _customer.Id = Convert.ToInt16(idLabel.Text);
            _customer.Contact = contactTextBox.Text;
            _customer.Address = addressTextBox.Text;
            if (_customerManager.ExistCustomer(_customer))
                MessageBox.Show("This name already exists");
            else
            {
                if (_customerManager.UpdateCustomer(_customer))
                {
                    MessageBox.Show("Customer updated");
                    ClearInput();
                }
                customerDataGridView.DataSource = _customerManager.ShowCustomer();
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this customer?", "Delete Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (_customerManager.DeleteCustomer(Convert.ToInt16(idLabel.Text)))
                    MessageBox.Show("Customer is deleted successfully");
                customerDataGridView.DataSource = _customerManager.ShowCustomer();
            }
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(customerNameTextBox.Text))
                customerNameTextBox.Text = null;
            if (String.IsNullOrEmpty(addressTextBox.Text))
                addressTextBox.Text = null;
            if (String.IsNullOrEmpty(contactTextBox.Text))
                contactTextBox.Text = null;
            _customer.Name = customerNameTextBox.Text;
            _customer.Address = addressTextBox.Text;
            _customer.Contact = contactTextBox.Text;
            if (_customerManager.SearchCustomer(_customer).Rows.Count < 1)
                MessageBox.Show("No Data Found");
            customerDataGridView.DataSource = _customerManager.SearchCustomer(_customer);
        }
        private void ClearInput()
        {
            customerNameTextBox.Clear();
            contactTextBox.Clear();
            addressTextBox.Clear();
        }
        private void customerDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updateButton.Enabled = true;
            deleteButton.Enabled = true;
            searchButton.Enabled = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.customerDataGridView.Rows[e.RowIndex];
                idLabel.Text = row.Cells[0].Value.ToString();
                customerNameTextBox.Text = row.Cells[1].Value.ToString();
                contactTextBox.Text = row.Cells[2].Value.ToString();
                addressTextBox.Text = row.Cells[3].Value.ToString();
            }
        }
        private void customerNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
           if (!(Char.IsLetterOrDigit(e.KeyChar) || (e.KeyChar == (Char)Keys.Back) || Char.IsWhiteSpace(e.KeyChar)))
            e.Handled = true;
        }
        private void contactTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (Char)Keys.Back)))
                e.Handled = true;
        }
    }
}
