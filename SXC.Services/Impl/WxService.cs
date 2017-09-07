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

            return Function.GetPicUrl(pic);
        }

        public List<NavDto> GetNavigations()
        {
            using (var db = base.NewDB())
            {
                var dblist = db.Navigations.Where(t => t.IsValid == true).OrderBy(t => t.Order).ToList();

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


        public List<TeacherDto> GetTeachers()
        {
            using (var db = base.NewDB())
            {
                var dblist = db.Teachers.Where(t => t.IsValid == true).OrderBy(t => t.Order).ToList();

                var res = new List<TeacherDto>();
                foreach (var item in dblist)
                {
                    res.Add(new TeacherDto
                    {
                        id = item.ID,
                        name = item.Name,
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

        public List<CourseDto> GetCourses()
        {
            using (var db = base.NewDB())
            {
                var dblist = db.Courses.Where(t => t.IsValid == true).OrderBy(t => t.Order).ToList();

                var res = new List<CourseDto>();
                foreach (var item in dblist)
                {
                    res.Add(new CourseDto
                    {
                        id = item.ID,
                        name = item.Name,
                        picurl = GetPicUrl(item.Pic),
                        //articleid = item.Article == null ? IntNull : item.Article.ID
                        articleid = item.ArticleID
                    });
                }

                return res;
            }
        }

        public List<PromotionDto> GetPromotions()
        {
            using (var db = base.NewDB())
            {
                var dblist = db.Promotions.Where(t => t.IsValid == true).OrderBy(t => t.BeginTime).ToList();

                var res = new List<PromotionDto>();
                foreach (var item in dblist)
                {
                    res.Add(new PromotionDto
                    {
                        id = item.ID,
                        name = item.Name,
                        desc = item.Desc,
                        picurl = GetPicUrl(item.Pic),
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
                        db.Users.Add(user);
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
                        userpf.RealName = userpfdto.realname;
                        userpf.Gender = userpfdto.gender;
                        userpf.Address = userpfdto.address;
                        userpf.IDCard = userpfdto.idcard;
                        userpf.MobilePhone = userpfdto.mobilephone;

                        db.SaveChanges();

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

                if (user.Agent == null || user.Agent.IsValid == false)
                {
                    return "当前用户代理缺失或无效";
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
                db.Database.Log=Console.WriteLine;

                //var dbitem = db.Users.Where(t => t.Agent.ParentAgent.User.AuthID == authid).Select(t=>new {t.UserProfile.NickName});

                //var dbitem = db.Agents.FirstOrDefault(t => t.Code == agentcode).ChildAgents.Select(t => new { t.User.UserProfile.NickName});

                var childlist = db.Agents.FirstOrDefault(t => t.Code == agentcode).ChildAgents;

                var dblist = from a in childlist
                             from u in db.UserProfiles
                             where a.User == u.User
                             select new { 
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
        

    }
}
