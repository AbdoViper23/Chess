namespace Chess
{
    partial class Pawn_promotion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pawn_promotion));
            this.Queen = new System.Windows.Forms.Button();
            this.rook = new System.Windows.Forms.Button();
            this.Bishop = new System.Windows.Forms.Button();
            this.Knight = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Queen
            // 
            this.Queen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Queen.BackColor = System.Drawing.Color.Lavender;
            this.Queen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Queen.Font = new System.Drawing.Font("Agency FB", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Queen.ForeColor = System.Drawing.Color.DodgerBlue;
            this.Queen.Location = new System.Drawing.Point(65, 92);
            this.Queen.Name = "Queen";
            this.Queen.Size = new System.Drawing.Size(154, 116);
            this.Queen.TabIndex = 0;
            this.Queen.Text = "Queen";
            this.Queen.UseVisualStyleBackColor = false;
            this.Queen.Click += new System.EventHandler(this.queen_Click);
            // 
            // rook
            // 
            this.rook.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rook.BackColor = System.Drawing.Color.Lavender;
            this.rook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rook.Font = new System.Drawing.Font("Agency FB", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rook.ForeColor = System.Drawing.Color.DodgerBlue;
            this.rook.Location = new System.Drawing.Point(235, 92);
            this.rook.Name = "rook";
            this.rook.Size = new System.Drawing.Size(154, 116);
            this.rook.TabIndex = 1;
            this.rook.Text = "Rook";
            this.rook.UseVisualStyleBackColor = false;
            this.rook.Click += new System.EventHandler(this.rook_Click);
            // 
            // Bishop
            // 
            this.Bishop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Bishop.BackColor = System.Drawing.Color.Lavender;
            this.Bishop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Bishop.Font = new System.Drawing.Font("Agency FB", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Bishop.ForeColor = System.Drawing.Color.DodgerBlue;
            this.Bishop.Location = new System.Drawing.Point(404, 92);
            this.Bishop.Name = "Bishop";
            this.Bishop.Size = new System.Drawing.Size(154, 116);
            this.Bishop.TabIndex = 2;
            this.Bishop.Text = "Bishop";
            this.Bishop.UseVisualStyleBackColor = false;
            this.Bishop.Click += new System.EventHandler(this.Bishop_Click);
            // 
            // Knight
            // 
            this.Knight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Knight.BackColor = System.Drawing.Color.Lavender;
            this.Knight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Knight.Font = new System.Drawing.Font("Agency FB", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Knight.ForeColor = System.Drawing.Color.DodgerBlue;
            this.Knight.Location = new System.Drawing.Point(575, 92);
            this.Knight.Name = "Knight";
            this.Knight.Size = new System.Drawing.Size(154, 116);
            this.Knight.TabIndex = 3;
            this.Knight.Text = "Knight";
            this.Knight.UseVisualStyleBackColor = false;
            this.Knight.Click += new System.EventHandler(this.Knight_Click);
            // 
            // Pawn_promotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(831, 306);
            this.Controls.Add(this.Knight);
            this.Controls.Add(this.Bishop);
            this.Controls.Add(this.rook);
            this.Controls.Add(this.Queen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Pawn_promotion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pawn promotion";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Queen;
        private System.Windows.Forms.Button rook;
        private System.Windows.Forms.Button Bishop;
        private System.Windows.Forms.Button Knight;
    }
}