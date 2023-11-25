using Webcomic.Models.Entities;

namespace Webcomic.Services.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag> GetTagByIdAsync(int tagId);
        Task<bool> TagExistsAsync(int tagId);
        Task<bool> CreateTagAsync(Tag tag);
        Task<bool> UpdateTagAsync(Tag tag);
        Task<bool> DeleteTagAsync(Tag tag);
        Task<bool> SaveAsync();

        Task<Tag> GetTagByNameAsync(string name);
    }
}
