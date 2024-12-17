using System.Data.SQLite;

namespace OnlineAddressBookWinUI.Contact
{
    public class EditContact
    {
        public void editContactDB(SQLiteConnection connection, string name, string phoneNo, string address, string contactGroup, string email, string oldPhoneNo)
        {
            string query = "UPDATE contact SET name=@name,phoneNo=@phoneNo,address=@address,contactGroup=@contactGroup WHERE phoneNo=@oldPhoneNo and email=@email";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@phoneNo", phoneNo);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@contactGroup", contactGroup);
                command.Parameters.AddWithValue("@oldPhoneNo", oldPhoneNo);
                command.Parameters.AddWithValue("@email", email);
                var rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
}
