namespace BusinessIntelligenceLabs
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLookup = new System.Windows.Forms.Button();
            this.lstBox = new System.Windows.Forms.ListBox();
            this.btnGetDatesFromSource = new System.Windows.Forms.Button();
            this.lstGetDatesFromSource = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnLookup
            // 
            this.btnLookup.Location = new System.Drawing.Point(171, 218);
            this.btnLookup.Name = "btnLookup";
            this.btnLookup.Size = new System.Drawing.Size(75, 23);
            this.btnLookup.TabIndex = 0;
            this.btnLookup.Text = "button1";
            this.btnLookup.UseVisualStyleBackColor = true;
            this.btnLookup.Click += new System.EventHandler(this.btnLookup_Click);
            // 
            // lstBox
            // 
            this.lstBox.FormattingEnabled = true;
            this.lstBox.Location = new System.Drawing.Point(36, 78);
            this.lstBox.Name = "lstBox";
            this.lstBox.Size = new System.Drawing.Size(210, 134);
            this.lstBox.TabIndex = 1;
            // 
            // btnGetDatesFromSource
            // 
            this.btnGetDatesFromSource.Location = new System.Drawing.Point(556, 244);
            this.btnGetDatesFromSource.Name = "btnGetDatesFromSource";
            this.btnGetDatesFromSource.Size = new System.Drawing.Size(115, 80);
            this.btnGetDatesFromSource.TabIndex = 2;
            this.btnGetDatesFromSource.Text = "Get Dates From Source";
            this.btnGetDatesFromSource.UseVisualStyleBackColor = true;
            this.btnGetDatesFromSource.Click += new System.EventHandler(this.btnGetDatesFromSource_Click);
            // 
            // lstGetDatesFromSource
            // 
            this.lstGetDatesFromSource.FormattingEnabled = true;
            this.lstGetDatesFromSource.HorizontalScrollbar = true;
            this.lstGetDatesFromSource.Location = new System.Drawing.Point(462, 78);
            this.lstGetDatesFromSource.Name = "lstGetDatesFromSource";
            this.lstGetDatesFromSource.Size = new System.Drawing.Size(209, 160);
            this.lstGetDatesFromSource.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstGetDatesFromSource);
            this.Controls.Add(this.btnGetDatesFromSource);
            this.Controls.Add(this.lstBox);
            this.Controls.Add(this.btnLookup);
            this.Name = "Form1";
            this.Text = " ";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLookup;
        private System.Windows.Forms.ListBox lstBox;
        private System.Windows.Forms.Button btnGetDatesFromSource;
        private System.Windows.Forms.ListBox lstGetDatesFromSource;
    }
}

