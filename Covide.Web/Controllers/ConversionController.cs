using Covide.Web.Abstractions;
using Covide.Web.Domain;
using Covide.Web.Domain.Models;
using Covide.Web.Infrastructure;
using Covide.Web.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Covide.Web.Controllers
{
    [ApiController]
    public class ConversionController : ControllerBase
    {
        private readonly ConversionService _conversionService;
        private readonly IRepository _repository;

        public ConversionController(ConversionService conversionService, IRepository repository)
        {
            _conversionService = conversionService;
            _repository = repository;
        }

        [HttpGet("{hex}")]
        public ActionResult Get([FromRoute] string hex)
        {
            string pattern = "^[0-9a-fA-F]{6}$";

            if (!Regex.Match(hex, pattern).Success)
            {
                return BadRequest();
            }

            ConversionModel conversionModel = _conversionService.ComputeConversionModel(hex);
            ColorCode colorCode = _repository.GetColorCodeByHex(hex.ToUpper());

            if (colorCode == null)
            {
                // do some more error checking
                return BadRequest();
            }

            var response = new GetColorCodeByHexResponse
            {
                HexTriplet = hex.ToLower(),
                ColorCodeName = colorCode.Name,
                RgbDecimal = conversionModel.RgbDecimal,
                RgbPercentage = conversionModel.RgbPercentage,
                Cmyk = conversionModel.Cmyk,
                Hsl = conversionModel.Hsl,
                Hsv = conversionModel.Hsv,
                Xyz = conversionModel.Xyz
            };

            return Ok(response);
        }
    }
}
