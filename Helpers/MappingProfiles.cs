using AutoMapper;
using Webcomic.Models.DTOs;
using Webcomic.Models.Entities;

namespace Webcomic.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Comic, ComicDto>().ReverseMap();
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<Chapter, ChapterDto>().ReverseMap();

        }

    }
}
