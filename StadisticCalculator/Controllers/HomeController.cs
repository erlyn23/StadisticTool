using System;
using Microsoft.AspNetCore.Mvc;
using StadisticCalculator.Services;
using StadisticCalculator.Models;

namespace StadisticCalculator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ExerciseParams model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    General general = new General(model.Numbers);

                    general.Delimiter = model.Delimiter;
                    general.ClassInterval = model.ClassInterval;

                    NumbersTools numbersTools = new NumbersTools(general);

                    Table table = new Table(general, numbersTools);
                    CentralTendencyMeasures centralTendencyMeasures = new CentralTendencyMeasures(general, table);

                    model.Range = table.GetRange();
                    model.Amplitude = table.GetAmplitude();
                    model.Intervals = table.GetIntervals();
                    model.ClassMarks = table.GetClassMark();
                    model.AbsolutesFrequencies = table.GetAbsolutesFrequency();
                    model.RelativeFrequencies = table.GetRelativeFrequencies();
                    model.TotalAcumulatedFrequencies = table.GetTotalAcumulatedFrequencies();


                    if (model.HasArithmeticMedia)
                        model.ArithmeticMedia = centralTendencyMeasures.GetArithmeticMedia();
                    if(model.HasMedian)
                        model.Median = centralTendencyMeasures.GetMedian();
                    if(model.HasFashion)
                        model.Fashion = centralTendencyMeasures.GetFashion();
                    if (model.HasVariance)
                        model.Variance = centralTendencyMeasures.GetVariance();
                    if (model.HasStandardDesviation)
                        model.StandardDesviation = centralTendencyMeasures.GetStandardDesviation();
                    if (model.HasVariationCoefficent)
                        model.VariationCoefficent = centralTendencyMeasures.GetVariationCoefficent();

                    ViewBag.Success = true;

                    return View(model);
                }
                else
                {
                    ViewBag.Error = "Ha ocurrido un error interno al generar la tabla, revisa si los datos proveídos son válidos";
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Error = $"Ha ocurrido un error interno al generar la tabla: {ex.Message}";
                return View();
            }
        }

        public IActionResult How()
        {
            return View();
        }

    }
}
