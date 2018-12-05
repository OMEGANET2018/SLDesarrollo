﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;
using System.Data.Objects;
using ConnectionState = System.Data.ConnectionState;

namespace Sigesoft.Node.WinClient.BLL
{
    public class HospitalizacionBL
    {
        public List<HospitalizacionList> GetHospitalizacionPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.hospitalizacion
                            join person B in dbContext.person on A.v_PersonId equals B.v_PersonId
                            //join G in dbContext.product on F.v_IdProductoDetalle equals G.v_ProductId

                            where A.i_IsDeleted == 0

                            select new HospitalizacionList
                            {
                                d_FechaAlta = A.d_FechaAlta,
                                d_FechaIngreso = A.d_FechaIngreso,
                                v_Paciente = B.v_FirstName + " " + B.v_FirstLastName + " " + B.v_SecondLastName,
                                v_HopitalizacionId = A.v_HopitalizacionId,
                                v_PersonId = A.v_PersonId,
                                i_IsDeleted = A.i_IsDeleted.Value,
                                v_Comentario = A.v_Comentario
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_FechaIngreso >= @0 && d_FechaIngreso <= @1", pdatBeginDate.Value, pdatEndDate.Value);
                }
                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                if (pintPageIndex.HasValue && pintResultsPerPage.HasValue)
                {
                    int intStartRowIndex = pintPageIndex.Value * pintResultsPerPage.Value;
                    query = query.Skip(intStartRowIndex);
                }
                if (pintResultsPerPage.HasValue)
                {
                    query = query.Take(pintResultsPerPage.Value);
                }

                List<HospitalizacionList> objData = query.ToList();

                // en hospitalizaciones tienes los padres
                var hospitalizaciones = (from a in objData
                                         select new HospitalizacionList
                                         {
                                             v_Paciente = a.v_Paciente,
                                             d_FechaIngreso = a.d_FechaIngreso,
                                             d_FechaAlta = a.d_FechaAlta,
                                             v_HopitalizacionId = a.v_HopitalizacionId,
                                             v_PersonId = a.v_PersonId,
                                             v_Comentario = a.v_Comentario
                                         }).ToList();

                var objtData = hospitalizaciones.AsEnumerable()
                    .Where(a => a.v_HopitalizacionId != null)
                    .GroupBy(b => b.v_HopitalizacionId)
                    .Select(group => group.First());

                List<HospitalizacionList> obj = objtData.ToList();

                HospitalizacionList hospit;
                List<HospitalizacionList> Lista = new List<HospitalizacionList>();

