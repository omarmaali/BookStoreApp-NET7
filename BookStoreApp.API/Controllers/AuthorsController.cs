using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using AutoMapper;
using BookStoreApp.API.Exceptions;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
          if (_context.Authors == null)
          {
              return NotFound();
          }
            var authors =  await _context.Authors.ToListAsync();
            List<AuthorDto> result = _mapper.Map<List<AuthorDto>>(authors);
            return result;
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
          if (_context.Authors == null)
          {
              throw new NotFoundException(id.ToString(), "Author");
          }
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                throw new NotFoundException(id.ToString(), "Author");
            }

            AuthorDto result = _mapper.Map<AuthorDto>(author);
            return result;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
            if (id != authorDto.Id)
            {
                return BadRequest();
            }

            Author author = _mapper.Map<Author>(authorDto);
            _context.Entry(author).State = EntityState.Modified;

        
            
                await _context.SaveChangesAsync();
            
           

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorDto)
        {
          Author author =   _mapper.Map<Author>(authorDto);
          if (_context.Authors == null)
          {
              return Problem("Entity set 'BookStoreDbContext.Authors'  is null.");
          }
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (_context.Authors == null)
            {
                throw new NotFoundException(id.ToString(), "Author");
            }
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                throw new NotFoundException(id.ToString(), "Author");
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return (_context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
