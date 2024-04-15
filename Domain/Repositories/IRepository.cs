namespace Domain.Repositories;

public interface IRepository
{
    Task<IList<T>> GetByIdsDtoAsync<T>(IEnumerable<Guid> ids);
    Task<T?> GetByIdDtoAsync<T>(Guid id);
}