using Microsoft.AspNetCore.Mvc;
using System;
using StadisticCalculator.Services;
using StadisticCalculator.Models;

namespace StadisticCalculator.Controllers
{
    public class ToolsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string numbers, char delimiter, bool isAscending)
        {
            try
            {
                General general = new General(numbers);
                general.Delimiter = delimiter;

                NumbersTools numbersTools = new NumbersTools(general);

                ViewBag.SortedNumbers = numbersTools.SortNumbers(isAscending);

                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Error = $"Ha ocurrido un error interno al organizar los números: {ex.Message}";
                return View();
            }
        }

        public IActionResult FormatText()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FormatText(string numbers, string option)
        {
            try
            {
                General general = new General(numbers);

                NumbersTools numbersTools = new NumbersTools(general);

                ViewBag.FormattedText = numbersTools.FormatNumbers(option);

                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Error = $"Ha ocurrido un error interno al formatear el texto: {ex.Message}";
                return View();
            }
        }
    }
}
