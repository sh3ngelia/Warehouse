using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Warehouse.DTO.Lookups;
using Warehouse.DTO.Users;
using Warehouse.Service;
using Warehouse.Service.Main;

namespace Warehouse.App
{
    public class EmployeeEditDialog : Form
    {
        private readonly AppServices _services;
        private readonly EmployeeDto _employee;

        private TextBox txtPersonalId = null!, txtFirstName = null!, txtLastName = null!;
        private TextBox txtPhone = null!, txtEmail = null!;
        private CheckedListBox chkRoles = null!;
        private List<RoleDto> allRoles = new();
        private List<int> currentRoleIds = new();
        private int? userId;

        public EmployeeEditDialog(EmployeeDto employee, AppServices services)
        {
            _employee = employee;
            _services = services;

            this.Text = "Edit Employee";
            this.Size = new Size(440, 560);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(240, 240, 240);

            LoadRoleData();
            BuildUI();
        }

        private void LoadRoleData()
        {
            try
            {
                allRoles = _services.Roles.GetAll(r => r.RoleId > 0).Where(r => r.IsDeleted == false).ToList();

                // User-ს ვპოულობთ Employee-ის მიხედვით
                var users = _services.Users.GetAll(u => u.EmployeeId > 0).Where(u => u.EmployeeId == _employee.EmployeeId && u.IsDeleted == false).ToList();
                if (users.Any())
                {
                    userId = users.First().EmployeeId;
                    // მიმდინარე როლები
                    currentRoleIds = _services.Roles.GetRolesForUser(userId.Value).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading role data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuildUI()
        {
            int y = 12;

            // ═══ Personal Info ═══
            var grpPersonal = new GroupBox { Text = "Personal Information", Location = new Point(12, y), Size = new Size(400, 120) };
            grpPersonal.Controls.Add(new Label { Text = "First Name", Location = new Point(12, 22), AutoSize = true });
            txtFirstName = new TextBox { Location = new Point(12, 40), Size = new Size(185, 24), Text = _employee.FirstName };
            grpPersonal.Controls.Add(txtFirstName);
            grpPersonal.Controls.Add(new Label { Text = "Last Name", Location = new Point(205, 22), AutoSize = true });
            txtLastName = new TextBox { Location = new Point(205, 40), Size = new Size(183, 24), Text = _employee.LastName };
            grpPersonal.Controls.Add(txtLastName);
            grpPersonal.Controls.Add(new Label { Text = "Personal ID", Location = new Point(12, 70), AutoSize = true });
            txtPersonalId = new TextBox { Location = new Point(12, 88), Size = new Size(376, 24), MaxLength = 11, Text = _employee.PersonalId };
            grpPersonal.Controls.Add(txtPersonalId);
            this.Controls.Add(grpPersonal);
            y += 130;

            // ═══ Contact ═══
            var grpContact = new GroupBox { Text = "Contact", Location = new Point(12, y), Size = new Size(400, 85) };
            grpContact.Controls.Add(new Label { Text = "Phone", Location = new Point(12, 22), AutoSize = true });
            txtPhone = new TextBox { Location = new Point(12, 40), Size = new Size(185, 24), Text = _employee.Phone };
            grpContact.Controls.Add(txtPhone);
            grpContact.Controls.Add(new Label { Text = "Email", Location = new Point(205, 22), AutoSize = true });
            txtEmail = new TextBox { Location = new Point(205, 40), Size = new Size(183, 24), Text = _employee.Email };
            grpContact.Controls.Add(txtEmail);
            this.Controls.Add(grpContact);
            y += 95;

            // ═══ Roles ═══
            var grpRoles = new GroupBox { Text = "Assigned Roles", Location = new Point(12, y), Size = new Size(400, 120) };

            if (userId == null)
            {
                grpRoles.Controls.Add(new Label
                {
                    Text = "This employee has no user account.",
                    Location = new Point(12, 28),
                    AutoSize = true,
                    ForeColor = Color.FromArgb(183, 28, 28)
                });
            }
            else
            {
                chkRoles = new CheckedListBox
                {
                    Location = new Point(12, 22),
                    Size = new Size(376, 88),
                    CheckOnClick = true,
                    BorderStyle = BorderStyle.FixedSingle
                };

                for (int i = 0; i < allRoles.Count; i++)
                {
                    bool isChecked = currentRoleIds.Contains(allRoles[i].RoleId ?? 0);
                    chkRoles.Items.Add(allRoles[i].Name, isChecked);
                }

                grpRoles.Controls.Add(chkRoles);
            }

            this.Controls.Add(grpRoles);
            y += 130;

            // ═══ Buttons ═══
            var btnSave = new Button
            {
                Text = "Update",
                Size = new Size(80, 30),
                Location = new Point(332, y),
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(26, 93, 184),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += (s, e) => SaveChanges();

            var btnCancel = new Button
            {
                Text = "Cancel",
                Size = new Size(80, 30),
                Location = new Point(244, y),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void SaveChanges()
        {
            try
            {
                // Employee update
                _employee.PersonalId = txtPersonalId.Text;
                _employee.FirstName = txtFirstName.Text;
                _employee.LastName = txtLastName.Text;
                _employee.Phone = txtPhone.Text;
                _employee.Email = txtEmail.Text;
                _services.Employees.Update(_employee);

                // Role changes
                if (userId != null && chkRoles != null)
                {
                    var rolePermSvc = _services.Roles;

                    // Remove all old roles
                    foreach (var oldRoleId in currentRoleIds)
                        rolePermSvc.UnassignRoleFromUser(userId.Value, oldRoleId);

                    // Assign all checked roles
                    for (int i = 0; i < chkRoles.Items.Count; i++)
                    {
                        if (chkRoles.GetItemChecked(i))
                        {
                            int roleId = allRoles[i].RoleId ?? 0;
                            rolePermSvc.AssignRoleToUser(userId.Value, roleId);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}