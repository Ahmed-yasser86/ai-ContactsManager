using BussinesLiar;
using BussinesLierCountries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactsUserInterface
{
    public partial class Form2 : Form
    {

        int cID;
        private enum Mode {AddNew, Update }
        Conntacts _conntacts;
        Mode _FormMode;
        //public Form2(int ID)
        //{

        //    if (ID != -1)
        //    {
        //        ID=this.ID;
        //         _FormMode = Mode.Update;
        //    }
        //    else
        //    {
        //        this.ID = ID;
        //        _FormMode = Mode.AddNew;
        //    }

        //}



        public Form2(int ID)
        {

            cID=ID;
            InitializeComponent();
            if (ID != -1)
            {
              //  ID=this.ID;
                _FormMode = Mode.Update;
            }
            else
            {
                
                _FormMode = Mode.AddNew;
            }
        }


        private  void LoadContactsToCB()
        {

            DataTable dt = ClsCountries.RetriveAllCountries();

  

            foreach(DataRow x in dt.Rows)
            {

                comboBox1.Items.Add(x["CountryName"]);

            }

        }

       private void _loadData()
        {

            LoadContactsToCB();
            comboBox1.SelectedIndex = 0;    
          
            
            if (_FormMode == Mode.AddNew)
            {
                
                _conntacts = new Conntacts();
                label2.Text = "AddNew";
                return;
            }

            _conntacts = Conntacts.FindConatct(cID);

            if (_conntacts == null) {


                MessageBox.Show("it will be closed no contact curies this acc num");
                this.Close();

            }
            label2.Text = "UpdateNew";
            textContactID.Text = _conntacts.AccountNumber.ToString();
            textFirstName.Text = _conntacts.FirstName;
            textLastName.Text = _conntacts.LastName;
            textEmail.Text = _conntacts.Email;
            TEXTphone.Text = _conntacts.Phone;




            comboBox1.SelectedIndex = comboBox1.FindString(ClsCountries.FindConatct(_conntacts.CountryID).CountryName);


        } 



        private void Form2_Load(object sender, EventArgs e)
        {
            _loadData();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            
            int CId = ClsCountries.FindConatctByname(comboBox1.Text.ToString()).CountryID;
          //  _conntacts.AccountNumber = ID;
            _conntacts.FirstName = textFirstName.Text;
            _conntacts.LastName = textLastName.Text;
            _conntacts.Email = textEmail.Text;
            _conntacts.Phone = TEXTphone.Text;
            _conntacts.CountryID = CId;

            _FormMode = Mode.Update;
            label2.Text = "edit mode";
            if (_conntacts.save())
            {
                MessageBox.Show("Yes succ");
            }
            else
            {

                MessageBox.Show("no not succ");

            }
        }
    }
}
