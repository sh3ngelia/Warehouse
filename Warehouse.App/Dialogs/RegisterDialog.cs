using System.Security.Cryptography;
using System.Text;
using Warehouse.DTO.Users;
using Warehouse.Service;
using Warehouse.Service.Main;

namespace Warehouse.App.Dialogs;

public class RegisterDialog : Form
{
    private readonly AppServices _services;
    private TextBox txtPersonalId = null!, txtFirstName = null!, txtLastName = null!;
    private TextBox txtPhone = null!, txtEmail = null!;
    private TextBox txtUsername = null!, txtPassword = null!, txtConfirm = null!;

    public RegisterDialog(AppServices services)
    {
        _services = services;
        this.Text = "Register";
        this.Size = new Size(380, 440);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false; this.MinimizeBox = false;
        this.StartPosition = FormStartPosition.CenterParent;
        this.Font = new Font("Segoe UI", 9F);
        this.BackColor = Color.FromArgb(240, 240, 240);

        int y = 12;

        // Employee info
        var grpEmp = new GroupBox { Text = "Employee Info", Location = new Point(12, y), Size = new Size(340, 150) };
        grpEmp.Controls.Add(new Label { Text = "First Name", Location = new Point(12, 22), AutoSize = true });
        txtFirstName = new TextBox { Location = new Point(12, 40), Size = new Size(155, 24) };
        grpEmp.Controls.Add(txtFirstName);
        grpEmp.Controls.Add(new Label { Text = "Last Name", Location = new Point(175, 22), AutoSize = true });
        txtLastName = new TextBox { Location = new Point(175, 40), Size = new Size(153, 24) };
        grpEmp.Controls.Add(txtLastName);
        grpEmp.Controls.Add(new Label { Text = "Personal ID", Location = new Point(12, 70), AutoSize = true });
        txtPersonalId = new TextBox { Location = new Point(12, 88), Size = new Size(316, 24), MaxLength = 11 };
        grpEmp.Controls.Add(txtPersonalId);
        grpEmp.Controls.Add(new Label { Text = "Phone", Location = new Point(12, 116), AutoSize = true });
        txtPhone = new TextBox { Location = new Point(60, 114), Size = new Size(107, 24) };
        grpEmp.Controls.Add(txtPhone);
        grpEmp.Controls.Add(new Label { Text = "Email", Location = new Point(175, 116), AutoSize = true });
        txtEmail = new TextBox { Location = new Point(210, 114), Size = new Size(118, 24) };
        grpEmp.Controls.Add(txtEmail);
        this.Controls.Add(grpEmp);
        y += 160;

        // User credentials
        var grpUser = new GroupBox { Text = "Login Credentials", Location = new Point(12, y), Size = new Size(340, 120) };
        grpUser.Controls.Add(new Label { Text = "Username", Location = new Point(12, 22), AutoSize = true });
        txtUsername = new TextBox { Location = new Point(12, 40), Size = new Size(316, 24) };
        grpUser.Controls.Add(txtUsername);
        grpUser.Controls.Add(new Label { Text = "Password", Location = new Point(12, 70), AutoSize = true });
        txtPassword = new TextBox { Location = new Point(12, 88), Size = new Size(150, 24), UseSystemPasswordChar = true };
        grpUser.Controls.Add(txtPassword);
        grpUser.Controls.Add(new Label { Text = "Confirm", Location = new Point(170, 70), AutoSize = true });
        txtConfirm = new TextBox { Location = new Point(170, 88), Size = new Size(158, 24), UseSystemPasswordChar = true };
        grpUser.Controls.Add(txtConfirm);
        this.Controls.Add(grpUser);
        y += 130;

        // Buttons
        var btnSave = new Button
        {
            Text = "Register",
            Size = new Size(90, 30),
            Location = new Point(262, y),
            BackColor = Color.FromArgb(26, 93, 184),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Click += BtnSave_Click;

        var btnCancel = new Button
        {
            Text = "Cancel",
            Size = new Size(80, 30),
            Location = new Point(174, y),
            DialogResult = DialogResult.Cancel
        };

        this.Controls.Add(btnSave);
        this.Controls.Add(btnCancel);
        this.CancelButton = btnCancel;

#if DEBUG
        txtFirstName.Text = "Test";
        txtLastName.Text = "Employee";
        txtPersonalId.Text = "99988877766";
        txtPhone.Text = "599111222";
        txtEmail.Text = "test.emp@debug.com";
        txtUsername.Text = "testuser";
        txtPassword.Text = "test1234";
        txtConfirm.Text = "test1234";
#endif

    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
        {
            MessageBox.Show("First name and last name are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(txtUsername.Text))
        {
            MessageBox.Show("Username is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (txtPassword.Text != txtConfirm.Text)
        {
            MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (txtPassword.Text.Length < 4)
        {
            MessageBox.Show("Password must be at least 4 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var person = new PersonRegistrationDto
            {
                PersonalId = txtPersonalId.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text,
                Username = txtUsername.Text,
                Password = SHA256.HashData(Encoding.UTF8.GetBytes(txtPassword.Text))
            };

            var id = _services.Users.RegisterEmployee(person);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Registration failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}