using ContactsApp.MVVM.Models;
using System.Collections.ObjectModel;
namespace ContactsApp.MVVM.Pages;

public partial class EditPage : ContentPage {
    public Models.Contact CurrentContact { get; set; }
    private readonly ObservableCollection<Models.Contact> _contacts;
    private readonly Models.Contact _originalContact;
    public EditPage(Models.Contact contact, ObservableCollection<Models.Contact> contacts) {
        InitializeComponent();
        _contacts = contacts;
        _originalContact = contact;
        CurrentContact = new Models.Contact {
            Name = contact.Name,
            Surname = contact.Surname
        };
        BindingContext = this;
    }

    private async void OnSaveClicked(object sender, EventArgs e) {
        if(string.IsNullOrWhiteSpace(CurrentContact.Name) ||
            string.IsNullOrWhiteSpace(CurrentContact.Surname)) {
            await DisplayAlert("Error", "Please enter both names", "OK");
            return;
        }
        var index = _contacts.IndexOf(_originalContact);
        if(index != -1) {
            _contacts[index] = new Models.Contact {
                Name = CurrentContact.Name,
                Surname = CurrentContact.Surname
            };
        }
        await Navigation.PopAsync();
    }
}