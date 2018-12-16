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
    public partial class Form2 : Form
    {
        private Offer Offer { get; }
        private Form1 _form1;
        private bool _startDateTextBox5Focus;
        private bool _endDateTextBox6Focus;


        public Form2()
        {
            InitializeComponent();
            Offer = new Offer();
            _startDateTextBox5Focus = false;
            _endDateTextBox6Focus = false;
        }

        public Form2(Form form)
        {
            InitializeComponent();
            _form1 = form as Form1;
            Offer = new Offer();
            _startDateTextBox5Focus = false;
            _endDateTextBox6Focus = false;
            if (_form1.UpdateFlag)
            {
                textBox1.Text = _form1.SelectedOffer.Title;
                textBox2.Text = _form1.SelectedOffer.Place;
                textBox3.Text = _form1.SelectedOffer.Purpose;
                textBox4.Text = _form1.SelectedOffer.BasePrice.ToString(CultureInfo.CurrentCulture);
                richTextBox1.Text = _form1.SelectedOffer.Content;
                textBox5.Text = _form1.SelectedOffer.Date.ToShortDateString();
                if (_form1.SelectedOffer.EndDate != null)
                    textBox6.Text = DateTime.Parse(_form1.SelectedOffer.EndDate.ToString()).ToShortDateString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Offer.Title = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Offer.Place = textBox2.Text;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Offer.Purpose = textBox3.Text;

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Offer.BasePrice = int.Parse(textBox4.Text);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            Offer.Content = richTextBox1.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Offer.Date = DateTime.Parse(textBox5.Text);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Offer.EndDate = DateTime.Parse(textBox6.Text);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Offer.Id = _form1.Catalog.Random.Next();
            _form1.Catalog.Offers.Add(Offer);
            _form1.listView1.Items.Add(new ListViewItem(_form1.Catalog.offerListViewBody(Offer)));
        }

        private void textBox6_OnMouseClick(object sender, MouseEventArgs e)
        {
            _endDateTextBox6Focus = true;
            _startDateTextBox5Focus = false;

            monthCalendar1.Visible = !monthCalendar1.Visible;

        }

        private void textBox5_OnMouseClick(object sender, MouseEventArgs e)
        {
            _startDateTextBox5Focus = true;
            _endDateTextBox6Focus = false;

            monthCalendar1.Visible = !monthCalendar1.Visible;

        }



        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            var startDate = monthCalendar1.SelectionStart.ToShortDateString();
            var endDate = monthCalendar1.SelectionEnd.ToShortDateString();

            if (DateTime.Parse(startDate) < DateTime.Parse(endDate) || _startDateTextBox5Focus)
                textBox5.Text = monthCalendar1.SelectionStart.ToShortDateString();
            if (DateTime.Parse(startDate) < DateTime.Parse(endDate) || _endDateTextBox6Focus)
                textBox6.Text = monthCalendar1.SelectionEnd.ToShortDateString();

        }
    }
}
