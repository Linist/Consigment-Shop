using ConsignmentShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartData = new List<Item>();
        private decimal storeProfit = 0;

        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        BindingSource vendorsBinding = new BindingSource();

        public ConsignmentShop() /*Constructor = dont have a return type*/
        {
            InitializeComponent();
            SetupData();

            /*this is the link betwwen the store item and the itembinding.datasource*/
            itemsBinding.DataSource = store.Items.Where( x=> x.Sold == false ).ToList();    /*a great filter to not add sold items*/
            itemListbox.DataSource = itemsBinding;

            /*what will be show in the itemslistbox?*/
            itemListbox.DisplayMember = "Display";
            itemListbox.ValueMember = "Display";

            /*cartbinding is sitting between a listbox*/
            cartBinding.DataSource = shoppingCartData;
            shoppingCartListbox.DataSource = cartBinding;

            shoppingCartListbox.DisplayMember = "Display";
            shoppingCartListbox.ValueMember = "Display";


            vendorsBinding.DataSource = store.Vendors;
            vendorlistBox.DataSource = vendorsBinding;

            vendorlistBox.DisplayMember = "Display";
            vendorlistBox.ValueMember = "Display";
        }

        private void SetupData()
        {

            /*THIS IS THE QUICK WAY*/
            /*in this line we make a new instans and then pupilate it at the same time and the add it to our Vendorlist*/
            store.Vendors.Add(new Vendor { FirstName = "Bill", LastName = "Smith" });
            store.Vendors.Add(new Vendor { FirstName = "Sue", LastName = "Jones" });


            store.Items.Add(new Item
            {
                Tittle = "Moby Dick",
                Description = "A book about a whale",
                Price = 4.50M,
                Owner = store.Vendors[0]
            });

            store.Items.Add(new Item
            {
                Tittle = "A tale of Two Cities",
                Description = "A book about a revolution",
                Price = 3.80M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Tittle = "Harry Potter Book 1",
                Description = "A book about a boy",
                Price = 4.50M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Tittle = "Jane Eyre",
                Description = "A book about a girl",
                Price = 1.50M,
                Owner = store.Vendors[0]
            });

            store.Name = "Seconds are Better";
        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            // Figure out what is selected from the items list
            //Copy that item to the shopping cart
            //Do we remove the item from the items list? - no

            // we need to know if its the type = item
            Item selectedItem = (Item)itemListbox.SelectedItem;

            //MessageBox.Show(selectedItem.Tittle);

            shoppingCartData.Add(selectedItem);

            cartBinding.ResetBindings(false);
        }

        private void makePurchase_Click(object sender, EventArgs e)
        {
            // Mark each item in the cart as sold
            // Clear the cart

            //mark each item
            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += (decimal)item.Owner.Commission * item.Price;   //vendors profit
                storeProfit += (1 - (decimal)item.Owner.Commission) * item.Price;       //store profit
            }

            //clear the data
            shoppingCartData.Clear();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();    /*a great filter to not add sold items*/

            storeProfitValue.Text = string.Format("${0}", storeProfit);

            // ressed the carbindings
            cartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorsBinding.ResetBindings(false);
        }
    }
}
