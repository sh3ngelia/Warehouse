using Warehouse.DTO.Contracts;
using Warehouse.Service;
using Warehouse.Service.Main;

namespace Warehouse.App
{
    public class ContractFormDialog : Form
    {
        private AppServices _services;
        private string _currentUser;

        // Selected IDs
        private int _customerId;
        private int _employeeId;

        // Display labels
        private Label lblCustomer = null!;
        private Label lblEmployee = null!;
        private ComboBox cmbStatus = null!;
        private DataGridView dgDetails = null!;

        public int CustomerIdValue => _customerId;
        public int EmployeeIdValue => _employeeId;
        public byte StatusValue => (byte)(cmbStatus.SelectedIndex + 1);
        public List<ContractDetailRow> Details { get; private set; } = new();

        public ContractFormDialog(ContractDto? existing, AppServices services, string currentUser)
        {
            _services = services;
            _currentUser = currentUser;
            bool isEdit = existing != null;
            _customerId = existing?.CustomerId ?? 0;
            _employeeId = existing?.EmployeeId ?? 0;

            this.Text = isEdit ? "Edit Contract" : "Add Contract";
            this.Size = new Size(640, 540);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(240, 240, 240);

            int y = 12;

            // ═══ Contract Info ═══
            var grpContract = new GroupBox { Text = "Contract Info", Location = new Point(12, y), Size = new Size(600, 115) };

            // Customer picker
            grpContract.Controls.Add(new Label { Text = "Customer", Location = new Point(12, 22), AutoSize = true });
            lblCustomer = new Label
            {
                Location = new Point(12, 42),
                Size = new Size(220, 24),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = _customerId > 0 ? GetCustomerDisplayName(_customerId) : "Click to select..."
            };
            grpContract.Controls.Add(lblCustomer);

            var btnPickCustomer = new Button
            {
                Text = "...",
                Location = new Point(234, 42),
                Size = new Size(30, 24)
            };
            btnPickCustomer.Click += (s, e) => PickCustomer();
            grpContract.Controls.Add(btnPickCustomer);

            // Employee picker
            grpContract.Controls.Add(new Label { Text = "Employee", Location = new Point(280, 22), AutoSize = true });
            lblEmployee = new Label
            {
                Location = new Point(280, 42),
                Size = new Size(220, 24),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = _employeeId > 0 ? GetEmployeeDisplayName(_employeeId) : "Click to select..."
            };
            grpContract.Controls.Add(lblEmployee);

            var btnPickEmployee = new Button
            {
                Text = "...",
                Location = new Point(502, 42),
                Size = new Size(30, 24)
            };
            btnPickEmployee.Click += (s, e) => PickEmployee();
            grpContract.Controls.Add(btnPickEmployee);

            // Status
            grpContract.Controls.Add(new Label { Text = "Status", Location = new Point(12, 76), AutoSize = true });
            cmbStatus = new ComboBox
            {
                Location = new Point(70, 74),
                Size = new Size(180, 24),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatus.Items.AddRange(new object[] { "Pending", "Active", "Completed", "Cancelled" });
            cmbStatus.SelectedIndex = existing != null ? existing.ContractStatus - 1 : 0;
            grpContract.Controls.Add(cmbStatus);

            this.Controls.Add(grpContract);
            y += 125;

            // ═══ Contract Details ═══
            var grpDetails = new GroupBox { Text = "Contract Details (Storage & Products)", Location = new Point(12, y), Size = new Size(600, 310) };

            dgDetails = new DataGridView
            {
                Location = new Point(12, 22),
                Size = new Size(576, 230),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9F),
                BackgroundColor = Color.White,
                EnableHeadersVisualStyles = false,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(240, 240, 240),
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold)
                }
            };