                foreach (var item in obj)
                {
                    hospit = new HospitalizacionList();

                    hospit.v_HopitalizacionId = item.v_HopitalizacionId;
                    hospit.v_PersonId = item.v_PersonId;
                    hospit.v_Paciente = item.v_Paciente;
                    hospit.d_FechaIngreso = item.d_FechaIngreso;
                    hospit.d_FechaAlta = item.d_FechaAlta;
                    hospit.v_Comentario = item.v_Comentario;
                    // estos son los hijos de 1 hopitalización
                    var servicios = BuscarServiciosHospitalizacion(item.v_HopitalizacionId).ToList();

                    List<HospitalizacionServiceList> HospitalizacionServicios = new List<HospitalizacionServiceList>();
                    if (servicios.Count > 0)
                    {
                        HospitalizacionServiceList oHospitalizacionServiceList;
                        ComponentesHospitalizacion oComponentesHospitalizacion;
                        foreach (var servicio in servicios)
                        {
                            oHospitalizacionServiceList = new HospitalizacionServiceList();
                            // acá vas poblando la entidad hijo hospitalización
                            oHospitalizacionServiceList.v_HopitalizacionId = servicio.v_HopitalizacionId;
                            oHospitalizacionServiceList.v_ServiceId = servicio.v_ServiceId;
                            oHospitalizacionServiceList.v_HospitalizacionServiceId = servicio.v_HospitalizacionServiceId;
                            oHospitalizacionServiceList.d_ServiceDate = servicio.d_ServiceDate;
                            oHospitalizacionServiceList.v_ProtocolName = servicio.v_ProtocolName;
                            oHospitalizacionServiceList.v_ProtocolId = servicio.v_ProtocolId;
                            // acá estoy agregando a las lista
                            HospitalizacionServicios.Add(servicio);
                        }
                        hospit.Servicios = HospitalizacionServicios;
                    }

                    #region Habitaciones
                    var habitacioenes = BuscarHospitalizacionHabitaciones(item.v_HopitalizacionId).ToList();
                    List<HospitalizacionHabitacionList> ListaHabitaciones = new List<HospitalizacionHabitacionList>();
                    HospitalizacionHabitacionList oHospitalizacionHabitacionList;
                    foreach (var habitacion in habitacioenes)
                    {
                        oHospitalizacionHabitacionList = new HospitalizacionHabitacionList();
                        oHospitalizacionHabitacionList.v_HospitalizacionHabitacionId = habitacion.v_HospitalizacionHabitacionId;
                        oHospitalizacionHabitacionList.v_HopitalizacionId = item.v_HopitalizacionId;
                        oHospitalizacionHabitacionList.i_HabitacionId = habitacion.i_HabitacionId;
                        oHospitalizacionHabitacionList.NroHabitacion = habitacion.NroHabitacion;
                        oHospitalizacionHabitacionList.d_StartDate = habitacion.d_StartDate;
                        oHospitalizacionHabitacionList.d_EndDate = habitacion.d_EndDate;
                        oHospitalizacionHabitacionList.d_Precio = habitacion.d_Precio;
                        if (habitacion.d_Precio != null)
                            oHospitalizacionHabitacionList.Total =
                                CalcularCostoHabitacion(habitacion.d_Precio.ToString(), habitacion.d_StartDate,
                                    habitacion.d_EndDate);
                        else
                            oHospitalizacionHabitacionList.Total = 0;
                        ListaHabitaciones.Add(oHospitalizacionHabitacionList);

                    }
                    hospit.Habitaciones = ListaHabitaciones;
                    #endregion

                    Lista.Add(hospit);
                }
                pobjOperationResult.Success = 1;
                return Lista;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public List<HospitalizacionList> GetHospitalizcion(ref OperationResult pobjOperationResult, string hospitalizacionId)
        {

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.hospitalizacion
                            join person B in dbContext.person on A.v_PersonId equals B.v_PersonId

                            where A.i_IsDeleted == 0 && A.v_HopitalizacionId == hospitalizacionId

                            select new HospitalizacionList
                            {
                                d_FechaAlta = A.d_FechaAlta,
                                d_FechaIngreso = A.d_FechaIngreso,
                                v_Paciente = B.v_FirstName + " " + B.v_FirstLastName + " " + B.v_SecondLastName,
                                v_HopitalizacionId = A.v_HopitalizacionId,
                                v_PersonId = A.v_PersonId,
                                i_IsDeleted = A.i_IsDeleted.Value,

                            };

                List<HospitalizacionList> objData = query.ToList();

                var hospitalizaciones = (from a in objData
                                         select new HospitalizacionList
                                         {
                                             v_Paciente = a.v_Paciente,
                                             d_FechaIngreso = a.d_FechaIngreso,
                                             d_FechaAlta = a.d_FechaAlta,
                                             v_HopitalizacionId = a.v_HopitalizacionId,
                                             v_PersonId = a.v_PersonId
                                         }).ToList();

                var objtData = hospitalizaciones.AsEnumerable()
                    .Where(a => a.v_HopitalizacionId != null)
                    .GroupBy(b => b.v_HopitalizacionId)
                    .Select(group => group.First());

                List<HospitalizacionList> obj = objtData.ToList();

                HospitalizacionList hospit;
                List<HospitalizacionList> Lista = new List<HospitalizacionList>();

                foreach (var item in obj)
                {
                    hospit = new HospitalizacionList();

                    hospit.v_HopitalizacionId = item.v_HopitalizacionId;
                    hospit.v_PersonId = item.v_PersonId;
                    hospit.v_Paciente = item.v_Paciente;
                    hospit.d_FechaIngreso = item.d_FechaIngreso;
                    hospit.d_FechaAlta = item.d_FechaAlta;

                    // estos son los hijos de 1 hopitalización
                    var servicios = BuscarServiciosHospitalizacion(item.v_HopitalizacionId).ToList();

                    List<HospitalizacionServiceList> HospitalizacionServicios = new List<HospitalizacionServiceList>();
                    if (servicios.Count > 0)
                    {
                        HospitalizacionServiceList oHospitalizacionServiceList;
                        foreach (var servicio in servicios)
                        {
                            oHospitalizacionServiceList = new HospitalizacionServiceList();
                            // acá vas poblando la entidad hijo hospitalización
                            oHospitalizacionServiceList.v_HopitalizacionId = servicio.v_HopitalizacionId;
                            oHospitalizacionServiceList.v_ServiceId = servicio.v_ServiceId;
                            oHospitalizacionServiceList.v_HospitalizacionServiceId = servicio.v_HospitalizacionServiceId;
                            oHospitalizacionServiceList.d_ServiceDate = servicio.d_ServiceDate;
                            oHospitalizacionServiceList.v_ProtocolName = servicio.v_ProtocolName;
                            oHospitalizacionServiceList.v_ProtocolId = servicio.v_ProtocolId;
                            // acá estoy agregando a las lista
                            HospitalizacionServicios.Add(servicio);
                        }
                        hospit.Servicios = HospitalizacionServicios;
                    }

                    #region Habitaciones
                    var habitacioenes = BuscarHospitalizacionHabitaciones(item.v_HopitalizacionId).ToList();
                    List<HospitalizacionHabitacionList> ListaHabitaciones = new List<HospitalizacionHabitacionList>();
                    HospitalizacionHabitacionList oHospitalizacionHabitacionList;
                    foreach (var habitacion in habitacioenes)
                    {
                        oHospitalizacionHabitacionList = new HospitalizacionHabitacionList();
                        oHospitalizacionHabitacionList.v_HospitalizacionHabitacionId = habitacion.v_HospitalizacionHabitacionId;
                        oHospitalizacionHabitacionList.v_HopitalizacionId = item.v_HopitalizacionId;
                        oHospitalizacionHabitacionList.i_HabitacionId = habitacion.i_HabitacionId;
                        oHospitalizacionHabitacionList.NroHabitacion = habitacion.NroHabitacion;
                        oHospitalizacionHabitacionList.d_StartDate = habitacion.d_StartDate;
                        oHospitalizacionHabitacionList.d_EndDate = habitacion.d_EndDate;
                        oHospitalizacionHabitacionList.d_Precio = habitacion.d_Precio;
                        if (habitacion.d_Precio != null)
                            oHospitalizacionHabitacionList.Total =
                                CalcularCostoHabitacion(habitacion.d_Precio.ToString(), habitacion.d_StartDate,
                                    habitacion.d_EndDate);
                        else
                            oHospitalizacionHabitacionList.Total = 0;
                        ListaHabitaciones.Add(oHospitalizacionHabitacionList);

                    }
                    hospit.Habitaciones = ListaHabitaciones;
                    #endregion

                    Lista.Add(hospit);
                }
                pobjOperationResult.Success = 1;
                return Lista;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        private decimal CalcularCostoHabitacion(string precio, DateTime? fechaIni, DateTime? fechafin)
        {
            var precioHabitacion = decimal.Parse(precio);
            var cantidadDias = 0;
            if (fechafin != null)
            {
                if (fechaIni != null)
                {
                    TimeSpan ts = fechafin.Value.Date.AddDays(1) - fechaIni.Value.Date;
                    cantidadDias = ts.Days;
                }
            }
            else
            {
                if (fechaIni != null)
                {
                    DateTime oldDate = fechaIni.Value.Date;
                    DateTime newDate = DateTime.Now.Date;

                    // Difference in days, hours, and minutes.
                    TimeSpan ts = newDate - oldDate;

                    // Difference in days.
                    cantidadDias = ts.Days;
                }
                //cantidadDias = int.Parse((DateTime.Today.AddTicks(-fechaIni.Value.Ticks).Day + 1).ToString());
            }

            return precioHabitacion * cantidadDias;
        }

        private List<HospitalizacionHabitacionList> BuscarHospitalizacionHabitaciones(string hospitalizacionId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var habitaciones = (from A in dbContext.hospitalizacionhabitacion
                                join D in dbContext.systemparameter on new { a = A.i_HabitacionId.Value, b = 309 } equals new { a = D.i_ParameterId, b = D.i_GroupId }
                                where A.v_HopitalizacionId == hospitalizacionId && A.i_IsDeleted == 0
                                select new HospitalizacionHabitacionList
                                {
                                    v_HospitalizacionHabitacionId = A.v_HospitalizacionHabitacionId,
                                    v_HopitalizacionId = A.v_HopitalizacionId,
                                    i_HabitacionId = A.i_HabitacionId.Value,
                                    NroHabitacion = D.v_Value1,
                                    d_StartDate = A.d_StartDate,
                                    d_EndDate = A.d_EndDate,
                                    d_Precio = A.d_Precio.Value
                                }).ToList();
            List<HospitalizacionHabitacionList> obj = habitaciones;

            return obj;

        }

        private List<HospitalizacionServiceList> BuscarServiciosHospitalizacion(string hospitalizacionId)
        {
            //acá hace un select a la tabla hospitalizacionService y buscas todos que tengan foranea HospitalizacionId
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var queryservice = from A in dbContext.hospitalizacion
                               join C in dbContext.hospitalizacionservice on A.v_HopitalizacionId equals C.v_HopitalizacionId
                               join D in dbContext.service on C.v_ServiceId equals D.v_ServiceId
                               join E in dbContext.protocol on D.v_ProtocolId equals E.v_ProtocolId
                               where A.v_HopitalizacionId == hospitalizacionId

                               select new HospitalizacionServiceList
                               {
                                   v_HospitalizacionServiceId = C.v_HospitalizacionServiceId,
                                   v_HopitalizacionId = C.v_HopitalizacionId,
                                   v_ServiceId = C.v_ServiceId,
                                   d_ServiceDate = D.d_ServiceDate.Value,
                                   v_ProtocolName = E.v_Name,
                                   v_ProtocolId = E.v_ProtocolId

                               };
            List<HospitalizacionServiceList> objData = queryservice.ToList();
            var hospitalizacionesservicios = (from a in objData
                                              select new HospitalizacionServiceList
                                              {
                                                  v_HospitalizacionServiceId = a.v_HospitalizacionServiceId,
                                                  v_HopitalizacionId = a.v_HopitalizacionId,
                                                  v_ServiceId = a.v_ServiceId,
                                                  d_ServiceDate = a.d_ServiceDate,
                                                  v_ProtocolName = a.v_ProtocolName,
                                                  v_ProtocolId = a.v_ProtocolId
                                              }).ToList();

            List<HospitalizacionServiceList> obj = hospitalizacionesservicios;


            HospitalizacionServiceList tickets;
            List<HospitalizacionServiceList> Lista = new List<HospitalizacionServiceList>();

            foreach (var item in obj)
            {
                tickets = new HospitalizacionServiceList();

                tickets.v_HospitalizacionServiceId = item.v_HospitalizacionServiceId;
                tickets.v_HopitalizacionId = item.v_HopitalizacionId;
                tickets.v_ServiceId = item.v_ServiceId;
                tickets.d_ServiceDate = item.d_ServiceDate;
                tickets.v_ProtocolName = item.v_ProtocolName;
                tickets.v_ProtocolId = item.v_ProtocolId;
                #region Tickets

                // estos son los hijos de 1 hopitalización
                var ticketss = BuscarTickets(item.v_ServiceId).ToList();

                List<TicketList> Tickets = new List<TicketList>();
                if (ticketss.Count > 0)
                {
                    TicketList ticketslist;
                    foreach (var tick in ticketss)
                    {
                        ticketslist = new TicketList();
                        // acá vas poblando la entidad hijo hospitalización
                        ticketslist.v_TicketId = tick.v_TicketId;
                        ticketslist.v_ServiceId = tick.v_ServiceId;
                        ticketslist.d_Fecha = tick.d_Fecha;
                        // acá estoy agregando a las lista
                        Tickets.Add(tick);
                    }
                    tickets.Tickets = Tickets;
                }

                #endregion

                #region Componentes

                OperationResult pobjOperationResult = new OperationResult();
                ServiceBL oServiceBL = new ServiceBL();
                var componentes = oServiceBL.GetServiceComponents_(ref pobjOperationResult, item.v_ServiceId).FindAll(p => p.r_Price != 0 || p.r_Price != 0.00);
                ComponentesHospitalizacion oComponentesHospitalizacion;

                List<ComponentesHospitalizacion> listaComponentes = new List<ComponentesHospitalizacion>();
                foreach (var componente in componentes)
                {
                    oComponentesHospitalizacion = new ComponentesHospitalizacion();
                    oComponentesHospitalizacion.ServiceComponentId = componente.v_ServiceComponentId;
                    oComponentesHospitalizacion.Categoria = componente.v_CategoryName;
                    oComponentesHospitalizacion.Componente = componente.v_ComponentName;
                    oComponentesHospitalizacion.Precio = float.Parse(componente.r_Price.ToString());
                    oComponentesHospitalizacion.MedicoTratante = componente.MedicoTratante;
                    oComponentesHospitalizacion.Ingreso = componente.d_InsertDate.Value;
                    listaComponentes.Add(oComponentesHospitalizacion);
                }
                tickets.Componentes = listaComponentes;

                #endregion

                Lista.Add(tickets);



            }

            return Lista;
        }

        public List<TicketList> BuscarTickets(string v_ServiceId)
        {
            //acá hace un select a la tabla hospitalizacionService y buscas todos que tengan foranea HospitalizacionId
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var queryticket = from A in dbContext.hospitalizacion
                              join C in dbContext.hospitalizacionservice on A.v_HopitalizacionId equals C.v_HopitalizacionId
                              join D in dbContext.service on C.v_ServiceId equals D.v_ServiceId
                              join E in dbContext.ticket on D.v_ServiceId equals E.v_ServiceId

                              where E.v_ServiceId == v_ServiceId

                              select new TicketList
                              {
                                  v_ServiceId = E.v_ServiceId,
                                  v_TicketId = E.v_TicketId,
                                  d_Fecha = E.d_Fecha,
                                  i_conCargoA = E.i_ConCargoA,
                                  i_tipoCuenta = E.i_TipoCuentaId

                              };
            List<TicketList> objData = queryticket.ToList();
            // en hospitalizaciones tienes los padres
            var ticket = (from a in objData
                          select new TicketList
                          {
                              v_TicketId = a.v_TicketId,
                              v_ServiceId = a.v_ServiceId,
                              d_Fecha = a.d_Fecha,
                              i_conCargoA = a.i_conCargoA,
                              i_tipoCuenta = a.i_tipoCuenta
                          }).ToList();

            var objtData = ticket.AsEnumerable()
                    .Where(a => a.v_TicketId != null)
                    .GroupBy(b => b.v_TicketId)
                    .Select(group => group.First());

            List<TicketList> obj = objtData.ToList();

            TicketList tickets;
            List<TicketList> Lista = new List<TicketList>();

            foreach (var item in obj)
            {
                tickets = new TicketList();

                tickets.v_TicketId = item.v_TicketId;
                tickets.v_ServiceId = item.v_ServiceId;
                tickets.d_Fecha = item.d_Fecha;
                tickets.i_conCargoA = item.i_conCargoA;
                tickets.i_tipoCuenta = item.i_tipoCuenta;

                // estos son los hijos de 1 hopitalización
                var ticketssdetalle = BuscarTicketsDetalle(item.v_TicketId).ToList();

                List<TicketDetalleList> Ticketsdetalle = new List<TicketDetalleList>();
                if (ticketssdetalle.Count > 0)
                {
                    TicketDetalleList ticketsdetallelist;
                    foreach (var tickdetalle in ticketssdetalle)
                    {
                        ticketsdetallelist = new TicketDetalleList();
                        // acá vas poblando la entidad hijo hospitalización
                        ticketsdetallelist.v_TicketId = tickdetalle.v_TicketId;
                        ticketsdetallelist.v_TicketDetalleId = tickdetalle.v_TicketDetalleId;
                        ticketsdetallelist.v_IdProductoDetalle = tickdetalle.v_IdProductoDetalle;
                        ticketsdetallelist.v_Descripcion = tickdetalle.v_Descripcion;
                        ticketsdetallelist.EsDespachado = tickdetalle.EsDespachado;
                        ticketsdetallelist.d_Cantidad = tickdetalle.d_Cantidad;
                        ticketsdetallelist.d_PrecioVenta = tickdetalle.d_PrecioVenta;
                        ticketsdetallelist.Total = tickdetalle.d_Cantidad * tickdetalle.d_PrecioVenta;
                        // acá estoy agregando a las lista
                        Ticketsdetalle.Add(ticketsdetallelist);
                    }
                    tickets.Productos = Ticketsdetalle;
                }
                Lista.Add(tickets);
            }

            return Lista;
        }


        public void AddHospitalizacion(ref OperationResult pobjOperationResult, hospitalizacionDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                hospitalizacion objEntity = hospitalizacionAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 350), "HP"); ;
                objEntity.v_HopitalizacionId = NewId;

