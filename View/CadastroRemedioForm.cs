using System;
using System.Globalization;
using System.Windows.Forms;
using FarmaciaCRUD.controller;
using FarmaciaCRUD.model;

namespace Farmacia
{
    public partial class CadastroRemedioForm : Form
    {
        private readonly RemedioController _controller;

        public CadastroRemedioForm(RemedioController controller)
        {
            InitializeComponent();

            _controller = controller;
        }

        private void SalvarBtn_Click(object sender, EventArgs e)
        {
            var cultura = System.Globalization.CultureInfo.GetCultureInfo("pt-BR");

            if (!decimal.TryParse(PrecoTxb.Text.Trim(),
                System.Globalization.NumberStyles.AllowDecimalPoint,
                cultura, out decimal preco))
            {
                MessageBox.Show("Digite um preço válido, como 12,90.");
                PrecoTxb.Focus();
                return;
            }

            var remedio = new Remedio
            {
                Nome = NomeTxb.Text.Trim(),
                Descricao = DescricaoTxb.Text.Trim(),
                Preco = preco
            };

            try
            {
                _controller.Cadastrar(remedio);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}