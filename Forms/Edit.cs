using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDo_Project.Data;

namespace ToDo_Project.Forms
{
    public partial class Edit : Form
    {
        private int id;
        public Edit()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddData();

            
        }
        public void AddData()
        {
            int selectedIndex = cType.SelectedIndex;
            cType.SelectedItem.ToString();
            string selectedValue = cType.Items[selectedIndex].ToString();

            Tasks t = new Tasks
            {
                TaskID = id,
                TaskTitle = txtTitle.Text,
                TaskDescription = txtDesc.Text,
                TaskDate = datePicker.Value,
                TaskStatus = selectedValue
            };

            using (TasksModel db = new TasksModel())
            {
                try
                {

                    db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        MessageBox.Show("Save succesfully", "ToDo");
                        this.Hide();
                    }

                }
                catch (Exception)
                {
                    
                }
            }
        }

        public void SetData(string title, string desc, DateTime date, string status, int _id)
        {
            id = _id;
            txtDesc.Text = desc;
            txtTitle.Text = title;
            datePicker.Value = date;
            cType.SelectedItem = status;
        }
    }

}
