using System;
using System.Globalization;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private readonly Form1 _form1;
        private bool _endDateTextBox6Focus;
        private bool _startDateTextBox5Focus;


        public Form2()
        {
            InitializeComponent();
            Offer = new Offer();
            _startDateTextBox5Focus = false;
            _endDateTextBox6Focus = false;
        }

        public Form2(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
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
                startDateTextBox5.Text = _form1.SelectedOffer.Date.ToShortDateString();
                if (_form1.SelectedOffer.EndDate != null)
                    endDateTextBox6.Text = DateTime.Parse(_form1.SelectedOffer.EndDate.ToString()).ToShortDateString();
                Offer = _form1.SelectedOffer;
            }

            button1.Text = _form1.UpdateFlag ? "Upadate" : "Create";
        }

        private Offer Offer { get; }


        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Offer.Title = textBox1.Text;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            Offer.Place = textBox2.Text;
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            Offer.Purpose = textBox3.Text;
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
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

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            Offer.Content = richTextBox1.Text;
        }

        private void StartDateTextBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Offer.Date = DateTime.Parse(startDateTextBox5.Text);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void EndDateTextBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Offer.EndDate = DateTime.Parse(endDateTextBox6.Text);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void EndDateTextBox6_OnMouseClick(object sender, MouseEventArgs e)
        {
            _endDateTextBox6Focus = true;
            _startDateTextBox5Focus = false;

            monthCalendar1.Visible = !monthCalendar1.Visible;
        }

        private void StartDateTextBox5_OnMouseClick(object sender, MouseEventArgs e)
        {
            _startDateTextBox5Focus = true;
            _endDateTextBox6Focus = false;

            monthCalendar1.Visible = !monthCalendar1.Visible;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!_form1.UpdateFlag)
            {
                Offer.Id = _form1.Catalog.Random.Next();
                _form1.Catalog.Offers.Add(Offer);
                _form1.listView1.Items.Add(new ListViewItem(_form1.Catalog.OfferListViewBody(Offer)));
            }
            else
            {
                _form1.SelectedOffer = Offer;
            }
        }


        private void MonthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            var startDate = monthCalendar1.SelectionStart.ToShortDateString();
            var endDate = monthCalendar1.SelectionEnd.ToShortDateString();

            if (DateTime.Parse(startDate) < DateTime.Parse(endDate) || _startDateTextBox5Focus)
                startDateTextBox5.Text = monthCalendar1.SelectionStart.ToShortDateString();
            if (DateTime.Parse(startDate) < DateTime.Parse(endDate) || _endDateTextBox6Focus)
                endDateTextBox6.Text = monthCalendar1.SelectionEnd.ToShortDateString();
        }
    }
}