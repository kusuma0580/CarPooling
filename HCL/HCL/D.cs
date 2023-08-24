using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace HCL
{
    internal class D
    {
        string _vehiclenumber = "";
        string _vehiclestatus = "";
        string _query = "";
        Sqlconnection _objConnect = new Sqlconnection();
        public void SendVehicleStatus()
        {
            Console.WriteLine("UPATE VEHICLE STATUS MODULE");
            Console.WriteLine("---------------------------");
            Console.WriteLine("                           ");
            Console.WriteLine("Please Enter your Vehicle Number");
            _vehiclenumber = Console.ReadLine();
            do
            {
                Console.WriteLine("Please Enter Current Status (AVAILABLE/ONRIDE)");
                _vehiclestatus = Console.ReadLine();
                if (_vehiclestatus == "AVAILABLE")
                {
                    _query = "update Vehicles set Vehicle_current_status='AVAILABLE' where Vehicle_Number='" + _vehiclenumber + "' ";

                    //Connect DB and Execute SQl statement
                    _objConnect.SqlExecuteScalar(_query);
                }
                if (_vehiclestatus == "ONRIDE")
                {
                    _query = "update Vehicles set Vehicle_current_status='ONRIDE' where Vehicle_Number='" + _vehiclenumber + "' ";

                    //Connect DB and Execute SQl statement
                    _objConnect.SqlExecuteScalar(_query);
                }
            }
            while (!(_vehiclestatus == "AVAILABLE" || _vehiclestatus == "ONRIDE"));

            DisplayWaitingRidersDetails();
        }

        public void DisplayWaitingRidersDetails()
        {
            try
            {

                _query = "select RIDER_NAME,RIDER_PHONENUMBER,RIDER_VEHICLE_TYPE,RIDER_PASSENGERS,RIDER_REQUEST_TIME " +
                    "from Riders where rider_status='WAITING' ";
                DataTable _dtvehicles = new DataTable();
                //Connect DB and Execute SQl statement
                Sqlconnection _objconnect = new Sqlconnection();
                _dtvehicles = _objconnect.SqlDatacollection(_query);

                if (_dtvehicles.Rows.Count > 0)
                {
                    Console.WriteLine("RIDER_NAME   RIDER_PHONENUMBER   RIDER_VEHICLE_TYPE  RIDER_PASSENGERS    RIDER_REQUEST_TIME");
                    for (int i = 0; i < _dtvehicles.Rows.Count; i++)
                    {
                        Console.WriteLine(_dtvehicles.Rows[i][0].ToString() + "   " + _dtvehicles.Rows[i][1].ToString() + "   " + _dtvehicles.Rows[i][2].ToString() + "   " + _dtvehicles.Rows[i][3].ToString() + "   " + _dtvehicles.Rows[i][4].ToString() + "   ");
                    }
                }
                else { Console.WriteLine("No RIder are waiting to ride now"); }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
