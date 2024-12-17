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
        protected EditContact editContact = new EditContact();

        public GroupModel GroupModel { get; set; }

        //constructor
        public AddorEditContact()
        {
            this.InitializeComponent();

            // Initialize the ViewModel and bind to DataContext
            GroupModel = new GroupModel();
            this.DataContext = GroupModel;
            version.Text = new Version().GetAppVersion();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is Send args)
            {
                email = args.Email;
                type = args.Type;
                contact = args.Contact;
            }
            setExistingGroups();
            if (type == "edit")
            {
                AddDetails(contact);
                oldPhoneNo = contact.PhoneNo;
            }
        }

        //groupSelection from xaml
        private void groupSelection(object sender, SelectionChangedEventArgs e)
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
                Background = (Brush)Application.Current.Resources["ContentDialogBackground"],
                Foreground = (Brush)Application.Current.Resources["ContentDialogForeground"],
            };
            StackPanel first = new StackPanel();

            TextBox groupNameInput = new TextBox
            {
                PlaceholderText = "Enter the group name",
                Margin = new Thickness(0, 10, 0, 10), // Add some margin for spacing
                Background = (Brush)Application.Current.Resources["DialogTextBoxBackground"],
                Foreground = (Brush)Application.Current.Resources["DialogTextBoxForeground"],
                BorderThickness = (Thickness)Application.Current.Resources["DialogTextBoxThickness"],
                BorderBrush = (SolidColorBrush)Application.Current.Resources["DialogTextBoxBorderBrush"],
            };

            groupNameInput.Resources["TextControlBorderThemeThicknessPointerOver"] = (Thickness)Application.Current.Resources["DialogTextBoxThickness"];
            groupNameInput.Resources["TextControlBorderThemeThicknessFocused"] = (Thickness)Application.Current.Resources["DialogTextBoxThickness"];
            groupNameInput.Resources["TextControlBorderBrushFocused"] = (SolidColorBrush)Application.Current.Resources["DialogTextBoxBorderBrush"];
            groupNameInput.Resources["TextControlBorderBrushPointerOver"] = (SolidColorBrush)Application.Current.Resources["DialogTextBoxBorderBrush"];
            groupNameInput.Resources["TextControlForegroundPointerOver"] = (Brush)Application.Current.Resources["DialogTextBoxForeground"];
            groupNameInput.Resources["TextControlForegroundFocused"] = (Brush)Application.Current.Resources["DialogTextBoxForeground"];
            groupNameInput.Resources["TextControlBackgroundPointerOver"] = (Brush)Application.Current.Resources["DialogTextBoxBackground"];
            groupNameInput.Resources["TextControlBackgroundFocused"] = (Brush)Application.Current.Resources["DialogTextBoxBackground"];

            TextBlock alertDialog = new TextBlock
            {
                Text = "",
                Foreground = (Brush)Application.Current.Resources["alertText"],
                Margin=new Thickness(0,10,0,10)
            };

            StackPanel second = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment= HorizontalAlignment.Center,
                Margin=new Thickness(0,20,0,20)
            };

            Button primaryButton = new Button
            {
                Content = "Submit",
                Background = (Brush)Application.Current.Resources["PrimaryButtonBackground"],
                Foreground = (Brush)Application.Current.Resources["PrimaryButtonForeground"],
                BorderBrush = (Brush)Application.Current.Resources["PrimaryButtonBackground"],
                BorderThickness = (Thickness)Application.Current.Resources["PrimaryButtonThickness"],
                Padding=new Thickness(20,5,20,5),
                Margin=new Thickness(25,10,25,10)
            };

            primaryButton.Resources["ButtonBackgroundPointerOver"] = (Brush)Application.Current.Resources["PrimaryButtonBackgroundPointerOver"];
            primaryButton.Resources["ButtonForegroundPointerOver"] = (Brush)Application.Current.Resources["PrimaryButtonForeground"];
            primaryButton.Resources["ButtonBackgroundPressed"] = (Brush)Application.Current.Resources["PrimaryButtonBackground"];
            primaryButton.Resources["ButtonForegroundPressed"] = (Brush)Application.Current.Resources["PrimaryButtonForeground"];
            primaryButton.Resources["ButtonBackgroundReleased"] = (Brush)Application.Current.Resources["PrimaryButtonBackground"];
            primaryButton.Resources["ButtonForegroundReleased"] = (Brush)Application.Current.Resources["PrimaryButtonForeground"];
            primaryButton.CornerRadius = (CornerRadius)Application.Current.Resources["DialogCornerRadius"];

            Button secondaryButton = new Button
            {
                Content = "Cancel",
                Background = (Brush)Application.Current.Resources["SecondaryButtonBackground"],
                Foreground = (Brush)Application.Current.Resources["SecondaryButtonForeground"],
                BorderBrush = (Brush)Application.Current.Resources["SecondaryButtonBackground"],
                BorderThickness = (Thickness)Application.Current.Resources["SecondaryButtonThickness"],
                Padding = new Thickness(20, 5, 20, 5),
                Margin = new Thickness(25, 10, 25, 10)
            };

            secondaryButton.Resources["ButtonBackgroundPointerOver"] = (Brush)Application.Current.Resources["SecondaryButtonBackgroundPointerOver"];
            secondaryButton.Resources["ButtonForegroundPointerOver"] = (Brush)Application.Current.Resources["SecondaryButtonForeground"];
            secondaryButton.Resources["ButtonBackgroundPressed"] = (Brush)Application.Current.Resources["SecondaryButtonBackground"];
            secondaryButton.Resources["ButtonForegroundPressed"] = (Brush)Application.Current.Resources["SecondaryButtonForeground"];
            secondaryButton.Resources["ButtonBackgroundReleased"] = (Brush)Application.Current.Resources["SecondaryButtonBackground"];
            secondaryButton.Resources["ButtonForegroundReleased"] = (Brush)Application.Current.Resources["SecondaryButtonForeground"];
            secondaryButton.CornerRadius = (CornerRadius)Application.Current.Resources["DialogCornerRadius"];

            second.Children.Add(primaryButton);
            second.Children.Add(secondaryButton);
            first.Children.Add(groupNameInput);
            first.Children.Add(alertDialog);
            first.Children.Add(second);

            // Assign the StackPanel to the ContentDialog's Content
            addNewGroupDialog.Content = first;
            addNewGroupDialog.XamlRoot = this.XamlRoot;

            primaryButton.Click += (sender, args) =>
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(groupNameInput.Text))
                {
                    alertDialog.Text = "Group name cannot be empty.";
                    alertDialog.Visibility = Visibility.Visible;
                }
                else if (!checkGroup(groupNameInput.Text))
                {
                    alertDialog.Text = "The group already exists.";
                    alertDialog.Visibility = Visibility.Visible;
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
                    addNewGroupDialog.Hide();
                }
            };

            secondaryButton.Click += (sender, args) =>
            {
                addNewGroupDialog.Hide();
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
                ShowAutoAddClosingDialog();
            }

            if (type == "edit")
            {
                EditContact editContact = new EditContact();
                editContact.editContactDB(MyConnection, name, phoneNo, address, groupStr, email, oldPhoneNo);
                ShowAutoEditClosingDialog();
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
            foreach (Group indGroup in GroupModel.Groups)
            {
                if (string.Equals(indGroup.Name, groupName, StringComparison.OrdinalIgnoreCase))
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
                if (item is Group selectedGroup)
                {
                    groupNames.Add(selectedGroup.Name);
                }
            }

            string res = "";
            groupNames.Sort();

            //make the list as string
            for (int index = 0; index < groupNames.Count; index++)
            {
                res += groupNames[index];
                if (index < groupNames.Count - 1)
                {
                    res += ",";
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
                Background = (Brush)Application.Current.Resources["ContentDialogBackground"],
                Foreground = (Brush)Application.Current.Resources["ContentDialogForeground"],
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
                Background = (Brush)Application.Current.Resources["ContentDialogBackground"],
                Foreground = (Brush)Application.Current.Resources["ContentDialogForeground"],
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
