using System;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Timers;
using cdws.Service;
using Newtonsoft.Json.Linq;

namespace cdws
{
    public partial class Cdws : ServiceBase
    {
        public Cdws()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var timer = new Timer(1000);
            timer.Elapsed += Get;
            timer.Start();
        }


        public static void Get(object sender, ElapsedEventArgs e)
        {
            var second = e.SignalTime.Second;
            if (second%60 == 0)
            {
                RunGet();
            }
        }


        public static void RunGet()
        {
            const string url = "http://api.1yyg.com/JPData?action=totalBuyCount";
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";
            try
            {
                var response = (HttpWebResponse) request.GetResponse();
                var stream = response.GetResponseStream();

                var content = "";
                if (stream != null)
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
                var replace = content.Replace("\'", "\"");
                var replace2 = replace.Replace("(", "");
                var replace3 = replace2.Replace(")", "");

                var jsonObj = JObject.Parse(replace3);
                var state = int.Parse(jsonObj["state"].ToString());
                var count = decimal.Parse(jsonObj["count"].ToString());
                var fundTotal = decimal.Parse(jsonObj["fundTotal"].ToString());

                var item = new CdwsItem
                {
                    ItemId = Guid.NewGuid(),
                    Count = count,
                    FoundTotal = fundTotal,
                    ReqStatus = state,
                    Time = DateTime.Now
                };
                Add.AddNewItem(item);
            }
            catch (WebException e)
            {
                if (e.InnerException != null)
                {
                    var exp = new ExceptionLog
                    {
                        ExceptionGuid = Guid.NewGuid(),
                        ExceptionDetails = e.InnerException != null ? string.Empty : e.InnerException.ToString()
                    };
                    Add.AddExceptionLog(exp);
                }
            }
        }

        protected override void OnStop()
        {
        }
    }
}