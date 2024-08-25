using Planist.Features.Storage;

namespace Planist
{
    public partial class App : Application
    {
        public App(IPlanistDb planistDb)
        {
            InitializeComponent();

            MainPage = new AppShell();

            // initialize the database
            planistDb.InitializeDatabase();
        }
    }
}
