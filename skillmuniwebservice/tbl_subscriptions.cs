//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace m2ostnextservice
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_subscriptions
    {
        public int ID_SUBSCRIPTION { get; set; }
        public int ID_USER { get; set; }
        public int ID_CONTENT { get; set; }
        public string STATUS { get; set; }
        public System.DateTime UPDATEDTIME { get; set; }
        public Nullable<System.DateTime> EXPIRY_DATE { get; set; }
    
        public virtual tbl_content tbl_content { get; set; }
        public virtual tbl_user tbl_user { get; set; }
    }
}
