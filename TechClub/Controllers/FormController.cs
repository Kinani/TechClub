﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using TechClub.Models;
using System.IO;

namespace TechClub.Controllers
{   
    public class FormController : Controller
    {
        // GET: Form
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThankYou()
        {
            return View("ThankYou");
        }
        [HttpPost]
        public ActionResult Submit(Applicant model)
        {
            if (Request != null)
            {
                var excelFileInfo = new FileInfo(Path.Combine(Server.MapPath("~/Documents/"), "applicants.xlsx"));
                string firstName = model.FName;

                using (var package = new ExcelPackage(excelFileInfo))
                {
                    var currentSheet = package.Workbook.Worksheets["FirstRound"];
                    
                    var noOfRow = currentSheet.Dimension.End.Row;
                    noOfRow++;
                    currentSheet.Cells[noOfRow, 1].Value = model.Id;
                    currentSheet.Cells[noOfRow, 2].Value = model.FName;
                    currentSheet.Cells[noOfRow, 3].Value = model.LName;
                    currentSheet.Cells[noOfRow, 4].Value = model.ThebesId;
                    currentSheet.Cells[noOfRow, 5].Value = model.Phone;
                    currentSheet.Cells[noOfRow, 6].Value = model.Email;
                    currentSheet.Cells[noOfRow, 7].Value = model.Level;
                    currentSheet.Cells[noOfRow, 8].Value = model.Specialization;
                    currentSheet.Cells[noOfRow, 9].Value = model.ChosenTeam;
                    currentSheet.Cells[noOfRow, 10].Value = model.Laptop;
                    currentSheet.Cells[noOfRow, 11].Value = model.BasicCoding;
                    currentSheet.Cells[noOfRow, 12].Value = model.AvailableOnDayX;
                    currentSheet.Cells[noOfRow, 13].Value = model.GotSomethingElseToSay;
                    currentSheet.Cells[noOfRow, 14].Value = model.WhatDoYouThinkOfThisForm;
                    package.Save();
                }
            }
            return View("ThankYou");
        }
            
    }

}
