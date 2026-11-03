namespace Farmacia.View
{
    partial class EditarRemedioForm
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
            Nometxt = new TextBox();
            Descricaotxt = new TextBox();
            Precotxt = new TextBox();
            SalvarBtn = new Button();
            SuspendLayout();
            // 
            // Nometxt
            // 
            Nometxt.Location = new Point(317, 75);
            Nometxt.Name = "Nometxt";
            Nometxt.Size = new Size(157, 23);
            Nometxt.TabIndex = 0;
            // 
            // Descricaotxt
            // 
            Descricaotxt.Location = new Point(317, 137);
            Descricaotxt.Name = "Descricaotxt";
            Descricaotxt.Size = new Size(157, 23);
            Descricaotxt.TabIndex = 1;
            // 
            // Precotxt
            // 
            Precotxt.Location = new Point(317, 207);
            Precotxt.Name = "Precotxt";
            Precotxt.Size = new Size(157, 23);
            Precotxt.TabIndex = 2;
            // 
            // SalvarBtn
            // 
            SalvarBtn.Location = new Point(364, 282);
            SalvarBtn.Name = "SalvarBtn";
            SalvarBtn.Size = new Size(75, 23);
            SalvarBtn.TabIndex = 3;
            SalvarBtn.Text = "Salvar";
            SalvarBtn.UseVisualStyleBackColor = true;
            SalvarBtn.Click += SalvarBtn_Click;
            // 
            // EditarRemedioForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(SalvarBtn);
            Controls.Add(Precotxt);
            Controls.Add(Descricaotxt);
            Controls.Add(Nometxt);
            Name = "EditarRemedioForm";
            Text = "EditarRemedioForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Nometxt;
        private TextBox Descricaotxt;
        private TextBox Precotxt;
        private Button SalvarBtn;
    }
}