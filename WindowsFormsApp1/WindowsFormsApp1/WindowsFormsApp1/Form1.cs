using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Catalog Catalog;
        public Offer SelectedOffer;
        public bool UpdateFlag;

        public Form1()
        {
            InitializeComponent();
            Catalog = new Catalog();
            ListViewInit();
        }

        public void ListViewInit()
        {
            listView1.Items.AddRange(Catalog.ReadOffers().ToArray());
            listView1.Columns.Add("Title", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Place", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Price", -2, HorizontalAlignment.Left);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
                return;
            var item = listView1.SelectedItems[0];
            PrintOffer(Catalog.GetOfferById(int.Parse(item.SubItems[item.SubItems.Count - 1].Text)));
        }


        private void PrintOffer(Offer offer)
        {
            label1.Text = offer.Title;
            label2.Text = offer.Place;
            label3.Text = offer.Purpose;
            richTextBox1.Text = offer.Content;
            label4.Text = offer.BasePrice.ToString(CultureInfo.CurrentCulture);
            label5.Text = offer.Date.ToShortDateString();
            label6.Text = offer.EndDate != null
                ? DateTime.Parse(offer.EndDate.ToString()).ToShortDateString()
                : "None";
            label7.Text = offer.Id.ToString();
        }

        private void listView1_OnMouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0)
            {
                SelectedOffer = Catalog.GetOfferById(
                   int.Parse(listView1.SelectedItems[0].SubItems[listView1.SelectedItems[0].SubItems.Count - 1].Text)
                   );
                var contextMenu = new ContextMenuStrip();
                contextMenu.Items.Add("Add Offer");
                contextMenu.Items.Add("Update Offer");
                contextMenu.Items.Add("Delete Offer");
                contextMenu.Show(listView1.PointToScreen(e.Location));
                contextMenu.Items[0].Click += (s, args) =>
                {
                    UpdateFlag = false;
                    var form2 = new Form2(this);
                    form2.Show();
                };

                contextMenu.Items[1].Click += (a, args) =>
                {
                    UpdateFlag = true;
                    var form2 = new Form2(this);
                    form2.Show();
                };

                contextMenu.Items[2].Click += (a, args) =>
                {
                    if (SelectedOffer == null)
                        return;
                    var messageBoxResult = MessageBox.Show(
                        "Are You sure to delete '" + SelectedOffer.Title + "' offer with ID: " + SelectedOffer.Id,
                        "Offer deleting",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning);
                    if (messageBoxResult == DialogResult.OK)
                    {
                        Catalog.DeleteOfferBySelectedOffer(SelectedOffer);
                        listView1.SelectedItems[0].Remove();
                    }
                };

            }

            //SelectedOffer = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //todo: show new form -> prompt for details -> data validation -> show final offer -> payment -> 'return'
        }
    }
}
