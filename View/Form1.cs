using Farmacia.View;
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

        private void removerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRemedios.SelectedRows.Count == 0 ||
    dgvRemedios.SelectedRows[0].DataBoundItem is not Remedio remedio)
            {
                MessageBox.Show("Selecione um remédio na tabela.");
                return;
            }

            var resposta = MessageBox.Show(
                $"Quer remover {remedio.Nome}?",
                "Confirmar remoção",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (resposta != DialogResult.Yes)
            {
                return;
            }

            if (_controller.Excluir(remedio.Id))
            {
                dgvRemedios.DataSource = _controller.Listar();
            }
            else
            {
                MessageBox.Show("Esse remédio não foi encontrado.");
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRemedios.SelectedRows.Count == 0 ||
                dgvRemedios.SelectedRows[0].DataBoundItem is not Remedio remedio)
            {
                MessageBox.Show("Selecione um remédio na tabela.");
                return;
            }

            using (var tela = new EditarRemedioForm(_controller, remedio))
            {
                if (tela.ShowDialog(this) == DialogResult.OK)
                {
                    dgvRemedios.DataSource = _controller.Listar();
                }
            }
        }
    }
}