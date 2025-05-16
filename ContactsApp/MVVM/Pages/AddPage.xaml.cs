using System.Collections.ObjectModel;
namespace ContactsApp.MVVM.Pages;
public partial class AddPage : ContentPage {
    private readonly ObservableCollection<Models.Contact> _contacts;
    public AddPage(ObservableCollection<Models.Contact> contacts) {
        InitializeComponent();
        _contacts = contacts;
    }
    private void OnConfirmClicked(object sender, EventArgs e) {
        if(string.IsNullOrWhiteSpace(FirstNameEntry.Text) ||
            string.IsNullOrWhiteSpace(LastNameEntry.Text)) {
            DisplayAlert("Error", "Please enter both first and last name", "OK");
            return;
        }
        _contacts.Add(new Models.Contact {
            Name = FirstNameEntry.Text,
            Surname = LastNameEntry.Text
        });
        Navigation.PopAsync();
    }
}