using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OnlineAddressBookWinUI.Contact
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Display : Page
    {
        public string email = "";
        public ObservableCollection<Contact> Contacts { get; set; }
        public ObservableCollection<string> GroupNames { get; set; }

        //constructor to initialize the component
        public Display()
        {
            this.InitializeComponent();
        }
        public void AddNewContact(object sender, RoutedEventArgs e)
        {
            Send send = new Send();
            send.Email = email;
            send.Type = "add";
            send.Contact = null;
            Frame rootFrame = ((App)Application.Current).RootFrame;
            Frame.Navigate(typeof(AddorEditContact), send);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is string message)
            {
                // Use the string
                email = message; 
            }
            LoadContacts();
            LoadGroupsFromDB();
        }

        //load the contacts
        private void LoadContacts()
        {
            Contacts = LoadContactsFromDB();
            contactList.ItemsSource = Contacts;
        }

        //logout function
        public void Logout(object sender,RoutedEventArgs e)
        {
            Frame rootFrame = ((App)Application.Current).RootFrame;
            Frame.Navigate(typeof(User.LoginPage));
        }

        //get the contacts from db
        private ObservableCollection<Contact> LoadContactsFromDB()
        {
            ObservableCollection<Contact> contacts = new ObservableCollection<Contact> ();
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "onlineAddressBook.db");
            string connectionString = $"Data Source={dbPath}";
            
            using (SQLiteConnection connection=new SQLiteConnection(connectionString))
            {
                connection.Open();

                if (connection.State.Equals("Close"))
                {
                    Console.Error.WriteLine("Error in opening db");
                    return contacts;
                }

                if (!TableExist(connection, "contact"))
                {
                    return contacts;
                }
                string query = "SELECT name,phoneNo,address,contactGroup FROM contact WHERE email=@email";
                
                using(SQLiteCommand command=new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                
                    using(SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {

                                //add the contacts
                                contacts.Add(new Contact
                                { 
                                    Name= reader["name"].ToString(),
                                    PhoneNo = reader["phoneNo"].ToString(),
                                    Address = reader["address"].ToString(),
                                    ContactGroup = reader["contactGroup"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            return contacts;
        }

        protected bool TableExist(SQLiteConnection connection, string tableName)
        {
            string query = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name=@tableName";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@tableName", tableName);
            var result = command.ExecuteScalar();
            return Convert.ToInt32(result) > 0;
        }

        //view by group function from xaml
        public void ViewGroup(Object sender, RoutedEventArgs e)
        {
            if(viewByGroup.SelectedItem is string selectedGroup)
            {
                if(selectedGroup=="No Group")
                {
                    selectedGroup = "";
                }
                LoadViewContacts(selectedGroup);
            }
        }

        //load the contacts from view by group
        private void LoadViewContacts(string selectedGroup)
        {
            Contacts = LoadContactsFromDB();
            ObservableCollection<Contact> groupContacts= new ObservableCollection<Contact>();
            
            foreach (Contact contact in Contacts)
            {
                string groupName = contact.ContactGroup;
                string tempGroup = "";
            
                foreach(char ch in groupName)
                {
                    if (ch == ',')
                    {
                        if (tempGroup == selectedGroup)
                        {
                            groupContacts.Add(contact);
                            tempGroup = "";
                            break;
                        }
                        tempGroup = "";
                    }
                    else
                    {
                        tempGroup += ch;
                    }
                }
                
                if (tempGroup == selectedGroup && !string.IsNullOrWhiteSpace(tempGroup))
                {
                    groupContacts.Add(contact);
                }
            }
            
            //if the option is show all then we want to show all the contacts
            if (selectedGroup != "Show All")
            {
                contactList.ItemsSource = groupContacts;
            }
            else
            {
                contactList.ItemsSource = Contacts;
            }
        }

        //get groups from DB
        protected void LoadGroupsFromDB()
        {
            ObservableCollection<string> groups = new ObservableCollection<string>();
            groups.Add("Show All");
            HashSet<string> groupSet = new HashSet<string>();
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "onlineAddressBook.db");
            string connectionString = $"Data Source={dbPath}";
            
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
            
                if (connection.State.Equals("Close"))
                {
                    Console.Error.WriteLine("Error in opening db");
                    return;
                }

                if (!TableExist(connection, "contact"))
                {
                    return;
                }
                string query = "SELECT contactGroup FROM contact WHERE email=@email";
                
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string indGroup = reader["contactGroup"].ToString();
                                string tempGroup = "";
                    
                                foreach (char ch in indGroup)
                                {
                                    if (ch == ',')
                                    {
                                        groupSet.Add(tempGroup);
                                        tempGroup = "";
                                    }
                                    else
                                    {
                                        tempGroup += ch;
                                    }
                                }
                                
                                if (!string.IsNullOrWhiteSpace(tempGroup))
                                {
                                    groupSet.Add(tempGroup);
                                }
                            }
                        }
                    }
                }
            }

            foreach (string group in groupSet)
            {
                groups.Add(group);
            }
            
            groups.Add("No Group");
            viewByGroup.ItemsSource = groups;
        }

        public void EditContactClick(object sender,RoutedEventArgs args)
        {
            var button=sender as Button;
            Contact contact = button?.Tag as Contact;
            Send send = new Send();
            send.Email = email;
            send.Type = "edit";
            send.Contact = contact;
            Frame rootFrame = ((App)Application.Current).RootFrame;
            Frame.Navigate(typeof(AddorEditContact), send);
        }
    }

    //contact class
    public class Contact
    {
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string ContactGroup { get; set; }
    }

    public class Send
    {
        public string Email { get; set; }
        public string Type { get; set; }
        public Contact Contact { get; set; }
    }
}
