using System.Collections.ObjectModel;
namespace ContactsApp.MVVM.Pages;
using ContactsApp.MVVM.Models;
public partial class MainPage : ContentPage {
    public ObservableCollection<Contact> Contacts { get; set; }
    public MainPage() {
        InitializeComponent();
        Contacts = new ObservableCollection<Contact>();
        BindingContext = this;

        LoadSampleData();
        SortContactsAlphabetically();
    }
    private async void RemovalConfirmation(object sender, EventArgs e) {
        if(ContactDisplay.SelectedItems == null || ContactDisplay.SelectedItems.Count == 0) {
            await DisplayAlert("Info", "No contacts selected", "OK");
            return;
        }
        bool answer = await DisplayAlert("Confirmation", $"Delete {ContactDisplay.SelectedItems.Count} contacts?", "Yes", "No");
        if(answer) {
            var itemsToRemove = ContactDisplay.SelectedItems.Cast<Contact>().ToList();
            foreach(var item in itemsToRemove) Contacts.Remove(item);
        }
    }
    private async void AddButton_Clicked(object sender, EventArgs e) {
        await Navigation.PushAsync(new AddPage(Contacts));
        SortContactsAlphabetically();
    }
    private void ContextMenuRemove_Clicked(object sender, EventArgs e) {
        if(sender is MenuFlyoutItem menuItem && menuItem.BindingContext is Contact contact) Contacts.Remove(contact);
    }
    private async void ContextMenuModify_Clicked(object sender, EventArgs e) {
        if(sender is MenuFlyoutItem menuItem && menuItem.BindingContext is Contact contact) await Navigation.PushAsync(new EditPage(contact, Contacts));
        SortContactsAlphabetically();
    }
    private void SortContactsAlphabetically() {
        var sortedContacts = Contacts.OrderBy(c => c.Surname).ThenBy(c => c.Name).ToList();
        Contacts.Clear();
        foreach(var contact in sortedContacts) Contacts.Add(contact);
    }
    void LoadSampleData() {
        Contacts.Add(new Contact { Name = "Tralalelo", Surname = "Tralala" });
        Contacts.Add(new Contact { Name = "Frigo Camello", Surname = "Buffo Fardello" });
        Contacts.Add(new Contact { Name = "Tung tung tung tung", Surname = "Sahur" });
        Contacts.Add(new Contact { Name = "Tripi Tropi", Surname = "Tropa Tripa" });
        Contacts.Add(new Contact { Name = "Fruli", Surname = "Frula" });
        Contacts.Add(new Contact { Name = "Odin din din din din din din dun", Surname = "Odin din din din madun" });
        Contacts.Add(new Contact { Name = "Balerina", Surname = "Cappuchina" });
        Contacts.Add(new Contact { Name = "Capuchino", Surname = "Assasino" });
        Contacts.Add(new Contact { Name = "Bombardiro", Surname = "Crocodilo" });
        Contacts.Add(new Contact { Name = "Chimpanzini", Surname = "Bananini" });
        Contacts.Add(new Contact { Name = "Bombini", Surname = "Goosini" });
        Contacts.Add(new Contact { Name = "Damian", Surname = "Postrozny" });
    }
}