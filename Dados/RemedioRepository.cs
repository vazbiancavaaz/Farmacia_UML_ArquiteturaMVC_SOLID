using System.Collections.Generic;
using FarmaciaCRUD.model;

namespace FarmaciaCRUD.dados
{
    public class RemedioRepository : IRepository<Remedio>
    {
        private readonly List<Remedio> _remedios = new List<Remedio>();
        private int _proximoId = 1;

        public List<Remedio> Listar()
        {
            return new List<Remedio>(_remedios);
        }

        public void Adicionar(Remedio remedio)
        {
            remedio.Id = _proximoId++;
            _remedios.Add(remedio);
        }

        public bool Atualizar(Remedio remedio)
        {
            int indice = _remedios.FindIndex(r => r.Id == remedio.Id);

            if (indice == -1)
                return false;

            _remedios[indice] = remedio;
            return true;
        }

        public bool Excluir(int id)
        {
            return _remedios.RemoveAll(r => r.Id == id) > 0;
        }
    }
}
