using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;

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
            version.Text += new Version().GetAppVersion();
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


        //get the contacts from db
        private ObservableCollection<Contact> LoadContactsFromDB()
        {
            ObservableCollection<Contact> contacts = new ObservableCollection<Contact>();
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "onlineAddressBook.db");
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
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

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                //add the contacts
                                contacts.Add(new Contact
                                {
                                    Name = reader["name"].ToString(),
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
            if (viewByGroup.SelectedItem is string selectedGroup)
            {
                if (selectedGroup == "No Group")
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
            ObservableCollection<Contact> groupContacts = new ObservableCollection<Contact>();

            foreach (Contact contact in Contacts)
            {
                string groupName = contact.ContactGroup;
                string tempGroup = "";

                if (selectedGroup == "" && groupName=="")
                {
                    groupContacts.Add(contact);
                    continue;
                }

                foreach (char ch in groupName)
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

        public void EditContactClick(object sender, RoutedEventArgs args)
        {
            var button = sender as Button;
            Contact contact = button?.Tag as Contact;
            Send send = new Send();
            send.Email = email;
            send.Type = "edit";
            send.Contact = contact;
            Frame rootFrame = ((App)Application.Current).RootFrame;
            Frame.Navigate(typeof(AddorEditContact), send);
        }

        public void DeleteContactClick(object sender, RoutedEventArgs args)
        {
            var button = sender as Button;
            Contact contact = button?.Tag as Contact;
            DeleteModal(contact);
            contactList.ItemsSource = null;
            contactList.ItemsSource = Contacts;
        }

        private void DeleteModal(Contact contact)
        {
            ContentDialog deleteDialog = new ContentDialog
            {
                Title = "Delete contact",
                Background = (Brush)Application.Current.Resources["ContentDialogBackground"],
                Foreground = (Brush)Application.Current.Resources["ContentDialogForeground"],
            };

            StackPanel first = new StackPanel();

            TextBlock contentText = new TextBlock
            {
                Text = "Are you sure you want to delete this contact?",
                Foreground = (Brush)Application.Current.Resources["ContentDialogForeground"],
                Margin = new Thickness(0, 10, 0, 10)
            };

            StackPanel second = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 20, 0, 20)
            };

            Button primaryButton = new Button
            {
                Content = "Yes",
                Background = (Brush)Application.Current.Resources["SecondaryButtonBackground"],
                Foreground = (Brush)Application.Current.Resources["SecondaryButtonForeground"],
                BorderBrush = (Brush)Application.Current.Resources["SecondaryButtonBackground"],
                BorderThickness = (Thickness)Application.Current.Resources["SecondaryButtonThickness"],
                Padding = new Thickness(45, 5, 45, 7),
                Margin = new Thickness(5, 10, 15, 10)
            };

            primaryButton.Resources["ButtonBackgroundPointerOver"] = (Brush)Application.Current.Resources["SecondaryButtonBackgroundPointerOver"];
            primaryButton.Resources["ButtonForegroundPointerOver"] = (Brush)Application.Current.Resources["SecondaryButtonForeground"];
            primaryButton.Resources["ButtonBackgroundPressed"] = (Brush)Application.Current.Resources["SecondaryButtonBackground"];
            primaryButton.Resources["ButtonForegroundPressed"] = (Brush)Application.Current.Resources["SecondaryButtonForeground"];
            primaryButton.Resources["ButtonBackgroundReleased"] = (Brush)Application.Current.Resources["SecondaryButtonBackground"];
            primaryButton.Resources["ButtonForegroundReleased"] = (Brush)Application.Current.Resources["SecondaryButtonForeground"];
            primaryButton.CornerRadius = (CornerRadius)Application.Current.Resources["DialogCornerRadius"];

            Button secondaryButton = new Button
            {
                Content = "No",
                Background = (Brush)Application.Current.Resources["PrimaryButtonBackground"],
                Foreground = (Brush)Application.Current.Resources["PrimaryButtonForeground"],
                BorderBrush = (Brush)Application.Current.Resources["PrimaryButtonBackground"],
                BorderThickness = (Thickness)Application.Current.Resources["PrimaryButtonThickness"],
                Padding = new Thickness(45, 5, 45, 7),
                Margin = new Thickness(15, 10, 5, 10)
            };

            secondaryButton.Resources["ButtonBackgroundPointerOver"] = (Brush)Application.Current.Resources["PrimaryButtonBackgroundPointerOver"];
            secondaryButton.Resources["ButtonForegroundPointerOver"] = (Brush)Application.Current.Resources["PrimaryButtonForeground"];
            secondaryButton.Resources["ButtonBackgroundPressed"] = (Brush)Application.Current.Resources["PrimaryButtonBackground"];
            secondaryButton.Resources["ButtonForegroundPressed"] = (Brush)Application.Current.Resources["PrimaryButtonForeground"];
            secondaryButton.Resources["ButtonBackgroundReleased"] = (Brush)Application.Current.Resources["PrimaryButtonBackground"];
            secondaryButton.Resources["ButtonForegroundReleased"] = (Brush)Application.Current.Resources["PrimaryButtonForeground"];
            secondaryButton.CornerRadius = (CornerRadius)Application.Current.Resources["DialogCornerRadius"];

            second.Children.Add(primaryButton);
            second.Children.Add(secondaryButton);
            first.Children.Add(contentText);
            first.Children.Add(second);
            deleteDialog.Content = first;
            deleteDialog.XamlRoot = this.XamlRoot;

            primaryButton.Click += (sender, args) =>
            {
                Contacts.Remove(contact);
                deleteDialog.Hide();
                DeleteContact(contact);
            };

            secondaryButton.Click += (sender, args) =>
            {
                deleteDialog.Hide();
            };
            deleteDialog.ShowAsync();
        }

        private void DeleteContact(Contact contact)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "onlineAddressBook.db");
            string connectionString = $"Data Source={dbPath}";

            using (SQLiteConnection myConnection = new SQLiteConnection(connectionString))
            {
                myConnection.Open();

                if (myConnection.State == ConnectionState.Closed)
                {
                    Console.Error.WriteLine("Error in opening db");
                    return;
                }

                if (!TableExist(myConnection, "contact"))
                {
                    return;
                }

                string query = "DELETE FROM contact WHERE email=@email AND phoneNo=@phoneNo";
                using (SQLiteCommand command = new SQLiteCommand(query, myConnection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@phoneNo", contact.PhoneNo);
                    var rowsAffected = command.ExecuteNonQuery();
                    showDeleteDialog();
                }
            }
        }

        private async void showDeleteDialog()
        {
            // Create a ContentDialog
            ContentDialog deleteDialog = new ContentDialog
            {
                Title = "Delete",
                Background = (Brush)Application.Current.Resources["ContentDialogBackground"],
                Foreground = (Brush)Application.Current.Resources["ContentDialogForeground"],
                
            };

            // Add content to the dialog
            TextBlock textBlock = new TextBlock
            {
                Text = "Contact deleted successfully",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 16,
            };

            deleteDialog.Content = textBlock;
            deleteDialog.XamlRoot = this.XamlRoot;
            // Show the dialog
            _ = deleteDialog.ShowAsync();

            // Wait for 1 second
            await Task.Delay(1000);

            // Close the dialog
            deleteDialog.Hide();
        }

        public void SearchContacts(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Contact> searchContacts = new ObservableCollection<Contact>();
            string searchText = searchInput.Text;
            foreach (Contact contact in Contacts)
            {
                string mainString = contact.Name.ToLower();
                string sub = searchText.ToLower();
                if (mainString.Contains(sub))
                {
                    searchContacts.Add(contact);
                }
            }
            if (string.IsNullOrWhiteSpace(searchText))
            {
                contactList.ItemsSource = Contacts;
                return;
            }
            contactList.ItemsSource = searchContacts;
        }

        public void LogoutModal(object sender, RoutedEventArgs args)
        {

            ContentDialog logoutDialog = new ContentDialog
            {
                Title = "Logout",
                Background = (Brush)Application.Current.Resources["ContentDialogBackground"],
                Foreground= (Brush)Application.Current.Resources["ContentDialogForeground"],
            };

            StackPanel first = new StackPanel();

            TextBlock contentText = new TextBlock
            {
                Text = "Are you sure you want to logout?",
                Foreground = (Brush)Application.Current.Resources["ContentDialogForeground"],
                Margin = new Thickness(0, 10, 0, 10)
            };

            StackPanel second = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 20, 0, 20)
            };

            Button primaryButton = new Button
            {
                Content = "Yes",
                Background = (Brush)Application.Current.Resources["SecondaryButtonBackground"],
                Foreground = (Brush)Application.Current.Resources["SecondaryButtonForeground"],
                BorderBrush = (Brush)Application.Current.Resources["SecondaryButtonBackground"],
                BorderThickness = (Thickness)Application.Current.Resources["SecondaryButtonThickness"],
                Padding = new Thickness(45, 5, 45, 7),
                Margin = new Thickness(5, 10, 15, 10)
            };

            primaryButton.Resources["ButtonBackgroundPointerOver"] = (Brush)Application.Current.Resources["SecondaryButtonBackgroundPointerOver"];
            primaryButton.Resources["ButtonForegroundPointerOver"] = (Brush)Application.Current.Resources["SecondaryButtonForeground"];
            primaryButton.Resources["ButtonBackgroundPressed"] = (Brush)Application.Current.Resources["SecondaryButtonBackground"];
            primaryButton.Resources["ButtonForegroundPressed"] = (Brush)Application.Current.Resources["SecondaryButtonForeground"];
            primaryButton.Resources["ButtonBackgroundReleased"] = (Brush)Application.Current.Resources["SecondaryButtonBackground"];
            primaryButton.Resources["ButtonForegroundReleased"] = (Brush)Application.Current.Resources["SecondaryButtonForeground"];
            primaryButton.CornerRadius = (CornerRadius)Application.Current.Resources["DialogCornerRadius"];

            Button secondaryButton = new Button
            {
                Content = "No",
                Background = (Brush)Application.Current.Resources["PrimaryButtonBackground"],
                Foreground = (Brush)Application.Current.Resources["PrimaryButtonForeground"],
                BorderBrush = (Brush)Application.Current.Resources["PrimaryButtonBackground"],
                BorderThickness = (Thickness)Application.Current.Resources["PrimaryButtonThickness"],
                Padding = new Thickness(45, 5, 45, 7),
                Margin = new Thickness(15, 10, 5, 10)
            };

            secondaryButton.Resources["ButtonBackgroundPointerOver"] = (Brush)Application.Current.Resources["PrimaryButtonBackgroundPointerOver"];
            secondaryButton.Resources["ButtonForegroundPointerOver"] = (Brush)Application.Current.Resources["PrimaryButtonForeground"];
            secondaryButton.Resources["ButtonBackgroundPressed"] = (Brush)Application.Current.Resources["PrimaryButtonBackground"];
            secondaryButton.Resources["ButtonForegroundPressed"] = (Brush)Application.Current.Resources["PrimaryButtonForeground"];
            secondaryButton.Resources["ButtonBackgroundReleased"] = (Brush)Application.Current.Resources["PrimaryButtonBackground"];
            secondaryButton.Resources["ButtonForegroundReleased"] = (Brush)Application.Current.Resources["PrimaryButtonForeground"];
            secondaryButton.CornerRadius = (CornerRadius)Application.Current.Resources["DialogCornerRadius"];

            second.Children.Add(primaryButton);
            second.Children.Add(secondaryButton);
            first.Children.Add(contentText);
            first.Children.Add(second);
            logoutDialog.Content = first;
            logoutDialog.XamlRoot = this.XamlRoot;

            primaryButton.Click += (sender, args) =>
            {
                logoutDialog.Hide();
                Frame rootFrame = ((App)Application.Current).RootFrame;
                Frame.Navigate(typeof(User.LoginPage));
            };

            secondaryButton.Click += (sender, args) =>
            {
                logoutDialog.Hide();
            };
           
            logoutDialog.ShowAsync();

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
