using System.Windows;

namespace ADO.NET
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void Click_Button_Basics(object sender, RoutedEventArgs e)  // The button shows the main window, the window displays all the data of the database tables.
        {
            this.Hide();
            new MainWindow().ShowDialog();
            this.Show();
        }

        private void Click_Button_Orm(object sender, RoutedEventArgs e)  // The button opens a window for changing data.
        {
            this.Hide();
            new OrmWindow().ShowDialog();
            this.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new DalWindow().ShowDialog();
            this.Show();
        }
    }
}
