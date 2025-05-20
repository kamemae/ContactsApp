using System.Collections.ObjectModel;
namespace ContactsApp.MVVM.Pages;
using ContactsApp.MVVM.Models;

public partial class MainPage : ContentPage {
    private ObservableCollection<Contact> AllContacts { get; set; } = new ObservableCollection<Contact>();
    public ObservableCollection<Contact> FilteredContacts { get; set; } = new ObservableCollection<Contact>();

    //the most amazing of them all, on appearing my beloved!
    protected override void OnAppearing() {
        base.OnAppearing();
        SortContactsAlphabetically();
    }

    public MainPage() {
        InitializeComponent();

        AllContacts = new ObservableCollection<Contact>();
        BindingContext = this;

        LoadSampleData();
        SortContactsAlphabetically();
    }

    //filter (mainly for searchin' puposes)
    private void ApplySearchFilter(string searchText) {
        var filtered = AllContacts.Where(c => (c.Name + " " + c.Surname).ToLower().Contains(searchText.ToLower())).ToList();
        FilteredContacts.Clear();
        foreach(var contact in filtered) FilteredContacts.Add(contact);
    }

    //aplhabetical sort by surname then name
    private void SortContactsAlphabetically() {
        var sorted = AllContacts.OrderBy(c => c.Surname).ThenBy(c => c.Name).ToList();
        AllContacts = new ObservableCollection<Contact>(sorted);
        ApplySearchFilter("");
    }


    //searchbar
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e) {
        ApplySearchFilter(e.NewTextValue);
    }


    //add
    private async void AddButton_Clicked(object sender, EventArgs e) {
        await Navigation.PushAsync(new AddPage(AllContacts));
    }


    //remove
    private void ContextMenuRemove_Clicked(object sender, EventArgs e) {
        if(sender is MenuFlyoutItem menuItem && menuItem.BindingContext is Contact contact) {
            AllContacts.Remove(contact);
            ApplySearchFilter("");
            Searchprompt.Text = "";
        }

    }
    private void SwipeItemRemove_Invoked(object sender, EventArgs e) {
        if(sender is SwipeItem swipeItem && swipeItem.BindingContext is Contact contact) {
            AllContacts.Remove(contact);
            ApplySearchFilter("");
            Searchprompt.Text = "";
        }
    }


    //remove multiple
    private async void RemoveButton_Clicked(object sender, EventArgs e) {
        if(ContactDisplay.SelectedItems == null || ContactDisplay.SelectedItems.Count == 0) {
            await DisplayAlert("Info", "No contacts selected", "OK");
            return;
        }
        if(await DisplayAlert("Confirmation", $"Are you sure you want to remove selected contacts?", "Yes", "No")) {
            var itemsToRemove = ContactDisplay.SelectedItems.Cast<Contact>().ToList();
            foreach(var item in itemsToRemove) AllContacts.Remove(item);
            ApplySearchFilter("");
        }
        Searchprompt.Text = "";
    }


    //modify
    private void ContextMenuModify_Clicked(object sender, EventArgs e) {
        if(sender is MenuFlyoutItem menuItem && menuItem.BindingContext is Contact contact) {
            Navigation.PushAsync(new EditPage(contact, AllContacts));
            ApplySearchFilter("");
        }
    }
    private async void SwipeItemModify_Invoked(object sender, EventArgs e) {
        if(sender is SwipeItem swipeItem && swipeItem.BindingContext is Contact contact) {
            await Navigation.PushAsync(new EditPage(contact, AllContacts));
            ApplySearchFilter("");
        }
    }

    //sample data
    void LoadSampleData() {
        AllContacts.Clear();
        AllContacts.Add(new Contact { Name = "Tralalelo", Surname = "Tralala" });
        AllContacts.Add(new Contact { Name = "Frigo Camello", Surname = "Buffo Fardello" });
        AllContacts.Add(new Contact { Name = "Tung tung tung tung", Surname = "Sahur" });
        AllContacts.Add(new Contact { Name = "Tripi Tropi", Surname = "Tropa Tripa" });
        AllContacts.Add(new Contact { Name = "Fruli", Surname = "Frula" });
        AllContacts.Add(new Contact { Name = "Odin din din din din din din dun", Surname = "Odin din din din madun" });
        AllContacts.Add(new Contact { Name = "Balerina", Surname = "Cappuchina" });
        AllContacts.Add(new Contact { Name = "Capuchino", Surname = "Assasino" });
        AllContacts.Add(new Contact { Name = "Bombardiro", Surname = "Crocodilo" });
        AllContacts.Add(new Contact { Name = "Chimpanzini", Surname = "Bananini" });
        AllContacts.Add(new Contact { Name = "Bombini", Surname = "Goosini" });
        AllContacts.Add(new Contact { Name = "Damian", Surname = "Postrozny" });
    }
}