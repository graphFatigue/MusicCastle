using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Domain.Entity
{
    public class Song : BaseEntity
    {
        public string Title { get; set; }
        public TimeSpan Length { get; set; }
        public string Artist { get; set; }
    }
}
