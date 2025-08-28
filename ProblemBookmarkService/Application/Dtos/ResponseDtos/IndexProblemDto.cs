 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ExternalModel;

namespace Application.Dtos.ResponseDtos
{
    public class IndexProblemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tag> Tag { get; set; }
        public int Level { get; set; }
    }
}
