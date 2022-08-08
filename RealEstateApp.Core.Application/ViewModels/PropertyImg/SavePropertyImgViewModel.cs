using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.PropertyImg
{
    public class SavePropertyImgViewModel : BaseViewModel
    {
        public int PropertyId { get; set; }
        public string ImgUrl { get; set; }
    }
}
