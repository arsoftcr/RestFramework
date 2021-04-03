using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestFramework
{
    public class ManagerRest
    {

        private static RestClient cliente;

        private static RestRequest solicitud;

        private static IRestResponse respuesta;

        public static RestClient Cliente { get => cliente; set => cliente = value; }
        public static RestRequest Solicitud { get => solicitud; set => solicitud = value; }
        public static IRestResponse Respuesta { get => respuesta; set => respuesta = value; }

        public static string Call(string url, Method metodo,
            List<RestSharp.Parameter> parameters = null, List<RestSharp.HttpHeader> headers = null)
        {
            Respuesta = null;

            string contenido = "";

            try
            {
                Cliente = new RestClient(url);

                Solicitud = new RestRequest(metodo);


                if ((headers ?? new List<HttpHeader>()).Count > 0)
                {

                    foreach (var item in headers)
                    {
                        Solicitud.AddHeader(item.Name, item.Value);
                    }


                }


                if ((parameters ?? new List<Parameter>()).Count > 0)
                {

                    foreach (var item in parameters)
                    {
                        Solicitud.AddParameter(item.Name, item.Value, item.Type);
                    }


                }

                Respuesta = Cliente.Execute(Solicitud);

                contenido = Respuesta != null ? Respuesta.Content : "sin respuesta";

            }
            catch (Exception e)
            {

                contenido = "error";
            }



            return contenido;

        }




    }
}
