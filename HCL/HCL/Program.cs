using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
namespace HCL
{
    class Program
    {
        static void Main(string[] args)
        {

            HCL.A _objadm = new A();
            //Console.WriteLine(_objadm.NewVehicleRegister());
            //_objadm.DisplayRegisterVehicle();
            _objadm.DisplayAvailableRegisterVehicle();
            //_objadm.DisplayOnRideRegisterVehicle();


            //HCL.D _objdri = new D();
            //_objdri.DisplayWaitingRidersDetails();
            //_objdri.SendVehicleStatus();


            //HCL.R _objrid = new R();
            //_objrid.DisplayAvailableVehicles();
            //_objrid.AddNewRiderDetails();
            //_objrid.BookNewRide();
            //_objrid.Canclebooking();
            //HCL.Sqlconnection obj = new Sqlconnection(); ;
            //obj.CreateTable();
            Console.ReadLine();
        }
    }
}