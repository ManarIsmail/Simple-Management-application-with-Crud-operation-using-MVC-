using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Code Field is Required" )]
        public string Code { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Dateofcreation { get; set; }=DateTime.Now;
    }
}
