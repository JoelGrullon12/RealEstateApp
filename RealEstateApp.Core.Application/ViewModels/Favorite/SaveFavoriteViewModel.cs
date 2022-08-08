using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.Favorite
{
    public class SaveFavoriteViewModel:BaseViewModel
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }
    }
}
