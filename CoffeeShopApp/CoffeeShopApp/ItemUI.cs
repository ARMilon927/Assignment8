using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeShopApp.Model;
using CoffeeShopApp.Manager;

namespace CoffeeShopApp
{
    public partial class ItemUI : Form
    {
        public ItemUI()
        {
            InitializeComponent();
        }
        Item _item = new Item();
        ItemManager _itemManager = new ItemManager();
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ValidItem())
                return;
            _item.Name = itemNameTextBox.Text;
            _item.Id = -1;
            if (_itemManager.ExistItem(_item))
                MessageBox.Show("This name already exists");
            else
            {
                _item.Price = Convert.ToInt16(priceTextBox.Text);
                MessageBox.Show(_itemManager.InsertItem(_item));
            }
        }
        private bool ValidItem()
        {
            if (String.IsNullOrEmpty(itemNameTextBox.Text))
            {
                MessageBox.Show("Please enter Item name");
                return true;
            }
            if (String.IsNullOrEmpty(priceTextBox.Text))
            {
                MessageBox.Show("Please enter price of the item");
                return true;
            }
            return false;
        }
        private void showButton_Click(object sender, EventArgs e)
        {
            if (_itemManager.ShowItem().Rows.Count < 1)
                MessageBox.Show("No Data Found");
            itemDataGridView.DataSource = _itemManager.ShowItem();
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this item?", "Delete Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {
                _itemManager.DeleteItem(Convert.ToInt16(idLabel.Text));
            }
            itemDataGridView.DataSource = _itemManager.ShowItem();
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (ValidItem())
                return;
            _item.Name = itemNameTextBox.Text;
            _item.Id = Convert.ToInt16(idLabel.Text);
            if (_itemManager.ExistItem(_item))
                MessageBox.Show("This name already exists");
            else
            {
                _item.Price = Convert.ToInt16(priceTextBox.Text);
                if (_itemManager.UpdateItem(_item) > 0)
                    MessageBox.Show("Item is updated");
            }
            itemDataGridView.DataSource = _itemManager.ShowItem();
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(itemNameTextBox.Text))
                itemNameTextBox.Text = null;
            if (String.IsNullOrEmpty(priceTextBox.Text))
                priceTextBox.Text = "0";
            _item.Name = itemNameTextBox.Text;
            _item.Price = Convert.ToInt16(priceTextBox.Text);
            if (_itemManager.SearchItem(_item).Rows.Count < 1)
                MessageBox.Show("No data found");
            itemDataGridView.DataSource = _itemManager.SearchItem(_item);
        }
        private void itemDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updateButton.Enabled = true;
            deleteButton.Enabled = true;
            searchButton.Enabled = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.itemDataGridView.Rows[e.RowIndex];
                idLabel.Text = row.Cells[0].Value.ToString();
                itemNameTextBox.Text = row.Cells[1].Value.ToString();
                priceTextBox.Text = row.Cells[2].Value.ToString();
            }
        }
        private void itemNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsLetterOrDigit(e.KeyChar) || (e.KeyChar == (Char)Keys.Back) || Char.IsWhiteSpace(e.KeyChar)))
                e.Handled = true;
        }
        private void priceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (Char)Keys.Back)))
                e.Handled = true;
        }
    }
}
