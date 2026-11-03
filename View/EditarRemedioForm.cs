using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Globalization;
using FarmaciaCRUD.controller;
using FarmaciaCRUD.model;

namespace Farmacia.View
{
    public partial class EditarRemedioForm : Form
    {
        public EditarRemedioForm()
        {
            InitializeComponent();
        }


        private RemedioController _controller = null!;
        private int _id;

        public EditarRemedioForm(RemedioController controller, Remedio remedio)
            : this()
        {
            _controller = controller;
            _id = remedio.Id;

            Text = "Editar remédio";
            Nometxt.Text = remedio.Nome;
            Descricaotxt.Text = remedio.Descricao;
            Precotxt.Text = remedio.Preco.ToString(CultureInfo.GetCultureInfo("pt-BR"));
        }

        private void SalvarBtn_Click(object sender, EventArgs e)
        {
            var cultura = CultureInfo.GetCultureInfo("pt-BR");

            if (!decimal.TryParse(Precotxt.Text.Trim(), NumberStyles.AllowDecimalPoint,
                cultura, out decimal preco))
            {
                MessageBox.Show("Digite um preço válido, como 12,90.");
                Precotxt.Focus();
                return;
            }

            try
            {
                var remedio = new Remedio
                {
                    Id = _id,
                    Nome = Nometxt.Text.Trim(),
                    Descricao = Descricaotxt.Text.Trim(),
                    Preco = preco
                };

                if (!_controller.Atualizar(remedio))
                {
                    MessageBox.Show("Esse remédio não foi encontrado.");
                    return;
                }
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
