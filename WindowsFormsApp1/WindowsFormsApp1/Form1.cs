using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, bool> _selectFlags = new Dictionary<string, bool>
        {
            {"start date", false},
            {"end date", false},
            {"nop", false}
        };

        private bool _button1Flag;
        private bool _endDateTextBox10Focus;

        private bool _startDateTextBox9Focus;
        public Catalog Catalog;
        public Client[] Clients;
        public Order Order;
        public Offer SelectedOffer;
        public bool UpdateFlag;


        public Form1()
        {
            InitializeComponent();
            Catalog = new Catalog();
            Order = new Order();
            ListViewInit();

            monthCalendar1.MinDate = DateTime.Today;

            AmountUpdate();
        }

        public void ListViewInit()
        {
            listView1.Items.AddRange(Catalog.ReadOffers().ToArray());
            listView1.Columns.Add("Title", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Place", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Price", -2, HorizontalAlignment.Left);
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
                return;
            var item = listView1.SelectedItems[0];
            PrintOffer(Catalog.GetOfferById(int.Parse(item.SubItems[item.SubItems.Count - 1].Text)));

            groupBox9.Visible = groupBox10.Visible = groupBox11.Visible = groupBox12.Visible = button1.Visible = true;
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

        private void ListView1_OnMouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) == 0) return;
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
                if (messageBoxResult != DialogResult.OK) return;
                Catalog.DeleteOfferFromList(SelectedOffer);
                listView1.SelectedItems[0].Remove();
            };
        }

        private void StartDateTextBox9_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var startDate = DateTime.Parse(startDateTextBox9.Text);
                if (startDate >= monthCalendar1.MinDate && startDate < monthCalendar1.MaxDate)
                {
                    Order.Departure = startDate;
                    startDateTextBox9.BackColor = Color.LightGreen;
                    _selectFlags["start date"] = true;
                    ButtonEnable();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                startDateTextBox9.BackColor = Color.LightCoral;
                _selectFlags["start date"] = false;
                ButtonEnable();
            }
        }

        private void StartDateTextBox9_OnMouseClick(object sender, MouseEventArgs e)
        {
            _startDateTextBox9Focus = true;
            _endDateTextBox10Focus = false;

            monthCalendar1.Visible = !monthCalendar1.Visible;
        }

        private void StartDateTextBox9_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            startDateTextBox9.BackColor = Color.LightCoral;
            _selectFlags["start date"] = false;
            ButtonEnable();
        }

        private void EndDateTextBox10_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var endDate = DateTime.Parse(endDateTextBox10.Text);
                if (endDate > monthCalendar1.MinDate && endDate <= monthCalendar1.MaxDate)
                {
                    Order.Homecomming = endDate;
                    endDateTextBox10.BackColor = Color.LightGreen;
                    _selectFlags["end date"] = true;
                    ButtonEnable();
                    AmountUpdate(beta: DateAmount());
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                endDateTextBox10.BackColor = Color.LightCoral;
                _selectFlags["end date"] = false;
                ButtonEnable();
            }
        }

        private void EndDateTextBox10_OnMouseClick(object sender, MouseEventArgs e)
        {
            _endDateTextBox10Focus = true;
            _startDateTextBox9Focus = false;

            monthCalendar1.Visible = !monthCalendar1.Visible;
        }

        private void EndDateTextBox10_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            endDateTextBox10.BackColor = Color.LightCoral;
            _selectFlags["end date"] = false;
            ButtonEnable();
        }


        private void MonthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            var startDate = monthCalendar1.SelectionStart.ToShortDateString();
            var endDate = monthCalendar1.SelectionEnd.ToShortDateString();

            if (DateTime.Parse(startDate) < DateTime.Parse(endDate) || _startDateTextBox9Focus)
                startDateTextBox9.Text = monthCalendar1.SelectionStart.ToShortDateString();
            if (DateTime.Parse(startDate) < DateTime.Parse(endDate) || _endDateTextBox10Focus)
                endDateTextBox10.Text = monthCalendar1.SelectionEnd.ToShortDateString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!_button1Flag)
            {
                var form3 = new Form3(this);
                form3.Closing += (o, args) =>
                {
                    form3.SaveToClient(form3.LastIndex);
                    AmountUpdate(ChronicAmount(), DateAmount());
                };
                form3.Show();
                _button1Flag = true;
                button1.Text = "Buy";
            }
            else
            {
                if (_button1Flag = MessageBox.Show(
                                       "Confirm payment?",
                                       "Payment",
                                       MessageBoxButtons.OKCancel,
                                       MessageBoxIcon.Question,
                                       MessageBoxDefaultButton.Button1) == DialogResult.OK)
                    MessageBox.Show(
                        "Thank You",
                        "Payment Confirmed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);

                button1.Text = "Select";
            }
        }

        private void ButtonEnable()
        {
            foreach (var flag in _selectFlags)
                if (!flag.Value)
                    return;
            button1.Enabled = true;
        }

        private void AmountUpdate(double alpha = 1, double beta = 1)
        {
            try
            {
                label8.Text =
                    (double.Parse(label4.Text) * int.Parse(numberComboBox1.Text) * alpha * beta).ToString(CultureInfo
                        .CurrentCulture);
                label9.Text =
                    (double.Parse(label8.Text) / int.Parse(numberComboBox1.Text)).ToString(CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                //
            }
        }

        private double ChronicAmount()
        {
            return 1.0 + (from client in Clients
                       from chronic in client.ChronicDesieses
                       where chronic != "None"
                       select 0.01).Sum();
        }

        private double DateAmount()
        {
            return (Order.Homecomming - Order.Departure).TotalDays;
        }

        private void NumberComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Clients = new Client[Order.PersonCount = int.Parse(numberComboBox1.Text)];
                for (var i = 0; i < int.Parse(numberComboBox1.Text); i++)
                    Clients[i] = new Client();
                _selectFlags["nop"] = true;
                ButtonEnable();
                AmountUpdate();
                numberComboBox1.BackColor = Color.LightGreen;

            }
            catch (Exception)
            {
                numberComboBox1.BackColor = Color.LightCoral;
            }
        }
    }
}