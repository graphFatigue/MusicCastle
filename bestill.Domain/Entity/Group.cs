using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestill.Domain.Entity
{
    public class Group : Artist
    {
        public List<Artist> Members { get; set; }
        //public List<UserSkill> Skills { get; set; }
    }
}
