namespace cdws.Service
{
    internal class Add
    {
        internal static CdwsContext Context = CdwsContext.Db;
        public static void AddNewItem(CdwsItem item)
        {
            Context.Cdwses.Add(item);
            Context.SaveChanges();
        }
        public static void AddExceptionLog(ExceptionLog exp)
        {
            Context.ExceptionLogs.Add(exp);
            Context.SaveChanges();
        }
    }
}
