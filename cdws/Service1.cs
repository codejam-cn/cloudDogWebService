using System;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Timers;
using cdws.Edmx;
using Newtonsoft.Json;

namespace cdws
{
    public partial class Cdws : ServiceBase
    {
        internal class Json
        {
            public int state { get; set; }
            public string count { get; set; }
            public string fundTotal { get; set; }
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

                Total total = new Total
                {
                    count = result.count,
                    fundTotal = result.fundTotal,
                    reqStatus = (int?) response.StatusCode,
                    time = DateTime.Now
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
