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
using ToDo_Project.Forms;

namespace ToDo_Project.Forms
{
    public partial class MainForm : Form
    {
        private enum Status
        {
            Done, 
            InProgress
        }

        // Global Vars
        private static int id;
        private static string title;
        private static string desc;
        private static DateTime date;
        private static string status;
        private static Tasks task = new Tasks();
        public MainForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataView.CurrentRow.Index != -1)
            {
                task.TaskID = Convert.ToInt32(dataView.CurrentRow.Cells["TaskID"].Value);
                labelID.Text = task.TaskID.ToString();

                using (TasksModel db = new TasksModel())
                {
                    task = db.Tasks.Where(x => x.TaskID == task.TaskID).FirstOrDefault();
                    id = task.TaskID;
                    title = task.TaskTitle;
                    desc = task.TaskDescription;
                    date = task.TaskDate;
                    status = task.TaskStatus;
                }

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (TasksModel db = new TasksModel())
            {
                using (Add form = new Add())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                    }
                }
            }
        }

        private async void SaveChanges()
        {
            try
            {
                using (TasksModel db = new TasksModel())
                {
                    int res = await db.SaveChangesAsync();
                    if (res > 0)
                    {
                        MessageBox.Show("Save succesfully", "ToDo");
                    }
                    else
                    {
                        MessageBox.Show("Error Saving", "ToDo");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error" + e.Message, "ToDo");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            using (TasksModel db = new TasksModel())
            {
                if (db.Tasks.ToList().Count == 0)
                {
                    db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Task', RESEED, 0)");
                    labelID.Text = "N\\A";
                }
                dataView.DataSource = db.Tasks.ToList<Tasks>();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                using (TasksModel db = new TasksModel())
                {
                    task = db.Tasks.Where(x => x.TaskID == id).FirstOrDefault();
                    using (Edit edit = new Edit())
                    {
                        edit.SetData(task.TaskTitle, task.TaskDescription, task.TaskDate, task.TaskStatus, task.TaskID);
                        if (edit.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                }
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                using (TasksModel db = new TasksModel())
                {
                    task = db.Tasks.Where(x => x.TaskID == id).FirstOrDefault();




                    db.Entry(task).State = System.Data.Entity.EntityState.Deleted;
                    if (db.SaveChanges() > 0)
                    {
                        MessageBox.Show("Save succesfully", "ToDo");
                    }
                }
            }
        }
    }
}
