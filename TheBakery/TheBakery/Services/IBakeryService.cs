namespace TheBakery.Services
{
    public interface IBakeryService<T, GetDto, PostDto, PutDto>
    {
        Task<(bool, T?)> CreateAsync(PostDto entity);
        Task<bool> UpdateAsync(Guid id, PutDto entity);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<GetDto>?> GetAllAsync();
        Task<GetDto?> GetAsync(Guid id);
        Task<bool> EntityExists(Guid id);
    }
}
