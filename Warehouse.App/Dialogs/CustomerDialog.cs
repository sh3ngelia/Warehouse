using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Warehouse.DTO.Customers;

namespace Warehouse.App.Dialogs
{
    public class CustomerDialog : Form
    {
        private ConcreteCustomerDto existingCustomer;
        private ComboBox cmbType;
        private TextBox txtFirstName, txtLastName, txtPersonalId;
        private TextBox txtCompanyName, txtTaxId, txtLegalAddress;
        private TextBox txtPhone, txtEmail;
        private GroupBox grpPersonal, grpLegal;
        private Label lblAvatar;
        private Label lblTitle, lblSubtitle;

        public CustomerDialog(ConcreteCustomerDto customer)
        {
            existingCustomer = customer;
            InitializeDialog();
            BuildUI();
            if (customer != null) PopulateFields();
        }

        private void InitializeDialog()
        {
            this.Text = existingCustomer == null ? "Add Customer" : "Edit Customer";
            this.Size = new Size(440, 560);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void BuildUI()
        {
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(16, 12, 16, 8)
            };

            int y = 12;

            // ── Avatar Row ──
            var avatarPanel = new Panel { Location = new Point(16, y), Size = new Size(390, 50) };

            lblAvatar = new Label
            {
                Size = new Size(42, 42),
                Location = new Point(0, 4),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(26, 93, 184)
            };
            lblAvatar.Paint += (s, e) =>
            {
                var path = new GraphicsPath();
                path.AddEllipse(0, 0, lblAvatar.Width - 1, lblAvatar.Height - 1);
                lblAvatar.Region = new Region(path);
            };
            lblAvatar.Text = existingCustomer != null ? existingCustomer.GetInitials() : "??";

            lblTitle = new Label
            {
                Text = existingCustomer != null ? existingCustomer.Name : "New Customer",
                Location = new Point(50, 4),
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30)
            };

            lblSubtitle = new Label
            {
                Text = existingCustomer != null ? $"Customer #{existingCustomer.Id} — {existingCustomer.Type}" : "Fill in the details below",
                Location = new Point(50, 26),
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(100, 100, 100)
            };

            avatarPanel.Controls.AddRange(new Control[] { lblAvatar, lblTitle, lblSubtitle });
            mainPanel.Controls.Add(avatarPanel);

            // Separator
            y += 58;
            var sep = new Label { Location = new Point(16, y), Size = new Size(390, 1), BackColor = Color.FromArgb(208, 208, 208) };
            mainPanel.Controls.Add(sep);
            y += 10;

            // ── Customer Type GroupBox ──
            var grpType = new GroupBox
            {
                Text = "Customer Type",
                Location = new Point(16, y),
                Size = new Size(390, 58),
                Font = new Font("Segoe UI", 8.5F)
            };

            cmbType = new ComboBox
            {
                Location = new Point(12, 24),
                Size = new Size(366, 24),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F)
            };
            cmbType.Items.AddRange(new object[] { "Physical Person", "Legal Entity" });
            cmbType.SelectedIndex = 0;
            cmbType.SelectedIndexChanged += CmbType_Changed;

            grpType.Controls.Add(cmbType);
            mainPanel.Controls.Add(grpType);
            y += 68;

            // ── Physical Person GroupBox ──
            grpPersonal = new GroupBox
            {
                Text = "Personal Information",
                Location = new Point(16, y),
                Size = new Size(390, 118),
                Font = new Font("Segoe UI", 8.5F)
            };

            var lbl1 = new Label { Text = "First Name", Location = new Point(12, 22), AutoSize = true, Font = new Font("Segoe UI", 8.5F) };
            txtFirstName = new TextBox { Location = new Point(12, 40), Size = new Size(175, 24), Font = new Font("Segoe UI", 9F) };
            txtFirstName.TextChanged += (s, e) => UpdateAvatar();

            var lbl2 = new Label { Text = "Last Name", Location = new Point(200, 22), AutoSize = true, Font = new Font("Segoe UI", 8.5F) };
            txtLastName = new TextBox { Location = new Point(200, 40), Size = new Size(178, 24), Font = new Font("Segoe UI", 9F) };
            txtLastName.TextChanged += (s, e) => UpdateAvatar();

            var lbl3 = new Label { Text = "Personal ID (11 digits)", Location = new Point(12, 70), AutoSize = true, Font = new Font("Segoe UI", 8.5F) };
            txtPersonalId = new TextBox { Location = new Point(12, 88), Size = new Size(366, 24), MaxLength = 11, Font = new Font("Segoe UI", 9F) };

            grpPersonal.Controls.AddRange(new Control[] { lbl1, txtFirstName, lbl2, txtLastName, lbl3, txtPersonalId });
            mainPanel.Controls.Add(grpPersonal);

            // ── Legal Entity GroupBox ──
            grpLegal = new GroupBox
            {
                Text = "Company Information",
                Location = new Point(16, y),
                Size = new Size(390, 148),
                Font = new Font("Segoe UI", 8.5F),
                Visible = false
            };

