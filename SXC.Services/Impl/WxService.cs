using SXC.Services.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SXC.Code;
using SXC.Core.Models;
using Newtonsoft.Json;
using System.Data.Entity;
using SXC.Services.Business;
using SXC.Code.Utility;
using System.Xml.Linq;
using SXC.Code.Cache;
using System.Web;

namespace SXC.Services.Impl
{
    public class WxService : ServiceBase
    {
        public int? IntNull = null;
        private string GetPicUrl(string pic)
        {
            //var host = System.Web.HttpContext.Current.Request.Url.Authority;
            //Server.MapPath();
            //return @"http://192.168.31.199/SxcWebApi/api/Image/" + Cryptography.Base64ForUrlEncode(pic);

            if (string.IsNullOrEmpty(pic))
            {
                return string.Empty;
            }

            return Function.GetPicUrl(pic);
        }

        public List<NavDto> GetNavigations(int type)
        {
            using (var db = base.NewDB())
            {
                var dblist = db.Navigations.Where(t => t.IsValid == true && t.Type == type).OrderBy(t => t.Order).ToList();

                var res = new List<NavDto>();
                foreach (var item in dblist)
                {
                    res.Add(new NavDto {
                        id = item.ID,
                        desc = item.Desc,
                        picurl = GetPicUrl(item.Pic),
                        target = item.Target,
                        articleid = item.ArticleID
                    });
                }

                return res;
            }
        }


        public List<TeacherDto> GetTeachers(int type=0)
        {
            using (var db = base.NewDB())
            {
                var dblist = db.Teachers.Where(t => t.IsValid == true && t.Type == type).OrderBy(t => t.Order).ToList();

                var res = new List<TeacherDto>();
                foreach (var item in dblist)
                {
                    res.Add(new TeacherDto
                    {
                        id = item.ID,
                        name = item.Name,
                        title = item.Title,
                        picurl = GetPicUrl(item.Pic),
                        introduction = item.Introduction,
                        character = item.Character, 
                        //articleid = item.Article == null ? IntNull : item.Article.ID
                        articleid = item.ArticleID
                    });
                }

                return res;
            }
        }

        public List<CourseDto> GetCourses(int? top=null)
        {
            using (var db = base.NewDB())
            {
                List<Course> dblist;

                if (top == null)
                {
                    dblist = db.Courses.Where(t => t.IsValid == true).OrderBy(t => t.Order).ToList();
                }
                else
                {
                    dblist = db.Courses.Where(t => t.IsValid == true).OrderBy(t => t.Order).Take(top.Value).ToList();
                }

                var res = new List<CourseDto>();
                foreach (var item in dblist)
                {
                    res.Add(new CourseDto
                    {
                        id = item.ID,
                        name = item.Name,
                        desc = item.Desc,
                        picurl = GetPicUrl(item.Pic),
                        hasvideo = item.HasVideo,
                        hasfreevideo = item.HasFreeVideo,
                        //articleid = item.Article == null ? IntNull : item.Article.ID
                        articleid = item.ArticleID
                    });
                }
                return res;
            }
        }

        public CourseDto GetCourse(int id)
        {
            using (var db = base.NewDB())
            {
                var dbitem = db.Courses.Find(id);

                if (dbitem == null)
                {
                    return null;
                }

                var res = new CourseDto
                    {
                        id = dbitem.ID,
                        name = dbitem.Name,
                        desc = dbitem.Desc,
                        period = dbitem.Period,
                        price = dbitem.Price,
                        hasvideo = dbitem.HasVideo,
                        hasfreevideo = dbitem.HasFreeVideo,
                        picurl = GetPicUrl(dbitem.Pic),
                        //articleid = item.Article == null ? IntNull : item.Article.ID
                        articleid = dbitem.ArticleID
                    };

                return res;
            }
        }


        public List<PromotionDto> GetPromotions(int type=0)
        {
            using (var db = base.NewDB())
            {
                var dblist = db.Promotions.Where(t => t.IsValid == true && t.Type == type).OrderByDescending(t => t.CreateTime).ToList();

                var res = new List<PromotionDto>();
                foreach (var item in dblist)
                {
                    res.Add(new PromotionDto
                    {
                        id = item.ID,
                        name = item.Name,
                        desc = item.Desc,
                        picurl = GetPicUrl(item.Pic),
                        date = item.CreateTime,
                        //articleid = item.Article == null ? IntNull : item.Article.ID
                        articleid = item.ArticleID
                    });
                }

                return res;
            }
        }

