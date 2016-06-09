using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cdws.Edmx;

namespace cdws.Service
{
    internal class Add
    {

        public static void AddNewItem(Total item)
        {
            try
            {
                using (CloudDogEntities context = new CloudDogEntities())
                {

                    var total = new Total
                    {
                        count = item.count,
                        fundTotal = item.fundTotal,
                        time = item.time,
                        reqStatus = item.reqStatus
                    };



                    context.Totals.Add(total);

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
