namespace PRG271._281_PROJ
{
    partial class AddFormcs
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtAnimalId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtSpecies = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtRecoveryScore = new System.Windows.Forms.TextBox();
            this.btnAddAnimal = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblAnimalId = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSpecies = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblRecoveryScore = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtAnimalId
            // 
            this.txtAnimalId.Location = new System.Drawing.Point(40, 85);
            this.txtAnimalId.Name = "txtAnimalId";
            this.txtAnimalId.Size = new System.Drawing.Size(250, 22);
            this.txtAnimalId.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(40, 140);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 22);
            this.txtName.TabIndex = 1;
            // 
            // txtSpecies
            // 
            this.txtSpecies.Location = new System.Drawing.Point(40, 195);
            this.txtSpecies.Name = "txtSpecies";
            this.txtSpecies.Size = new System.Drawing.Size(250, 22);
            this.txtSpecies.TabIndex = 2;
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(40, 250);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(250, 22);
            this.txtAge.TabIndex = 3;
            // 
            // txtRecoveryScore
            // 
            this.txtRecoveryScore.Location = new System.Drawing.Point(40, 305);
            this.txtRecoveryScore.Name = "txtRecoveryScore";
            this.txtRecoveryScore.Size = new System.Drawing.Size(250, 22);
            this.txtRecoveryScore.TabIndex = 4;
            // 
            // btnAddAnimal
            // 
            this.btnAddAnimal.Location = new System.Drawing.Point(40, 355);
            this.btnAddAnimal.Name = "btnAddAnimal";
            this.btnAddAnimal.Size = new System.Drawing.Size(110, 32);
            this.btnAddAnimal.TabIndex = 5;
            this.btnAddAnimal.Text = "Add Animal";
            this.btnAddAnimal.UseVisualStyleBackColor = true;
            this.btnAddAnimal.Click += new System.EventHandler(this.btnAddAnimal_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(180, 355);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(110, 32);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblAnimalId
            // 
            this.lblAnimalId.AutoSize = true;
            this.lblAnimalId.Location = new System.Drawing.Point(40, 65);
            this.lblAnimalId.Name = "lblAnimalId";
            this.lblAnimalId.Size = new System.Drawing.Size(72, 16);
            this.lblAnimalId.TabIndex = 7;
            this.lblAnimalId.Text = "Animal ID";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(40, 120);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(44, 16);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "Name";
            // 
            // lblSpecies
            // 
            this.lblSpecies.AutoSize = true;
            this.lblSpecies.Location = new System.Drawing.Point(40, 175);
            this.lblSpecies.Name = "lblSpecies";
            this.lblSpecies.Size = new System.Drawing.Size(56, 16);
            this.lblSpecies.TabIndex = 9;
            this.lblSpecies.Text = "Species";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(40, 230);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(29, 16);
            this.lblAge.TabIndex = 10;
            this.lblAge.Text = "Age";
            // 
            // lblRecoveryScore
            // 
            this.lblRecoveryScore.AutoSize = true;
            this.lblRecoveryScore.Location = new System.Drawing.Point(40, 285);
            this.lblRecoveryScore.Name = "lblRecoveryScore";
            this.lblRecoveryScore.Size = new System.Drawing.Size(101, 16);
            this.lblRecoveryScore.TabIndex = 11;
            this.lblRecoveryScore.Text = "Recovery Score";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(40, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(143, 20);
            this.lblTitle.TabIndex = 12;
            this.lblTitle.Text = "Add New Animal";
            // 
            // AddFormcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 425);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblRecoveryScore);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblSpecies);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblAnimalId);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAddAnimal);
            this.Controls.Add(this.txtRecoveryScore);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.txtSpecies);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtAnimalId);
            this.Name = "AddFormcs";
            this.Text = "Add Animal";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtAnimalId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtSpecies;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TextBox txtRecoveryScore;
        private System.Windows.Forms.Button btnAddAnimal;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblAnimalId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSpecies;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblRecoveryScore;
        private System.Windows.Forms.Label lblTitle;
    }
}