            var lbl4 = new Label { Text = "Company Name", Location = new Point(12, 22), AutoSize = true, Font = new Font("Segoe UI", 8.5F) };
            txtCompanyName = new TextBox { Location = new Point(12, 40), Size = new Size(366, 24), Font = new Font("Segoe UI", 9F) };

            var lbl5 = new Label { Text = "Tax ID", Location = new Point(12, 70), AutoSize = true, Font = new Font("Segoe UI", 8.5F) };
            txtTaxId = new TextBox { Location = new Point(12, 88), Size = new Size(366, 24), Font = new Font("Segoe UI", 9F) };

            var lbl6 = new Label { Text = "Legal Address", Location = new Point(12, 118), AutoSize = true, Font = new Font("Segoe UI", 8.5F) };
            txtLegalAddress = new TextBox { Location = new Point(12, 136), Size = new Size(366, 24), Font = new Font("Segoe UI", 9F) };

            grpLegal.Controls.AddRange(new Control[] { lbl4, txtCompanyName, lbl5, txtTaxId, lbl6, txtLegalAddress });
            mainPanel.Controls.Add(grpLegal);
            y += 128;

            // ── Contact Info GroupBox ──
            var grpContact = new GroupBox
            {
                Text = "Contact Information",
                Location = new Point(16, y),
                Size = new Size(390, 88),
                Font = new Font("Segoe UI", 8.5F)
            };

            var lbl7 = new Label { Text = "Phone", Location = new Point(12, 22), AutoSize = true, Font = new Font("Segoe UI", 8.5F) };
            txtPhone = new TextBox { Location = new Point(12, 40), Size = new Size(175, 24), Font = new Font("Segoe UI", 9F) };

            var lbl8 = new Label { Text = "Email", Location = new Point(200, 22), AutoSize = true, Font = new Font("Segoe UI", 8.5F) };
            txtEmail = new TextBox { Location = new Point(200, 40), Size = new Size(178, 24), Font = new Font("Segoe UI", 9F) };

            grpContact.Controls.AddRange(new Control[] { lbl7, txtPhone, lbl8, txtEmail });
            mainPanel.Controls.Add(grpContact);

            // ── Buttons ──
            var btnPanel = new Panel { Dock = DockStyle.Bottom, Height = 48 };

            var btnSave = new Button
            {
                Text = existingCustomer == null ? "💾  Save" : "💾  Update",
                Size = new Size(90, 32),
                Location = new Point(330, 8),
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(26, 93, 184),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;

            var btnCancel = new Button
            {
                Text = "Cancel",
                Size = new Size(80, 32),
                Location = new Point(236, 8),
                DialogResult = DialogResult.Cancel,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F)
            };
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(170, 170, 170);

            btnPanel.Controls.AddRange(new Control[] { btnSave, btnCancel });

            this.Controls.Add(mainPanel);
            this.Controls.Add(btnPanel);
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void CmbType_Changed(object sender, EventArgs e)
        {
            bool isPhysical = cmbType.SelectedIndex == 0;
            grpPersonal.Visible = isPhysical;
            grpLegal.Visible = !isPhysical;
        }

        private void UpdateAvatar()
        {
            var f = txtFirstName.Text.Length > 0 ? txtFirstName.Text[0].ToString().ToUpper() : "";
            var l = txtLastName.Text.Length > 0 ? txtLastName.Text[0].ToString().ToUpper() : "";
            lblAvatar.Text = f + l;
            if (string.IsNullOrEmpty(lblAvatar.Text)) lblAvatar.Text = "??";
            lblTitle.Text = $"{txtFirstName.Text} {txtLastName.Text}".Trim();
            if (string.IsNullOrEmpty(lblTitle.Text)) lblTitle.Text = "New Customer";
        }

        private void PopulateFields()
        {
            if (existingCustomer.Type == CustomerType.Legal)
            {
                cmbType.SelectedIndex = 1;
                txtCompanyName.Text = existingCustomer.Name;
                txtTaxId.Text = existingCustomer.PersonalId;
            }
            else
            {
                cmbType.SelectedIndex = 0;
                var parts = existingCustomer.Name.Split(' ', 2);
                txtFirstName.Text = parts.Length > 0 ? parts[0] : "";
                txtLastName.Text = parts.Length > 1 ? parts[1] : "";
                txtPersonalId.Text = existingCustomer.PersonalId;
            }
            txtPhone.Text = existingCustomer.Phone;
            txtEmail.Text = existingCustomer.Email;
        }

        public ConcreteCustomerDto GetCustomer()
        {
            var c = new ConcreteCustomerDto();
            if (cmbType.SelectedIndex == 0) // Physical
            {
                c.Type = CustomerType.Physical;
                c.Name = $"{txtFirstName.Text} {txtLastName.Text}".Trim();
                c.PersonalId = txtPersonalId.Text;
            }
            else // Legal
            {
                c.Type = CustomerType.Legal;
                c.Name = txtCompanyName.Text;
                c.PersonalId = txtTaxId.Text;
            }
            c.Phone = txtPhone.Text;
            c.Email = txtEmail.Text;
            return c;
        }
    }
}