        public List<PromotionDto> GetPromotions(int type = 0, int? top = null)
        {
            using (var db = base.NewDB())
            {
                List<Promotion> dblist;
                //var dblist = db.Promotions.Where(t => t.IsValid == true && t.Type == type).OrderByDescending(t => t.CreateTime).ToList();
                if (top == null)
                {
                    dblist = db.Promotions.Where(t => t.IsValid == true && t.Type == type).OrderByDescending(t => t.CreateTime).ToList();
                }
                else
                {
                    dblist = db.Promotions.Where(t => t.IsValid == true && t.Type == type).OrderByDescending(t => t.CreateTime).Take(top.Value).ToList();
                }

                var res = new List<PromotionDto>();
                foreach (var item in dblist)
                {
                    res.Add(new PromotionDto
                    {
                        id = item.ID,
                        name = item.Name,
                        desc = item.Desc,
                        picurl = GetPicUrl(item.Pic),
                        date = item.CreateTime,
                        //articleid = item.Article == null ? IntNull : item.Article.ID
                        articleid = item.ArticleID
                    });
                }
                return res;
            }
        }

        public ArticleDto GetArticle(int? id)
        {
            using (var db = base.NewDB())
            {
                var dbitem = id == null ? db.Articles.FirstOrDefault(t => t.Type == -1 && t.Title == "404") : db.Articles.Find(id);

                //var content = dbitem == null ? string.Empty : dbitem.Content;
                //content = content.Replace("{host}", Function.GetHostAndApp());

                if(dbitem == null)
                {
                    return null;
                }

                ArticleDto articledto = new ArticleDto
                {
                    author = dbitem.Author,
                    title = dbitem.Title,
                    createtime = dbitem.CreateTime,
                    content = dbitem.Content.Replace("{host}", Function.GetHostAndApp())
                };

                return articledto;

                //return new ArticleDto { content = content};
            }
        }

        public ArticleDto GetArticleByTitle(string title)
        {
            using (var db = base.NewDB())
            {
                var dbitem = db.Articles.FirstOrDefault(t => t.Title == title);

                if (dbitem == null)
                {
                    dbitem = db.Articles.FirstOrDefault(t => t.Type == -1 && t.Title == "404");
                }

                //var content = dbitem == null ? string.Empty : dbitem.Content;
                //content = content.Replace("{host}", Function.GetHostAndApp());

                if (dbitem == null)
                {
                    return null;
                }

                ArticleDto articledto = new ArticleDto
                {
                    author = dbitem.Author,
                    title = dbitem.Title,
                    createtime = dbitem.CreateTime,
                    content = dbitem.Content.Replace("{host}", Function.GetHostAndApp())
                };

                return articledto;

                //return new ArticleDto { content = dbitem == null ? null : dbitem.Content };
                //return new ArticleDto { content = content };
            }
        }


