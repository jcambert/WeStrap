using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.ComponentModel.DataAnnotations;

namespace WeStrapApplication.Models
{
    public class BookModel
    {
        [Required]
        public Date Start{ get; set; }
        [Required]
        public Date End { get; set; }
        [Required]
        public string Location { get; set; }
        [Range(1,int.MaxValue)]
        public int Adults { get; set; } = 1;
        [Range(0, int.MaxValue)]
        public int Childs { get; set; } = 0;
    }
}
