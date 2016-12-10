using System;
using System.ComponentModel.DataAnnotations;

namespace cdws.Service
{
    public class ExceptionLog
    {
        [Key]
        public Guid ExceptionGuid { get; set; }

        public string ExceptionDetails { get; set; }
    }
}
