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
    
    public partial class tbl_feedback_bank_link
    {
        public int ID_FEEDBACK_BANK_LINK { get; set; }
        public int ID_FEEDBACK_BANK { get; set; }
        public int ID_CONTENT_ANSWER { get; set; }
        public string ID_ANSWER_ASSOCIATION { get; set; }
        public string STATUS { get; set; }
        public System.DateTime UPDATED_DATE_TIME { get; set; }
    
        public virtual tbl_feedback_bank tbl_feedback_bank { get; set; }
    }
}
