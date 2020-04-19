using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using SensorWebApp.Models;
using System.IO.Ports;
using System.Threading;

namespace SensorWebApp.Controllers
{
    public class CRUDController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                CRUDModel model = new CRUDModel();
                SerialPort serialPort = new SerialPort("COM5", 9600);
                SQLiteConnection conn = model.CreateConnection();
                serialPort.Open();
                Thread.Sleep(200);
                String Data = serialPort.ReadExisting();
                if (Data != "" && Data != "\n")
                {
                    model.InsertData(conn, Data);
                }
                DataTable dt = model.ReadData(conn);
                serialPort.Close();
                Console.WriteLine(Data);
                return View("Index", dt);
            }
            catch (Exception e)
            {
                CRUDModel model = new CRUDModel();
                SQLiteConnection conn = model.CreateConnection();
                DataTable dt = model.ReadData(conn);
                return View("Index", dt);

            }
        }
    }
}