//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/01 - 16:28:33
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BE
{

    /// <summary>
    /// Assembler for <see cref="devolvervalorescomponenteResult"/> and <see cref="devolvervalorescomponenteResultDto"/>.
    /// </summary>
    public static partial class devolvervalorescomponenteResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="devolvervalorescomponenteResultDto"/> converted from <see cref="devolvervalorescomponenteResult"/>.</param>
        static partial void OnDTO(this devolvervalorescomponenteResult entity, devolvervalorescomponenteResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="devolvervalorescomponenteResult"/> converted from <see cref="devolvervalorescomponenteResultDto"/>.</param>
        static partial void OnEntity(this devolvervalorescomponenteResultDto dto, devolvervalorescomponenteResult entity);

        /// <summary>
        /// Converts this instance of <see cref="devolvervalorescomponenteResultDto"/> to an instance of <see cref="devolvervalorescomponenteResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="devolvervalorescomponenteResultDto"/> to convert.</param>
        public static devolvervalorescomponenteResult ToEntity(this devolvervalorescomponenteResultDto dto)
        {
            if (dto == null) return null;

            var entity = new devolvervalorescomponenteResult();

            entity.COMPONENTE = dto.COMPONENTE;
            entity.PRECIOCOMP = dto.PRECIOCOMP;
            entity.IDCAMPO = dto.IDCAMPO;
            entity.VALUE1 = dto.VALUE1;
            entity.v_ServiceId = dto.v_ServiceId;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="devolvervalorescomponenteResult"/> to an instance of <see cref="devolvervalorescomponenteResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="devolvervalorescomponenteResult"/> to convert.</param>
        public static devolvervalorescomponenteResultDto ToDTO(this devolvervalorescomponenteResult entity)
        {
            if (entity == null) return null;

            var dto = new devolvervalorescomponenteResultDto();

            dto.COMPONENTE = entity.COMPONENTE;
            dto.PRECIOCOMP = entity.PRECIOCOMP;
            dto.IDCAMPO = entity.IDCAMPO;
            dto.VALUE1 = entity.VALUE1;
            dto.v_ServiceId = entity.v_ServiceId;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="devolvervalorescomponenteResultDto"/> to an instance of <see cref="devolvervalorescomponenteResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<devolvervalorescomponenteResult> ToEntities(this IEnumerable<devolvervalorescomponenteResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="devolvervalorescomponenteResult"/> to an instance of <see cref="devolvervalorescomponenteResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<devolvervalorescomponenteResultDto> ToDTOs(this IEnumerable<devolvervalorescomponenteResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}