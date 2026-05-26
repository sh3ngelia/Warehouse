using Warehouse.App.Dialogs;
using Warehouse.DTO.Users;
using Warehouse.Service.Main;

namespace Warehouse.App;

public partial class LoginForm : Form
{
    private readonly AppServices _services;
    public UserDto? LoggedInUser { get; private set; } = null;

    public LoginForm(AppServices services)
    {
        InitializeComponent();
#if DEBUG
        txtUsername.Text = "admin";
        txtPassword.Text = "admin123";
#endif
        _services = services;
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            UserDto? user = _services.Authorization.Login(txtUsername.Text, txtPassword.Text);

            if (user != null)
            {
                LoggedInUser = user;
                DialogResult = DialogResult.OK;
                return;
            }

            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Login error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}