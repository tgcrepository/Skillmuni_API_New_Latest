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
    
    public partial class tbl_saksham_assessment_master
    {
        public int id_saksham_assessment_master { get; set; }
        public string roll_no { get; set; }
        public string id_teacher { get; set; }
        public string id_organization { get; set; }
        public string year { get; set; }
        public Nullable<System.DateTime> doj { get; set; }
        public Nullable<System.DateTime> assessment_date { get; set; }
        public Nullable<System.DateTime> growth_date_capture { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<System.DateTime> last_modified { get; set; }
    }
}
