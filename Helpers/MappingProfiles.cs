using AutoMapper;
using Webcomic.Models.DTOs;
using Webcomic.Models.Entities;

namespace Webcomic.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Chapter, ChapterDto>();
            CreateMap<ChapterDto, Chapter>();
            CreateMap<Comic, ComicDto>();
            CreateMap<ComicDto, Comic>();

        }

    }
}
