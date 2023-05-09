namespace Panta2.Core.Models.User
{
    public class EditFavoriteFromUserModel
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public bool IsFavorite { get; set; }
    }
}
