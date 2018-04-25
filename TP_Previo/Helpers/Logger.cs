using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TP_Previo.Helpers
{
    public class Logger
    {
        string path;
        HttpServerUtility server = HttpContext.Current.Server;
        public Logger(string locationString)
        {
            path = server.MapPath(@locationString);
        }

        public void Log(string eventToLog)
        {
            StreamWriter wr = new StreamWriter(path,true);
            JObject jsonObject = new JObject();
            jsonObject.Add("TimeStamp", DateTime.Now);
            jsonObject.Add("Event", eventToLog);
            using (JsonTextWriter writer = new JsonTextWriter(wr))
            {
                jsonObject.WriteTo(writer);
            }
            wr.Close();
        }
    }
}