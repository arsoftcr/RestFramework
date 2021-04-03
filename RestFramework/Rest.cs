using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestFramework
{
    public class Rest : IDisposable
    {

        public static List<HttpHeader> HttpHeaders { get; set; }
        public static List<Parameter> Parameters { get; set; }

        public async Task<string> Data(string purl, Method method,
            string json = "", object param = null,
            List<HttpHeader> pHttpHeaders = null, List<Parameter> pParameters = null)
        {
            string ok = "";

            string payload = string.Empty;

            await Task.Run(() =>
            {
                bool paramete = false;
                bool header = false;
                bool jsonreq = false;
                if (!string.IsNullOrWhiteSpace(json))
                {
                    jsonreq = true;
                }
                if (pParameters != null)
                {
                    paramete = true;
                }
                if (pHttpHeaders != null)
                {
                    header = true;
                }
                if (jsonreq && paramete && header)
                {
                    payload = ManagerRest.Call($"{purl}",
                            method, pParameters, pHttpHeaders);
                }
                if (!jsonreq && !paramete && !header)
                {
                    payload = ManagerRest.Call($"{purl}",
                           method, null, null);
                }
                if (!jsonreq && paramete && header)
                {
                    payload = ManagerRest.Call($"{purl}",
                         method, pParameters, pHttpHeaders);
                }
                if (jsonreq && !paramete && header)
                {
                    payload = ManagerRest.Call($"{purl}",
                         method, pParameters, pHttpHeaders);
                }
                if (jsonreq && paramete && !header)
                {
                    payload = ManagerRest.Call($"{purl}",
                         method, pParameters, null);
                }
                if (!jsonreq && !paramete && header)
                {
                    payload = ManagerRest.Call($"{purl}",
                         method, null, pHttpHeaders);
                }
                if (jsonreq && !paramete && !header)
                {
                    payload = ManagerRest.Call($"{purl}",
                         method, pParameters, null);
                }

                ok = payload;

            });


            return ok;
        }

        public void Dispose()
        {
            HttpHeaders = null;
            Parameters = null;
        }
    }
}
