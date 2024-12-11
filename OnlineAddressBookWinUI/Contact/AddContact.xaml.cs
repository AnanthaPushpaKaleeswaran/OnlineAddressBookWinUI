using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using OnlineAddressBookWinUI.User;
using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;

namespace OnlineAddressBookWinUI.Contact
{
    //main AddContact class
    public sealed partial class AddContact : Page
    {
        //data members
        public string name = "";
        public string phoneNo = "";
        public string address = "";
        private int id = 1;

        public GroupModel GroupModel { get; set; }

        //constructor
        public AddContact()
        {
            this.InitializeComponent();

            // Initialize the ViewModel and bind to DataContext
            GroupModel = new GroupModel();
            this.DataContext = GroupModel;
            setExistingGroups();
        }

        //groupSelection from xaml
        private void groupSelection(object sender,SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                if (item is Group selectedGroup)
                {
                    if (selectedGroup.ID == 0)
                    {
                        AddNewGroup();

                    }
                }
            }

        }

        //add new group using content dialog
        private async void AddNewGroup()
        {
            ContentDialog addNewGroupDialog = new ContentDialog
            {
                Title = "Add New Group",
                PrimaryButtonText = "Ok",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary
            };

            TextBox groupNameInput = new TextBox
            {
                PlaceholderText = "Enter the group name",
                Margin = new Thickness(0, 10, 0, 10) // Add some margin for spacing
            };

            TextBlock alertDialog = new TextBlock
            {
                Text = "",
                Visibility = Visibility.Collapsed // Initially hidden
            };

            // Use a StackPanel to hold both elements
            StackPanel dialogContent = new StackPanel();
            dialogContent.Children.Add(groupNameInput);
            dialogContent.Children.Add(alertDialog);

            // Assign the StackPanel to the ContentDialog's Content
            addNewGroupDialog.Content = dialogContent;
            addNewGroupDialog.XamlRoot = this.XamlRoot;
            ContentDialogResult result = await addNewGroupDialog.ShowAsync();
            
            if (result == ContentDialogResult.Primary)
            {
                bool check=checkGroup(groupNameInput.Text);
                if (check && !string.IsNullOrEmpty(groupNameInput.Text))
                {
                    Group newGroup = new Group
                    {
                        Name = groupNameInput.Text,
                        ID = id
                    };
                    id++;
                    GroupModel.Groups.Add(newGroup);
                }
                else
                {
                    alertDialog.Text = "The group already exists";
                    return;
                }
            }
        }

        //submit
        public void Submit(object sender, RoutedEventArgs e)
        {
            name = nameInput.Text;
            phoneNo = phoneNoInput.Text;
            address = addressInput.Text;
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "onlineAddressBook.db");
            string ConnectionString = $"Data Source={dbPath}";
            SQLiteConnection MyConnection = new SQLiteConnection(ConnectionString);
            MyConnection.Open();

            if (MyConnection.State.Equals("close"))
            {
                Console.Error.WriteLine("Error in opening db");
                return;
            }

            if (!tableExist(MyConnection, "contact"))
            {
                createTable(MyConnection, "contact");
            }

            if (!ValidPhone())
            {
                alert.Text = "The phone number must be 10 numbers";
                return;
            }
        }

        //check table exists
        protected bool tableExist(SQLiteConnection connection, string tableName)
        {
            string query = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name=@tableName";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@tableName", tableName);
            var result = command.ExecuteScalar();
            return Convert.ToInt32(result) > 0;
        }

        //set the existing groups in db
        private void setExistingGroups()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "onlineAddressBook.db");
            string ConnectionString = $"Data Source={dbPath}";
            SQLiteConnection MyConnection = new SQLiteConnection(ConnectionString);
            MyConnection.Open();

            if (MyConnection.State.Equals("close"))
            {
                Console.Error.WriteLine("Error in opening db");
                return;
            }

            if (!tableExist(MyConnection, "contact"))
            {
                createTable(MyConnection, "contact");
            }

            GroupModel.Groups.Clear();
            GroupModel.Groups.Add(new Group { Name = "New Group", ID = 0 });
            string query = "SELECT contactGroup FROM contact WHERE email=@email";
            
            using(SQLiteCommand command = new SQLiteCommand(query, MyConnection))
            {
                command.Parameters.AddWithValue("@email", Session.email);
            
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            string groupDB = reader["contactGroup"].ToString();
                
                            if (!string.IsNullOrEmpty(groupDB))
                            {
                                string groupName="";
                            
                                foreach(char ch in groupDB)
                                {
                                    if (ch == ',')
                                    {
                                        bool checkLoop=checkGroup(groupName);
                                        if (checkLoop)
                                        {
                                            Group newGroup = new Group { Name = groupName, ID = id };
                                            id++;
                                            GroupModel.Groups.Add(newGroup);
                                        }
                                        groupName = "";
                                    }
                                    else
                                    {
                                        groupName += ch;
                                    }
                                }
                                bool check = checkGroup(groupName);
                                if (check)
                                {
                                    Group newGroup = new Group { Name = groupName, ID = id };
                                    id++;
                                    GroupModel.Groups.Add(newGroup);
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool checkGroup(string groupName)
        {
            foreach(Group indGroup in GroupModel.Groups)
            {
                if(indGroup.Name == groupName)
                {
                    return false;
                }
            }
            return true;
        }

        //table creation
        protected void createTable(SQLiteConnection connection, string tableName)
        {
            string query = $@"CREATE TABLE IF NOT EXISTS [{tableName}] (name VARCHAR(40),phoneNo VARCHAR(10),address VARCHAR(100),contactGroup VARCHAR(100),email VARCHAR(50),PRIMARY KEY(phoneNo,email),FOREIGN KEY(email) REFERENCES user(email) ON DELETE CASCADE)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
            Console.WriteLine("Table created successfully");
        }

        private bool ValidPhone()
        {
            string regexstr = @"^[0-9]{10}$";
            Regex re = new Regex(regexstr);
            return re.IsMatch(phoneNo);
        }
    }

    //group model class
    public class GroupModel
    {
        public ObservableCollection<Group> Groups { get; set; }

        public GroupModel()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group { Name = "New Group", ID = 0 },
            };
        }
    }

    //group class
    public class Group
    {
        public required string Name { get; set; }
        public int ID { get; set; }
    }
}
