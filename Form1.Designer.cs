namespace IAProject
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
            this.components = new System.ComponentModel.Container();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.segmented1 = new Emgu.CV.UI.ImageBox();
            this.segmented2 = new Emgu.CV.UI.ImageBox();
            this.segmented3 = new Emgu.CV.UI.ImageBox();
            this.segmented4 = new Emgu.CV.UI.ImageBox();
            this.segmented5 = new Emgu.CV.UI.ImageBox();
            this.segmented6 = new Emgu.CV.UI.ImageBox();
            this.segmented7 = new Emgu.CV.UI.ImageBox();
            this.segmented8 = new Emgu.CV.UI.ImageBox();
            this.displayEquation = new System.Windows.Forms.TextBox();
            this.calculate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented8)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(3, 50);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(500, 500);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // imageBox2
            // 
            this.imageBox2.Location = new System.Drawing.Point(556, 48);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(500, 500);
            this.imageBox2.TabIndex = 2;
            this.imageBox2.TabStop = false;
            // 
            // segmented1
            // 
            this.segmented1.Location = new System.Drawing.Point(27, 576);
            this.segmented1.Name = "segmented1";
            this.segmented1.Size = new System.Drawing.Size(28, 28);
            this.segmented1.TabIndex = 2;
            this.segmented1.TabStop = false;
            // 
            // segmented2
            // 
            this.segmented2.Location = new System.Drawing.Point(81, 576);
            this.segmented2.Name = "segmented2";
            this.segmented2.Size = new System.Drawing.Size(28, 28);
            this.segmented2.TabIndex = 3;
            this.segmented2.TabStop = false;
            // 
            // segmented3
            // 
            this.segmented3.Location = new System.Drawing.Point(127, 576);
            this.segmented3.Name = "segmented3";
            this.segmented3.Size = new System.Drawing.Size(28, 28);
            this.segmented3.TabIndex = 4;
            this.segmented3.TabStop = false;
            // 
            // segmented4
            // 
            this.segmented4.Location = new System.Drawing.Point(179, 576);
            this.segmented4.Name = "segmented4";
            this.segmented4.Size = new System.Drawing.Size(28, 28);
            this.segmented4.TabIndex = 5;
            this.segmented4.TabStop = false;
            // 
            // segmented5
            // 
            this.segmented5.Location = new System.Drawing.Point(227, 576);
            this.segmented5.Name = "segmented5";
            this.segmented5.Size = new System.Drawing.Size(28, 28);
            this.segmented5.TabIndex = 6;
            this.segmented5.TabStop = false;
            // 
            // segmented6
            // 
            this.segmented6.Location = new System.Drawing.Point(278, 576);
            this.segmented6.Name = "segmented6";
            this.segmented6.Size = new System.Drawing.Size(28, 28);
            this.segmented6.TabIndex = 7;
            this.segmented6.TabStop = false;
            // 
            // segmented7
            // 
            this.segmented7.Location = new System.Drawing.Point(350, 576);
            this.segmented7.Name = "segmented7";
            this.segmented7.Size = new System.Drawing.Size(28, 28);
            this.segmented7.TabIndex = 8;
            this.segmented7.TabStop = false;
            // 
            // segmented8
            // 
            this.segmented8.Location = new System.Drawing.Point(399, 576);
            this.segmented8.Name = "segmented8";
            this.segmented8.Size = new System.Drawing.Size(28, 28);
            this.segmented8.TabIndex = 9;
            this.segmented8.TabStop = false;
            // 
            // displayEquation
            // 
            this.displayEquation.Location = new System.Drawing.Point(592, 597);
            this.displayEquation.Name = "displayEquation";
            this.displayEquation.Size = new System.Drawing.Size(95, 20);
            this.displayEquation.TabIndex = 10;
            // 
            // calculate
            // 
            this.calculate.Location = new System.Drawing.Point(717, 593);
            this.calculate.Name = "calculate";
            this.calculate.Size = new System.Drawing.Size(90, 24);
            this.calculate.TabIndex = 11;
            this.calculate.Text = "Calculate";
            this.calculate.UseVisualStyleBackColor = true;
            this.calculate.Click += new System.EventHandler(this.calculate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(845, 597);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Answer- ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(913, 593);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(82, 20);
            this.textBox1.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 662);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.calculate);
            this.Controls.Add(this.displayEquation);
            this.Controls.Add(this.segmented8);
            this.Controls.Add(this.segmented7);
            this.Controls.Add(this.segmented6);
            this.Controls.Add(this.segmented5);
            this.Controls.Add(this.segmented4);
            this.Controls.Add(this.segmented3);
            this.Controls.Add(this.segmented2);
            this.Controls.Add(this.segmented1);
            this.Controls.Add(this.imageBox2);
            this.Controls.Add(this.imageBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.segmented8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private Emgu.CV.UI.ImageBox imageBox2;
        private Emgu.CV.UI.ImageBox segmented1;
        private Emgu.CV.UI.ImageBox segmented2;
        private Emgu.CV.UI.ImageBox segmented3;
        private Emgu.CV.UI.ImageBox segmented4;
        private Emgu.CV.UI.ImageBox segmented5;
        private Emgu.CV.UI.ImageBox segmented6;
        private Emgu.CV.UI.ImageBox segmented7;
        private Emgu.CV.UI.ImageBox segmented8;
        private System.Windows.Forms.TextBox displayEquation;
        private System.Windows.Forms.Button calculate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

