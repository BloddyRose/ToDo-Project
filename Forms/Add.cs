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
    public partial class Add : Form
    {
        public Add()
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
                TaskTitle = txtTitle.Text,
                TaskDescription = txtDesc.Text,
                TaskDate = datePicker.Value,
                TaskStatus = selectedValue
            };

            using (TasksModel db = new TasksModel())
            {
                db.Tasks.Add(t);
                if (db.SaveChanges() > 0)
                {
                    MessageBox.Show("Save succesfully", "ToDo");
                    this.Hide();
                }
            }
        }
    }

}
