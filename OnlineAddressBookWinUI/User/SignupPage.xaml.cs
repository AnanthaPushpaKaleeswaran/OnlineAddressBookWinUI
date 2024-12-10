using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.IO;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OnlineAddressBookWinUI.User
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class SignupPage : Page
    {
        protected string email = "";
        protected string password = "";
        protected int n;
        protected int publicKey;
        public SignupPage()
        {
            this.InitializeComponent();
        }
        public void goBack(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = ((App)Application.Current).RootFrame;
            Frame.Navigate(typeof(LoginPage), rootFrame);
        }
        public void Signup(object sender, RoutedEventArgs e)
        {
            email = emailInput.Text;
            password = passwordInput.Password;
            if(!ValidEmail())
            {
                alert.Text = "Please enter a vaid email";
                return;
            }
            alert.Text = "";    
            if(!ValidPass())
            {
                alert.Text = "Please enter the password between 8 and below 50 characters.\nThe password must contain a upper case letter, a lower case letter, a number and a symbol";
                return;
            }
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "onlineAddressBook.db");
            string ConnectionString = $"Data Source={dbPath}";
            SQLiteConnection MyConnection = new SQLiteConnection(ConnectionString);
            MyConnection.Open();
            if (MyConnection.State.Equals("close"))
            {
                Console.Error.WriteLine("Error in opening db");
                return;
            }
            if (!tableExist(MyConnection, "user"))
            {
                createTable(MyConnection, "user");
            }
            encryptPassword();
            string query = "SELECT COUNT(1) FROM user WHERE email=@email";
            SQLiteCommand command = new SQLiteCommand(query, MyConnection);
            command.Parameters.AddWithValue("@email", email);
            int userCount = Convert.ToInt32(command.ExecuteScalar());
            if (userCount > 0)
            {
                alert.Text = "User already exists.Login to continue";
                return;
            }

            if (!insertRecord(MyConnection))
            {
                return;
            }
            alert.Text = "User added successfully";
            MyConnection.Close();
        }

        private bool ValidEmail()
        {
            string regexstr = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,50}$";
            Regex re = new Regex(regexstr);
            return re.IsMatch(email);
        }
        private bool ValidPass()
        {
            string regexstr = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&-+=()]).{8,50}";
            Regex re = new Regex(regexstr);
            return re.IsMatch(password);
        }
        protected bool tableExist(SQLiteConnection connection, string tableName)
        {
            string query = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name=@tableName";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@tableName", tableName);
            var result = command.ExecuteScalar();
            return Convert.ToInt32(result) > 0;
        }
        protected void createTable(SQLiteConnection connection, string tableName)
        {
            string query = $@"CREATE TABLE IF NOT EXISTS [{tableName}] (email VARCHAR(50), password VARCHAR(30))";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
            Console.WriteLine("Table created successfully");
        }
        protected void encryptPassword()
        {
            SetKeys();

            List<int> encoded = new List<int>();

            foreach (char letter in password)
            {
                encoded.Add(Encrypt((int)letter));
            }
            string encPass = "";
            foreach (int letter in encoded)
            {
                encPass += letter.ToString();
                encPass += getRandomAlphabet();
            }
            password = encPass;
        }
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

        }
        public int Encrypt(int message)
        {
            int e = publicKey;
            int encrypted_text = 1;
            while (e > 0)
            {
                encrypted_text *= message;
                encrypted_text %= n;
                e -= 1;
            }
            return encrypted_text;
        }
        public int GCD(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }
            return GCD(b, a % b);
        }
        protected bool insertRecord(SQLiteConnection connection)
        {
            try
            {
                string query = "INSERT INTO user VALUES (@Email, @Password)";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in record inserting");
                return false;
            }
        }
        protected char getRandomAlphabet()
        {
            Random random = new Random();
            // random lowercase letter
            int a = random.Next(0, 26);
            char ch = (char)('a' + a);
            return ch;
        }
    }
}
