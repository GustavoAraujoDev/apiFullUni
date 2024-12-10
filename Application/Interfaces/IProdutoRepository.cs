using apiFullUni.domain.Entities;

namespace apiFullUni.Application.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto?> GetByIdAsync(int id);
        Task<IEnumerable<Produto>> GetAllAsync();
        Task AddAsync(Produto produto);
        Task UpdateAsync(Produto produto);

    }
}
