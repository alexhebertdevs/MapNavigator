using System.ComponentModel.DataAnnotations;

namespace MapNavigatorWeb.InputModels.Map
{
    public class MapActionInputs
    {
        [Required]
        public string Instructions { get; set; }
    }
}