            // Storage column with picker
            var colStorage = new DataGridViewTextBoxColumn { Name = "StorageName", HeaderText = "Storage", ReadOnly = true };
            var colStorageId = new DataGridViewTextBoxColumn { Name = "StorageId", HeaderText = "Storage ID", Width = 70 };
            var colPrice = new DataGridViewTextBoxColumn { Name = "Price", HeaderText = "Price" };
            var colProduct = new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "Product", ReadOnly = true };
            var colProductId = new DataGridViewTextBoxColumn { Name = "ProductId", HeaderText = "Product ID", Width = 70 };
            var colQuantity = new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Quantity" };
            var colPickStorage = new DataGridViewButtonColumn { Name = "PickStorage", HeaderText = "", Text = "...", UseColumnTextForButtonValue = true, Width = 30 };
            var colPickProduct = new DataGridViewButtonColumn { Name = "PickProduct", HeaderText = "", Text = "...", UseColumnTextForButtonValue = true, Width = 30 };

            var colSId = new DataGridViewTextBoxColumn { Name = "StorageId", Visible = false };
            var colPId = new DataGridViewTextBoxColumn { Name = "ProductId", Visible = false };

            dgDetails.Columns.AddRange(new DataGridViewColumn[] {
                colStorageName(), colPickStorage, colSId,
                new DataGridViewTextBoxColumn { Name = "Price", HeaderText = "Price" },
                colProductName(), colPickProduct, colPId,
                new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Quantity" }
            });

            dgDetails.CellContentClick += DgDetails_CellClick;

            grpDetails.Controls.Add(dgDetails);

            var btnAddRow = new Button { Text = "+ Add Row", Location = new Point(12, 262), Size = new Size(90, 28), FlatStyle = FlatStyle.Flat };
            btnAddRow.Click += (s, e) => dgDetails.Rows.Add("Click ...", "", "", "", "Click ...", "", "", "");

            var btnRemoveRow = new Button { Text = "- Remove", Location = new Point(110, 262), Size = new Size(90, 28), FlatStyle = FlatStyle.Flat, ForeColor = Color.FromArgb(183, 28, 28) };
            btnRemoveRow.Click += (s, e) =>
            {
                if (dgDetails.SelectedRows.Count > 0 && dgDetails.Rows.Count > 0)
                    dgDetails.Rows.RemoveAt(dgDetails.SelectedRows[0].Index);
            };

            grpDetails.Controls.Add(btnAddRow);
            grpDetails.Controls.Add(btnRemoveRow);
            this.Controls.Add(grpDetails);
            y += 320;

            // ═══ Buttons ═══
            var btnSave = new Button
            {
                Text = isEdit ? "Update" : "Save",
                Size = new Size(80, 32),
                Location = new Point(532, y),
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(26, 93, 184),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += (s, e) => CollectDetails();

            var btnCancel = new Button { Text = "Cancel", Size = new Size(80, 32), Location = new Point(444, y), DialogResult = DialogResult.Cancel };
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;

            // Default row
            if (!isEdit)
                dgDetails.Rows.Add("Click ...", "", "", "", "Click ...", "", "", "");

#if DEBUG
            if (!isEdit)
            {
                cmbStatus.SelectedIndex = 0;
            }
#endif

        }

        // ═══ Column helpers ═══
        private DataGridViewTextBoxColumn colStorageName() => new() { Name = "StorageName", HeaderText = "Storage", ReadOnly = true };
        private DataGridViewTextBoxColumn colProductName() => new() { Name = "ProductName", HeaderText = "Product", ReadOnly = true };

        // ═══ PICKER: Customer ═══
        private void PickCustomer()
        {
            try
            {
                var customers = _services.Customers.GetAll(s => _currentUser == "admin" || s.IsDeleted == false).ToList();

                var displayList = new List<object>();
                foreach (var c in customers)
                {
                    string name = "";
                    if (!c.CustomerType)
                    {
                        var p = _services.Customers.GetPhysical(c.CustomerId ?? 0);
                        name = p != null ? $"{p.FirstName} {p.LastName}" : "Unknown";
                    }
                    else
                    {
                        var l = _services.Customers.GetLegal(c.CustomerId ?? 0);
                        name = l?.Name ?? "Unknown";
                    }

                    displayList.Add(new
                    {
                        CustomerId = c.CustomerId ?? 0,
                        Name = name,
                        Type = c.CustomerType ? "Legal" : "Physical",
                        Phone = c.Phone,
                        Email = c.Email
                    });
                }

                using var picker = new SearchPickerDialog("Select Customer", displayList, "CustomerId");
                if (picker.ShowDialog(this) == DialogResult.OK)
                {
                    _customerId = picker.SelectedId;
                    dynamic sel = picker.SelectedItem!;
                    lblCustomer.Text = $"[{_customerId}] {sel.Name}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}");
            }
        }

        // ═══ PICKER: Employee ═══
        private void PickEmployee()
        {
            try
            {
                var displayList = _services.Employees.GetAll(s => _currentUser == "admin" || s.IsDeleted == false)
                    .Select(e => (object)new
                    {
                        EmployeeId = e.EmployeeId,
                        Name = $"{e.FirstName} {e.LastName}",
                        Phone = e.Phone,
                        Email = e.Email
                    }).ToList();

                using var picker = new SearchPickerDialog("Select Employee", displayList, "EmployeeId");
                if (picker.ShowDialog(this) == DialogResult.OK)
                {
                    _employeeId = picker.SelectedId;
                    dynamic sel = picker.SelectedItem!;
                    lblEmployee.Text = $"[{_employeeId}] {sel.Name}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees: {ex.Message}");
            }
        }


        // ═══ Grid cell click — Storage & Product pickers ═══
        private void DgDetails_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var colName = dgDetails.Columns[e.ColumnIndex].Name;

            if (colName == "PickStorage")
            {
                PickStorage(e.RowIndex);
            }
            else if (colName == "PickProduct")
            {
                PickProduct(e.RowIndex);
            }
        }

        // ═══ PICKER: Storage ═══
        private void PickStorage(int rowIndex)
        {
            try
            {
                var storages = _services.Storages.GetAll(s => _currentUser == "admin" || s.IsDeleted == false).ToList();

                var displayList = storages.Select(s => (object)new
                {
                    StorageId = s.StorageId ?? 0,
                    Name = s.Name,
                    Address = s.Address,
                    Capacity = s.Capacity,
                    Price = s.Price
                }).ToList();

                using var picker = new SearchPickerDialog("Select Storage", displayList, "StorageId");
                if (picker.ShowDialog(this) == DialogResult.OK)
                {
                    dynamic sel = picker.SelectedItem!;
                    dgDetails.Rows[rowIndex].Cells["StorageName"].Value = $"[{sel.StorageId}] {sel.Name}";
                    dgDetails.Rows[rowIndex].Cells["StorageId"].Value = sel.StorageId;
                    dgDetails.Rows[rowIndex].Cells["Price"].Value = sel.Price;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading storages: {ex.Message}");
            }
        }

        // ═══ PICKER: Product ═══
        private void PickProduct(int rowIndex)
        {
            try
            {
                var products = _services.Products.GetAll(p => _currentUser == "admin" || p.IsDeleted == false).ToList();

                var displayList = products.Select(p => (object)new
                {
                    ProductId = p.ProductId ?? 0,
                    Name = p.Name,
                    SKU = p.SKU,
                    Description = p.Description ?? ""
                }).ToList();

                using var picker = new SearchPickerDialog("Select Product", displayList, "ProductId");
                if (picker.ShowDialog(this) == DialogResult.OK)
                {
                    dynamic sel = picker.SelectedItem!;
                    dgDetails.Rows[rowIndex].Cells["ProductName"].Value = $"[{sel.ProductId}] {sel.Name}";
                    dgDetails.Rows[rowIndex].Cells["ProductId"].Value = sel.ProductId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}");
            }
        }

        // ═══ Collect details ═══
        private void CollectDetails()
        {
            Details.Clear();
            foreach (DataGridViewRow row in dgDetails.Rows)
            {
                if (row.IsNewRow) continue;
                Details.Add(new ContractDetailRow
                {
                    StorageId = int.TryParse(row.Cells["StorageId"].Value?.ToString(), out var s) ? s : 0,
                    Price = decimal.TryParse(row.Cells["Price"].Value?.ToString(), out var p) ? p : 0,
                    ProductId = int.TryParse(row.Cells["ProductId"].Value?.ToString(), out var pr) ? pr : 0,
                    Quantity = int.TryParse(row.Cells["Quantity"].Value?.ToString(), out var q) ? q : 0
                });
            }
        }

        // ═══ Display name helpers ═══
        private string GetCustomerDisplayName(int id)
        {
            try
            {
                var c = _services.Customers.Get(id);
                if (c == null) return $"[{id}]";
                if (!c.CustomerType)
                {
                    var p = _services.PhysicalCustomers.Get(id);
                    return p != null ? $"[{id}] {p.FirstName} {p.LastName}" : $"[{id}]";
                }
                else
                {
                    var l = _services.LegalCustomers.Get(id);
                    return l != null ? $"[{id}] {l.Name}" : $"[{id}]";
                }
            }
            catch { return $"[{id}]"; }
        }

        private string GetEmployeeDisplayName(int id)
        {
            try
            {
                var e = _services.Employees.Get(id);
                return e != null ? $"[{id}] {e.FirstName} {e.LastName}" : $"[{id}]";
            }
            catch { return $"[{id}]"; }
        }
    }

    // ═══ Helper class ═══
    public class ContractDetailRow
    {
        public int StorageId { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}