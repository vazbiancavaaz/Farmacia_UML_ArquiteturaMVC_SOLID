using Farmacia.View;
using FarmaciaCRUD.controller;
using FarmaciaCRUD.dados;
using FarmaciaCRUD.model;
using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Data.Sqlite;

namespace Farmacia
{
    public partial class Form1 : Form
    {
        private readonly RemedioController _controller =
            new RemedioController(new RemedioRepository());

        private readonly CarrinhoController _carrinhoController =
            new CarrinhoController(new CarrinhoRepository());

        public Form1()
        {
            InitializeComponent();

            dgvRemedios.AutoGenerateColumns = false;
            dgvRemedios.RowTemplate.Height = 56;
            dgvRemedios.DataSource = _controller.Listar();
            ConfigurarCarrinho();
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
        private void ConfigurarCarrinho()
        {
            var barra = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 48,
                Padding = new Padding(4),
                WrapContents = false
            };
            var adicionar = new Button
            {
                Text = "Adicionar ao carrinho", AutoSize = true,
                Padding = new Padding(8, 4, 8, 4)
            };
            adicionar.Click += AdicionarAoCarrinho_Click;
            var visualizar = new Button
            {
                Text = "Ver carrinho", AutoSize = true,
                Padding = new Padding(8, 4, 8, 4)
            };
            visualizar.Click += VerCarrinho_Click;
            barra.Controls.Add(adicionar);
            barra.Controls.Add(visualizar);

            var conteudo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            dgvRemedios.Dock = DockStyle.Fill;
            dgvRemedios.AllowUserToAddRows = false;
            dgvRemedios.AllowUserToDeleteRows = false;
            conteudo.Controls.Add(dgvRemedios);
            conteudo.Controls.Add(barra);
            Controls.Add(conteudo);
            conteudo.BringToFront();
            Text = "Farmácia";
            MinimumSize = new Size(800, 420);
        }

        private void AdicionarAoCarrinho_Click(object? sender, EventArgs e)
        {
            if (dgvRemedios.CurrentRow?.DataBoundItem is not Remedio remedio)
            {
                MessageBox.Show(this, "Selecione um remédio na tabela.");
                return;
            }

            try
            {
                _carrinhoController.Adicionar(remedio.Id);
                MessageBox.Show(this, $"Uma unidade de {remedio.Nome} foi adicionada ao carrinho.");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            catch (SqliteException)
            {
                MessageBox.Show(this, "Não foi possível gravar no banco. Tente novamente.");
            }
        }

        private void VerCarrinho_Click(object? sender, EventArgs e)
        {
            using var tela = new CarrinhoForm(_carrinhoController);
            tela.ShowDialog(this);
        }
    }
}