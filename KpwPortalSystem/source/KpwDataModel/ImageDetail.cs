using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nairc.KpwFramework.DataModel
{
    public class ImageDetail
    {
        public string Name { get; set; }

        public string ThumbImage { get; set; }

        public string Image { get; set; }

        public string BigImage { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
