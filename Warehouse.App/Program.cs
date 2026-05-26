using Microsoft.Data.SqlClient;
using Warehouse.App.Configuration;
using Warehouse.App.Forms;
using Warehouse.DTO.Users;
using Warehouse.Service.Main;

namespace Warehouse.App
{
    internal static class Program
    {
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            while (true)
            {
                try
                {
                    using var connection = new SqlConnection(ConfigurationManager.ConnectionString);
                    //connection.Open();

                    var services = new AppServices(connection);

                    using (var loginForm = new LoginForm(services))
                    {
                        if (loginForm.ShowDialog() != DialogResult.OK)
                            return;
                        var loggedInUser = loginForm.LoggedInUser;
                        LocalStorage.LoggedUserId = loggedInUser?.EmployeeId ?? 0;
                        LocalStorage.LoggedUsername = loggedInUser?.Username ?? string.Empty;
                    }

                    var mainForm = new CustomerManagementForm(services);

                    Application.Run(mainForm);

                    if (!mainForm.LoggedOut) return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fatal error: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    internal static class LocalStorage
    {
        public static int LoggedUserId { get; set; }
        public static string LoggedUsername { get; set; } = string.Empty;
    }
}