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
    
    public partial class tbl_content_type_link
    {
        public int ID_CONTENT_TYPE_LINK { get; set; }
        public int ID_CONTENT_ANSWER { get; set; }
        public int ID_CONTENT_TYPE { get; set; }
        public string LINK_VALUE { get; set; }
        public string DESCRIPTION { get; set; }
        public string STATUS { get; set; }
        public System.DateTime UPDATED_DATE_TIME { get; set; }
    
        public virtual tbl_content_answer tbl_content_answer { get; set; }
        public virtual tbl_content_type tbl_content_type { get; set; }
    }
}
