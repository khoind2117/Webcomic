using AutoMapper;
using Webcomic.Data;
using Webcomic.Models.DTOs;
using Webcomic.Models.DTOs.ChapterDtos;
using Webcomic.Models.DTOs.ComicDtos;
using Webcomic.Models.DTOs.TagDtos;
using Webcomic.Models.Entities;

namespace Webcomic.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // CreateMap<Source, Destination>();

            #region Comic
            CreateMap<Comic, ListComicDto>();

            CreateMap<Comic, DetailComicDto>()
            .ForMember(dest => dest.TagList, opt => opt.MapFrom(src =>
                src.ComicTags.Select(ct => ct.Tag.Name).ToList()));
            //.ForMember(dest => dest.ChapterNumber, opt => opt.MapFrom(src =>
            //    src.Chapters.Select(c => c.ChapterNumber).ToList()))
            //.ForMember(dest => dest.ChapterName, opt => opt.MapFrom(src =>
            //    src.Chapters.Select(c => c.Name).ToList()));

            CreateMap<CreateComicDto, Comic>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ComicTags, opt => opt.MapFrom(src =>
                    src.SelectedTagIds.Select(tagId => new ComicTag { TagId = tagId }).ToList()));

            CreateMap<UpdateComicDto, Comic>()
                .ForMember(dest => dest.ComicTags, opt => opt.MapFrom(src =>
                    src.SelectedTagIds.Select(tagId => new ComicTag { TagId = tagId }).ToList()));              ;
            #endregion

            #region Tag
            CreateMap<Tag, TagDto>().ReverseMap();
            #endregion

            #region Chapter
            CreateMap<Chapter, ListChapterDto>();

            CreateMap<CreateChapterDto, Chapter>()
                .ForMember(dest => dest.UploadDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ComicId, opt => opt.MapFrom(src => src.ComicId));

            CreateMap<UpdateChapterDto, Chapter>();
            #endregion






        }

    }
}
