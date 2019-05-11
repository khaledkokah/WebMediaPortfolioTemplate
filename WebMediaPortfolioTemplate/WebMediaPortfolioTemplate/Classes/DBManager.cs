using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using WebMediaPortfolioTemplate.Classes;
using WebMediaPortfolioTemplate.UserControls;
using WebMediaPortfolioTemplate.Models;

/// <summary>
/// DBManager class contains all the methods for interaction with database using ADO.Net
/// </summary>

namespace WebMediaPortfolioTemplate.Classes
{
    public static class DBManager
    {
        //Method to get current user id
        public static string getCurrentUserId()
        {
            try
            {
                MembershipUser m;
                m = Membership.GetUser();
                if (m != null)
                {
                    return m.ProviderUserKey.ToString();
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //Method to get current user type
        public static int getUserType(string userID)
        {
            try
            {
                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
                SqlCommand Comm = new SqlCommand("", Conn);
                int userType;
                Comm.CommandText = "Select UserType from aspnet_Users where UserId = '" + userID + "'";
                Conn.Open();
                if (nullCorrection(Comm.ExecuteScalar()).ToString() != "0")
                {
                    userType = (int)Comm.ExecuteScalar();
                    return userType;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //Method to get current user name
        public static string getUserName(string userId)
        {
            try
            {
                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
                SqlCommand Comm = new SqlCommand("", Conn);
                string userName;
                // Comm.CommandText = "Select UserName from aspnet_Users  where UserId = '" + userId + "'";
                Comm.CommandText = "Select Username from [User] WHERE Id=@Id";
                Comm.Parameters.AddWithValue("@Id", userId);
                Conn.Open();
                if (nullCorrection(Comm.ExecuteScalar()).ToString() != "0")
                {
                    userName = Comm.ExecuteScalar().ToString();
                    return userName;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //Method to get  user id by name
        public static string getUserId(string userName)
        {
            try
            {
                MembershipUser m;
                m = Membership.GetUser(userName);
                if (m != null)
                {
                    return m.ProviderUserKey.ToString();
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //Method to get membership user by name
        public static MembershipUser GetUser(string userName)
        {

            MembershipUser m;
            m = Membership.GetUser(userName);

            return m;

        }

        //Custom method to check isDouble
        public static double isDouble(object param)
        {
            double val = 0;
            Double.TryParse(param.ToString(), out val);
            return val;
        }

        //Custom method to check isInt
        public static int isInt(object param)
        {
            int val = 0;
            Int32.TryParse(param.ToString(), out val);
            return val;
        }


        //Custom method to currect null entries
        public static Object nullCorrection(Object targetValue)
        {
            if (targetValue.Equals(DBNull.Value))
            {
                return 0;
            }

            else
            {
                return targetValue;
            }
        }

        //Method to fill datatable
        public static DataTable FillDataTable(string tableName, string condition = "")
        {
            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter Da;
            DataTable Dt;

            Comm.CommandText = "Select * from " + tableName;
            if (condition != "")
            {
                Comm.CommandText = Comm.CommandText + " " + condition;
            }
            Da = new SqlDataAdapter();
            Da.SelectCommand = Comm;

            Dt = new DataTable();
            Da.Fill(Dt);
            return Dt;
        }

        //Method to get field by ID WHERE ID=VAL FROM TABLENAME
        public static string getField(string IDFieldName, string IDFieldVal, string tableName, string fieldName)
        {
            try
            {
                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
                SqlCommand Comm = new SqlCommand("", Conn);

                DataTable Dt = new DataTable();
                SqlDataAdapter Da = new SqlDataAdapter();
                Comm.CommandText = "Select * from " + tableName + " Where " + IDFieldName + "=@IDFieldVal";
                Comm.Parameters.AddWithValue("@IDFieldVal", IDFieldVal);

                Da.SelectCommand = Comm;
                Dt.Clear();
                Da.Fill(Dt);

                return Dt.Rows[0][fieldName].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //Method to get user email by providing user id
        public static string getUserEmail(string userID)
        {
            try
            {
                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
                SqlCommand Comm = new SqlCommand("", Conn);
                DataTable Dt = new DataTable();
                SqlDataAdapter Da = new SqlDataAdapter();
                string email = "";
                Comm.CommandText = "Select UserEmail from Users WHERE UserID=@UserID";
                Comm.Parameters.AddWithValue("UserID", userID);
                Da.SelectCommand = Comm;
                Da.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    email = Dt.Rows[0]["UserEmail"].ToString();
                    return email;
                }

                return email;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //Method to convert null to string
        public static string NullToString(object Value)
        {
            return Value == null ? "" : Value.ToString();
        }

        //Simple execute sql statement
        public static DataTable ExecuteStatement(string sqlStatement)
        {
            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter Da;
            DataTable Dt;

            Comm = new SqlCommand("", Conn);
            Comm.CommandText = sqlStatement;

            Da = new SqlDataAdapter();
            Da.SelectCommand = Comm;

            Dt = new DataTable();
            Da.Fill(Dt);

            return Dt;
        }

        //Method to get value with WHERE clause
        public static string getVal(string table, string idField, string valField, string val)
        {
            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            Comm.CommandText = "Select * from " + table + " WHERE " + valField + "=Lower(@valField)";
            Comm.Parameters.AddWithValue("valField", val);
            da.SelectCommand = Comm;
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return dt.Rows[0][idField].ToString();

            return "";
        }

        //Custom method to convert date to short or long format
        public static string ReadDate(string dateVal, string format = "")
        {
            DateTime date;
            bool isDate = DateTime.TryParse(dateVal, out date);
            if (isDate)
            {
                if (format != "")
                    return date.ToString(format);

                else
                    return date.ToLongDateString();
            }

            return "";
        }

        //Method to validate and save date to sql
        public static DateTime? SaveDate(string dateVal, string format = "")
        {
            DateTime date;
            bool isDate = DateTime.TryParse(dateVal, out date);
            if (isDate)
                return date;

            return null;
        }

        //Custom method to parse and save date correctly in sql
        public static void HandleDateField_Save(SqlCommand Comm, string fieldName, string dVal)
        {
            DateTime date;

            bool isDate = DateTime.TryParse(dVal, out date);
            if (isDate)
            {
                Comm.Parameters.AddWithValue(fieldName, date.ToString("yyyy/MM/dd"));
            }
            else
            {
                Comm.Parameters.AddWithValue(fieldName, DBNull.Value);
            }
        }

        //Custom method to save any checkbox values in sql
        public static void HandleCheckBoxField_Save(SqlCommand Comm, CheckBox chkBox, string fieldName)
        {
            if (chkBox.Checked == true)
            {
                Comm.Parameters.AddWithValue(fieldName, 1);
            }
            else
            {
                Comm.Parameters.AddWithValue(fieldName, 0);
            }
        }

        //Get checkbox value and return string
        public static string GetCheckBoxVal(CheckBox chkBox)
        {
            if (chkBox.Checked == true)
                return "1";
            else
                return "0";
        }

        //Custom method to replace any value to DBNull (useful for values with null values)
        public static object ReplaceToDBNull(object oldVal)
        {
            if (DBNull.Value.Equals(oldVal) || oldVal == null || oldVal.ToString() == "")
                return DBNull.Value;
            else
                return oldVal;
        }

        //Custom function to replace null values with other values
        public static string ReplaceNull(object oldVal, object newVal, bool db = false, bool isDate = false)
        {
            if (db)//if the value is for database 
            {
                if (DBNull.Value.Equals(oldVal))
                    return newVal.ToString();
                else
                    return oldVal.ToString();
            }
            else
            {
                if (oldVal == null || oldVal.Equals(""))
                    return newVal.ToString();
                else
                    return oldVal.ToString();
            }
        }

        //Get checkbox value and return boolean
        public static bool ReadCheckBox(object dbVal)
        {
            if (dbVal == null || dbVal.Equals(""))
                return false;
            else
                return Convert.ToBoolean(Convert.ToInt32(dbVal));
        }

        //Get coding description
        public static string GetCodingDesc(string id)
        {
            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
            SqlCommand Comm = new SqlCommand("", Conn);

            DataTable dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter();

            Comm.CommandText = "Select CODING_DESCRIPTION from COM_CODING WHERE CODING_ID=@Id";
            Comm.Parameters.AddWithValue("Id", id);
            Da.SelectCommand = Comm;
            Da.Fill(dt);

            if (dt.Rows.Count > 0)
                return dt.Rows[0]["CODING_DESCRIPTION"].ToString();
            else
                return "";

        }

        //Get coding type description
        public static string GetCodingTypeDesc(string id)
        {
            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
            SqlCommand Comm = new SqlCommand("", Conn);

            DataTable dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter();

            Comm.CommandText = "Select DESCRIPTION from COM_CODINGTYPE WHERE CODINGTYPE_ID=@Id";
            Comm.Parameters.AddWithValue("Id", id);
            Da.SelectCommand = Comm;
            Da.Fill(dt);

            if (dt.Rows.Count > 0)
                return dt.Rows[0]["DESCRIPTION"].ToString();
            else
                return "";

        }

        //Convert any list to datatable
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        //Get a user account from database
        public static UserAccount GetUserAccount(string userId)
        {
            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
            SqlCommand Comm = new SqlCommand("", Conn);

            DataTable dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter();

            Comm.CommandText = "Select * from [User] WHERE [Id]=@Id";
            Comm.Parameters.AddWithValue("Id", userId);
            Da.SelectCommand = Comm;
            Da.Fill(dt);

            UserAccount acc = null;
            if (dt.Rows.Count > 0)
            {
                acc = new UserAccount
                {
                    Id = dt.Rows[0]["Id"].ToString(),
                    Name = dt.Rows[0]["UserName"].ToString(),
                    UserName = dt.Rows[0]["UserName"].ToString(),
                    Email = dt.Rows[0]["Email"].ToString(),
                    //CompanyId = compId,
                    //DepartmentId = deptId,
                    //JobId = jobId,
                    //PositionId = positionId,
                    //Level = jobLevel,
                    //isOnBehalf = isOnBehalf,
                    //JobType = jobType,
                    //IsCritical = Convert.ToBoolean(jobCritical),
                    //IsImportant = Convert.ToBoolean(jobImportant)
                };
                return acc;
            }

            return null;
        }

        //Method to add the fixed stamp fields in modal
        public static void AddStampFields(SqlCommand Comm, UserAccount user, ModalStamp ms, bool UpdateMode)
        {
            //== FIXED FIELDS ==
            DBManager.HandleDateField_Save(Comm, "VALID_FROM", ms.TxtValidFrom.Value);
            DBManager.HandleDateField_Save(Comm, "VALID_TO", ms.TxtValidTo.Value);
            if (!UpdateMode)
            {
                Comm.Parameters.AddWithValue("@CREATE_BY", user.Id);
                Comm.Parameters.AddWithValue("@CREATE_DATE", DateTime.Now);
            }

            Comm.Parameters.AddWithValue("@UPDATE_BY", user.Id);
            Comm.Parameters.AddWithValue("@UPDATE_DATE", DateTime.Now);
        }


        //Method that returns datatable object. Command object parameters will be cleared when done.
        public static DataTable FillDataTable(SqlCommand Comm)
        {
            SqlDataAdapter Da;
            DataTable Dt;

            Da = new SqlDataAdapter();
            Da.SelectCommand = Comm;

            Dt = new DataTable();
            Da.Fill(Dt);

            Comm.Parameters.Clear();
            return Dt;
        }

        //Method for sorting any datatable
        public static DataTable SortDataTable(DataTable dt, string sortQuery)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = sortQuery;
            dt = dv.ToTable();
            return dt;
        }

        //Custom user validation for this project using a simple stored procedure
        //Note: basic encryption is applied for security
        public static string ValidateUser(string username, string password)
        {
            try
            {
                string userId = "";
                using (SqlConnection con = new SqlConnection(AppConstants.connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_VALIDATE_USER"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Connection = con;
                        con.Open();
                        userId = cmd.ExecuteScalar().ToString();
                        con.Close();

                        if (userId != "-1" && userId != "-2")
                        {
                            if (Encryption.ValidatePassword(username, password) != "1")
                                userId = "-1";//invalid password
                        }
                    }
                }

                return userId;

            }
            catch(Exception ex)
            {
                return "";
            }
        }
    }
}