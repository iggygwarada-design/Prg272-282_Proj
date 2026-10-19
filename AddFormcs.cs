using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PRG271._281_PROJ
{
    public partial class AddFormcs : Form
    {
        private readonly string animalsFile = Path.Combine(Application.StartupPath, "animals.txt");

        // This event is raised after a record has been successfully added.
        // Form1 can use it to refresh its DataGridView.
        public event EventHandler AnimalAdded;

        public AddFormcs()
        {
            InitializeComponent();
            txtAnimalId.Focus();
        }

        private void btnAddAnimal_Click(object sender, EventArgs e)
        {
            // Trim every input before validating or saving it.
            string animalId = txtAnimalId.Text.Trim().ToUpper();
            string name = txtName.Text.Trim();
            string species = txtSpecies.Text.Trim();
            string ageText = txtAge.Text.Trim();
            string recoveryScoreText = txtRecoveryScore.Text.Trim();

            // Validate each field separately and focus the field with the problem.
            if (!ValidateAnimalId(animalId))
            {
                txtAnimalId.Focus();
                return;
            }

            if (AnimalIdAlreadyExists(animalId))
            {
                MessageBox.Show("Animal ID already exists. Please enter a unique Animal ID.",
                    "Invalid Animal ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAnimalId.Focus();
                return;
            }

            if (!ValidateTextField(name, "Name"))
            {
                txtName.Focus();
                return;
            }

            if (!ValidateTextField(species, "Species"))
            {
                txtSpecies.Focus();
                return;
            }

            if (!int.TryParse(ageText, out int age) || age < 0 || age > 100)
            {
                MessageBox.Show("Age must be a whole number between 0 and 100.",
                    "Invalid Age", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return;
            }

            if (!int.TryParse(recoveryScoreText, out int recoveryScore) ||
                recoveryScore < 0 || recoveryScore > 100)
            {
                MessageBox.Show("Recovery Score must be a whole number between 0 and 100.",
                    "Invalid Recovery Score", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRecoveryScore.Focus();
                return;
            }

            try
            {
                // Member 1's shared classification method must be used here.
                // Do not create a second classification method in this form.
                var classification = BusinessLogic.ClassifyRecoveryScore(recoveryScore);

                // Create the exact pipe-separated record required by the project brief.
                string record = string.Join("|", animalId, name, species, age,
                    recoveryScore, classification.Status, classification.HousingUnit);

                // Append keeps all existing animal records unchanged.
                File.AppendAllText(animalsFile, record + Environment.NewLine);

                MessageBox.Show(
                    "Animal added successfully.\n\n" +
                    "Status: " + classification.Status + "\n" +
                    "Housing Unit: " + classification.HousingUnit,
                    "Animal Added", MessageBoxButtons.OK, MessageBoxIcon.Information);

                AnimalAdded?.Invoke(this, EventArgs.Empty);
                ClearForm();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(
                    "The application does not have permission to write to animals.txt.",
                    "File Access Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show(
                    "The animal file could not be accessed. It may be in use or unavailable.",
                    "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The animal could not be added.\n\nDetails: " + ex.Message,
                    "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateAnimalId(string animalId)
        {
            if (string.IsNullOrWhiteSpace(animalId))
            {
                MessageBox.Show("Animal ID is required.", "Invalid Animal ID",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Required format: WR- followed by exactly four digits.
            if (animalId.Length != 7 ||
                !animalId.StartsWith("WR-") ||
                !animalId.Substring(3).All(char.IsDigit))
            {
                MessageBox.Show("Animal ID must be in the format WR-0000.",
                    "Invalid Animal ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool ValidateTextField(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                MessageBox.Show(fieldName + " is required.", "Invalid " + fieldName,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (value.Length > 30)
            {
                MessageBox.Show(fieldName + " must be between 1 and 30 characters.",
                    "Invalid " + fieldName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (value.Contains("|"))
            {
                MessageBox.Show(fieldName + " cannot contain the | character.",
                    "Invalid " + fieldName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool AnimalIdAlreadyExists(string animalId)
        {
            if (!File.Exists(animalsFile))
            {
                return false;
            }

            foreach (string line in File.ReadLines(animalsFile))
            {
                string[] parts = line.Split('|');

                // Only compare valid-looking records that contain an ID field.
                if (parts.Length > 0 &&
                    string.Equals(parts[0].Trim(), animalId,
                        StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private void ClearForm()
        {
            txtAnimalId.Clear();
            txtName.Clear();
            txtSpecies.Clear();
            txtAge.Clear();
            txtRecoveryScore.Clear();
            txtAnimalId.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
