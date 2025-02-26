﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using m2ostnextservice.Models;


namespace m2ostnextservice.Controllers
{
    public class getBriefListForStudyAbroadController : ApiController
    {

        private db_m2ostEntities db = new db_m2ostEntities();

        public HttpResponseMessage Get(int UID, int OID, string ENC)
        {
            string uids = new Utility().mysqlTrim(UID.ToString());
            string oids = new Utility().mysqlTrim(OID.ToString());
            string ENCS = new Utility().mysqlTrim(ENC);
            int sldnos = 0;

            tbl_brief_tile_level_brief_restriction rest = new tbl_brief_tile_level_brief_restriction();
            //tbl_academy_level_brief_restriction acres = new tbl_academy_level_brief_restriction();
            List<tbl_restriction_user_log> lg = new List<tbl_restriction_user_log>();
            List<BriefAPIResource> list = new List<BriefAPIResource>();
            List<BriefAPIResource> reslist = new List<BriefAPIResource>();
            BriefResponse res = new BriefResponse();

            tbl_brief_category_tile tile = db.tbl_brief_category_tile.Where(t => t.tile_code.ToLower() == ENCS.ToLower()).FirstOrDefault();
            using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
            {
               // acres = db.Database.SqlQuery<tbl_academy_level_brief_restriction>("select * from tbl_academy_level_brief_restriction where id_academy = {0}", id_academy).FirstOrDefault();
                //rest = db.Database.SqlQuery<tbl_brief_tile_level_brief_restriction>("select * from tbl_brief_tile_level_brief_restriction where id_brief_tile = {0}", tile.id_brief_category_tile).FirstOrDefault();

            }

            if (tile != null)
            {
                // string addition = "SELECT id_brief_category FROM tbl_brief_tile_category_mapping where id_organization=" + OID + " and id_brief_category_tile=" + tile.id_brief_category_tile + " ";
                // string sqls = "select * from tbl_brief_user_assignment where id_user='" + uids + "' and assignment_status='S'  and id_brief_master in (SELECT id_brief_master FROM tbl_brief_master where status='A' and id_organization=" + OID + " and id_brief_category in (" + addition + "))";
                //string sqls = "select * from tbl_brief_user_assignment where id_user='" + uids + "' and assignment_status='S'  and id_brief_master in (SELECT id_brief_master FROM tbl_brief_master where status='A' and id_organization=" + OID + ")";
                //string sqls = "select * from tbl_brief_master where status='A'and id_organization={0}";

                //using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                //{
                //    if (rest != null)
                //    {
                //        //if (rest.time == 1)
                //        //{
                //        //    DateTime tdy = DateTime.Now.Date;

                //        //    lg = db.Database.SqlQuery<tbl_restriction_user_log>("select * from tbl_restriction_user_log where UID = {0} and id_brief_tile={1} and date(updated_date_time)={2} and id_academy={3}", UID, tile.id_brief_category_tile, tdy, id_academy).ToList();
                //        //}
                //        //else if (rest.time == 2)
                //        //{
                //        //    int tdy = DateTime.Now.Hour;
                //        //    DateTime dt = DateTime.Now.Date;
                //        //    lg = db.Database.SqlQuery<tbl_restriction_user_log>("select * from tbl_restriction_user_log where UID = {0} and id_brief_tile={1} and EXTRACT(HOUR  FROM updated_date_time)={2} and  date(updated_date_time)={3} and id_academy={4}", UID, tile.id_brief_category_tile, tdy, dt, id_academy).ToList();


                //        //}
                //    }
                //}
                //List<tbl_brief_master> check1 = db.tbl_brief_master.SqlQuery(sqls,OID).ToList();
                //foreach (tbl_brief_master item in check1)
                //{
                //    tbl_brief_read_status read = db.tbl_brief_read_status.Where(t => t.id_brief_master == item.id_brief_master && t.id_user == UID).FirstOrDefault();
                //    if (read == null)
                //    {
                //        tbl_brief_read_status rst = new tbl_brief_read_status();
                //        rst.id_brief_master = item.id_brief_master;
                //        rst.id_user = UID;
                //        rst.id_organization = OID;
                //        rst.read_status = 0;
                //        rst.action_status = 0;
                //        rst.id_organization = OID;
                //        rst.status = "A";
                //        rst.updated_date_time = DateTime.Now;
                //        db.tbl_brief_read_status.Add(rst);
                //        db.SaveChanges();
                //    }
                //}
                List<tbl_brief_master> mas = new List<tbl_brief_master>();
                using (m2ostnextserviceDbContext contxt = new m2ostnextserviceDbContext())
                {
                    int idcattile = contxt.Database.SqlQuery<int>("SELECT id_brief_category_tile FROM tbl_brief_category_tile where tile_code={0}", ENC).FirstOrDefault();
                    List<tbl_brief_master> idcats = contxt.Database.SqlQuery<tbl_brief_master>("SELECT * FROM tbl_brief_category_tile INNER JOIN tbl_brief_tile_category_mapping ON tbl_brief_category_tile.id_brief_category_tile = tbl_brief_tile_category_mapping.id_brief_category_tile where tbl_brief_category_tile.id_organization={0} and tbl_brief_tile_category_mapping.id_brief_category_tile={1} ;", OID, idcattile).ToList();
                    foreach (var idcat in idcats)
                    {
                        if (idcat.id_brief_category != 0)
                        {
                            //List<tbl_brief_master>  instob = contxt.Database.SqlQuery<tbl_brief_master>("select * from tbl_brief_master where status='A' and id_brief_category={0}", idcat.id_brief_category).ToList();
                            List<tbl_brief_master> instob = new BriefModel().getBriefList("select * from tbl_brief_master where status='A' and id_brief_category=" + idcat.id_brief_category, OID);
                            mas.AddRange(instob);

                        }


                    }

                }

                string sqlb = "SELECT a.id_organization,question_count, brief_title, brief_code, brief_description, CASE WHEN scheduled_status = 'NA' THEN published_datetime WHEN published_status = 'NA' THEN scheduled_datetime ELSE NULL END datetimestamp, CASE WHEN scheduled_status = 'NA' THEN 'P' WHEN published_status = 'NA' THEN 'S' ELSE NULL END scheduled_type, a.override_dnd, a.id_brief_master, b.id_user, a.is_add_question is_question_attached, c.action_status, c.read_status, d.brief_category, e.brief_subcategory, d.id_brief_category, e.id_brief_subcategory ";
                sqlb += " FROM tbl_brief_master a, tbl_brief_user_assignment b, tbl_brief_read_status c, tbl_brief_category d, tbl_brief_subcategory e WHERE a.status='A' and a.id_brief_master = b.id_brief_master AND a.id_brief_master = c.id_brief_master AND b.id_user = c.id_user AND a.id_brief_category = d.id_brief_category AND a.id_brief_sub_category = e.id_brief_subcategory AND a.id_brief_sub_category = e.id_brief_subcategory AND b.id_user = '" + uids + "' AND a.id_organization = '" + oids + "' AND (published_datetime < NOW() OR scheduled_datetime < NOW())  AND a.id_brief_category IN (SELECT id_brief_category  FROM tbl_brief_tile_category_mapping WHERE id_organization = " + OID + " AND id_brief_category_tile = " + tile.id_brief_category_tile + ") ORDER BY datetimestamp DESC ";//LIMIT 50

                //list = new BriefModel().getBriefAPIResourceList(sqlb);
                int srno = 1;
                int chk = lg.Count + 1;
                foreach (var itm in mas)
                {
                    BriefAPIResource inst = new BriefAPIResource();
                    inst.SRNO = srno;
                    inst.brief_title = itm.brief_title;
                    inst.brief_description = itm.brief_description;
                    inst.id_brief_category = itm.id_brief_category;
                    inst.id_brief_master = itm.id_brief_master;
                    inst.id_organization = OID;
                    inst.id_user = UID;
                    inst.brief_code = itm.brief_code;
                    inst.BrfDate = itm.updated_date_time;
                    inst.brief_attachment_flag = itm.brief_attachment_flag;
                    if (inst.brief_attachment_flag == 4)
                    {
                        inst.brief_attachement_url = itm.brief_attachement_url;

                    }
                    using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                    {
                        inst.brief_category = db.Database.SqlQuery<string>("select brief_category from tbl_brief_category where id_brief_category={0}", inst.id_brief_category).FirstOrDefault();
                        tbl_brief_user_feedback_master feed = db.Database.SqlQuery<tbl_brief_user_feedback_master>("select * from tbl_brief_user_feedback_master where UID={0} and  id_brief_master= {1} and updated_date_time= (SELECT MAX(updated_date_time) FROM tbl_brief_user_feedback_master WHERE UID={2} and  id_brief_master ={3} );", UID, itm.id_brief_master, UID, itm.id_brief_master).FirstOrDefault();
                        if (feed != null)
                        {
                            inst.liked = feed.liked;
                            inst.disliked = feed.disliked;


                        }
                        else
                        {
                            inst.liked = 0;
                            inst.disliked = 0;
                        }
                    }


                    //itm.SRNO = srno;
                    srno++;
                    tbl_brief_log log = db.tbl_brief_log.Where(t => t.attempt_no == 1 && t.id_brief_master == itm.id_brief_master && t.id_user == UID).FirstOrDefault();
                    if (log != null)
                    {

                        inst.RESULTSTATUS = 1;
                        inst.RESULTSCORE = Convert.ToDouble(log.brief_result);
                    }
                    else
                    {
                        inst.RESULTSTATUS = 0;
                        inst.RESULTSCORE = 0;
                    }
                    /*--------------------------------------------------updated------------------------------------------------*/
                    // tbl_brief_master master = db.tbl_brief_master.Where(t => t.id_brief_master == itm.id_brief_master).FirstOrDefault();
                    // tbl_brief_master master = itm;
                    tbl_brief_master_template mTemplate = db.tbl_brief_master_template.Where(t => t.id_brief_master == itm.id_brief_master).FirstOrDefault();
                    if (mTemplate != null)
                    {
                        inst.brief_template = mTemplate.brief_template;
                    }
                    else
                    {
                        inst.brief_template = "0";
                    }
                    List<tbl_brief_master_body> mbody = db.tbl_brief_master_body.Where(t => t.id_brief_master == itm.id_brief_master).OrderBy(t => t.srno).ToList();
                    List<BriefRow> bList = new List<BriefRow>();
                    foreach (tbl_brief_master_body row in mbody)
                    {
                        BriefRow irow = new BriefRow();
                        irow.media_type = Convert.ToInt32(row.media_type);
                        irow.resouce_code = row.resouce_code;
                        irow.resource_order = mTemplate.resource_order;
                        irow.brief_destination = row.brief_destination;
                        irow.resource_number = row.resource_number;
                        irow.srno = Convert.ToInt32(row.srno);
                        irow.resource_type = Convert.ToInt32(row.resource_type);
                        irow.resouce_data = row.resouce_data;
                        irow.resouce_code = row.resouce_code;
                        irow.media_type = Convert.ToInt32(row.media_type);
                        irow.resource_mime = row.resource_mime;
                        irow.file_extension = row.file_extension;
                        irow.file_type = row.file_type;
                        if (irow.resouce_data.Contains("$"))
                        {
                            int i = 0;
                        }


                        bList.Add(irow);
                    }

                    inst.briefResource = bList;
                    using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                    {

                        tbl_brief_m2ost_category_mapping inst_obj = new tbl_brief_m2ost_category_mapping();
                        inst.cat_mapping = db.Database.SqlQuery<tbl_brief_m2ost_category_mapping>("select * from tbl_brief_m2ost_category_mapping where id_brief={0}", itm.id_brief_master).FirstOrDefault();
                        if (inst.cat_mapping == null)
                        {
                            inst_obj.id_brief = itm.id_brief_master;
                            inst_obj.id_category = 0;
                            inst_obj.id_mapping = 0;
                            inst_obj.id_org = 0;
                            inst_obj.status = "A";
                            inst_obj.type = 2;//url
                            inst_obj.URL = ConfigurationManager.AppSettings["RightSwipe_URL"].ToString();
                            inst.cat_mapping = inst_obj;
                        }
                        //else
                        //{
                        //    itm.cat_mapping = inst_obj;
                        //}
                    }

                    reslist.Add(inst);
                    /*--------------------------------------------------updated------------------------------------------------*/

                    /*  tbl_brief_user_assignment uassign = db.tbl_brief_user_assignment.Where(t => t.id_brief_master == itm.id_brief_master && t.id_user == UID).FirstOrDefault();
                      if (uassign != null)
                      {
                          if (uassign.assignment_status == "S")
                          {
                              uassign.assignment_status = "R";
                              db.SaveChanges();
                          }
                      }*/




                    //if (log != null)
                    //{

                    //    reslist.Add(inst);
                    //    sldnos++;

                    //}
                    //else
                    //{
                    //    if (rest != null)
                    //    {
                    //        if (chk <= rest.brief_count)
                    //        {
                    //            reslist.Add(inst);
                    //            chk++;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        reslist.Add(inst);
                    //        //sldnos++;
                    //    }
                    //}




                }
            }
            else
            {
            }





            if (reslist != null)
            {

                res.BriefList = reslist;

                res.BriefList = res.BriefList.OrderBy(x => x.RESULTSTATUS).ThenBy(x => x.BrfDate).ToList();

                res.ValidationNumber = sldnos;

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            else
            {

                return Request.CreateResponse(HttpStatusCode.NoContent, res);
            }
        }

    }
}
