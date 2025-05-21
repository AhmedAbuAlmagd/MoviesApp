using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoviesApp.Data.Models
{
    [PrimaryKey(nameof(MovieId), nameof(CategoryId))]
    public class MovieCategory
    {
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        [JsonIgnore]
        public virtual Movie Movie { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
