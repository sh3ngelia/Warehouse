using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warehouse.App
{
    public partial class ContractsViewForm : Form
    {
        public ContractsViewForm(string title, object dataSource)
        {
            InitializeComponent();
            this.Text = title;
            dataGridView1.DataSource = dataSource;
            dataGridView1.Dock = DockStyle.Fill;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }

    }
}
