﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPJ.Info
{
    public class Enumeradores
    {
        public enum eTipoUsuario
        {
            ADMINISTRADOR,
            USUARIO
        }

        public enum eEscogerPrecioPor
        {
            MASALTO,
            MASBAJO,
            CERCANO
        }

        public enum eValorOptimo
        {
            MINIMO,
            MAXIMO
        }
    }
}