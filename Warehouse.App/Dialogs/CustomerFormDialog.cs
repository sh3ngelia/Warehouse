using Warehouse.DTO.Customer;
using Warehouse.DTO.Locations;

namespace Warehouse.App.Dialogs
{
    // ═══════════════════════════════════════════════════
    // DIALOG: Customer
    // ═══════════════════════════════════════════════════
    public class CustomerFormDialog : Form
    {
        private ComboBox cmbType = null!;
        private TextBox txtFirstName = null!, txtLastName = null!, txtPersonalId = null!;
        private TextBox txtCompanyName = null!, txtAddress = null!;
        private TextBox txtPhone = null!, txtEmail = null!;
        private GroupBox grpPhysical = null!, grpLegal = null!;

        public bool IsLegal => cmbType.SelectedIndex == 1;
        public string FirstNameValue => txtFirstName.Text;
        public string LastNameValue => txtLastName.Text;
        public string PersonalIdValue => txtPersonalId.Text;
        public string CompanyNameValue => txtCompanyName.Text;
        public string AddressValue => txtAddress.Text;
        public string PhoneValue => txtPhone.Text;
        public string EmailValue => txtEmail.Text;

        public CustomerFormDialog(CustomerDto? customer, PhysicalCustomerDto? physical, LegalCustomerDto? legal)
        {
            bool isEdit = customer != null;
            this.Text = isEdit ? "Edit Customer" : "Add Customer";
            this.Size = new Size(420, 470);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(240, 240, 240);

            int y = 12;

            var grpType = new GroupBox { Text = "Customer Type", Location = new Point(12, y), Size = new Size(380, 55) };
            cmbType = new ComboBox { Location = new Point(12, 22), Size = new Size(356, 24), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbType.Items.AddRange(new object[] { "Physical Person", "Legal Entity" });
            cmbType.SelectedIndex = customer?.CustomerType == true ? 1 : 0;
            cmbType.SelectedIndexChanged += (s, e) => { grpPhysical.Visible = !IsLegal; grpLegal.Visible = IsLegal; };
            grpType.Controls.Add(cmbType);
            this.Controls.Add(grpType);
            y += 65;

            grpPhysical = new GroupBox { Text = "Personal Information", Location = new Point(12, y), Size = new Size(380, 120) };
            grpPhysical.Controls.Add(new Label { Text = "First Name", Location = new Point(12, 22), AutoSize = true });
            txtFirstName = new TextBox { Location = new Point(12, 40), Size = new Size(170, 24), Text = physical?.FirstName ?? "" };
            grpPhysical.Controls.Add(txtFirstName);
            grpPhysical.Controls.Add(new Label { Text = "Last Name", Location = new Point(196, 22), AutoSize = true });
            txtLastName = new TextBox { Location = new Point(196, 40), Size = new Size(172, 24), Text = physical?.LastName ?? "" };
            grpPhysical.Controls.Add(txtLastName);
            grpPhysical.Controls.Add(new Label { Text = "Personal ID", Location = new Point(12, 70), AutoSize = true });
            txtPersonalId = new TextBox { Location = new Point(12, 88), Size = new Size(356, 24), MaxLength = 11, Text = physical?.PersonalId ?? "" };
            grpPhysical.Controls.Add(txtPersonalId);
            this.Controls.Add(grpPhysical);

            grpLegal = new GroupBox { Text = "Company Information", Location = new Point(12, y), Size = new Size(380, 120), Visible = false };
            grpLegal.Controls.Add(new Label { Text = "Company Name", Location = new Point(12, 22), AutoSize = true });
            txtCompanyName = new TextBox { Location = new Point(12, 40), Size = new Size(356, 24), Text = legal?.Name ?? "" };
            grpLegal.Controls.Add(txtCompanyName);
            grpLegal.Controls.Add(new Label { Text = "Address", Location = new Point(12, 70), AutoSize = true });
            txtAddress = new TextBox { Location = new Point(12, 88), Size = new Size(356, 24), Text = legal?.Address ?? "" };
            grpLegal.Controls.Add(txtAddress);
            this.Controls.Add(grpLegal);
            y += 130;

            if (customer?.CustomerType == true) { grpPhysical.Visible = false; grpLegal.Visible = true; }

            var grpContact = new GroupBox { Text = "Contact", Location = new Point(12, y), Size = new Size(380, 85) };
            grpContact.Controls.Add(new Label { Text = "Phone", Location = new Point(12, 22), AutoSize = true });
            txtPhone = new TextBox { Location = new Point(12, 40), Size = new Size(170, 24), Text = customer?.Phone ?? "" };
            grpContact.Controls.Add(txtPhone);
            grpContact.Controls.Add(new Label { Text = "Email", Location = new Point(196, 22), AutoSize = true });
            txtEmail = new TextBox { Location = new Point(196, 40), Size = new Size(172, 24), Text = customer?.Email ?? "" };
            grpContact.Controls.Add(txtEmail);
            this.Controls.Add(grpContact);
            y += 95;

            var btnSave = new Button { Text = isEdit ? "Update" : "Save", Size = new Size(80, 30), Location = new Point(312, y), DialogResult = DialogResult.OK, BackColor = Color.FromArgb(26, 93, 184), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.FlatAppearance.BorderSize = 0;
            var btnCancel = new Button { Text = "Cancel", Size = new Size(80, 30), Location = new Point(224, y), DialogResult = DialogResult.Cancel };
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);
            this.AcceptButton = btnSave; this.CancelButton = btnCancel;

#if DEBUG
            txtFirstName.Text = "Test";
            txtLastName.Text = "Customer";
            txtPersonalId.Text = "12345678901";
            txtPhone.Text = "599000000";
            txtEmail.Text = "test@debug.com";
#endif

        }
    }
}