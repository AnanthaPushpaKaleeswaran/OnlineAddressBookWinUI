using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Data.SQLite;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OnlineAddressBookWinUI.User
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        protected string password = "";
        public string email = "";
        protected int n;
        protected int privateKey;
        protected int publicKey;

        public LoginPage()
        {
            this.InitializeComponent();
            emailInput.Text = "ananth@gmail.com";
            passwordInput.Password = "Ananth@123";
            version.Text += new Version().GetAppVersion();
        }

        //login function from xaml
        public void Login(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> user = new Dictionary<string, string>();
            email = emailInput.Text;
            password = passwordInput.Password;
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "onlineAddressBook.db");

            //check db exist or not
            if (!dbExist(dbPath))
            {
                return;
            }

            string ConnectionString = $"Data Source={dbPath}";
            SQLiteConnection MyConnection = new SQLiteConnection(ConnectionString);
            MyConnection.Open();

            if (MyConnection.State.Equals("close"))
            {
                return;
            }

            if (!tableExist(MyConnection, "user"))
            {
                return;
            }
            getAllUsers(MyConnection, user);

            //check user present
            if (user.ContainsKey(email))
            {
                string dicPassword = user[email];

                //check password match 
                if (dicPassword != password)
                {
                    passwordAlert.Text = "Please check your password";
                    return;
                }
            }
            else
            {
                passwordAlert.Text = "";
                alert.Text = "No user exists. Please signup to continue";
                return;
            }

            MyConnection.Close();
            Frame rootFrame = ((App)Application.Current).RootFrame;
            Frame.Navigate(typeof(Contact.Display), email);
        }

        //go to signup page
        public void GoToSignup(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = ((App)Application.Current).RootFrame;
            Frame.Navigate(typeof(SignupPage));
        }

        //db exist function
        protected bool dbExist(string dbName)
        {
            if (File.Exists(dbName))
            {
                return true;
            }
            return false;
        }

        protected bool tableExist(SQLiteConnection connection, string tableName)
        {
            string query = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name=@tableName";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@tableName", tableName);
            var result = command.ExecuteScalar();
            return Convert.ToInt32(result) > 0;
        }

        //get all the users from db
        protected void getAllUsers(SQLiteConnection MyConnection, Dictionary<string, string> user)
        {
            SetKeys();
            string query = "SELECT * FROM user";

            using (SQLiteCommand command = new SQLiteCommand(query, MyConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string getPass = reader["password"].ToString();
                            string decPass = "";
                            string decPassword = "";

                            foreach (char ch in getPass)
                            {
                                if (!Char.IsLetter(ch))
                                {
                                    decPass += ch;
                                }
                                else
                                {
                                    decPassword += Convert.ToChar(Decrypt(Convert.ToInt32(decPass)));
                                    decPass = "";
                                }
                            }
                            user.Add(reader["email"].ToString(), decPassword);
                        }
                    }
                }
            }
        }

        //set the keys
        public void SetKeys()
        {
            int prime1 = 67;
            int prime2 = 61;
            n = prime1 * prime2;
            int fi = (prime1 - 1) * (prime2 - 1);
            int e = 2;

            while (true)
            {
                if (GCD(e, fi) == 1)
                {
                    break;
                }
                e += 1;
            }

            publicKey = e;
            int d = 2;

            while (true)
            {
                if ((d * e) % fi == 1)
                {
                    break;
                }
                d += 1;
            }
            privateKey = d;
        }

        //gcd calculation
        public int GCD(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }
            return GCD(b, a % b);
        }

        //decrypt the text
        public int Decrypt(int encrypted_text)
        {
            int d = privateKey;
            int decrypted = 1;

            while (d > 0)
            {
                decrypted *= encrypted_text;
                decrypted %= n;
                d -= 1;
            }
            return decrypted;
        }

    }
}
