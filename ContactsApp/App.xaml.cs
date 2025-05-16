namespace ContactsApp
{
    using ContactsApp.MVVM.Pages;
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ContactsApp.MVVM.Pages.MainPage());
        }
    }
}
