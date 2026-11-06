using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using FarmaciaCRUD.controller;
using FarmaciaCRUD.model;

namespace Farmacia.View
{
    public class CarrinhoForm : Form
    {
        private readonly CarrinhoController _controller;
        private readonly string _operacao = Guid.NewGuid().ToString();
        private readonly DataGridView _lista = new DataGridView();
        private readonly NumericUpDown _quantidade = new NumericUpDown();
        private readonly Label _total = new Label();
        private readonly Button _alterar = new Button();
        private readonly Button _remover = new Button();
        private readonly Button _finalizar = new Button();
        private readonly CultureInfo _cultura = CultureInfo.GetCultureInfo("pt-BR");

        public CarrinhoForm(CarrinhoController controller)
        {
            _controller = controller;
            Text = "Carrinho de compras";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(960, 480);
            MinimumSize = new Size(900, 420);
            Font = new Font("Segoe UI", 10);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 3
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            Controls.Add(layout);

            _lista.Dock = DockStyle.Fill;
            _lista.AutoGenerateColumns = false;
            _lista.ReadOnly = true;
            _lista.AllowUserToAddRows = false;
            _lista.AllowUserToDeleteRows = false;
            _lista.MultiSelect = false;
            _lista.RowHeadersVisible = false;
            _lista.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _lista.BackgroundColor = Color.White;
            _lista.RowTemplate.Height = 36;
            _lista.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Remédio", DataPropertyName = "Nome",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 180
            });
            _lista.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Quantidade", DataPropertyName = "Quantidade", Width = 115
            });
            _lista.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Preço unitário", DataPropertyName = "PrecoUnitario", Width = 155,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", FormatProvider = _cultura,
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            _lista.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Subtotal", DataPropertyName = "Subtotal", Width = 155,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", FormatProvider = _cultura,
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            _lista.SelectionChanged += (_, _) => AtualizarSelecao();
            layout.Controls.Add(_lista, 0, 0);

            var edicao = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill, AutoSize = true,
                Padding = new Padding(0, 10, 0, 6)
            };
            edicao.Controls.Add(new Label
            {
                Text = "Quantidade", AutoSize = true, Margin = new Padding(3, 8, 6, 0)
            });
            _quantidade.Minimum = 1;
            _quantidade.Maximum = 999;
            _quantidade.Width = 80;
            _quantidade.Margin = new Padding(3, 5, 12, 3);
            edicao.Controls.Add(_quantidade);
            _alterar.Text = "Salvar quantidade";
            _alterar.AutoSize = true;
            _alterar.Padding = new Padding(8, 4, 8, 4);
            _alterar.Click += (_, _) => ExecutarAcao(AlterarQuantidade);
            edicao.Controls.Add(_alterar);
            _remover.Text = "Excluir do carrinho";
            _remover.AutoSize = true;
            _remover.Padding = new Padding(8, 4, 8, 4);
            _remover.Click += (_, _) => ExecutarAcao(RemoverItem);
            edicao.Controls.Add(_remover);
            var atualizar = new Button
            {
                Text = "Atualizar lista", AutoSize = true, Padding = new Padding(8, 4, 8, 4)
            };
            atualizar.Click += (_, _) => ExecutarAcao(Recarregar);
            edicao.Controls.Add(atualizar);
            layout.Controls.Add(edicao, 0, 1);

            var rodape = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill, AutoSize = true, Padding = new Padding(0, 6, 0, 0)
            };
            _total.AutoSize = true;
            _total.Font = new Font(Font, FontStyle.Bold);
            _total.Margin = new Padding(3, 8, 24, 0);
            rodape.Controls.Add(_total);
            _finalizar.Text = "Finalizar compra";
            _finalizar.AutoSize = true;
            _finalizar.Padding = new Padding(10, 5, 10, 5);
            _finalizar.Click += (_, _) => ExecutarAcao(FinalizarCompra);
            rodape.Controls.Add(_finalizar);
            var fechar = new Button
            {
                Text = "Continuar comprando", AutoSize = true,
                Padding = new Padding(10, 5, 10, 5), DialogResult = DialogResult.Cancel
            };
            rodape.Controls.Add(fechar);
            CancelButton = fechar;
            layout.Controls.Add(rodape, 0, 2);
            Shown += (_, _) => ExecutarAcao(Recarregar);
        }

        private void Recarregar()
        {
            var itens = _controller.Listar();
            _lista.DataSource = itens;
            _total.Text = "Total: " + itens.Sum(item => item.Subtotal).ToString("C2", _cultura);
            _finalizar.Enabled = itens.Count > 0;
            AtualizarSelecao();
        }

        private void AtualizarSelecao()
        {
            var item = _lista.CurrentRow?.DataBoundItem as ItemCarrinho;
            _alterar.Enabled = item != null;
            _remover.Enabled = item != null;
            _quantidade.Enabled = item != null;
            _quantidade.Value = item?.Quantidade ?? 1;
        }

        private void AlterarQuantidade()
        {
            if (_lista.CurrentRow?.DataBoundItem is not ItemCarrinho item)
                return;

            _controller.AlterarQuantidade(item.RemedioId, decimal.ToInt32(_quantidade.Value));
            Recarregar();
        }

        private void RemoverItem()
        {
            if (_lista.CurrentRow?.DataBoundItem is not ItemCarrinho item)
                return;

            var resposta = MessageBox.Show(this, $"Excluir {item.Nome} do carrinho?",
                "Excluir item", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (resposta != DialogResult.Yes)
                return;

            _controller.Remover(item.RemedioId);
            Recarregar();
        }

        private void FinalizarCompra()
        {
            // Lê novamente o carrinho antes de mostrar a confirmação.
            Recarregar();
            if (!_finalizar.Enabled)
                return;

            var resposta = MessageBox.Show(this, _total.Text + "\nConfirmar a compra?",
                "Finalizar compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (resposta != DialogResult.Yes)
                return;

            _finalizar.Enabled = false;
            try
            {
                Compra compra = _controller.FinalizarCompra(_operacao);
                MessageBox.Show(this,
                    $"Compra nº {compra.Id} salva!\nTotal: {compra.Total.ToString("C2", _cultura)}",
                    "Compra finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            finally
            {
                _finalizar.Enabled = true;
            }
        }

        private void ExecutarAcao(Action acao)
        {
            try
            {
                acao();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Confira os dados");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(this, ex.Message, "Carrinho");
            }
            catch (SqliteException)
            {
                MessageBox.Show(this,
                    "Não foi possível acessar o banco. Confira o acesso ao arquivo e tente novamente.",
                    "Banco de dados");
            }
        }
    }
}
