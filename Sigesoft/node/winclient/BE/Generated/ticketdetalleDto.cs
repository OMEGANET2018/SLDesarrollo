//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/01 - 16:28:29
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
    public partial class ticketdetalleDto
    {
        [DataMember()]
        public String v_TicketDetalleId { get; set; }

        [DataMember()]
        public String v_TicketId { get; set; }

        [DataMember()]
        public String v_Descripcion { get; set; }

        [DataMember()]
        public String v_IdProductoDetalle { get; set; }

        [DataMember()]
        public String v_CodInterno { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_Cantidad { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_PrecioVenta { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_SaldoPaciente { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_SaldoAseguradora { get; set; }

        [DataMember()]
        public Nullable<Int32> i_EsDespachado { get; set; }

        [DataMember()]
        public String v_IdUnidadProductiva { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsDeleted { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UpdateUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        [DataMember()]
        public ticketDto ticket { get; set; }

        public ticketdetalleDto()
        {
        }

        public ticketdetalleDto(String v_TicketDetalleId, String v_TicketId, String v_Descripcion, String v_IdProductoDetalle, String v_CodInterno, Nullable<Decimal> d_Cantidad, Nullable<Decimal> d_PrecioVenta, Nullable<Decimal> d_SaldoPaciente, Nullable<Decimal> d_SaldoAseguradora, Nullable<Int32> i_EsDespachado, String v_IdUnidadProductiva, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, ticketDto ticket)
        {
			this.v_TicketDetalleId = v_TicketDetalleId;
			this.v_TicketId = v_TicketId;
			this.v_Descripcion = v_Descripcion;
			this.v_IdProductoDetalle = v_IdProductoDetalle;
			this.v_CodInterno = v_CodInterno;
			this.d_Cantidad = d_Cantidad;
			this.d_PrecioVenta = d_PrecioVenta;
			this.d_SaldoPaciente = d_SaldoPaciente;
			this.d_SaldoAseguradora = d_SaldoAseguradora;
			this.i_EsDespachado = i_EsDespachado;
			this.v_IdUnidadProductiva = v_IdUnidadProductiva;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.ticket = ticket;
        }
    }
}