                dbContext.AddTohospitalizacion(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
            }
        }

        public void UpdateHospitalizacion(ref OperationResult pobjOperationResult, hospitalizacionDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.hospitalizacion
                                       where a.v_HopitalizacionId == pobjDtoEntity.v_HopitalizacionId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_IsDeleted = 0;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                hospitalizacion objEntity = hospitalizacionAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.hospitalizacion.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
            }
        }

        public hospitalizacionDto GetHospitalizacion(ref OperationResult pobjOperationResult, string v_HopitalizacionId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                hospitalizacionDto objDtoEntity = null;

                var objEntity = (from a in dbContext.hospitalizacion
                                 where a.v_HopitalizacionId == v_HopitalizacionId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = hospitalizacionAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }


        private List<TicketDetalleList> BuscarTicketsDetalle(string v_TicketId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var queryticketdetalle = from A in dbContext.hospitalizacion
                                     join C in dbContext.hospitalizacionservice on A.v_HopitalizacionId equals C.v_HopitalizacionId
                                     join D in dbContext.service on C.v_ServiceId equals D.v_ServiceId
                                     join E in dbContext.ticket on D.v_ServiceId equals E.v_ServiceId
                                     join F in dbContext.ticketdetalle on E.v_TicketId equals F.v_TicketId
                                     //join G in dbContext.productsformigration on F.v_IdProductoDetalle equals G.v_ProductId
                                     where E.v_TicketId == v_TicketId && F.i_IsDeleted == 0

                                     select new TicketDetalleList
                                     {
                                         v_TicketDetalleId = F.v_TicketDetalleId,
                                         v_TicketId = F.v_TicketId,
                                         d_Cantidad = F.d_Cantidad.Value,
                                         v_Descripcion = F.v_Descripcion,
                                         v_IdProductoDetalle = F.v_IdProductoDetalle,
                                         i_EsDespachado = F.i_EsDespachado.Value,
                                         d_PrecioVenta = F.d_PrecioVenta.Value
                                     };
            List<TicketDetalleList> objData = queryticketdetalle.ToList();
            var ticketdetalle = (from a in objData
                                 select new TicketDetalleList
                                 {
                                     v_TicketId = a.v_TicketId,
                                     v_IdProductoDetalle = a.v_IdProductoDetalle,
                                     v_TicketDetalleId = a.v_TicketDetalleId,
                                     d_Cantidad = a.d_Cantidad,
                                     v_Descripcion = a.v_Descripcion,
                                     i_EsDespachado = a.i_EsDespachado,
                                     EsDespachado = a.i_EsDespachado == 0 ? "NO" : "SI",
                                     d_PrecioVenta = a.d_PrecioVenta
                                 }).ToList();

            return ticketdetalle;
        }

        public List<MedicoList> GetMedicosGrid(string pstrFilterExpression)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.medico
                            join B in dbContext.systemuser on A.i_SystemUserId equals B.i_SystemUserId
                            join C in dbContext.person on B.v_PersonId equals C.v_PersonId
                            join D in dbContext.systemparameter on new { a = A.i_GrupoId.Value, b = 119 } equals new { a = D.i_ParameterId, b = D.i_GroupId } into D_join
                            from D in D_join.DefaultIfEmpty()
                            where A.i_IsDeleted == 0
                            select new MedicoList
                            {
                                i_SystemUserId = B.i_SystemUserId,
                                v_MedicoId = A.v_MedicoId,
                                Medico = C.v_FirstName + " " + C.v_FirstLastName + " " + C.v_SecondLastName,
                                i_GrupoId = A.i_GrupoId.Value,
                                Grupo = D.v_Value1,
                                r_Clinica = A.r_Clinica.Value,
                                r_Medico = A.r_Medico.Value,
                                Usuario = B.v_UserName
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public medicoDto GetMedico(ref OperationResult pobjOperationResult, string v_MedicoId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                medicoDto objDtoEntity = null;

                var objEntity = (from a in dbContext.medico
                                 where a.v_MedicoId == v_MedicoId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = medicoAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public void AddMedico(ref OperationResult pobjOperationResult, medicoDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                medico objEntity = medicoAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 341), "MD"); ;
                objEntity.v_MedicoId = NewId;

                dbContext.AddTomedico(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "MEDICO", "v_MedicoId=" + NewId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "MEDICO", "v_MedicoId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void UpdateMedico(ref OperationResult pobjOperationResult, medicoDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.medico
                                       where a.v_MedicoId == pobjDtoEntity.v_MedicoId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_IsDeleted = 0;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                medico objEntity = medicoAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.medico.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "MEDICO", "v_MedicoId=" + objEntity.v_MedicoId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "MEDICO", "v_MedicoId=" + pobjDtoEntity.v_MedicoId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void DeleteMedico(ref OperationResult pobjOperationResult, string v_MedicoId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.medico
                                       where a.v_MedicoId == v_MedicoId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "MEDICO", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "MEDICO", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public List<LiquidacionMedicoList> LiquidacionMedicos(string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.servicecomponent
                            join A1 in dbContext.service on A.v_ServiceId equals A1.v_ServiceId
                            join B in dbContext.systemuser on A.i_MedicoTratanteId equals B.i_SystemUserId
                            join C in dbContext.person on B.v_PersonId equals C.v_PersonId
                            join D in dbContext.person on A1.v_PersonId equals D.v_PersonId
                            join E in dbContext.systemparameter on new { a = A1.i_MasterServiceId.Value, b = 119 } equals new { a = E.i_ParameterId, b = E.i_GroupId } into E_join
                            from E in E_join.DefaultIfEmpty()
                            join F in dbContext.component on A.v_ComponentId equals F.v_ComponentId

                            where A.i_IsDeleted == 0 && A1.i_MasterServiceId != 2 && (A.r_Price != 0.00 || A.r_Price != 0)

                            select new LiquidacionMedicoList
                            {
                                MedicoTratanteId = B.i_SystemUserId,
                                MedicoTratante = C.v_FirstName + " " + C.v_FirstLastName + " " + C.v_SecondLastName,
                                Paciente = D.v_FirstName + " " + D.v_FirstLastName + " " + D.v_SecondLastName,
                                d_ServiceDate = A1.d_ServiceDate,
                                v_ServiceId = A.v_ServiceId,
                                Tipo = E.v_Value1,
                                v_ServiceComponentId = A.v_ServiceComponentId,
                                r_CostoComponente = A.r_Price.Value,
                                Componente = F.v_Name
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_ServiceDate >= @0 && d_ServiceDate <= @1", pdatBeginDate.Value, pdatEndDate.Value);
                }

                var medicos = query.ToList().GroupBy(g => g.MedicoTratanteId).Select(s => s.First()).ToList();

                List<LiquidacionMedicoList> listaFinal = new List<LiquidacionMedicoList>();
                LiquidacionMedicoList oLiquidacionMedicoList;
                foreach (var medico in medicos)
                {
                    oLiquidacionMedicoList = new LiquidacionMedicoList();
                    oLiquidacionMedicoList.MedicoTratante = medico.MedicoTratante;

                    var servicioMedico =
                        query.ToList().FindAll(p => p.MedicoTratanteId == medico.MedicoTratanteId).ToList();
                    var listaServicios = new List<LiquidacionServicios>();
                    foreach (var servicio in servicioMedico)
                    {
                        var oLiquidacionServicios = new LiquidacionServicios();
                        oLiquidacionServicios.Paciente = servicio.Paciente;
                        oLiquidacionServicios.v_ServiceId = servicio.v_ServiceId;
                        oLiquidacionServicios.d_ServiceDate = servicio.d_ServiceDate;
                        oLiquidacionServicios.Tipo = servicio.Tipo;
                        oLiquidacionServicios.Componente = servicio.Componente;
                        //obtener datos de costo y comisiones
                        if (medico.MedicoTratanteId != null)
                        {
                            var pagos = ObtenerPagos(servicio.v_ServiceId, medico.MedicoTratanteId.Value, servicio.r_CostoComponente);

                            oLiquidacionServicios.r_costo = pagos.r_costo;
                            oLiquidacionServicios.r_Comision = pagos.r_Comision;
                            oLiquidacionServicios.r_Total = pagos.r_Total;
                        }
                        listaServicios.Add(oLiquidacionServicios);
                    }

                    oLiquidacionMedicoList.Servicios = listaServicios;

                    listaFinal.Add(oLiquidacionMedicoList);
                }

                return listaFinal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private PagosComisiones ObtenerPagos(string serviceId, int medicoTratanteId, float costoComponente)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            var comisionMedico = (from A in dbContext.medico
                                  where A.i_IsDeleted == 0 && A.i_SystemUserId == medicoTratanteId
                                  select A).FirstOrDefault();
            var oPagosComisiones = new PagosComisiones();
            if (comisionMedico != null)
            {
                //var costoServicio = new ServiceBL().GetServiceCost(serviceId);
                oPagosComisiones.r_costo = decimal.Parse(costoComponente.ToString()); //decimal.Parse(costoServicio);
                oPagosComisiones.r_Comision = oPagosComisiones.r_costo * comisionMedico.r_Medico.Value / 100;
                oPagosComisiones.r_Total = oPagosComisiones.r_costo * comisionMedico.r_Clinica.Value / 100; ;

            }
            else
            {
                oPagosComisiones.r_costo = 0;
                oPagosComisiones.r_Comision = 0;
                oPagosComisiones.r_Total = 0;
            }

            return oPagosComisiones;
        }

        public hospitalizacionserviceDto GetHospitServ(string hospitalizacionId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                hospitalizacionserviceDto objDtoEntity = null;

                var objEntity = (from a in dbContext.hospitalizacionservice
                                 where a.v_HopitalizacionId == hospitalizacionId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = hospitalizacionserviceAssembler.ToDTO(objEntity);

                return objDtoEntity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public hospitalizacionhabitacionDto GetHospitalizacionHabitacion(ref OperationResult pobjOperationResult, string v_HopitalizacionId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                hospitalizacionhabitacionDto objDtoEntity = null;

                var objEntity = (from a in dbContext.hospitalizacionhabitacion
                                 where a.v_HopitalizacionId == v_HopitalizacionId
                                 orderby a.d_InsertDate descending
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = hospitalizacionhabitacionAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }
    }
}