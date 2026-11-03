using System.Collections.Generic;

namespace FarmaciaCRUD.dados
{
    public interface IRepository<T>
    {
        List<T> Listar();
        void Adicionar(T item);
        bool Atualizar(T item);
        bool Excluir(int id);
    }
}
