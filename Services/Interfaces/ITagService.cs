using Webcomic.Models.Entities;

namespace Webcomic.Services.Interfaces
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAllTags();
        Tag GetTagById(int tagId);
        bool TagExists(int tagId);
        bool CreateTag(Tag tag);
        bool UpdateTag(Tag tag);
        bool DeleteTag(Tag tag);
        bool Save();
    }
}
