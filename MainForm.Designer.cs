namespace PhotoOrganiser
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartButton = new System.Windows.Forms.Button();
            this.SourceFolder = new System.Windows.Forms.TextBox();
            this.DestFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TotalPhotos = new System.Windows.Forms.TextBox();
            this.TotalDuplicates = new System.Windows.Forms.TextBox();
            this.TotalNoExif = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Searching = new System.Windows.Forms.TextBox();
            this.SearchingString = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(157, 134);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(549, 50);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // SourceFolder
            // 
            this.SourceFolder.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SourceFolder.Location = new System.Drawing.Point(157, 23);
            this.SourceFolder.Name = "SourceFolder";
            this.SourceFolder.Size = new System.Drawing.Size(549, 37);
            this.SourceFolder.TabIndex = 1;
            this.SourceFolder.Text = "G:\\FolderEasierToScan";
            // 
            // DestFolder
            // 
            this.DestFolder.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DestFolder.Location = new System.Drawing.Point(157, 78);
            this.DestFolder.Name = "DestFolder";
            this.DestFolder.Size = new System.Drawing.Size(549, 37);
            this.DestFolder.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(38, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "Source :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(38, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 30);
            this.label2.TabIndex = 4;
            this.label2.Text = "Dest :";
            // 
            // TotalPhotos
            // 
            this.TotalPhotos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TotalPhotos.Location = new System.Drawing.Point(406, 387);
            this.TotalPhotos.Name = "TotalPhotos";
            this.TotalPhotos.Size = new System.Drawing.Size(300, 37);
            this.TotalPhotos.TabIndex = 5;
            // 
            // TotalDuplicates
            // 
            this.TotalDuplicates.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TotalDuplicates.Location = new System.Drawing.Point(406, 430);
            this.TotalDuplicates.Name = "TotalDuplicates";
            this.TotalDuplicates.Size = new System.Drawing.Size(300, 37);
            this.TotalDuplicates.TabIndex = 6;
            // 
            // TotalNoExif
            // 
            this.TotalNoExif.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TotalNoExif.Location = new System.Drawing.Point(406, 473);
            this.TotalNoExif.Name = "TotalNoExif";
            this.TotalNoExif.Size = new System.Drawing.Size(300, 37);
            this.TotalNoExif.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(157, 394);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 30);
            this.label3.TabIndex = 8;
            this.label3.Text = "Total :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(157, 437);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 30);
            this.label4.TabIndex = 9;
            this.label4.Text = "Total Duplicates :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(157, 480);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 30);
            this.label5.TabIndex = 10;
            this.label5.Text = "Total No Exif :";
            // 
            // Searching
            // 
            this.Searching.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Searching.Location = new System.Drawing.Point(157, 201);
            this.Searching.Multiline = true;
            this.Searching.Name = "Searching";
            this.Searching.Size = new System.Drawing.Size(549, 180);
            this.Searching.TabIndex = 11;
            // 
            // SearchingString
            // 
            this.SearchingString.AutoSize = true;
            this.SearchingString.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SearchingString.Location = new System.Drawing.Point(38, 204);
            this.SearchingString.Name = "SearchingString";
            this.SearchingString.Size = new System.Drawing.Size(119, 30);
            this.SearchingString.TabIndex = 12;
            this.SearchingString.Text = "Searching :";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 743);
            this.Controls.Add(this.SearchingString);
            this.Controls.Add(this.Searching);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TotalNoExif);
            this.Controls.Add(this.TotalDuplicates);
            this.Controls.Add(this.TotalPhotos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DestFolder);
            this.Controls.Add(this.SourceFolder);
            this.Controls.Add(this.StartButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button StartButton;
        private TextBox SourceFolder;
        private TextBox DestFolder;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label SearchingString;
        public TextBox TotalPhotos;
        public TextBox TotalDuplicates;
        public TextBox TotalNoExif;
        public TextBox Searching;
    }
}