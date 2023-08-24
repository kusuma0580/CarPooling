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
    internal class Sqlconnection
    {
        public string conString = "";
        public SqlConnection con;

        public Sqlconnection()
        {
            conString = "Data Source=DESKTOP-R980BA0\\SQLEXPRESS;Integrated Security=SSPI; Initial Catalog=MYTESTDB";
        }
        public void GetSqlConnect()
        {
            if (con == null || con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
            {
                con = new SqlConnection(conString);
                con.Open();
            }
        }
        public void CloseSqlConnect()
        {
            if (con != null && con.State == ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
            }
        }
        public DataTable SqlDatacollection(string Query)
        {
            GetSqlConnect();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public void SqlExecuteNonQuery(string Query)
        {
            GetSqlConnect();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();
            CloseSqlConnect();
        }
        public void SqlExecuteScalar(string Query)
        {
            GetSqlConnect();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteScalar();
            CloseSqlConnect();
        }
        /*static void CreateDB()
        {
            string connectionString = "Data Source=DESKTOP-R980BA0\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=;";
            string cmdText = "create database MYTESTDB";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            if (sqlConnection.State != ConnectionState.Open)
            {
                try
                {
                    sqlConnection.Open();
                    Console.WriteLine("connection is open");
                    SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("database created");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                    Console.WriteLine("connection closed");
                }
            }
        }*/
        public void CreateTable()
        {
            SqlConnection sqlConnection = new SqlConnection(conString);
            //string sqlStatement = "create table Vehicles(ID integer primary key identity,Vehicle_Number varchar(50), Vehicle_Type varchar(50), Vehicle_RC_Number int, Vehicle_Age int, Vehicle_Capacity int, Vehicle_current_status varchar(50))";
            //string sqlStatement = "create table Riders(ID integer primary key identity,RIDER_NAME varchar(50),RIDER_PHONENUMBER varchar(50),RIDER_AGE varchar(50),RIDER_GENDER varchar(50),RIDER_VEHICLE_TYPE varchar(50),RIDER_PASSENGERS varchar(50),RIDER_REQUEST_TIME DATETIME DEFAULT CURRENT_TIMESTAMP,rider_status varchar(50))";
            string sqlStatement = "create table Booking(ID integer primary key identity,BOOKING_ID int,BOOKING_RIDER_NAME varchar(50),BOOKING_RIDER_PHONENUMBER varchar(50),BOOKING_RIDER_VEHICLE_TYPE varchar(50),BOOKING_RIDER_PASSENGERS varchar(50), booking_status varchar(50),booking_cancel_reason varchar(50),BOOKING_CANCEL_TIME DATETIME DEFAULT CURRENT_TIMESTAMP)";
            

            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                Console.WriteLine("Connection is open");
                SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("table is created");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                Console.WriteLine("Connection closed");
            }
        }
    }
}

