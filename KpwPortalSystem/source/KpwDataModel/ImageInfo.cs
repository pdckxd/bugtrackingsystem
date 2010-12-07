using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nairc.KpwFramework.DataModel
{ 

    public class ImageInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string Image { get; set; }
        public string ThumbImage { get; set; }
        public string BigImage { get; set; }
        public string FitsImage { get; set; }
        public string StarName { get; set; }
        public int Ra { get; set; }
        public int Dec { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
