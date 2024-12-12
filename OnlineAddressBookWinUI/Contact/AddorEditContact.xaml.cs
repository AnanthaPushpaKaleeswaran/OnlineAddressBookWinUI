using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using OnlineAddressBookWinUI.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnlineAddressBookWinUI.Contact
{
    //main AddContact class
    public sealed partial class AddorEditContact : Page
    {
        //data members
        public string name = "";
        public string phoneNo = "";
        public string address = "";
        private int id = 1;
        public string email = "";
        protected string type = "";
        protected string oldPhoneNo = "";
        protected Contact contact;
        protected EditContact editContact=new EditContact();

        public GroupModel GroupModel { get; set; }

        //constructor
        public AddorEditContact()
        {
            this.InitializeComponent();

            // Initialize the ViewModel and bind to DataContext
            GroupModel = new GroupModel();
            this.DataContext = GroupModel;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is Send args)
            {
                email = args.Email;
                type=args.Type;
                contact=args.Contact;
            }
            setExistingGroups();
            if (type == "edit")
            {
                AddDetails(contact);
                oldPhoneNo = contact.PhoneNo;
            }
        }

        //groupSelection from xaml
        private void groupSelection(object sender,SelectionChangedEventArgs e)
        {
            foreach (var item in groupList.SelectedItems)
            {
                if (item is Group selectedGroup)
                {
                    if (selectedGroup.ID == 0)
                    {
                        AddNewGroup();
                        groupList.SelectedItems.Remove(selectedGroup);
                        break;
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
                Text = ""
            };

            // Use a StackPanel to hold both elements
            StackPanel dialogContent = new StackPanel();
            dialogContent.Children.Add(groupNameInput);
            dialogContent.Children.Add(alertDialog);

            // Assign the StackPanel to the ContentDialog's Content
            addNewGroupDialog.Content = dialogContent;
            addNewGroupDialog.XamlRoot = this.XamlRoot;

            addNewGroupDialog.PrimaryButtonClick += (sender, args) =>
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(groupNameInput.Text))
                {
                    alertDialog.Text = "Group name cannot be empty.";
                    alertDialog.Visibility = Visibility.Visible;
                    args.Cancel = true; // Prevent dialog from closing
                }
                else if (!checkGroup(groupNameInput.Text))
                {
                    alertDialog.Text = "The group already exists.";
                    alertDialog.Visibility = Visibility.Visible;
                    args.Cancel = true;
                }
                else
                {
                    // Input is valid, hide the error message
                    alertDialog.Visibility = Visibility.Collapsed;
                    string groupInput = groupNameInput.Text;
                    string modiGroupName = char.ToUpper(groupInput[0]) + groupInput.Substring(1);
                    // Add the new group
                    Group newGroup = new Group
                    {
                        Name = modiGroupName,
                        ID = id
                    };
                    id++;
                    GroupModel.Groups.Add(newGroup);
                    groupList.SelectedItems.Add(newGroup);
                }
            };

            // Show the dialog
            await addNewGroupDialog.ShowAsync();
        }

        //go back to display page
        public void Cancel(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = ((App)Application.Current).RootFrame;
            Frame.Navigate(typeof(Display), email);
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

            //check the connection
            if (MyConnection.State.Equals("close"))
            {
                Console.Error.WriteLine("Error in opening db");
                return;
            }

            //table exist
            if (!TableExist(MyConnection, "contact"))
            {
                createContactTable(MyConnection);
            }

            //check the phone number
            if (!ValidPhone())
            {
                alert.Text = "The phone number must be 10 numbers";
                return;
            }

            if (type == "add")
            {
                //check the contact exist
                if (ContactExist(MyConnection))
                {
                    alert.Text = "Contact already exists";
                    return;
                }

                //check name exist
                if (NameExist(MyConnection))
                {
                    alert.Text = "Name already exists";
                    return;
                }
            }

            //valid name
            if (!ValidName())
            {
                alert.Text = "Please enter the valid name";
                return;
            }

            //valid address
            if (string.IsNullOrWhiteSpace(address))
            {
                alert.Text = "Please enter the address";
                return;
            }

            string groupStr = makeGroupString();

            if (type == "add")
            {
                AddContact addContact = new AddContact();
                addContact.AddContactToDB(MyConnection, name, phoneNo, address, groupStr, email);
                ShowAutoEditClosingDialog();
            }

            if (type == "edit")
            {
                EditContact editContact = new EditContact();
                editContact.editContactDB(MyConnection, name, phoneNo, address, groupStr, email, oldPhoneNo);
                ShowAutoAddClosingDialog();
            }

            Frame rootFrame = ((App)Application.Current).RootFrame;
            Frame.Navigate(typeof(Display), email);
            MyConnection.Close();
        }

        //check table exists
        protected bool TableExist(SQLiteConnection connection, string tableName)
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
                    createContactTable(myConnection);
                }

                GroupModel.Groups.Clear();
                GroupModel.Groups.Add(new Group { Name = "New Group", ID = 0 });

                string query = "SELECT contactGroup FROM contact WHERE email=@email";

                using (SQLiteCommand command = new SQLiteCommand(query, myConnection))
                {
                    command.Parameters.AddWithValue("@email", email);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        //check table have values
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string groupDB = reader["contactGroup"].ToString();

                                if (!string.IsNullOrEmpty(groupDB))
                                {
                                    string groupName = string.Empty;
                                    StringBuilder groupBuilder = new StringBuilder();

                                    foreach (char ch in groupDB)
                                    {
                                        if (ch == ',')
                                        {
                                            string groupToAdd = groupBuilder.ToString();
                                            if (!string.IsNullOrEmpty(groupToAdd) && checkGroup(groupToAdd))
                                            {
                                                string modiGroupName = char.ToUpper(groupToAdd[0]) + groupToAdd.Substring(1);
                                                Group newGroup = new Group { Name = modiGroupName, ID = id };
                                                id++;
                                                GroupModel.Groups.Add(newGroup);
                                            }
                                            groupBuilder.Clear(); // Reset the group builder
                                        }
                                        else
                                        {
                                            groupBuilder.Append(ch); // Add character to the group name
                                        }
                                    }

                                    // Check for the last group after the loop
                                    string lastGroup = groupBuilder.ToString();
                                    if (!string.IsNullOrEmpty(lastGroup) && checkGroup(lastGroup))
                                    {
                                        Group newGroup = new Group { Name = lastGroup, ID = id };
                                        id++;
                                        GroupModel.Groups.Add(newGroup);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //check the group is present or not
        private bool checkGroup(string groupName)
        {
            foreach(Group indGroup in GroupModel.Groups)
            {
                if(string.Equals(indGroup.Name, groupName, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }

        //table creation
        protected void createContactTable(SQLiteConnection connection)
        {
            string query = $@"CREATE TABLE IF NOT EXISTS contact (name VARCHAR(40),phoneNo VARCHAR(10),address VARCHAR(100),contactGroup VARCHAR(100),email VARCHAR(50),PRIMARY KEY(phoneNo,email),FOREIGN KEY(email) REFERENCES user(email) ON DELETE CASCADE)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
            Console.WriteLine("Table created successfully");
        }

        //valid phone number
        private bool ValidPhone()
        {
            string regexstr = @"^[0-9]{10}$";
            Regex re = new Regex(regexstr);
            return re.IsMatch(phoneNo);
        }

        //check the contact already exist or not
        private bool ContactExist(SQLiteConnection connection)
        {
            string query = "SELECT COUNT(*) FROM contact WHERE phoneNo=@phoneNo AND email=@email";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@phoneNo", phoneNo);
                var result = command.ExecuteScalar();
                return Convert.ToInt32(result) > 0;
            };
        }

        //check the name already exist or not
        private bool NameExist(SQLiteConnection connection)
        {
            string query = "SELECT COUNT(*) FROM contact WHERE name=@name AND email=@email";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@name", name);
                var result = command.ExecuteScalar();
                return Convert.ToInt32(result) > 0;
            };
        }

        //valid name
        private bool ValidName()
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            string regexstr = @"^[a-zA-Z0-9\s]+$";
            Regex re = new Regex(regexstr);
            return re.IsMatch(name);
        }

        //make the grouplist as string for storing
        private string makeGroupString()
        {
            List<string> groupNames = new List<string>();
            int groupCount = groupList.SelectedItems.Count;

            //traverse the selected items in the grouplist
            foreach (var item in groupList.SelectedItems)
            {
                if(item is Group selectedGroup)
                {
                    groupNames.Add(selectedGroup.Name);
                }
            }

            string res = "";
            groupNames.Sort();
            
            //make the list as string
            for(int index=0;index<groupNames.Count;index++)
            {
                res+= groupNames[index];
                if (index < groupNames.Count - 1)
                {
                    res+= ",";
                }
            }
            return res;
        }

        private void AddDetails(Contact contact)
        {
            // Populate input fields with contact details
            nameInput.Text = contact.Name;
            phoneNoInput.Text = contact.PhoneNo;
            addressInput.Text = contact.Address;

            // Clear previously selected items in the group list
            groupList.SelectedItems.Clear();

            // Parse and process the contact's groups
            string[] groups = contact.ContactGroup.Split(','); // Split groups by ','

            foreach (string groupName in groups)
            {
                if (!string.IsNullOrWhiteSpace(groupName))
                {
                    CheckAndAdd(groupName.Trim());
                }
            }
        }

        private void CheckAndAdd(string groupName)
        {
            foreach (Group indGroup in GroupModel.Groups)
            {
                if (indGroup.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase))
                {
                    groupList.SelectedItems.Add(indGroup);
                    break;
                }
            }
        }

        private async void ShowAutoEditClosingDialog()
        {
            // Create a ContentDialog
            ContentDialog addNewGroupDialog = new ContentDialog
            {
                Title = "Edit",
                PrimaryButtonText = string.Empty, // Hide primary button
                CloseButtonText = string.Empty   // Hide close button
            };

            // Add content to the dialog
            TextBlock textBlock = new TextBlock
            {
                Text = "Contact edited successfully",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 16,
            };
            addNewGroupDialog.Content = textBlock;
            addNewGroupDialog.XamlRoot = this.XamlRoot;
            // Show the dialog
            _ = addNewGroupDialog.ShowAsync();

            // Wait for 1 second
            await Task.Delay(1000);

            // Close the dialog
            addNewGroupDialog.Hide();
        }

        private async void ShowAutoAddClosingDialog()
        {
            // Create a ContentDialog
            ContentDialog addNewGroupDialog = new ContentDialog
            {
                Title = "Add",
                PrimaryButtonText = string.Empty, // Hide primary button
                CloseButtonText = string.Empty   // Hide close button
            };

            // Add content to the dialog
            TextBlock textBlock = new TextBlock
            {
                Text = "Contact added successfully",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 16,
            };
            addNewGroupDialog.Content = textBlock;
            addNewGroupDialog.XamlRoot = this.XamlRoot;
            // Show the dialog
            _ = addNewGroupDialog.ShowAsync();

            // Wait for 1 second
            await Task.Delay(1000);

            // Close the dialog
            addNewGroupDialog.Hide();
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
