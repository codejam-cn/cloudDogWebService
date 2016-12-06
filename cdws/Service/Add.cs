using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace cdws.Service
{
    public class CdwsItem
    {
        public long Count { get; set; }
        public long FoundTotal { get; set; }
        public DateTime Time { get; set; }
        public int ReqStatus { get; set; }
    }

    public class CdwsContext : DbContext
    {
        public CdwsContext(): base("name=DefaultConnection")
        {
            
        }

        public virtual List<CdwsItem> Cdwses { get; set; }
    }


    internal class Add
    {

        public static void AddNewItem(CdwsItem item)
        {
            try
            {
                using (var context = new CdwsContext())
                {
                    var total = new CdwsItem
                    {
                        Count = item.Count,
                        FoundTotal = item.FoundTotal,
                        Time = item.Time,
                        ReqStatus = item.ReqStatus
                    };
                    context.Cdwses.Add(item);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var a = e;
                throw;
            }

        }
    }
}
