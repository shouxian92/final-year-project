namespace FYP_MATRIC_READ_WRITE
{
    partial class Main
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
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.txtRead = new System.Windows.Forms.TextBox();
            this.txtWrite = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(12, 41);
            this.btnRead.Margin = new System.Windows.Forms.Padding(4);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(149, 25);
            this.btnRead.TabIndex = 38;
            this.btnRead.Text = "Read";
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(169, 41);
            this.btnWrite.Margin = new System.Windows.Forms.Padding(4);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(149, 25);
            this.btnWrite.TabIndex = 55;
            this.btnWrite.Text = "Write";
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // txtRead
            // 
            this.txtRead.Location = new System.Drawing.Point(12, 12);
            this.txtRead.Name = "txtRead";
            this.txtRead.Size = new System.Drawing.Size(149, 22);
            this.txtRead.TabIndex = 72;
            // 
            // txtWrite
            // 
            this.txtWrite.Location = new System.Drawing.Point(169, 12);
            this.txtWrite.Name = "txtWrite";
            this.txtWrite.Size = new System.Drawing.Size(149, 22);
            this.txtWrite.TabIndex = 73;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(12, 74);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(4);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(306, 22);
            this.txtStatus.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(330, 110);
            this.Controls.Add(this.txtWrite);
            this.Controls.Add(this.txtRead);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.txtStatus);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Matric Card Reader / Writer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.TextBox txtRead;
        private System.Windows.Forms.TextBox txtWrite;
        private System.Windows.Forms.TextBox txtStatus;
    }
}