        public UserDto GetUser(dynamic wxuser)
        {
            if (((IDictionary<string, object>)wxuser).ContainsKey("openid") && !string.IsNullOrEmpty(wxuser.openid))
            {
                using (var db = base.NewDB())
                {
                    string openid = wxuser.openid;

                    var dbitem = db.UserAuths.FirstOrDefault(t => t.IdentityType == "wx" && t.Identifier == openid);

                    if (dbitem == null)
                    {
                        User user = new User();
                        user = db.Users.Add(user);
                        //db.SaveChanges();

                        UserProfile userpf = new UserProfile { 
                             ID=user.ID,
                             NickName = wxuser.userinfo["nickName"].ToString(),
                             AvatarUrl = wxuser.userinfo["avatarUrl"].ToString()
                        };
                        db.UserProfiles.Add(userpf);
                        //db.SaveChanges();


                        UserAuth ua = new UserAuth
                        {
                            IdentityType="wx",
                            Identifier = openid,
                            User = user
                        };

                        db.UserAuths.Add(ua);


                        Agent agent = new Agent
                        {
                            User = user
                        };
                        db.Agents.Add(agent);

                        UserIntegral ui = new UserIntegral
                        {
                            User = user
                        };
                        db.UserIntegrals.Add(ui);

                        

                        try
                        {
                            if (!string.IsNullOrEmpty(wxuser.sharecode))
                            {
                                Guid authid = ConvertHelper.StrToGuid(wxuser.sharecode, default(Guid));
                                var shareui = db.UserIntegrals.FirstOrDefault(t => t.User.AuthID == authid);
                                if (shareui != null)
                                {
                                    var bus = new IntegralBus(db);
                                    bus.IntegralProcess(shareui, "分享有礼", user);

                                    //分享直接成为上级
                                    var shareagent = shareui.User.Agent;
                                    if(shareagent.IsValid??false)
                                    {
                                        agent.ParentAgent = shareui.User.Agent;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                        db.SaveChanges();

                        dbitem = db.UserAuths.FirstOrDefault(t => t.IdentityType == "wx" && t.Identifier == openid);
                    }

                    dbitem.LastActiveTime = DateTime.Now;
                    db.SaveChanges();
                    //dbitem = db.UserAuths.FirstOrDefault(t => t.IdentityType == "wx" && t.Identifier == openid);

                    UserDto userdto = new UserDto
                    {
                        //id = dbitem.User.ID,
                        authid = dbitem.User.AuthID,
                        //agentcode = dbitem.User.UserProfile.AgentCode, 
                        //agentcode = dbitem.User.Agent.Code, 
                        //isagentvalid = dbitem.User.Agent.IsValid,
                        isvalid=dbitem.User.IsValid, 
                        isverified=dbitem.User.UserProfile.IsVerified
                    };

                    if (dbitem.User.UserProfile.NickName != wxuser.userinfo["nickName"].ToString())
                    {
                        dbitem.User.UserProfile.NickName = wxuser.userinfo["nickName"].ToString();
                    }

                    if (dbitem.User.UserProfile.AvatarUrl != wxuser.userinfo["avatarUrl"].ToString())
                    {
                        dbitem.User.UserProfile.AvatarUrl = wxuser.userinfo["avatarUrl"].ToString();
                    }

                    if (db.Entry(dbitem.User.UserProfile).State == EntityState.Modified)
                    {
                        db.SaveChanges();
                    }

                    //if (dbitem.User.Agent == null)
                    //{
                    //    Agent agent = new Agent
                    //    {
                    //        User = dbitem.User
                    //    };
                    //    dbitem.User.Agent = db.Agents.Add(agent);

                    //    db.SaveChanges();
                    //}

                    if (dbitem.User.Agent != null && (dbitem.User.Agent.IsValid??false))
                    {
                        userdto.agentcode = dbitem.User.Agent.Code;
                        userdto.isagentvalid = dbitem.User.Agent.IsValid;
                    }

                    var signin = db.IntegralSignIns.FirstOrDefault(t => t.UserIntegral.User.ID == dbitem.User.ID);

                    if (signin != null && signin.LastTime != null && signin.LastTime.Value.Date == DateTime.Now.Date)
                    {
                        userdto.issignin = true;
                    }

                    //return new ArticleDto { content = dbitem == null ? null : dbitem.Content };
                    return userdto;
                }
            }
            else
            {
                return null;
            }
        }


        public UserDto GetUserTest(dynamic wxuser)
        {
            if (((IDictionary<string, object>)wxuser).ContainsKey("openid") && !string.IsNullOrEmpty(wxuser.openid))
            {
                using (var db = base.NewDB())
                {
                    string openid = wxuser.openid;

                    var dbitem = db.UserAuths.FirstOrDefault(t => t.IdentityType == "wx" && t.Identifier == openid);

                    if (dbitem == null)
                    {


                        User user = new User();
                        db.Users.Add(user);
                        //db.SaveChanges();

                        UserProfile userpf = new UserProfile
                        {
                            ID = user.ID,
                            NickName = "XXX"//wxuser.userinfo["nickName"].ToString()
                        };
                        db.UserProfiles.Add(userpf);
                        //db.SaveChanges();


                        UserAuth ua = new UserAuth
                        {
                            IdentityType = "wx",
                            Identifier = openid,
                            User = user
                        };

                        db.UserAuths.Add(ua);


                        Agent agent = new Agent
                        {
                            User = user
                        };
                        db.Agents.Add(agent);

                        db.SaveChanges();

                    }

                    dbitem = db.UserAuths.FirstOrDefault(t => t.IdentityType == "wx" && t.Identifier == openid);

                    UserDto userdto = new UserDto
                    {
                        //id = dbitem.User.ID,
                        authid = dbitem.User.AuthID,
                        //agentcode = dbitem.User.UserProfile.AgentCode, 
                        //agentcode = dbitem.User.Agent.Code, 
                        //isagentvalid = dbitem.User.Agent.IsValid,
                        isvalid = dbitem.User.IsValid,
                        isverified = dbitem.User.UserProfile.IsVerified
                    };
                    //return new ArticleDto { content = dbitem == null ? null : dbitem.Content };

                    if (dbitem.User.Agent != null && (dbitem.User.Agent.IsValid ?? false))
                    {
                        userdto.agentcode = dbitem.User.Agent.Code;
                        userdto.isagentvalid = dbitem.User.Agent.IsValid;
                    }

                    return userdto;
                }
            }
            else
            {
                return null;
            }

        }


        public UserProfileDto GetUserProfile(Guid authid)
        {
            using (var db = base.NewDB())
            {
                var user = db.Users.FirstOrDefault(u => u.AuthID == authid && u.IsValid == true);

                if (user != null)
                {
                    var userpf = user.UserProfile;

                    UserProfileDto userpfdto = new UserProfileDto
                    {
                        authid = authid,
                        nickname = userpf.NickName,
                        realname = userpf.RealName,
                        gender = userpf.Gender,
                        address = userpf.Address,
                        idcard = userpf.IDCard,
                        mobilephone = userpf.MobilePhone
                    };

                    return userpfdto;
                }
                else
                {
                    return null;
                }
            }
        }


        public string EditUserProfile(UserProfileDto userpfdto)
        {
            try
            {
                using (var db = base.NewDB())
                {
                    var user = db.Users.FirstOrDefault(u => u.AuthID == userpfdto.authid);

                    if (user != null)
                    {
                        var userpf = user.UserProfile;

                        if (!(userpf.IsVerified ?? false))
                        {
                            userpf.RealName = userpfdto.realname;
                            userpf.Gender = userpfdto.gender;
                            userpf.Address = userpfdto.address;
                            userpf.IDCard = userpfdto.idcard;
                            userpf.MobilePhone = userpfdto.mobilephone;

                            db.SaveChanges();
                        }
                        else
                        {
                            return "已经验证，不可修改";
                        }
                        

                        return string.Empty;
                    }
                    else
                    {
                        return "未找到对应用户";
                    }
                }
            }
            catch (Exception ex)
            {
                return "保存失败";
            }
            
        }

        public string BindSupAgent(BindSupAgentDto req)
        {
            using (var db = base.NewDB())
            {
                var user = db.Users.FirstOrDefault(u => u.AuthID == req.authid && u.IsValid == true);

                if (user == null)
                {
                    return "当前用户信息缺失或无效";
                }

                //if (user.Agent == null || user.Agent.IsValid == false)
                //{
                //    return "当前用户代理缺失或无效";
                //}

                //当前无效但是可以添加上级
                if (user.Agent == null )
                {
                    return "当前用户代理缺失";
                }

                if (user.Agent.PID != null)
                {
                    return "已有上级代理";
                }

                var supagent = db.Agents.FirstOrDefault(t => t.Code == req.agentcode && t.IsValid == true);

                if (supagent == null)
                {
                    return "目标代理缺失或无效";
                }

                if (user.Agent == supagent)
                {
                    return "目标代理不符合规定";
                }

                if (IsSubAgent(db, user.Agent.ID, supagent.ID))
                {
                    return "目标代理是下级";
                }

                user.Agent.ParentAgent = supagent;
                user.Agent.SupAgentBindTime = DateTime.Now;

                db.SaveChanges();

                return string.Empty;
            }
        }

        public bool IsSubAgent(DbContext db,int cid, int pid)
        {
            return Function.IsInTreeBySql(db, cid, pid, "Sxc_Agent");
        }

        public dynamic GetSubAgentUser(string agentcode)
        {
            using (var db = base.NewDB())
            {
                db.Database.Log = Console.WriteLine;

                //var dbitem = db.Users.Where(t => t.Agent.ParentAgent.User.AuthID == authid).Select(t=>new {t.UserProfile.NickName});

                //var dbitem = db.Agents.FirstOrDefault(t => t.Code == agentcode).ChildAgents.Select(t => new { t.User.UserProfile.NickName});

                var childlist = db.Agents.FirstOrDefault(t => t.Code == agentcode).ChildAgents.Select(t => new { t.ID, t.Level, t.Type, t.SupAgentBindTime });

                //var dblist = from a in childlist
                //             from u in db.UserProfiles
                //             where a.ID == u.ID
                //             select new { 
                //                 avatarUrl = u.AvatarUrl,
                //                 nickname = u.NickName, 
                //                 realname = u.RealName,
                //                 mobilePhone = u.MobilePhone,
                //                 supagentbindTime = a.SupAgentBindTime,
                //                 type = a.Type,
                //                 level = a.Level
                //             };

                var dblist = from a in childlist
                             join u in db.UserProfiles
                             on a.ID equals u.ID
                             select new
                             {
                                 avatarUrl = u.AvatarUrl,
                                 nickname = u.NickName,
                                 realname = u.RealName,
                                 mobilePhone = u.MobilePhone,
                                 supagentbindTime = a.SupAgentBindTime,
                                 type = a.Type,
                                 level = a.Level
                             };


                //var dblist = db.Agents.FirstOrDefault(t => t.Code == agentcode).ChildAgents.ToList();

                dynamic res = dblist.ToList();

                return res;
            }
        }

        public dynamic GetAgentTypeInfo(int type,int level)
        {
            var res = new { typedesc = "无", leveldesc = "无" };
            try
            {
                if (type > 0 && level > 0)
                {
                    string key = "AgentTypesInfo";
                    List<AgentTypeDto> data;
                    if (CacheHelper.Exist(key))
                    {
                        data = CacheHelper.Get<List<AgentTypeDto>>("AgentTypesInfo");
                    }
                    else
                    {
                        var doc = XDocument.Load(HttpContext.Current.Server.MapPath("/Configs/AgentTypes.xml"));
                        data = doc.Descendants("AgentLevel").Select(t => new AgentTypeDto { typeid = t.Attribute("id").Value, typedesc = t.Attribute("desc").Value, levelid = t.Ancestors().First().Attribute("id").Value, leveldesc = t.Ancestors().First().Attribute("desc").Value }).ToList();
                        CacheHelper.Set(key, data);
                    }

                    var item = data.Where(t => t.typeid == type.ToString() && t.levelid == level.ToString()).Single();

                    res = new { typedesc = item.typedesc, leveldesc = item.leveldesc };
                }

                return res;
            }
            catch (Exception ex)
            {
                return res;
            }

            
        }

        public dynamic GetAgentRelationship(string agentcode)
        {
            using (var db = base.NewDB())
            {
                var current = db.Agents.FirstOrDefault(t => t.Code == agentcode);

                var ddd=db.Database.Log;

                var childlist = current.ChildAgents.Select(t => new { t.ID, t.Level, t.Type, t.SupAgentBindTime });

                var parent = current.ParentAgent;

                var parentdto = new object() { };

                var agenttype = GetAgentTypeInfo(current.Type, current.Level);

                string agentarea=(current.Area==null?"":current.Area.Area);

                var my = new { typedesc = agenttype.typedesc, leveldesc = agenttype.leveldesc, area = agentarea };
                

                if(parent!=null)
                {
                    parentdto = new
                    {

                        avatarUrl = parent.User.UserProfile.AvatarUrl,
                        nickname = parent.User.UserProfile.NickName,
                        realname = parent.User.UserProfile.RealName,
                        mobilePhone = parent.User.UserProfile.MobilePhone,
                        level = parent.Level,
                        type = parent.Type,
                    };
                }
                

                var dblist = from a in childlist
                             join u in db.UserProfiles
                             on a.ID equals u.ID
                             select new
                             {
                                 avatarUrl = u.AvatarUrl,
                                 nickname = u.NickName,
                                 realname = u.RealName,
                                 mobilePhone = u.MobilePhone,
                                 supagentbindTime = a.SupAgentBindTime,
                                 type = a.Type,
                                 level = a.Level
                             };

                //dynamic res = dblist.ToList();

                dynamic res = new { sup = parentdto, sub = dblist.ToList(), self = my };

                return res;
            }
        }

        public AgentDto GetUserAgent(Guid authid)
        {
            using (var db = base.NewDB())
            {
                var dbitem = db.Users.FirstOrDefault(t => t.AuthID == authid);

                if (dbitem == null || dbitem.Agent == null)
                {
                    return null;
                }

                AgentDto agentDto = new AgentDto
                {
                    code = dbitem.Agent.Code,
                    commission = dbitem.Agent.Commission,
                    type = dbitem.Agent.Type,
                    level = dbitem.Agent.Level,
                    isvalid = dbitem.Agent.IsValid
                };

                return agentDto;
            }
        }

        public string GetNickNameByAgentCode(string code)
        {
            using (var db = base.NewDB())
            {
                return db.Agents.Single(t => t.Code == code).User.UserProfile.NickName;
            }
        }

        public string CooperationJoin(CooperationDto coopdto)
        {
            using (var db = base.NewDB())
            {
                var dbitem = db.Users.FirstOrDefault(t => t.AuthID == coopdto.authid);

                if (dbitem == null)
                {
                    return "申请用户异常";
                }

                Cooperation coop = new Cooperation
                {
                    Address = coopdto.address,
                    User = dbitem,
                    AreaID = coopdto.areaid,
                    Level = coopdto.level,
                    Memo = coopdto.memo,
                    MobilePhone = coopdto.mobilephone,
                    Name = coopdto.name,
                    Type = coopdto.type,
                    AgentAreaInfo = coopdto.agentareainfo,
                    AreaInfo=coopdto.areainfo
                };

                db.Cooperations.Add(coop);

                db.SaveChanges();

                return string.Empty;
            }
        }


        public string CoursesReservation(ReservationDto reservationdto)
        {
            using (var db = base.NewDB())
            {
                var dbitem = db.Users.FirstOrDefault(t => t.AuthID == reservationdto.authid);

                if (dbitem == null || dbitem.Agent == null)
                {
                    return "申请用户异常";
                }

                Reservation reservation = new Reservation
                {
                    Address = reservationdto.address,
                    User = dbitem,
                    Memo = reservationdto.memo,
                    MobilePhone = reservationdto.mobilephone,
                    Name = reservationdto.name,
                    //PurposeID = reservationdto.purposeid
                    Purpose = reservationdto.purpose,
                    AreaInfo = reservationdto.areainfo
                };

                List<ReservationCourse> rclist=new List<ReservationCourse>();
                foreach(var id in reservationdto.courseids.Split(','))
                {
                    rclist.Add(new ReservationCourse
                    {
                        CourseID = int.Parse(id),
                        Reservation = reservation
                    });
                }

                reservation.ReservationCourses = rclist;

                db.Reservations.Add(reservation);

                db.SaveChanges();

                return string.Empty;
            }
        }

        public List<CommissionRecordDto> GetUserCommission(Guid authid)
        {
            using (var db = base.NewDB())
            {

                var dblist = db.CommissionRecords.Include(t=>t.UserPayment.User.UserProfile).Where(t => t.Agent.User.AuthID == authid).OrderByDescending(t => t.ID);

                var res = new List<CommissionRecordDto>();
                foreach (var item in dblist)
                {
                    res.Add(new CommissionRecordDto
                    {
                        ID = item.ID,
                        Commission = item.Commission,
                        CreateTime = item.CreateTime,
                        Memo = item.Memo,
                        PayUser = string.Format(@"{0}[{1}]", item.UserPayment.User.UserProfile.NickName, item.UserPayment.User.UserProfile.RealName)
                    });
                }
                return res;
            }
        }


        

    }
}
