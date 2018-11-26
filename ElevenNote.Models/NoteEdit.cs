using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class NoteEdit
    {
        public int NoteId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Title must be at least 2 characters long")]
        [MaxLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
        public string Title { get; set; }

        [MaxLength(8000)]
        public string Content { get; set; }
    }
}
