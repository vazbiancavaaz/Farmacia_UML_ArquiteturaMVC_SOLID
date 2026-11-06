using System.Collections.Generic;
using FarmaciaCRUD.model;

namespace FarmaciaCRUD.dados
{
    public interface ICarrinhoRepository
    {
        List<ItemCarrinho> Listar();
        void Adicionar(int remedioId);
        void AlterarQuantidade(int remedioId, int quantidade);
        void Remover(int remedioId);
        Compra FinalizarCompra(string operacao);
    }
}
