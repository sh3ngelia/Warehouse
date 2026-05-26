using Warehouse.DTO.Users;

namespace Warehouse.App
{
    public partial class MainForm : Form
    {
        private readonly UserDto _currentUser;

        public MainForm(UserDto currentUser)
        {
            _currentUser = currentUser;
            InitializeComponent();
            logoutToolStripMenuItem.Image = SystemIcons.Shield.ToBitmap();
            editToolStripMenuItem.Image = SystemIcons.Question.ToBitmap();

            toolStripStatusLabel1.Text = $"Logged in: {currentUser.Username}";
        }
    }
}