using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeShopApp.Manager;
using CoffeeShopApp.Model;

namespace CoffeeShopApp
{
    public partial class OrderUI : Form
    {
        public OrderUI()
        {
            InitializeComponent();
        }
        OrderManager _orderManager = new OrderManager();
        Order _order = new Order();
        private void OrderUI_Load(object sender, EventArgs e)
        {
            customerComboBox.DataSource = _orderManager.GetCustomer();
            customerComboBox.SelectedIndex = -1;
            customerComboBox.Text = "--Select--";
            itemComboBox.DataSource = _orderManager.GetItem();
            itemComboBox.SelectedIndex = -1;
            itemComboBox.Text = "--Select--";
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            if(ValidOrder())
                return;
            _order.CustomerId = Convert.ToInt16(customerComboBox.SelectedValue);
            _order.ItemId = Convert.ToInt16(itemComboBox.SelectedValue);
            _order.Quantity = Convert.ToInt16(quantityTextBox.Text);
            int price = _orderManager.GetItemPrice(Convert.ToInt16(itemComboBox.SelectedValue));
            _order.TotalPrice = _order.Quantity * price;
            totalPriceTextBox.Text = _order.TotalPrice.ToString();
            if (_orderManager.InsertOrder(_order))
            {
                MessageBox.Show("Order is Saved");
            }
            else
            {
                MessageBox.Show("Order is not Saved");
            }
            ClearInput();
        }
        private void ClearInput()
        {
            totalPriceTextBox.Clear();
            customerComboBox.SelectedIndex = -1;
            customerComboBox.Text = "--Select--";
            itemComboBox.SelectedIndex = -1;
            itemComboBox.Text = "--Select--";
            quantityTextBox.Clear();
        }

        private bool ValidOrder()
        {
            if (customerComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Please select Customer Name");
                return true;
            }
            if (itemComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Please select Item Name");
                return true;
            }
            if (String.IsNullOrEmpty(quantityTextBox.Text))
            {
                MessageBox.Show("Please enter quantity");
                return true;
            }
            return false;
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            if (_orderManager.ShowOrder().Rows.Count < 0)
                MessageBox.Show("No data found");
            orderDataGridView.DataSource = _orderManager.ShowOrder();
            ClearInput();
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (ValidOrder())
                return;
            _order.CustomerId = Convert.ToInt16(customerComboBox.SelectedValue);
            _order.ItemId = Convert.ToInt16(itemComboBox.SelectedValue);
            _order.Quantity = Convert.ToInt16(quantityTextBox.Text);
            int price = _orderManager.GetItemPrice(Convert.ToInt16(itemComboBox.SelectedValue));
            _order.TotalPrice = _order.Quantity * price;
            _order.Id = Convert.ToInt16(idLabel.Text);
            if (_orderManager.UpdateOrder(_order))
                MessageBox.Show("Order is updated");
            orderDataGridView.DataSource = _orderManager.ShowOrder();
            ClearInput();
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (ValidOrder())
                return;
            _order.Id = Convert.ToInt16(idLabel.Text);
            if (MessageBox.Show("Do you want to delete this item?", "Delete Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {
                if(_orderManager.DeleteItem(_order.Id))
                    MessageBox.Show("Order is deleted successfully");
            }
            orderDataGridView.DataSource = _orderManager.ShowOrder();   
            ClearInput();
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (customerComboBox.SelectedIndex < 0)
            {
                customerComboBox.SelectedValue = "-1";
            }
            if (itemComboBox.SelectedIndex < 0)
            {
                itemComboBox.Text = "-1";
            }
            if (String.IsNullOrEmpty(quantityTextBox.Text))
            {
                quantityTextBox.Text = "0";
            }
            string customer = customerComboBox.Text;
            string item = itemComboBox.Text;
            int quantity = Convert.ToInt16(quantityTextBox.Text);
            orderDataGridView.DataSource = _orderManager.SearchOrder(customer, item, quantity);
        }
        private void orderDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearInput();
            updateButton.Enabled = true;
            deleteButton.Enabled = true;
            searchButton.Enabled = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.orderDataGridView.Rows[e.RowIndex];
                idLabel.Text = row.Cells[0].Value.ToString();
                customerComboBox.Text = row.Cells[1].Value.ToString();
                itemComboBox.Text = row.Cells[2].Value.ToString();
                quantityTextBox.Text = row.Cells[3].Value.ToString();
                totalPriceTextBox.Text = row.Cells[5].Value.ToString();
            }
        }
        private void quantityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (Char)Keys.Back)))
                e.Handled = true;
        }
    }
}
