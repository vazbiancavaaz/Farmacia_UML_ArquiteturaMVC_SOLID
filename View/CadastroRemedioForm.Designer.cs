namespace Farmacia
{
    partial class CadastroRemedioForm
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
            NomeTxb = new TextBox();
            DescricaoTxb = new TextBox();
            PrecoTxb = new TextBox();
            SalvarBtn = new Button();
            NomeLb = new Label();
            PrecoLb = new Label();
            DescricaoLb = new Label();
            SuspendLayout();
            // 
            // NomeTxb
            // 
            NomeTxb.Location = new Point(120, 67);
            NomeTxb.Name = "NomeTxb";
            NomeTxb.Size = new Size(200, 23);
            NomeTxb.TabIndex = 0;
            // 
            // DescricaoTxb
            // 
            DescricaoTxb.Location = new Point(120, 173);
            DescricaoTxb.Name = "DescricaoTxb";
            DescricaoTxb.Size = new Size(200, 23);
            DescricaoTxb.TabIndex = 1;
            // 
            // PrecoTxb
            // 
            PrecoTxb.Location = new Point(120, 279);
            PrecoTxb.Name = "PrecoTxb";
            PrecoTxb.Size = new Size(200, 23);
            PrecoTxb.TabIndex = 2;
            // 
            // SalvarBtn
            // 
            SalvarBtn.Location = new Point(183, 336);
            SalvarBtn.Name = "SalvarBtn";
            SalvarBtn.Size = new Size(75, 23);
            SalvarBtn.TabIndex = 3;
            SalvarBtn.Text = "Salvar";
            SalvarBtn.UseVisualStyleBackColor = true;
            SalvarBtn.Click += SalvarBtn_Click;
            // 
            // NomeLb
            // 
            NomeLb.AutoSize = true;
            NomeLb.Location = new Point(200, 49);
            NomeLb.Name = "NomeLb";
            NomeLb.Size = new Size(40, 15);
            NomeLb.TabIndex = 4;
            NomeLb.Text = "Nome";
            // 
            // PrecoLb
            // 
            PrecoLb.AutoSize = true;
            PrecoLb.Location = new Point(202, 261);
            PrecoLb.Name = "PrecoLb";
            PrecoLb.Size = new Size(37, 15);
            PrecoLb.TabIndex = 5;
            PrecoLb.Text = "Preço";
            // 
            // DescricaoLb
            // 
            DescricaoLb.AutoSize = true;
            DescricaoLb.Location = new Point(191, 155);
            DescricaoLb.Name = "DescricaoLb";
            DescricaoLb.Size = new Size(58, 15);
            DescricaoLb.TabIndex = 6;
            DescricaoLb.Text = "Descrição";
            // 
            // CadastroRemedioForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(425, 450);
            Controls.Add(DescricaoLb);
            Controls.Add(PrecoLb);
            Controls.Add(NomeLb);
            Controls.Add(SalvarBtn);
            Controls.Add(PrecoTxb);
            Controls.Add(DescricaoTxb);
            Controls.Add(NomeTxb);
            Name = "CadastroRemedioForm";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox NomeTxb;
        private TextBox DescricaoTxb;
        private TextBox PrecoTxb;
        private Button SalvarBtn;
        private Label NomeLb;
        private Label PrecoLb;
        private Label DescricaoLb;
    }
}