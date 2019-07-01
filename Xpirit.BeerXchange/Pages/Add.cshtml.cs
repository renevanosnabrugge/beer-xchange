using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Xpirit.BeerXchange.Model;

namespace Xpirit.BeerXchange
{
    public class AddModel : PageModel
    {
        private readonly Xpirit.BeerXchange.BeerXchangeContext _context;

        public AddModel(Xpirit.BeerXchange.BeerXchangeContext context)
        {
            _context = context;
        }

        public List<Beer> SwitchedForBeers { get; set; }

        public async Task<IActionResult> OnGet()
        {
            SwitchedForBeers = await _context.Beer.Where(b => b.RemovedBy == null).ToListAsync();
            return Page();
        }

        [BindProperty]
        public Beer Beer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Beer.SwitchedForId != null && Beer.SwitchedForId != -1)
            {
                var switchedBeer = _context.Beer.Single<Beer>(b => b.Id == Beer.SwitchedForId);

                switchedBeer.RemovedBy = User.FindFirst("name").Value;
                switchedBeer.RemovedDate = DateTime.Now;
            }
            else
            {
                Beer.SwitchedFor = null;
                Beer.SwitchedForId = null;
            }

            Beer.AddedDate = DateTime.Now;
            Beer.CreatedBy = User.FindFirst("name").Value;
            _context.Beer.Add(Beer);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public JsonResult OnPostScanAsync(IFormFile formFile)
        {
        ////https://ironsoftware.com/csharp/barcode/#barcode-quickstart
              //https://docs.microsoft.com/en-us/aspnet/core/razor-pages/upload-files?view=aspnetcore-2.2
              //https://stackoverflow.com/questions/32391146/read-barcode-via-camera-in-an-asp-net-mvc-5-application
              //string barcode = "";
              //try
              //{
              //    string path = "";
              //    if (file.ContentLength > 0)
              //    {
              //        var fileName = Path.GetFileName(file.FileName);
              //        path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
              //        file.SaveAs(path);
              //    }

            //    // Now we try to read the barcode
            //    // Instantiate BarCodeReader object
            //    BarCodeReader reader = new BarCodeReader(path, BarCodeReadType.Code39Standard);
            //    System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            //    System.Diagnostics.Debug.WriteLine("Width:" + img.Width + " - Height:" + img.Height);

            //    try
            //    {
            //        // read Code39 bar code
            //        while (reader.Read())
            //        {

            //            // detect bar code orientation
            //            ViewBag.Title = reader.GetCodeText();
            //            barcode = reader.GetCodeText();
            //        }
            //        reader.Close();
            //    }

            //    catch (Exception exp)
            //    {

            //        System.Console.Write(exp.Message);
            //    }


            //}
            //catch (Exception ex)
            //{
            //    ViewBag.Title = ex.Message;
            //}
            return new JsonResult("");


        }
    }
}