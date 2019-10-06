using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using WEBPJ.Data;
using WEBPJ.Info;
using System.Collections.Generic;

namespace WEBPJ.Reportes
{
    public partial class Rpt_001 : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public Rpt_001()
        {
            InitializeComponent();
        }

        private void Rpt_001_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;

            int IdProducto = p_IdProducto.Value == null ? 0 : Convert.ToInt32(p_IdProducto.Value);
            string ProvCodigo = p_ProvCodigo.Value == null ? "" : Convert.ToString(p_ProvCodigo.Value);
            string IdUsuario = p_IdUsuario.Value == null ? "" : Convert.ToString(p_IdUsuario.Value);
            string Estado = p_Estado.Value == null ? "" : Convert.ToString(p_Estado.Value);
            DateTime fecha_ini = p_fecha_ini.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_ini.Value);
            DateTime fecha_fin = p_fecha_fin.Value == null ? DateTime.Now : Convert.ToDateTime(p_fecha_fin.Value);

            Compra_Data data_compra = new Compra_Data();
            List<Compra_Info> lst_rpt = data_compra.get_list_rpt(ProvCodigo, IdUsuario, IdProducto, Estado, fecha_ini, fecha_fin);
            this.DataSource = lst_rpt;
        }
    }
}
