using System;
using System.Collections.Generic;
using FarmaciaCRUD.dados;
using FarmaciaCRUD.model;

namespace FarmaciaCRUD.controller
{
    public class CarrinhoController
    {
        private readonly ICarrinhoRepository _repositorio;

        public CarrinhoController(ICarrinhoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public List<ItemCarrinho> Listar() => _repositorio.Listar();

        public void Adicionar(int remedioId)
        {
            ValidarId(remedioId);
            _repositorio.Adicionar(remedioId);
        }

        public void AlterarQuantidade(int remedioId, int quantidade)
        {
            ValidarId(remedioId);
            if (quantidade < 1 || quantidade > 999)
                throw new ArgumentException("A quantidade deve estar entre 1 e 999.");

            _repositorio.AlterarQuantidade(remedioId, quantidade);
        }

        public void Remover(int remedioId)
        {
            ValidarId(remedioId);
            _repositorio.Remover(remedioId);
        }

        public Compra FinalizarCompra(string operacao)
        {
            if (!Guid.TryParse(operacao, out _))
                throw new ArgumentException("A identificação da compra é inválida.");

            return _repositorio.FinalizarCompra(operacao);
        }

        private static void ValidarId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Selecione um remédio válido.");
        }
    }
}
