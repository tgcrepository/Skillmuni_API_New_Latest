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
    
    public partial class tbl_survey
    {
        public tbl_survey()
        {
            this.tbl_survey_bank_link = new HashSet<tbl_survey_bank_link>();
            this.tbl_survey_data = new HashSet<tbl_survey_data>();
        }
    
        public int ID_SURVEY { get; set; }
        public Nullable<int> ID_CONTENT_ANSWER { get; set; }
        public Nullable<int> ID_ANSWER_STEP { get; set; }
        public string SURVEY_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string SURVEY_IMAGE { get; set; }
        public System.DateTime START_DATE { get; set; }
        public System.DateTime END_DATE { get; set; }
        public string STATUS { get; set; }
        public System.DateTime UPDATED_DATE_TIME { get; set; }
    
        public virtual tbl_content_answer tbl_content_answer { get; set; }
        public virtual tbl_content_answer_steps tbl_content_answer_steps { get; set; }
        public virtual ICollection<tbl_survey_bank_link> tbl_survey_bank_link { get; set; }
        public virtual ICollection<tbl_survey_data> tbl_survey_data { get; set; }
    }
}
