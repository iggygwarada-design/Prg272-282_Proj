using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRG271._281_PROJ
{
    public class BusinessLogic
    {
        public bool ConditionsMet(string id,string Name,string species,string age)
        {
         bool met = true;
            //checks the id
            string[] parts = id.Split('-');
            if (parts[0].ToUpper()!="wr" || parts[1].Length != 4  || parts[1].All(char.IsDigit))
            { met = false;
                MessageBox.Show("Error! Invalid animal ID ENTERED");
            }
            
            //checks the name
            if (Name.Length>30||Name.Length<1||Name.Contains("|"))
            {
                met = false;
                MessageBox.Show("Error! Invalid NAME ENTERED");
            }

            //checks the Species
            if (species.Length > 30 || species.Length < 1 || species.Contains("|"))
            {
                met = false;
                MessageBox.Show("Error! Invalid SPECIES ENTERED");
            }

            //Check the age
            if (!int.TryParse(age,out int parsedAge)|| int.Parse(age)>100 || int.Parse(age)<0)
            {
                met = false;
                MessageBox.Show("Error! Invalid Age ENTERED");
            }
            return met;
        }
       public void Clculate()
        {
            
        }
    }

    //making a fileHandler that can delete animals and catch some errors
    public class FileHandler
{
    public string dataFile = "animals.txt";
    public string summaryFile = "summary.txt";

    public List<string> ReadAnimals()
    {
        List<string> lines = new List<string>();
        try
        {
            if (File.Exists(dataFile))
            {
                lines.AddRange(File.ReadAllLines(dataFile));
            }
        }
        catch (Exception ex)
        {
            ShowFileError(ex, "reading " + dataFile);
        }
        return lines;
    }

    public bool DeleteAnimal(string id)
    {
        try
        {
            if (!File.Exists(dataFile))
            {
                MessageBox.Show("The file " + dataFile + " could not be found.", "File error");
                return false;
            }

            string[] lines = File.ReadAllLines(dataFile);
            List<string> keep = new List<string>();
            bool found = false;

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (found == false && parts[0].Trim().ToUpper() == id.Trim().ToUpper())
                {
                    found = true;
                }
                else
                {
                    keep.Add(line);
                }
            }

            if (found == false)
            {
                MessageBox.Show("Animal " + id + " is not in " + dataFile + ", so nothing was deleted.", "Not found");
                return false;
            }

            File.Copy(dataFile, dataFile + ".bak", true);
            File.WriteAllLines(dataFile, keep);
            return true;
        }
        catch (Exception ex)
        {
            ShowFileError(ex, "deleting from " + dataFile);
            return false;
        }
    }

    public bool SaveSummary(string text)
    {
        try
        {
            File.WriteAllText(summaryFile, text);
            return true;
        }
        catch (Exception ex)
        {
            ShowFileError(ex, "saving " + summaryFile);
            return false;
        }
    }

    private void ShowFileError(Exception ex, string whatWeWereDoing)
    {
        string message;

        if (ex is FileNotFoundException)
        {
            message = "The file could not be found. Make sure it is in the same folder as the program.";
        }
        else if (ex is DirectoryNotFoundException)
        {
            message = "The folder for the file could not be found.";
        }
        else if (ex is UnauthorizedAccessException)
        {
            message = "The program is not allowed to change this file. It may be read-only.";
        }
        else if (ex is IOException)
        {
            message = "The file is being used by another program, or the disk is full. Close other programs that may be using it and try again.";
        }
        else
        {
            message = "An unexpected error happened: " + ex.Message;
        }

        MessageBox.Show("There was a problem while " + whatWeWereDoing + ".\n\n" + message,
            "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }



    //calculator or summary logic for FileHandler that gets all the animals and makes em numbers
    public class SummaryLogic
{
    public int totalAnimals;
    public double averageAge;
    public double averageScore;

    public Dictionary<string, int> statusCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

    public void Calculate(List<string> lines)
    {
        totalAnimals = 0;
        averageAge = 0;
        averageScore = 0;
        statusCounts.Clear();

        double ageTotal = 0;
        int ageCount = 0;
        double scoreTotal = 0;
        int scoreCount = 0;

        foreach (string line in lines)
        {
            string[] parts = line.Split('|');

            if (parts.Length < 4)
            {
                continue;
            }

            totalAnimals++;

            int age;
            if (int.TryParse(parts[3], out age))
            {
                ageTotal += age;
                ageCount++;
            }

            if (parts.Length > 4)
            {
                double score;
                string scoreText = parts[4].Trim().Replace(',', '.');
                if (double.TryParse(scoreText, NumberStyles.Float, CultureInfo.InvariantCulture, out score))
                {
                    scoreTotal += score;
                    scoreCount++;
                }
            }

            string status = "Unspecified";
            if (parts.Length > 5 && parts[5].Trim() != "")
            {
                status = parts[5].Trim();
            }

            if (statusCounts.ContainsKey(status))
            {
                statusCounts[status]++;
            }
            else
            {
                statusCounts.Add(status, 1);
            }
        }

        if (ageCount > 0)
        {
            averageAge = ageTotal / ageCount;
        }
        if (scoreCount > 0)
        {
            averageScore = scoreTotal / scoreCount;
        }
    }

    public string MakeReportText()
    {
        string nl = Environment.NewLine;

        string text = "WILDLIFE REHABILITATION CENTRE - SUMMARY REPORT" + nl;
        text += "Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + nl;
        text += "----------------------------------------------" + nl;
        text += "Total animals: " + totalAnimals + nl;
        text += "Average age: " + averageAge.ToString("0.0") + nl;
        text += "Average recovery score: " + averageScore.ToString("0.0") + nl;
        text += nl + "Animals per status:" + nl;

        foreach (KeyValuePair<string, int> item in statusCounts)
        {
            text += "  " + item.Key + ": " + item.Value + nl;
        }

        return text;
    }
   }
}
