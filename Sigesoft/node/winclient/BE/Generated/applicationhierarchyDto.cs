//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/03/25 - 11:52:14
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
    public partial class applicationhierarchyDto
    {
        [DataMember()]
        public Int32 i_ApplicationHierarchyId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ApplicationHierarchyTypeId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Level { get; set; }

        [DataMember()]
        public String v_Description { get; set; }

        [DataMember()]
        public String v_Form { get; set; }

        [DataMember()]
        public String v_Code { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ParentId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ScopeId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_TypeFormId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ExternalUserFunctionalityTypeId { get; set; }

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
        public List<protocolsystemuserDto> protocolsystemuser { get; set; }

        [DataMember()]
        public List<rolenodeprofileDto> rolenodeprofile { get; set; }

        [DataMember()]
        public List<systemusergobalprofileDto> systemusergobalprofile { get; set; }

        public applicationhierarchyDto()
        {
        }

        public applicationhierarchyDto(Int32 i_ApplicationHierarchyId, Nullable<Int32> i_ApplicationHierarchyTypeId, Nullable<Int32> i_Level, String v_Description, String v_Form, String v_Code, Nullable<Int32> i_ParentId, Nullable<Int32> i_ScopeId, Nullable<Int32> i_TypeFormId, Nullable<Int32> i_ExternalUserFunctionalityTypeId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, List<protocolsystemuserDto> protocolsystemuser, List<rolenodeprofileDto> rolenodeprofile, List<systemusergobalprofileDto> systemusergobalprofile)
        {
			this.i_ApplicationHierarchyId = i_ApplicationHierarchyId;
			this.i_ApplicationHierarchyTypeId = i_ApplicationHierarchyTypeId;
			this.i_Level = i_Level;
			this.v_Description = v_Description;
			this.v_Form = v_Form;
			this.v_Code = v_Code;
			this.i_ParentId = i_ParentId;
			this.i_ScopeId = i_ScopeId;
			this.i_TypeFormId = i_TypeFormId;
			this.i_ExternalUserFunctionalityTypeId = i_ExternalUserFunctionalityTypeId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.protocolsystemuser = protocolsystemuser;
			this.rolenodeprofile = rolenodeprofile;
			this.systemusergobalprofile = systemusergobalprofile;
        }
    }
}
