namespace Panta2.Core.Models
{
    public class SerivceWithIsFavoriteModel
    {
        public int ServiceId { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public string? Link { get; set; }
        public bool? IsFavorite { get; set;}
    }
}
