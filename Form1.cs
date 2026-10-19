using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG271._281_PROJ
{
    public partial class Form1 : Form
    {

        FileHandler files = new FileHandler();
            SummaryLogic summary = new SummaryLogic();

            Label lblTotalValue;
            Label lblAvgAgeValue;
            Label lblAvgScoreValue;
            ListBox lstStatus;
            Button btnSummary;
            
        public Form1()
        {
            InitializeComponent();
            btnEditDone.Enabled = false;
            btnCheck.Enabled = false;
            BusinessLogic logic = new BusinessLogic();

            SetUpSummaryControls();
            ShowSummary();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            
            
               
                btnCheck.Enabled = true; 
                int indexRow;
                indexRow = dgvView.CurrentRow.Index;
                DataGridViewRow row = dgvView.Rows[indexRow];

                txtID.Text = row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString();
                txtSpecies.Text = row.Cells[2].Value.ToString();
                txtAge.Text = row.Cells[3].Value.ToString();
                txtScore.Text = "Leave Blank";
                txtStatus.Text = "leave Blank";
                txtUnit.Text = "leave Blank";
            
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            ////waiting.....
            Thread.Sleep(1000);
            BusinessLogic logic = new BusinessLogic();
           if( logic.ConditionsMet(txtID.Text,txtName.Text,txtSpecies.Text,txtAge.Text))
            { btnEditDone.Enabled = true;
                MessageBox.Show("ALL ENTERED DATA IS VALID");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvView.CurrentRow;
            if (row == null || row.Cells[0].Value == null)
            {
                MessageBox.Show("Please select an animal in the table first.");
                return;
            }

            string id = Convert.ToString(row.Cells[0].Value);
            string name = Convert.ToString(row.Cells[1].Value);

            DialogResult answer = MessageBox.Show(
                "Are you sure you want to delete this animal?\n\nID: " + id + "\nName: " + name,
                "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer == DialogResult.No)
            {
                return;
            }

            answer = MessageBox.Show(
                "FINAL WARNING: " + name + " will be permanently deleted. Continue?",
                "Are you absolutely sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2);
            if (answer == DialogResult.No)
            {
                return;
            }

            if (files.DeleteAnimal(id))
            {
                dgvView.Rows.Remove(row);
                ShowSummary();
                MessageBox.Show("Animal " + id + " was deleted.");
            }
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            ShowSummary();

            if (files.SaveSummary(summary.MakeReportText()))
            {
                MessageBox.Show("Summary saved to:\n" + System.IO.Path.GetFullPath(files.summaryFile));
            }
        }

        private void ShowSummary()
        {
            summary.Calculate(files.ReadAnimals());

            lblTotalValue.Text = summary.totalAnimals.ToString();
            lblAvgAgeValue.Text = summary.averageAge.ToString("0.0");
            lblAvgScoreValue.Text = summary.averageScore.ToString("0.0");

            lstStatus.Items.Clear();
            foreach (KeyValuePair<string, int> item in summary.statusCounts)
            {
                lstStatus.Items.Add(item.Key + ": " + item.Value);
            }
        }

        private void SetUpSummaryControls()
        {
            this.ClientSize = new Size(816, 590);
            dgvView.AllowUserToDeleteRows = false;

            MakeLabel("Total animals", 12, 455);
            MakeLabel("Average age", 12, 485);
            MakeLabel("Average recovery score", 12, 515);
            MakeLabel("Animals per status", 300, 455);

            lblTotalValue = MakeLabel("0", 190, 455);
            lblAvgAgeValue = MakeLabel("0", 190, 485);
            lblAvgScoreValue = MakeLabel("0", 190, 515);

            lstStatus = new ListBox();
            lstStatus.Location = new Point(300, 475);
            lstStatus.Size = new Size(280, 95);
            this.Controls.Add(lstStatus);

            btnSummary = new Button();
            btnSummary.Text = "SUMMARY";
            btnSummary.Location = new Point(610, 475);
            btnSummary.Size = new Size(100, 30);
            btnSummary.Click += btnSummary_Click;
            this.Controls.Add(btnSummary);

            btnDelete.Click += btnDelete_Click;
        }

        private Label MakeLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            this.Controls.Add(lbl);
            return lbl;
        }
    }
 
}
