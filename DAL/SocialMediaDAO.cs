using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class SocialMediaDAO : PostContext
    {
        public int AddSocialMedia(SocialMedia social)
        {
            try
            {
                db.SocialMedia.Add(social);
                db.SaveChanges();
                return social.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<SocialMediaDTO> GetSocialMedias()
        {
            List<SocialMedia> list = db.SocialMedia.Where(x => x.isDeleted == false).ToList();
            List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();
            foreach (var item in list)
            {
                SocialMediaDTO dto = new SocialMediaDTO();
                dto.Name = item.Name;
                dto.Link = item.Link;
                dto.ImagePath = item.ImagePath;
                dto.ID = item.ID;
                dtolist.Add(dto);

            }
            return dtolist;
        }

        public SocialMediaDTO GetSocialMediaWithID(int ID)
        {
            SocialMedia socialmedia = db.SocialMedia.First(x => x.ID == ID);
            SocialMediaDTO dto = new SocialMediaDTO();
            dto.ID = socialmedia.ID;
            dto.Name = socialmedia.Name;
            dto.Link = socialmedia.Link;
            dto.ImagePath = socialmedia.ImagePath;
            return dto;
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
            try
            {
                SocialMedia social = db.SocialMedia.First(x => x.ID == model.ID);
                string oldImagePath = social.ImagePath;
                social.Name = model.Name;
                social.Link = model.Link;
                if (model.ImagePath != null)
                    social.ImagePath = model.ImagePath;
                //social.ImagePath = model.ImagePath;
                social.LastUpdateDate = DateTime.Now;
                social.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return oldImagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
