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

            // Member 3's shared method checks ID, Name, Species and Age.
            BusinessLogic logic = new BusinessLogic();
            if (!logic.ConditionsMet(animalId, name, species, ageText))
            {
                // Move to the field that needs correcting.
                if (animalId.Length != 7 || !animalId.StartsWith("WR-") ||
                    !animalId.Substring(3).All(char.IsDigit))
                    txtAnimalId.Focus();
                else if (name.Length < 1 || name.Length > 30 || name.Contains("|"))
                    txtName.Focus();
                else if (species.Length < 1 || species.Length > 30 || species.Contains("|"))
                    txtSpecies.Focus();
                else
                    txtAge.Focus();
                return;
            }

            // Recovery Score is not covered by ConditionsMet().
            if (!int.TryParse(recoveryScoreText, out int recoveryScore) ||
                recoveryScore < 0 || recoveryScore > 100)
            {
                MessageBox.Show("Recovery Score must be a whole number between 0 and 100.");
                txtRecoveryScore.Focus();
                return;
            }

            int age = int.Parse(ageText);

            try
            {
                // The classification is shared with the Update feature.
                var classification = BusinessLogic.ClassifyRecoveryScore(recoveryScore);

                // Check the file here so a read error is caught before saving.
                if (AnimalIdAlreadyExists(animalId))
                {
                    MessageBox.Show("Animal ID already exists. Please enter another ID.");
                    txtAnimalId.Focus();
                    return;
                }

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
