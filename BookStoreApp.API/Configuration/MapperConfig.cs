using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Models.Book;

namespace BookStoreApp.API.Configuration
{
    public class MapperConfig :Profile
    {
        public MapperConfig()
        {
            // Author Mapping
            CreateMap<AuthorCreateDto, Author>().ReverseMap();
            CreateMap<AuthorDto,Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();  

            // Book Mapping
            CreateMap<Book, BookListDto>()
                .ForMember(s=>s.AuthorName, d=>d.MapFrom(map=> $"{map.Author.FirstName} {map.Author.LastName}")) 
                .ReverseMap();

            CreateMap<Book, BookDetailsDto>()
                .ForMember(s => s.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();

            CreateMap<Book,BookCreateDto>().ReverseMap();
            CreateMap<Book,BookUpdateDto>().ReverseMap();   
        }
    }
}
