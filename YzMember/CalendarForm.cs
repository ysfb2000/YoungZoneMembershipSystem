using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YzMember
{
    public partial class CalendarForm : Form
    {
        public CalendarForm()
        {
            InitializeComponent();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            label1.Text = monthCalendar1.SelectionStart.Date.ToShortDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YzMemberClass1.TAdministrator.dtime = this.monthCalendar1.SelectionStart;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            YzMemberClass1.TAdministrator.dtime = DateTime.Parse("1900-1-1");
            this.Close();
        }

    }
}
