//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/10/20 - 18:43:46
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
    public partial class professionalDto
    {
        [DataMember()]
        public String v_PersonId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ProfessionId { get; set; }

        [DataMember()]
        public String v_ProfessionalCode { get; set; }

        [DataMember()]
        public String v_ProfessionalInformation { get; set; }

        [DataMember()]
        public Byte[] b_SignatureImage { get; set; }

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
        public Nullable<Int32> i_UpdateNodeId { get; set; }

        [DataMember()]
        public personDto person { get; set; }

        public professionalDto()
        {
        }

        public professionalDto(String v_PersonId, Nullable<Int32> i_ProfessionId, String v_ProfessionalCode, String v_ProfessionalInformation, Byte[] b_SignatureImage, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, Nullable<Int32> i_UpdateNodeId, personDto person)
        {
			this.v_PersonId = v_PersonId;
			this.i_ProfessionId = i_ProfessionId;
			this.v_ProfessionalCode = v_ProfessionalCode;
			this.v_ProfessionalInformation = v_ProfessionalInformation;
			this.b_SignatureImage = b_SignatureImage;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.i_UpdateNodeId = i_UpdateNodeId;
			this.person = person;
        }
    }
}
