using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2ORM.Classes
{
    public class Song
    {
        public string SongName { get; set; }

        public string ПутьПесни { get; set; } = null!;

        public string Жанр { get; set; } = null!;

        public string? Альбом { get; set; }

        public string Исполнитель { get; set; } = null!;

        public string? Плейлист { get; set; }

        public string UserName { get; set; } = null!;
    }
}
