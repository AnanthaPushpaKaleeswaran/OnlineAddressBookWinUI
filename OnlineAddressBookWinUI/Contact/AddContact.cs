using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OnlineAddressBookWinUI.Contact
{
    internal class AddContact
    {
        public void AddContactToDB(SQLiteConnection MyConnection,string name,string phoneNo,string address,string groupStr,string email)
        {
            string query = "INSERT INTO contact VALUES (@name,@phoneNo,@address,@contactGroup,@email)";

            using (SQLiteCommand command = new SQLiteCommand(query, MyConnection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@phoneNo", phoneNo);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@contactGroup", groupStr);
                command.Parameters.AddWithValue("@email", email);
                int rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
}
