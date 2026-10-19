using System;
using System.Linq;
using System.Windows.Forms;

namespace PRG271._281_PROJ
{
    public class BusinessLogic
    {
        // Used by both Add and Update forms.
        public bool ConditionsMet(string id, string Name, string species, string age)
        {
            id = (id ?? "").Trim().ToUpperInvariant();
            Name = (Name ?? "").Trim();
            species = (species ?? "").Trim();
            age = (age ?? "").Trim();

            if (id.Length != 7 || !id.StartsWith("WR-") || !id.Substring(3).All(char.IsDigit))
            {
                MessageBox.Show("Animal ID must be WR- followed by four digits.");
                return false;
            }
            if (Name.Length < 1 || Name.Length > 30 || Name.Contains("|"))
            {
                MessageBox.Show("Name must be 1 to 30 characters and cannot contain |.");
                return false;
            }
            if (species.Length < 1 || species.Length > 30 || species.Contains("|"))
            {
                MessageBox.Show("Species must be 1 to 30 characters and cannot contain |.");
                return false;
            }
            if (!int.TryParse(age, out int parsedAge) || parsedAge < 0 || parsedAge > 100)
            {
                MessageBox.Show("Age must be a whole number from 0 to 100.");
                return false;
            }
            return true;
        }

        // One classification method for Add and Update. Member 1 should approve ownership.
        public static (string Status, string HousingUnit) ClassifyRecoveryScore(int score)
        {
            if (score < 0 || score > 100) throw new ArgumentOutOfRangeException(nameof(score));
            if (score <= 19) return ("Critical", "Intensive Care Unit");
            if (score <= 39) return ("Serious", "High-Dependency Ward");
            if (score <= 59) return ("Stable", "Recovery Ward");
            if (score <= 79) return ("Recovering", "Outdoor Enclosure");
            return ("Release-Ready", "Pre-Release Camp");
        }
    }
}
