using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;


namespace UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        UserBLL userbll = new UserBLL();
        // GET: Admin/Login
        public ActionResult Index()
        {
            UserDTO dto = new UserDTO();
            return View(dto);
        }
        [HttpPost]
        public ActionResult Index(UserDTO model)
        {
            /*var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();*/
            if (!ModelState.IsValid)
            {
                //return model.RequiredAttribute();
                UserDTO user = userbll.GetUserWithUsernameAndPassword(model);
                if (user.ID != 0)
                {
                    UserStatic.UserID = user.ID;
                    UserStatic.isAdmin = user.IsAdmin;
                    UserStatic.NameSurname = user.Name;
                    UserStatic.ImagePath = user.ImagePath;
                    LogBLL.AddLog(General.ProcessType.Login,General.TableName.Login, 12);
                    return RedirectToAction("Index", "Post");

                }
                else
                //Console.WriteLine(errors);
                return View(model);   
            }
            else
                return View(model);
        }

    }
}