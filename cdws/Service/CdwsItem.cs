using System;
using System.ComponentModel.DataAnnotations;

namespace cdws.Service
{
    public class CdwsItem
    {
        [Key]
        public Guid ItemId { get; set; }
        public decimal Count { get; set; }
        public decimal FoundTotal { get; set; }
        public DateTime Time { get; set; }
        public int ReqStatus { get; set; }
    }
}
