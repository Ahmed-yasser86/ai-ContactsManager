using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinesLiar;
using BussinesLierCountries;

namespace ContactsUserInterface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {

            dataGridView1.DataSource = Conntacts.GetTable();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           Form2 FRM2= new Form2(-1);
            FRM2.ShowDialog();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show();
        }

        private void editeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2((int)dataGridView1.CurrentRow.Cells[0].Value);
            form2.ShowDialog();

        }

        private void dELETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Conntacts.DeletContact((int)dataGridView1.CurrentRow.Cells[0].Value);
        }
    }
}
