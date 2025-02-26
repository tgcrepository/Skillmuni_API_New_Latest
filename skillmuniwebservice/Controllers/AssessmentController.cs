using m2ostnextservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    [RoutePrefix("api/Assessment")]
    public class AssessmentController : ApiController
    {
        [HttpGet]
        [Route("GetAssessmentSheet")]
        public IHttpActionResult GetAssessmentSheet(string brfcode, int UID, int OID, int ACID, int BriefTileID = 0)
        {
            try
            {
                int _orgid = OID, _userid = UID;
                UserScoreResponse scoreres = new UserScoreResponse();
                BriefModel briefModel = new BriefModel();
                BriefResource briefData = briefModel.getBriefData(brfcode, _userid, _orgid);
                List<BriefChart> right = new List<BriefChart>();
                List<BriefChart> wrong = new List<BriefChart>();

                if (briefData.RESULTSTATUS == 1)
                {
                    string str = APIString.API + "getUserScore?UID=" + UID + "&OID=" + OID;
                    string jsonres = new UniversityScoringlogic().getApiResponseString(str);
                    scoreres = JsonConvert.DeserializeObject<UserScoreResponse>(jsonres);

                    using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                    {
                        var splscorelog = db.Database.SqlQuery<tbl_user_game_special_metric_score_log>(
                            "SELECT * FROM tbl_user_game_special_metric_score_log WHERE id_brief={0} AND id_user={1}",
                            briefData.BRIEF.id_brief_master, UID
                        ).FirstOrDefault();

                        if (splscorelog != null)
                        {
                            briefData.SplScore = splscorelog.score;
                        }

                        foreach (var itm in briefData.RESULT.briefReturn)
                        {
                            itm.questiontheme = db.Database.SqlQuery<int>(
                                "SELECT question_theme_type FROM tbl_brief_question WHERE id_brief_question={0}",
                                itm.id_question
                            ).FirstOrDefault();

                            if (itm.questiontheme == 2)
                            {
                                itm.questionchoicetype = db.Database.SqlQuery<int>(
                                    "SELECT question_choice_type FROM tbl_brief_question WHERE id_brief_question={0}",
                                    itm.id_question
                                ).FirstOrDefault();

                                itm.answerchoicetype = db.Database.SqlQuery<int>(
                                    "SELECT choice_type FROM tbl_brief_answer WHERE id_brief_answer={0}",
                                    itm.id_answer
                                ).FirstOrDefault();

                                if (itm.id_wans > 0)
                                {
                                    itm.wanschoicetype = db.Database.SqlQuery<int>(
                                        "SELECT choice_type FROM tbl_brief_answer WHERE id_brief_answer={0}",
                                        itm.id_wans
                                    ).FirstOrDefault();
                                }

                                if (itm.questionchoicetype == 2 || itm.questionchoicetype == 3)
                                {
                                    itm.questionimg = db.Database.SqlQuery<string>(
                                        "SELECT question_image FROM tbl_brief_question WHERE id_brief_question={0}",
                                        itm.id_question
                                    ).FirstOrDefault();
                                }

                                if (itm.answerchoicetype == 2 || itm.answerchoicetype == 3)
                                {
                                    itm.answerimg = db.Database.SqlQuery<string>(
                                        "SELECT choice_image FROM tbl_brief_answer WHERE id_brief_answer={0}",
                                        itm.id_answer
                                    ).FirstOrDefault();
                                }

                                if (itm.id_wans > 0 && (itm.wanschoicetype == 2 || itm.wanschoicetype == 3))
                                {
                                    itm.wansimg = db.Database.SqlQuery<string>(
                                        "SELECT choice_image FROM tbl_brief_answer WHERE id_brief_answer={0}",
                                        itm.id_wans
                                    ).FirstOrDefault();
                                }
                            }
                        }
                    }
                }
                return Ok(new
                {
                    right,
                    wrong,
                    brief = briefData,
                    UID,
                    OID,
                    ACID,
                    BriefTileID,
                    scoreres
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}