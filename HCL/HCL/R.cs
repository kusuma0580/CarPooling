using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace HCL
{
    internal class R
    {
        string _ridername = "";
        string _riderphonenumber = "";
        string _riderage = "";
        string _ridergender = "";

        string _riderbookvehtype = "";
        string _riderbooknumpass = "";
        Int32 _riderbookingid = 0;
        string _riderbookingcancel = "";

        string _vehiclenumber = "";
        string _vehiclestatus = "";
        string _query = "";
        Sqlconnection _objConnect = new Sqlconnection();
        Random dtra = new Random();
        public void DisplayAvailableVehicles()
        {
            try
            {
                _query = "select Vehicle_Number,Vehicle_Type,Vehicle_RC_Number,Vehicle_Age,Vehicle_Capacity " +
                    "from Vehicles where Vehicle_current_status='AVAILABLE' ";
                DataTable _dtvehicles = new DataTable();
                //Connect DB and Execute SQl statement
                Sqlconnection _objconnect = new Sqlconnection();
                _dtvehicles = _objconnect.SqlDatacollection(_query);
                Console.WriteLine("BELOW VEHICLE'S ARE AVAILABLE TO RIDE NOW");
                Console.WriteLine("------------------------------");
                Console.WriteLine("                              ");

                if (_dtvehicles.Rows.Count > 0)
                {
                    Console.WriteLine("Vehicle_Number   Vehicle_Type    Vehicle_RC_Number   Vehicle_Age Vehicle_Capacity");
                    for (int i = 0; i < _dtvehicles.Rows.Count; i++)
                    {
                        Console.WriteLine(_dtvehicles.Rows[i][0].ToString() + "   " + _dtvehicles.Rows[i][1].ToString() + "   " + _dtvehicles.Rows[i][2].ToString() + "   " + _dtvehicles.Rows[i][3].ToString() + "   " + _dtvehicles.Rows[i][4].ToString() + "   ");
                    }
                    Console.WriteLine("                              ");
                    Console.WriteLine("Book Your Ride now");
                }
                else { Console.WriteLine("No Vechile Available to Ride Now"); }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void AddNewRiderDetails()
        {
            try
            {
                Console.WriteLine("ADDING NEW RIDER DETAILS");
                Console.WriteLine("------------------------------");
                Console.WriteLine("                              ");
                Console.WriteLine("Enter Rider Name:");
                _ridername = Console.ReadLine();
                Console.WriteLine("Enter Phone Number:");
                _riderphonenumber = Console.ReadLine();
                Console.WriteLine("Enter Rider Age");
                _riderage = Console.ReadLine();
                Console.WriteLine("Enter Rider Gender");
                _ridergender = Console.ReadLine();

                Regex name = new Regex(@"^[a-zA-Z]+$");
                int age = Int32.Parse(_riderage);
                if (name.IsMatch(_ridername) != true)
                {
                    Console.WriteLine("enter valid name");
                }
                else if (age < 0 || age > 150)
                {
                    Console.WriteLine("enter valid age");
                }
                else {
                    _query = "insert into Riders (RIDER_NAME,RIDER_PHONENUMBER,RIDER_AGE,RIDER_GENDER) " +
                        "values ('" + _ridername + "','" + _riderphonenumber + "','" + _riderage + "','" + _ridergender + "')";
                    DataTable _dtvehicles = new DataTable();
                    _objConnect.SqlExecuteScalar(_query);
                    Console.WriteLine("Rider Details Added Successfully");
                    Console.WriteLine("                              ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void BookNewRide()
        {
            try
            {
                Console.WriteLine("BOOKING NEW RIDE DETAILS");
                Console.WriteLine("------------------------------");
                Console.WriteLine("                              ");
                Console.WriteLine("Enter Rider Name:");
                _ridername = Console.ReadLine();
                //Validate Rider Name
                DataTable _dtridchk = ValidateRider();
                if (_dtridchk.Rows.Count > 0)
                {
                    Console.WriteLine("Enter Vehicle Type:");
                    _riderbookvehtype = Console.ReadLine();
                    Console.WriteLine("Enter Number of Passengers:");
                    _riderbooknumpass = Console.ReadLine();

                    _riderbookingid = dtra.Next();

                    _query = "insert into Booking (BOOKING_ID,BOOKING_RIDER_NAME,BOOKING_RIDER_PHONENUMBER,BOOKING_RIDER_VEHICLE_TYPE,BOOKING_RIDER_PASSENGERS) " +
                        " VALUES (" + _riderbookingid + ",'" + _dtridchk.Rows[0][0].ToString() + "','" + _dtridchk.Rows[0][1].ToString() + "','" + _riderbookvehtype + "','" + _riderbooknumpass + "')";
                    _objConnect.SqlExecuteNonQuery(_query);

                    Console.WriteLine("Rider Booking Successfull your Booking ID:" + _riderbookingid);

                }
                else
                {
                    Console.WriteLine("Rider Details Not Exist, Please Add has new");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private DataTable ValidateRider()
        {
            DataTable _dtriderchk = new DataTable();
            try
            {
                _query = "select RIDER_NAME,RIDER_PHONENUMBER,RIDER_AGE,RIDER_GENDER from Riders " +
                    "where RIDER_NAME='" + _ridername + "' ";
                _dtriderchk = _objConnect.SqlDatacollection(_query);
            }
            catch (Exception ex)
            {
            }
            return _dtriderchk;
        }

        public void Canclebooking()
        {
            try
            {
                Console.WriteLine("CANCEL BOOKING ");
                Console.WriteLine("------------------------------");
                Console.WriteLine("                              ");
                Console.WriteLine("Enter Booking ID:");
                _riderbookingid = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Enter Cancellation Reason:");
                _riderbookingcancel = Console.ReadLine();
                _query = "update Booking  set booking_status='CANCELED',booking_cancel_reason='" + _riderbookingcancel + "',BOOKING_CANCEL_TIME=sysdate where booking_id=" + _riderbookingid + "";
                _objConnect.SqlExecuteNonQuery(_query);

                Console.WriteLine("Booking Cancel Successfull");

            }
            catch (Exception ex)
            {
            }
        }
    }

}
