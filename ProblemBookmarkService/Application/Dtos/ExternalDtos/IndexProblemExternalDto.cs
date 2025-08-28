using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ExternalDtos
{
    public class IndexProblemExternalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public List<TagExternalDto> Tags { get; set; }
        public bool IsDelete { get; set; }
    }
}
