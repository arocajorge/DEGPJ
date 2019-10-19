using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPJ.Info
{
    public class indocume_Info
    {
        public string tipo { get; set; }
        public decimal numero { get; set; }
        public decimal pedido { get; set; }
        public System.DateTime fecha { get; set; }
        public System.DateTime fecha_fac { get; set; }
        public System.DateTime fecha_com { get; set; }
        public string producto { get; set; }
        public string nombre { get; set; }
        public decimal cantidad { get; set; }
        public string proveedor { get; set; }
        public string concepto { get; set; }
        public decimal plazo { get; set; }
        public decimal porc_desc { get; set; }
        public decimal factor { get; set; }
        public decimal total { get; set; }
        public decimal costo { get; set; }
        public decimal fob { get; set; }
        public string orden { get; set; }
        public string comentario { get; set; }
        public string tip_aplic { get; set; }
        public decimal num_aplic { get; set; }
        public string tip_aplix { get; set; }
        public decimal num_aplix { get; set; }
        public bool bloqueado { get; set; }
        public bool aprobado { get; set; }
        public string usuario { get; set; }
        public System.DateTime digitado { get; set; }
        public System.DateTime fecha_apr { get; set; }
        public decimal cantidad_op { get; set; }
        public bool impreso { get; set; }
        public bool eliminado { get; set; }
        public string bodega_int { get; set; }
        public string bodega_orig { get; set; }
        public bool solicita { get; set; }
        public bool aceptado { get; set; }
        public System.DateTime fecha_soli { get; set; }
        public System.DateTime fecha_recep { get; set; }
        public System.DateTime fecha_acep { get; set; }
        public bool recibido { get; set; }
        public string lote { get; set; }
        public string num_recibo { get; set; }
        public string usr_agr { get; set; }
        public string usr_cor { get; set; }
        public string centro { get; set; }
    }
}