using BLL.Models.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ConceptModel :ConceptSimpleModel
    {
        public ConceptModel(){}
        public ConceptModel(ConceptSimpleModel simpleModel)
        {
            Id = simpleModel.Id;
            Title = simpleModel.Title;
            Ideas = null;
        }
        public IEnumerable<ConceptIdeaModel> Ideas { get; set; }
    }
    public class ConceptSimpleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
