using System;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Timers;
using cdws.Service;
using Newtonsoft.Json;

namespace cdws
{
    public partial class Cdws : ServiceBase
    {
        internal class Json
        {
            public int State { get; set; }
            public long Count { get; set; }
            public long FundTotal { get; set; }
        }


        public Cdws()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Timer timer = new Timer(1000);
            timer.Elapsed += Get;
            timer.Start();
        }


        public static void Test()
        {
            Timer timer = new Timer(1000);
            timer.Elapsed += Get;
            timer.Start();
        }

        public static void Get(object sender, ElapsedEventArgs e)
        {
            int second = e.SignalTime.Second;
            if (second % 60 == 0)
            {
                RunGet();
            }
        }



        public static void RunGet()
        {
            const string url = "http://api.1yyg.com/JPData?action=totalBuyCount";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            //var aaa = request.GetResponse();

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();

                string content;
                using (var sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                }
                var replace = content.Replace("\'", "\"");
                var replace2 = replace.Replace("(", "");
                var replace3 = replace2.Replace(")", "");

                Json result = (Json)JsonConvert.DeserializeObject(replace3, typeof(Json));

                CdwsItem total = new CdwsItem
                {
                   

                    Count = result.Count,
                    FoundTotal = result.FundTotal,
                    ReqStatus = (int) response.StatusCode,
                    Time = DateTime.Now
                };

                Service.Add.AddNewItem(total);


            }
            catch (WebException e)
            {
                var status = ((HttpWebResponse)e.Response).StatusCode;

            }
        }

        protected override void OnStop()
        {
        }
    }
}
