using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Core.DTO
{
    public record WatchlistDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime AddedDate { get; set; }
        public MovieDTO Movie { get; set; }
    }
}
