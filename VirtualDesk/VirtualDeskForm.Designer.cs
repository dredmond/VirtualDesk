namespace VirtualDesk
{
    partial class VirtualDeskForm
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
            this.desktop1 = new System.Windows.Forms.PictureBox();
            this.desktop2 = new System.Windows.Forms.PictureBox();
            this.desktop3 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.desktop1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.desktop2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.desktop3)).BeginInit();
            this.SuspendLayout();
            // 
            // desktop1
            // 
            this.desktop1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.desktop1.Location = new System.Drawing.Point(12, 12);
            this.desktop1.Name = "desktop1";
            this.desktop1.Size = new System.Drawing.Size(104, 80);
            this.desktop1.TabIndex = 0;
            this.desktop1.TabStop = false;
            this.desktop1.Click += new System.EventHandler(this.desktop1_Click);
            // 
            // desktop2
            // 
            this.desktop2.BackColor = System.Drawing.Color.Lime;
            this.desktop2.Location = new System.Drawing.Point(122, 12);
            this.desktop2.Name = "desktop2";
            this.desktop2.Size = new System.Drawing.Size(104, 80);
            this.desktop2.TabIndex = 1;
            this.desktop2.TabStop = false;
            this.desktop2.Click += new System.EventHandler(this.desktop2_Click);
            // 
            // desktop3
            // 
            this.desktop3.Location = new System.Drawing.Point(232, 12);
            this.desktop3.Name = "desktop3";
            this.desktop3.Size = new System.Drawing.Size(104, 80);
            this.desktop3.TabIndex = 2;
            this.desktop3.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 98);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(324, 117);
            this.textBox1.TabIndex = 3;
            // 
            // VirtualDeskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 227);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.desktop3);
            this.Controls.Add(this.desktop2);
            this.Controls.Add(this.desktop1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VirtualDeskForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Virtual Desk";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.desktop1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.desktop2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.desktop3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox desktop1;
        private System.Windows.Forms.PictureBox desktop2;
        private System.Windows.Forms.PictureBox desktop3;
        private System.Windows.Forms.TextBox textBox1;
    }
}

