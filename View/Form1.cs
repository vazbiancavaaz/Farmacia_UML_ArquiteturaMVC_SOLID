using FarmaciaCRUD.controller;
using FarmaciaCRUD.dados;
using FarmaciaCRUD.model;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Farmacia
{
    public partial class Form1 : Form
    {
        private readonly RemedioController _controller =
            new RemedioController(new RemedioRepository());

        public Form1()
        {
            InitializeComponent();

            _controller.Cadastrar(new Remedio
            {
                Nome = "Dipirona",
                Descricao = "Frasco de gotas, 20 mL",
                Preco = 9.50m
            });

            dgvRemedios.AutoGenerateColumns = false;
            dgvRemedios.RowTemplate.Height = 56;
            dgvRemedios.DataSource = _controller.Listar();
        }

        private void dataGridView1_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
        }

        private void adicionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void adicionarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var tela = new CadastroRemedioForm(_controller))
            {
                if (tela.ShowDialog(this) == DialogResult.OK)
                {
                    dgvRemedios.DataSource = _controller.Listar();
                }
            }
        }
    }
}