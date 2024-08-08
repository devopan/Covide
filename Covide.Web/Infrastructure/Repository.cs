using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Covide.Web.Infrastructure.Entities;
using System.Linq;

namespace Covide.Web.Infrastructure
{
    public class Repository : IRepository, IDisposable
    {
        private CovideDataContext context;

        public Repository(CovideDataContext context)
        {
            this.context = context;
        }

        //public IEnumerable<ColorCode> GetColorCodes()
        //{
        //    return context.ColorCodes.ToList();
        //}

        public ColorCode GetColorCodeByHex(string colorHex)
        {
            return context.ColorCodes.Find(colorHex);
        }

        //public void InsertColorCode(ColorCode color)
        //{
        //    context.ColorCodes.Add(color);
        //}

        //public void DeleteColorCode(string colorHex)
        //{
        //    ColorCode color = context.ColorCodes.Find(colorHex);
        //    context.ColorCodes.Remove(color);
        //}

        //public void UpdateColorCode(ColorCode color)
        //{
        //    context.Entry(color).State = EntityState.Modified;
        //}

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
