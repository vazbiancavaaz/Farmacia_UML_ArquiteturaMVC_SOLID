namespace Farmacia
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dgvRemedios = new DataGridView();
            IMG = new DataGridViewImageColumn();
            Nome = new DataGridViewTextBoxColumn();
            Descrição = new DataGridViewTextBoxColumn();
            Preço = new DataGridViewTextBoxColumn();
            menuStrip1 = new MenuStrip();
            adicionarToolStripMenuItem = new ToolStripMenuItem();
            adicionarToolStripMenuItem1 = new ToolStripMenuItem();
            removerToolStripMenuItem = new ToolStripMenuItem();
            editarToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dgvRemedios).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvRemedios
            // 
            dgvRemedios.AllowUserToResizeRows = false;
            dgvRemedios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRemedios.Columns.AddRange(new DataGridViewColumn[] { IMG, Nome, Descrição, Preço });
            dgvRemedios.Location = new Point(12, 54);
            dgvRemedios.MultiSelect = false;
            dgvRemedios.Name = "dgvRemedios";
            dgvRemedios.ReadOnly = true;
            dgvRemedios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRemedios.Size = new Size(1147, 384);
            dgvRemedios.TabIndex = 0;
            dgvRemedios.CellContentClick += dataGridView1_CellContentClick;
            // 
            // IMG
            // 
            IMG.HeaderText = "";
            IMG.Image = (Image)resources.GetObject("IMG.Image");
            IMG.ImageLayout = DataGridViewImageCellLayout.Zoom;
            IMG.Name = "IMG";
            IMG.ReadOnly = true;
            // 
            // Nome
            // 
            Nome.DataPropertyName = "Nome";
            Nome.HeaderText = "Nome";
            Nome.Name = "Nome";
            Nome.ReadOnly = true;
            Nome.Width = 180;
            // 
            // Descrição
            // 
            Descrição.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Descrição.DataPropertyName = "Descricao";
            Descrição.HeaderText = "Descrição";
            Descrição.Name = "Descrição";
            Descrição.ReadOnly = true;
            // 
            // Preço
            // 
            Preço.DataPropertyName = "Preco";
            Preço.HeaderText = "Preço";
            Preço.Name = "Preço";
            Preço.ReadOnly = true;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { adicionarToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1171, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // adicionarToolStripMenuItem
            // 
            adicionarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { adicionarToolStripMenuItem1, removerToolStripMenuItem, editarToolStripMenuItem });
            adicionarToolStripMenuItem.Name = "adicionarToolStripMenuItem";
            adicionarToolStripMenuItem.Size = new Size(50, 20);
            adicionarToolStripMenuItem.Text = "Menu";
            adicionarToolStripMenuItem.Click += adicionarToolStripMenuItem_Click;
            // 
            // adicionarToolStripMenuItem1
            // 
            adicionarToolStripMenuItem1.Name = "adicionarToolStripMenuItem1";
            adicionarToolStripMenuItem1.Size = new Size(180, 22);
            adicionarToolStripMenuItem1.Text = "Adicionar";
            adicionarToolStripMenuItem1.Click += adicionarToolStripMenuItem1_Click;
            // 
            // removerToolStripMenuItem
            // 
            removerToolStripMenuItem.Name = "removerToolStripMenuItem";
            removerToolStripMenuItem.Size = new Size(180, 22);
            removerToolStripMenuItem.Text = "Remover";
            removerToolStripMenuItem.Click += removerToolStripMenuItem_Click;
            // 
            // editarToolStripMenuItem
            // 
            editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            editarToolStripMenuItem.Size = new Size(180, 22);
            editarToolStripMenuItem.Text = "Editar";
            editarToolStripMenuItem.Click += editarToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1171, 450);
            Controls.Add(dgvRemedios);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvRemedios).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvRemedios;
        private DataGridViewImageColumn IMG;
        private DataGridViewTextBoxColumn Nome;
        private DataGridViewTextBoxColumn Descrição;
        private DataGridViewTextBoxColumn Preço;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem adicionarToolStripMenuItem;
        private ToolStripMenuItem adicionarToolStripMenuItem1;
        private ToolStripMenuItem removerToolStripMenuItem;
        private ToolStripMenuItem editarToolStripMenuItem;
    }
}
