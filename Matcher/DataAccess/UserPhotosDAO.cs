using Matcher.Models;

namespace Matcher.DataAccess
{
	public class UserPhotosDAO
	{
		private readonly VoVoContext _context;
		public UserPhotosDAO(VoVoContext context) { 
			_context = context;
		}
		public List<string> GetListPhotosByUsrID(int usrID)
		{
			return _context.UserPhotos.Where(up=>up.UserId == usrID).Select(u=>u.PhotoLink).ToList();
		}

		public string GetAvatarByUsrID(int usrID)
		{
			return _context.UserPhotos.Where(up => up.UserId == usrID).Select(u => u.PhotoLink).FirstOrDefault();
		}

	}
}
