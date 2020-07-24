using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Auto.Model.Entities
{
    public static class EntityEnum
    {
        public enum Gender
        {
            [Display(Name = "女性")]
            Female = 0,
            [Display(Name = "男性")]
            Male = 1,
            [Display(Name = "保密")]
            Secret = 2
        }
    }
}
