using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        private readonly List<CheckBox> _checkBoxs = new List<CheckBox>();

        private readonly string[] _chronics =
        {
            "None",
            "Chronic 1",
            "Chronic 2",
            "Chronic 3",
            "Chronic 4",
            "Chronic 5",
            "Chronic 6"
        };

        private readonly Form1 _form1;

        private bool _button1ClickedFlag;
        public int LastIndex;

        public Form3()
        {
            InitializeComponent();
            CheckBoxesListInit();
            button1.Location = new Point(_checkBoxs.Last().Location.X, _checkBoxs.Last().Location.Y + 23);
        }

        public Form3(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
            CheckBoxesListInit();
            button1.Location = new Point(_checkBoxs.Last().Location.X, _checkBoxs.Last().Location.Y + 23);
            for (var i = 1; i < form1.Clients.Length; i++)
            {
                var tp = new TabPage
                {
                    Location = new Point(4, 22),
                    Padding = new Padding(3),
                    Size = new Size(345, 233),
                    TabIndex = 1 + i,
                    Text = "tabPage" + (i + 1),
                    Name = "tabPage" + (i + 1),
                    UseVisualStyleBackColor = true,
                    AutoScroll = true
                };
                tabControl1.Controls.Add(tp);
            }

            for (var i = 0; i < form1.Clients.Length; i++)
            {
                tabControl1.SelectedIndex = i;
                SaveToClient(i);
                TabBuilder(tabControl1.SelectedTab);
                CheckBuilder(LastIndex = tabControl1.SelectedIndex);
            }
        }

        //            var i = -1;
        //            foreach (var item in _chronics)
        //                i = CheckBoxInit(item, i);
        private void CheckBoxesListInit()
        {
            _chronics.Aggregate(-1, (current, item) => CheckBoxInit(item, current));
        }

        private int CheckBoxInit(string text, int index)
        {
            var checkBox = new CheckBox
            {
                AutoSize = true,
                Location = new Point(7, 7 + ++index * 23),
                Name = "checkBox1",
                Size = new Size(52, 17),
                TabIndex = index,
                Text = text,
                UseVisualStyleBackColor = true
            };

            checkBox.CheckedChanged += (sender, args) =>
            {
                var flag = false;
                if (checkBox == _checkBoxs.First())
                {
                    for (var j = 1; j < _checkBoxs.Count; j++)
                        _checkBoxs[j].Enabled = !checkBox.Checked;
                }
                else
                {
                    for (var j = 1; j < _checkBoxs.Count; j++)
                    {
                        if (_checkBoxs[j].Checked)
                        {
                            flag = false;
                            break;
                        }

                        flag = true;
                    }

                    _checkBoxs.First().Checked = flag;
                }
            };

            if (index != 0)
            {
                var cms = new ContextMenuStrip {Items = {"Delete"}};
                cms.ItemClicked += (sender, args) =>
                {
                    _checkBoxs.Remove(checkBox);
                    checkBox.Dispose();
                    LocationReBuilder();
                };
                checkBox.ContextMenuStrip = cms;

                checkBox.MouseClick += (sender, args) =>
                {
                    if ((args.Button & MouseButtons.Right) != 0)
                        checkBox.ContextMenuStrip.Show(checkBox.PointToScreen(args.Location));
                };
            }

            _checkBoxs.Add(checkBox);
            return index;
        }

        public void SaveToClient(int index)
        {
            var toClientList = new List<string>();
            foreach (var box in _checkBoxs)
                if (box.Checked)
                    toClientList.Add(box.Text);
            _form1.Clients[index].ChronicDesieses = toClientList;
        }


        private void CheckBuilder(int index)
        {
            if (!_form1.Clients[index].ChronicDesieses.Any())
            {
                _checkBoxs[0].Checked = true;
                return;
            }

            foreach (var checkBox in _checkBoxs)
                checkBox.Checked = _form1.Clients[index].ChronicDesieses.Contains(checkBox.Text);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!_button1ClickedFlag)
            {
                textBox1.Location = new Point(89, button1.Location.Y);
                textBox1.Visible = true;
                button1.Text = "Add";
                _button1ClickedFlag = true;
            }
            else
            {
                textBox1.Visible = false;
                button1.Text = "Other";
                _button1ClickedFlag = false;
                _checkBoxs[CheckBoxInit(textBox1.Text, _checkBoxs.Count - 1)].Checked = true;
                tabControl1.SelectedTab.Controls.Clear();
                LocationReBuilder();
                TabBuilder(tabControl1.SelectedTab);
            }
        }

        private void LocationReBuilder()
        {
            var i = -1;
            foreach (var checkBox in _checkBoxs)
                checkBox.Location = new Point(7, 7 + ++i * 23);
            button1.Location = new Point(7, 7 + ++i * 23);
        }

        private void TabBuilder(TabPage eTabPage)
        {
            try
            {
                // ReSharper disable once CoVariantArrayConversion
                eTabPage.Controls.AddRange(_checkBoxs.ToArray());
                eTabPage.Controls.AddRange(new Control[] {button1, textBox1});
            }
            catch (Exception)
            {
                //
            }
        }

        private void TabControl1Selected(object sender, TabControlEventArgs e)
        {
            CheckBuilder(LastIndex = e.TabPageIndex);
            TabBuilder(e.TabPage);
        }

        private void BeforeTabControl1Selected(object sender, TabControlCancelEventArgs e)
        {
            SaveToClient(LastIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}