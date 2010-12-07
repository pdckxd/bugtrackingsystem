using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nairc.KpwFramework.DataModel;

namespace Nairc.KpwDataAccess
{
    public interface IImageInfoDB
    {
        List<ImageInfo> GetTop20Images();

        List<ImageInfo> GetImageInfoByDate(DateTime date);

        ImageInfo GetImageByID(int id);

        int AddImageInfo(ImageInfo image);

        void UpdateImage(ImageInfo image);
    }
}
