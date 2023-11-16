using Microsoft.EntityFrameworkCore;
using Webcomic.Data;
using Webcomic.Models.Entities;
using Webcomic.Services.Interfaces;

namespace Webcomic.Services.Implementatiions
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;

        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateTagAsync(Tag tag)
        {
            _context.Add(tag);
            return await SaveAsync();
        }

        public async Task<bool> DeleteTagAsync(Tag tag)
        {
            _context.Remove(tag);
            return await SaveAsync();
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetTagByIdAsync(int tagId)
        {
            return await _context.Tags.FindAsync(tagId);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> TagExistsAsync(int tagId)
        {
            return await _context.Tags.AnyAsync(t => t.Id == tagId);
        }

        public async Task<bool> UpdateTagAsync(Tag tag)
        {
            _context.Update(tag);
            return await SaveAsync();
        }
    }
}
