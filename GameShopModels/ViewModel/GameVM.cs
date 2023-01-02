using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModels.ViewModel
{
    public class GameVM
    {
        //Sadece Game Modeli değil Tür ve Stüdyo modellerini birlikte kullanacağım için Viewmodel oluşturdum.

        public Game Game { get; set; }
        // Dropdown içinde gösterticeğim için selectlistıtem kullandım.
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> StudioList { get; set; }
    }
}
