using System;
using Covide.Web.Infrastructure.Entities;

namespace Covide.Web.Infrastructure
{
    public interface IRepository : IDisposable
    {
        //IEnumerable<ColorCode> GetColorCodes();
        ColorCode GetColorCodeByHex(string colorHex);
        //void InsertColorCode(ColorCode color);
        //void DeleteColorCode(string colorHex);
        //void UpdateColorCode(ColorCode color);
        void Save();
    }
}
