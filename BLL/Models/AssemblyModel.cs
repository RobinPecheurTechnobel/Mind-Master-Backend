using BLL.Models.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class AssemblyModel : AssemblySimpleModel
    {
        public AssemblyModel() { }
        public AssemblyModel(AssemblySimpleModel simpleModel)
        {
            Id = simpleModel.Id;
            Title = simpleModel.Title;
            Concepts = null;
        }
        public IEnumerable<ConceptAssemblyModel> Concepts { get; set; }
    }

    public class AssemblySimpleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
