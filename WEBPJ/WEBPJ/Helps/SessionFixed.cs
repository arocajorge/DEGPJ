using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPJ.Helps
{
    public interface ISessionValueProvider
    {
        string IdUsuario { get; set; }
        string IdTransaccionSession { get; set; }
        string IdTransaccionSessionActual { get; set; }
        string TipoUsuario { get; set; }
    }
    public static class SessionFixed
    {
        private static ISessionValueProvider _sessionValueProvider;
        public static void SetSessionValueProvider(ISessionValueProvider provider)
        {
            _sessionValueProvider = provider;
        }

        public static string IdUsuario
        {
            get { return _sessionValueProvider.IdUsuario; }
            set { _sessionValueProvider.IdUsuario = value; }
        }
        public static string IdTransaccionSession
        {
            get { return _sessionValueProvider.IdTransaccionSession; }
            set { _sessionValueProvider.IdTransaccionSession = value; }
        }
        public static string IdTransaccionSessionActual
        {
            get { return _sessionValueProvider.IdTransaccionSessionActual; }
            set { _sessionValueProvider.IdTransaccionSessionActual = value; }
        }
        public static string TipoUsuario
        {
            get { return _sessionValueProvider.TipoUsuario; }
            set { _sessionValueProvider.TipoUsuario = value; }
        }
    }

    public class WebSessionValueProvider : ISessionValueProvider
    {
        private const string _IdVendedor = "PJ_IdUsuario";
        private const string _IdTransaccionSession = "PJ_IdTransaccionSesssion";
        private const string _IdTransaccionSessionActual = "PJ_IdTransaccionSessionActual";
        private const string _TipoUsuario = "PJ_TipoUsuario";

        public string IdUsuario
        {
            get { return (string)HttpContext.Current.Session[_IdVendedor]; }
            set { HttpContext.Current.Session[_IdVendedor] = value; }
        }

        public string IdTransaccionSession
        {
            get { return (string)HttpContext.Current.Session[_IdTransaccionSession]; }
            set { HttpContext.Current.Session[_IdTransaccionSession] = value; }
        }
        public string IdTransaccionSessionActual
        {
            get { return (string)HttpContext.Current.Session[_IdTransaccionSessionActual]; }
            set { HttpContext.Current.Session[_IdTransaccionSessionActual] = value; }
        }
        public string TipoUsuario
        {
            get { return (string)HttpContext.Current.Session[_TipoUsuario]; }
            set { HttpContext.Current.Session[_TipoUsuario] = value; }
        }
    }
}