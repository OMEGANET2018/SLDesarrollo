//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/03/25 - 12:08:57
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;
using Sigesoft.Server.WebClientAdmin.DAL;

namespace Sigesoft.Server.WebClientAdmin.BE
{

    /// <summary>
    /// Assembler for <see cref="consolidadoexamenesResult"/> and <see cref="consolidadoexamenesResultDto"/>.
    /// </summary>
    public static partial class consolidadoexamenesResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="consolidadoexamenesResultDto"/> converted from <see cref="consolidadoexamenesResult"/>.</param>
        static partial void OnDTO(this consolidadoexamenesResult entity, consolidadoexamenesResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="consolidadoexamenesResult"/> converted from <see cref="consolidadoexamenesResultDto"/>.</param>
        static partial void OnEntity(this consolidadoexamenesResultDto dto, consolidadoexamenesResult entity);

        /// <summary>
        /// Converts this instance of <see cref="consolidadoexamenesResultDto"/> to an instance of <see cref="consolidadoexamenesResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="consolidadoexamenesResultDto"/> to convert.</param>
        public static consolidadoexamenesResult ToEntity(this consolidadoexamenesResultDto dto)
        {
            if (dto == null) return null;

            var entity = new consolidadoexamenesResult();

            entity.T_Paciente = dto.T_Paciente;
            entity.T_FechaServicio = dto.T_FechaServicio;
            entity.T_EmpresaCliente = dto.T_EmpresaCliente;
            entity.T_Id_Servicio = dto.T_Id_Servicio;
            entity.T_Nombre_Completo = dto.T_Nombre_Completo;
            entity.T_Id_Componente = dto.T_Id_Componente;
            entity.T_Total = dto.T_Total;
            entity.T_GESO = dto.T_GESO;
            entity.T_IdProtocolo = dto.T_IdProtocolo;
            entity.T_Categoria = dto.T_Categoria;
            entity.T_ServiceComponenteId = dto.T_ServiceComponenteId;
            entity.T_REALIZADO = dto.T_REALIZADO;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="consolidadoexamenesResult"/> to an instance of <see cref="consolidadoexamenesResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="consolidadoexamenesResult"/> to convert.</param>
        public static consolidadoexamenesResultDto ToDTO(this consolidadoexamenesResult entity)
        {
            if (entity == null) return null;

            var dto = new consolidadoexamenesResultDto();

            dto.T_Paciente = entity.T_Paciente;
            dto.T_FechaServicio = entity.T_FechaServicio;
            dto.T_EmpresaCliente = entity.T_EmpresaCliente;
            dto.T_Id_Servicio = entity.T_Id_Servicio;
            dto.T_Nombre_Completo = entity.T_Nombre_Completo;
            dto.T_Id_Componente = entity.T_Id_Componente;
            dto.T_Total = entity.T_Total;
            dto.T_GESO = entity.T_GESO;
            dto.T_IdProtocolo = entity.T_IdProtocolo;
            dto.T_Categoria = entity.T_Categoria;
            dto.T_ServiceComponenteId = entity.T_ServiceComponenteId;
            dto.T_REALIZADO = entity.T_REALIZADO;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="consolidadoexamenesResultDto"/> to an instance of <see cref="consolidadoexamenesResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<consolidadoexamenesResult> ToEntities(this IEnumerable<consolidadoexamenesResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="consolidadoexamenesResult"/> to an instance of <see cref="consolidadoexamenesResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<consolidadoexamenesResultDto> ToDTOs(this IEnumerable<consolidadoexamenesResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
