namespace TetrisGameDemo
{
    partial class Form1
    {
        
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pictureBoxGame = new PictureBox();
            timerGame = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBoxGame).BeginInit();
            SuspendLayout();
           
            pictureBoxGame.BackColor = Color.Black;
            pictureBoxGame.Location = new Point(0, 0);
            pictureBoxGame.Name = "pictureBoxGame";
            pictureBoxGame.Size = new Size(803, 464);
            pictureBoxGame.TabIndex = 0;
            pictureBoxGame.TabStop = false;
           
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 461);
            Controls.Add(pictureBoxGame);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBoxGame).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBoxGame;
        private System.Windows.Forms.Timer timerGame;
    }
}
