//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/03/25 - 11:52:13
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract()]
    public partial class consolidadoexamenesResultDto
    {
        [DataMember()]
        public String T_Paciente { get; set; }

        [DataMember()]
        public Nullable<DateTime> T_FechaServicio { get; set; }

        [DataMember()]
        public String T_EmpresaCliente { get; set; }

        [DataMember()]
        public String T_Id_Servicio { get; set; }

        [DataMember()]
        public String T_Nombre_Completo { get; set; }

        [DataMember()]
        public String T_Id_Componente { get; set; }

        [DataMember()]
        public Nullable<Decimal> T_Total { get; set; }

        [DataMember()]
        public String T_GESO { get; set; }

        [DataMember()]
        public String T_IdProtocolo { get; set; }

        [DataMember()]
        public String T_Categoria { get; set; }

        [DataMember()]
        public String T_ServiceComponenteId { get; set; }

        [DataMember()]
        public String T_REALIZADO { get; set; }

        public consolidadoexamenesResultDto()
        {
        }

        public consolidadoexamenesResultDto(String t_Paciente, Nullable<DateTime> t_FechaServicio, String t_EmpresaCliente, String t_Id_Servicio, String t_Nombre_Completo, String t_Id_Componente, Nullable<Decimal> t_Total, String t_GESO, String t_IdProtocolo, String t_Categoria, String t_ServiceComponenteId, String t_REALIZADO)
        {
			this.T_Paciente = t_Paciente;
			this.T_FechaServicio = t_FechaServicio;
			this.T_EmpresaCliente = t_EmpresaCliente;
			this.T_Id_Servicio = t_Id_Servicio;
			this.T_Nombre_Completo = t_Nombre_Completo;
			this.T_Id_Componente = t_Id_Componente;
			this.T_Total = t_Total;
			this.T_GESO = t_GESO;
			this.T_IdProtocolo = t_IdProtocolo;
			this.T_Categoria = t_Categoria;
			this.T_ServiceComponenteId = t_ServiceComponenteId;
			this.T_REALIZADO = t_REALIZADO;
        }
    }
}
