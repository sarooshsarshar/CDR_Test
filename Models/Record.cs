using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Record
    {
        public int Id { get; set; }
        public  long?  Caller_id { get; set; }
        public long? Recipient { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateOnly? Call_date { get; set;  }
        public TimeSpan? End_time { get; set; }
        public int? Duration { get; set; }

        [Column(TypeName = "decimal(18, 3)")] // 18 is the maximum precision, 3 is the scale (decimal places)        
        public decimal? Cost { get; set; }
        public string? Reference { get; set; }
        public string? Currency { get; set; }
    }
}