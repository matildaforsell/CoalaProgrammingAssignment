using System.Data;
using System.Data.SqlClient;

namespace EventAPI
{
    public class DataAccessLayer
    {
        private static string dBConnection = "Server=localhost; Database=EventDB; User Id = BossesBolagAB; Password=Coala";

        public List<string[]> Get()
        {
            SqlConnection sqlConnecton = new SqlConnection(dBConnection);

            SqlCommand sqlCommand = new SqlCommand("SELECT Event.name, sku, price, ShipTo.name, ShipTo.address, ShipTo.city, ShipTo.state, ShipTo.zip, BillTo.name, BillTo.address, BillTo.city, BillTo.state, BillTo.zip FROM Event INNER JOIN ShipTo ON Event.ssnShipTo = ShipTo.ssn INNER JOIN BillTo ON Event.ssnBillTo = BillTo.ssn");

            sqlCommand.Connection = sqlConnecton;

            try
            {
                sqlConnecton.Open();
                using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                {

                    List<string[]> listOfObjects = new List<string[]>();

                        while (dataReader.Read())
                        {
                            string[] stringArray = new string[dataReader.FieldCount];

                            for (int i = 0; i < dataReader.FieldCount; i++)
                            {

                                stringArray[i] = dataReader.GetName(i) + " : " + dataReader.GetValue(i).ToString();
                                
                            }

                            listOfObjects.Add(stringArray);
                        }

                        return listOfObjects;             
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string Delete(string eventID)
        {
            SqlConnection sqlConnecton = new SqlConnection(dBConnection);

            SqlCommand sqlCommand = new SqlCommand("DELETE Event WHERE eventID LIKE '" + eventID + "'");

            sqlCommand.Connection = sqlConnecton;

            try
            {
                sqlConnecton.Open();
                using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                {

                    List<string[]> listOfObjects = new List<string[]>();

                    return "Event with eventID " + eventID + " is removed.";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Create(Object jsonFormat)
        {
            SqlConnection sqlConnecton = new SqlConnection(dBConnection);

            string jsonString = jsonFormat.ToString();

            string[] stringArray = jsonString.Split('"', ':', '}', '{', ',');

            Random rnd = new Random();

            SqlCommand sqlCommand = new SqlCommand("INSERT INTO ShipTo VALUES (@ssnShipTo, @nameShipTo, @addressShipTo, @cityShipTo, @stateShipTo, @zipShipTo) INSERT INTO BillTo VALUES (@ssnBillTo, @nameBillTo, @addressBillTo, @cityBillTo, @stateBillTo, @zipBillTo) INSERT INTO Event VALUES (@eventID, @name, @sku, @price, @ssnShipTo, @ssnBillTo)");

            sqlCommand.Parameters.AddWithValue("@eventID", rnd.Next());
            sqlCommand.Parameters.AddWithValue("@name", stringArray[5]);
            sqlCommand.Parameters.AddWithValue("@sku", stringArray[11]);
            sqlCommand.Parameters.AddWithValue("@price", double.Parse(stringArray[16].Trim(), System.Globalization.CultureInfo.InvariantCulture));
            sqlCommand.Parameters.AddWithValue("@ssnShipTo", rnd.Next());
            sqlCommand.Parameters.AddWithValue("@nameShipTo", stringArray[25]);
            sqlCommand.Parameters.AddWithValue("@addressShipTo", stringArray[31]);
            sqlCommand.Parameters.AddWithValue("@cityShipTo", stringArray[37]);
            sqlCommand.Parameters.AddWithValue("@stateShipTo", stringArray[43]);
            sqlCommand.Parameters.AddWithValue("@zipShipTo", stringArray[49]);
            sqlCommand.Parameters.AddWithValue("@ssnBillTo", rnd.Next());
            sqlCommand.Parameters.AddWithValue("@nameBillTo", stringArray[60]);
            sqlCommand.Parameters.AddWithValue("@addressBillTo", stringArray[66]);
            sqlCommand.Parameters.AddWithValue("@cityBillTo", stringArray[72]);
            sqlCommand.Parameters.AddWithValue("@stateBillTo", stringArray[78]);
            sqlCommand.Parameters.AddWithValue("@zipBillTo", stringArray[84]);

            sqlCommand.Connection = sqlConnecton;

            try
            {
                sqlConnecton.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

