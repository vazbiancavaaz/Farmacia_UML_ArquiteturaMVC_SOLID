using System;
using System.Collections.Generic;
using FarmaciaCRUD.dados;
using FarmaciaCRUD.model;

namespace FarmaciaCRUD.controller
{
    public class RemedioController
    {
        private readonly IRepository<Remedio> _repositorio;

        public RemedioController(IRepository<Remedio> repositorio)
        {
            _repositorio = repositorio;
        }

        public List<Remedio> Listar()
        {
            return _repositorio.Listar();
        }

        public void Cadastrar(Remedio remedio)
        {
            Validar(remedio);
            _repositorio.Adicionar(remedio);
        }

        public bool Atualizar(Remedio remedio)
        {
            Validar(remedio);
            return _repositorio.Atualizar(remedio);
        }

        public bool Excluir(int id)
        {
            return _repositorio.Excluir(id);
        }

        private static void Validar(Remedio remedio)
        {
            if (string.IsNullOrWhiteSpace(remedio.Nome))
                throw new ArgumentException("Informe o nome do remédio.");

            if (remedio.Preco <= 0)
                throw new ArgumentException("O preço deve ser maior que zero.");
        }
    }
}