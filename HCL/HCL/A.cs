using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace HCL
{
    internal class A
    {
        string _vehiclenumber = "";
        string _vehicletype = "";
        string _vehiclercnumber = " ";
        int _vehicleage = 0;
        int _vehiclecapacity = 0;
        string _vehicleStatus = "AVAILABLE";
        string _query = "";
        string _action = "";
        ArrayList _vehiclelist = new ArrayList();
        Sqlconnection _objConnect = new Sqlconnection();

        public A()
        {

        }
        public string NewVehicleRegister()
        {
            try
            {
                Console.WriteLine("Enter Vehicle Number: ");
                _vehiclenumber = Console.ReadLine();

                DataTable _dtvehchk = ValidateVehicle();
                if (_dtvehchk.Rows.Count == 0)
                {
                    Console.WriteLine("Enter vehicletype in Uppercase");
                    Console.WriteLine("Enter Vehicle Type: ");
                    _vehicletype = Console.ReadLine();
                    Console.WriteLine("Enter Vehicle RC Number: ");
                    _vehiclercnumber = Console.ReadLine();
                    Console.WriteLine("Enter Vehicle Age(Number): ");
                    _vehicleage = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Vehicle Capacity(Number): ");
                    _vehiclecapacity = Int32.Parse(Console.ReadLine());
                    Regex rc = new Regex(@"^[A-Z]{2}[0-9]{2}[A-Z]{2}[0-9]{4}$");
                    Regex num = new Regex(@"^[A-Z]{2}[0-9]{2}[0-9]{4}$");
                    //Insert statement 
                    if (rc.IsMatch(_vehiclercnumber) != true || num.IsMatch(_vehiclenumber) != true)
                    {
                        if (_vehicleage < 0 || _vehicleage > 16)
                        {
                            Console.WriteLine("vehicles that are 1-15 years old caan only be used");
                        }
                        else if(_vehicletype != "CAR"  || _vehicletype != "BIKE")
                        {
                            Console.WriteLine("Vehicle must be CAR or BIKE");
                        }
                        else
                        {
                            _query = "insert into Vehicles (Vehicle_Number,Vehicle_Type,Vehicle_RC_Number,Vehicle_Age,Vehicle_Capacity,Vehicle_current_status) values ('" + _vehiclenumber + "','" + _vehicletype + "','" + _vehiclercnumber + "'," + _vehicleage + "," + _vehiclecapacity + ",'" + _vehicleStatus + "')";

                            //Connect DB and Execute SQl statement
                            _objConnect.SqlExecuteScalar(_query);
                        }
                    }

                    else
                    {
                        Console.WriteLine("Vehicle Number or Vehicle RC Number is invalid");
                    }
                }
                else
                {
                    Console.WriteLine("Vehicle Number Already Exist");
                }

                //DisplayRegisterVehicle();
                return "success";
            }
            catch (Exception ex)
            {
                return "failed";
            }
        }
        public void DisplayRegisterVehicle()
        {
            try
            {
                _query = "select Vehicle_Number,Vehicle_Type,Vehicle_RC_Number,Vehicle_Age,Vehicle_Capacity,Vehicle_current_status " +
                    "from Vehicles ";
                DataTable _dtvehicles = new DataTable();
                //Connect DB and Execute SQl statement
                Sqlconnection _objconnect = new Sqlconnection();
                _dtvehicles = _objconnect.SqlDatacollection(_query);

                Console.WriteLine("BELOW VEHICLE'S ARE REGISTED ALL TIME");
                Console.WriteLine("------------------------------");
                Console.WriteLine("                              ");

                if (_dtvehicles.Rows.Count > 0)
                {
                    Console.WriteLine("Vehicle_Number   Vehicle_Type    Vehicle_RC_Number   Vehicle_Age   Vehicle_Capacity    Vehicle_Status");
                    for (int i = 0; i < _dtvehicles.Rows.Count; i++)
                    {
                        Console.WriteLine(_dtvehicles.Rows[i][0].ToString() + "   " + _dtvehicles.Rows[i][1].ToString() + "   " + _dtvehicles.Rows[i][2].ToString() + "   " + _dtvehicles.Rows[i][3].ToString() + "   " + _dtvehicles.Rows[i][4].ToString() + "   " + _dtvehicles.Rows[i][5].ToString() + "   ");
                    }
                }
                //else if (_vehiclelist.Count > 0)
                //{
                //    Console.WriteLine("Vehicle_Number   Vehicle_Type    Vehicle_RC_Number   Vehicle_Age Vehicle_Capacity");
                //    foreach (ArrayList item in _vehiclelist)
                //    {
                //        Console.WriteLine(item[0].ToString() + "   " + item[1].ToString() + "   " + item[2].ToString() + "   " + item[3].ToString() + "   " + item[4].ToString() + "   ");   
                //    }
                //}
                else { Console.WriteLine("No Vechile have been Registered yet."); }

            }
            catch (Exception ex)
            {

            }
        }

        private DataTable ValidateVehicle()
        {
            DataTable _dtriderchk = new DataTable();
            try
            {
                _query = "select Vehicle_Number,Vehicle_Type,Vehicle_RC_Number,Vehicle_Age,Vehicle_Capacity,Vehicle_current_status " +
                    "from Vehicles Vehicle_Number='" + _vehiclenumber + "' ";
                _dtriderchk = _objConnect.SqlDatacollection(_query);
            }
            catch (Exception ex)
            {
            }
            return _dtriderchk;
        }

        public void DisplayAvailableRegisterVehicle()
        {
            try
            {
                _query = "select Vehicle_Number,Vehicle_Type,Vehicle_RC_Number,Vehicle_Age,Vehicle_Capacity from Vehicles where Vehicle_current_status='AVAILABLE' ";
                DataTable _dtvehicles = new DataTable();
                //Connect DB and Execute SQl statement
                Sqlconnection _objconnect = new Sqlconnection();
                _dtvehicles = _objconnect.SqlDatacollection(_query);
                Console.WriteLine("BELOW VEHICLE'S ARE AVAILABLE NOW");
                Console.WriteLine("------------------------------");
                Console.WriteLine("                              ");

                if (_dtvehicles.Rows.Count > 0)
                {
                    Console.WriteLine("Vehicle_Number   Vehicle_Type    Vehicle_RC_Number   Vehicle_Age Vehicle_Capacity");
                    for (int i = 0; i < _dtvehicles.Rows.Count; i++)
                    {
                        Console.WriteLine(_dtvehicles.Rows[i][0].ToString() + "   " + _dtvehicles.Rows[i][1].ToString() + "   " + _dtvehicles.Rows[i][2].ToString() + "   " + _dtvehicles.Rows[i][3].ToString() + "   " + _dtvehicles.Rows[i][4].ToString() + "   ");
                    }

                }
                else { Console.WriteLine("No Vechile Available to Ride Now"); }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void DisplayOnRideRegisterVehicle()
        {
            try
            {
                _query = "select Vehicle_Number,Vehicle_Type,Vehicle_RC_Number,Vehicle_Age,Vehicle_Capacity from Vehicles where Vehicle_current_status='ONRIDE' ";
                DataTable _dtvehicles = new DataTable();
                //Connect DB and Execute SQl statement
                Sqlconnection _objconnect = new Sqlconnection();
                _dtvehicles = _objconnect.SqlDatacollection(_query);

                Console.WriteLine("BELOW VEHICLE'S ARE ONRIDE NOW");
                Console.WriteLine("------------------------------");
                Console.WriteLine("                              ");

                if (_dtvehicles.Rows.Count > 0)
                {
                    Console.WriteLine("Vehicle_Number   Vehicle_Type    Vehicle_RC_Number   Vehicle_Age Vehicle_Capacity");
                    for (int i = 0; i < _dtvehicles.Rows.Count; i++)
                    {
                        Console.WriteLine(_dtvehicles.Rows[i][0].ToString() + "   " + _dtvehicles.Rows[i][1].ToString() + "   " + _dtvehicles.Rows[i][2].ToString() + "   " + _dtvehicles.Rows[i][3].ToString() + "   " + _dtvehicles.Rows[i][4].ToString() + "   ");
                    }

                }
                else { Console.WriteLine("No Vechile'S are OnRide Now"); }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

