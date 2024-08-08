using Covide.Web.Domain.Models;

namespace Covide.Web.Domain
{
    public interface IConversionService
    {
        ConversionModel ComputeConversionModel(string hex);
    }
}