namespace FYP_SEAT_NUMBER_CHECKER
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
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.pnlHallSelection = new System.Windows.Forms.Panel();
            this.cmbHalls = new System.Windows.Forms.ComboBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.pnlSeatNumber = new System.Windows.Forms.Panel();
            this.lblSeatNumber = new System.Windows.Forms.Label();
            this.lblHall = new System.Windows.Forms.Label();
            this.lblReturn = new System.Windows.Forms.Label();
            this.pnlProgress.SuspendLayout();
            this.pnlHallSelection.SuspendLayout();
            this.pnlSeatNumber.SuspendLayout();
            this.SuspendLayout();
            // 
            // pBar
            // 
            this.pBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBar.Location = new System.Drawing.Point(202, 0);
            this.pBar.MarqueeAnimationSpeed = 25;
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(352, 23);
            this.pBar.Step = 50;
            this.pBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pBar.TabIndex = 0;
            // 
            // lblInstruction
            // 
            this.lblInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstruction.Location = new System.Drawing.Point(12, 63);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(758, 29);
            this.lblInstruction.TabIndex = 2;
            this.lblInstruction.Text = "Fetching Halls...";
            this.lblInstruction.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // pnlProgress
            // 
            this.pnlProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlProgress.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlProgress.Controls.Add(this.pBar);
            this.pnlProgress.Location = new System.Drawing.Point(12, 95);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(758, 73);
            this.pnlProgress.TabIndex = 5;
            // 
            // pnlHallSelection
            // 
            this.pnlHallSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHallSelection.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlHallSelection.Controls.Add(this.cmbHalls);
            this.pnlHallSelection.Controls.Add(this.btnNext);
            this.pnlHallSelection.Location = new System.Drawing.Point(12, 174);
            this.pnlHallSelection.Name = "pnlHallSelection";
            this.pnlHallSelection.Size = new System.Drawing.Size(758, 73);
            this.pnlHallSelection.TabIndex = 7;
            // 
            // cmbHalls
            // 
            this.cmbHalls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbHalls.FormattingEnabled = true;
            this.cmbHalls.Location = new System.Drawing.Point(202, 0);
            this.cmbHalls.Name = "cmbHalls";
            this.cmbHalls.Size = new System.Drawing.Size(234, 24);
            this.cmbHalls.TabIndex = 3;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(442, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(112, 24);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // pnlSeatNumber
            // 
            this.pnlSeatNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSeatNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlSeatNumber.Controls.Add(this.lblSeatNumber);
            this.pnlSeatNumber.Location = new System.Drawing.Point(12, 253);
            this.pnlSeatNumber.Name = "pnlSeatNumber";
            this.pnlSeatNumber.Size = new System.Drawing.Size(758, 73);
            this.pnlSeatNumber.TabIndex = 8;
            // 
            // lblSeatNumber
            // 
            this.lblSeatNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSeatNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeatNumber.Location = new System.Drawing.Point(0, 0);
            this.lblSeatNumber.Name = "lblSeatNumber";
            this.lblSeatNumber.Size = new System.Drawing.Size(758, 29);
            this.lblSeatNumber.TabIndex = 10;
            this.lblSeatNumber.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblHall
            // 
            this.lblHall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHall.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHall.Location = new System.Drawing.Point(12, 9);
            this.lblHall.Name = "lblHall";
            this.lblHall.Size = new System.Drawing.Size(758, 38);
            this.lblHall.TabIndex = 9;
            this.lblHall.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReturn
            // 
            this.lblReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReturn.Location = new System.Drawing.Point(454, 521);
            this.lblReturn.Name = "lblReturn";
            this.lblReturn.Size = new System.Drawing.Size(316, 23);
            this.lblReturn.TabIndex = 10;
            this.lblReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.lblReturn);
            this.Controls.Add(this.lblHall);
            this.Controls.Add(this.pnlSeatNumber);
            this.Controls.Add(this.pnlHallSelection);
            this.Controls.Add(this.pnlProgress);
            this.Controls.Add(this.lblInstruction);
            this.Name = "Main";
            this.Text = "Seat Number Checker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.pnlProgress.ResumeLayout(false);
            this.pnlHallSelection.ResumeLayout(false);
            this.pnlSeatNumber.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.Panel pnlHallSelection;
        private System.Windows.Forms.ComboBox cmbHalls;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Panel pnlSeatNumber;
        private System.Windows.Forms.Label lblSeatNumber;
        private System.Windows.Forms.Label lblHall;
        private System.Windows.Forms.Label lblReturn;
    }
}

