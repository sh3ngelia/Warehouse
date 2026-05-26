using System.Text;
using Warehouse.App.Dialogs;
using Warehouse.DTO.Contracts;
using Warehouse.DTO.Customer;
using Warehouse.DTO.Locations;
using Warehouse.DTO.Lookups;
using Warehouse.DTO.Products;
using Warehouse.DTO.Storage;
using Warehouse.DTO.Users;
using Warehouse.Service;
using Warehouse.Service.Abstracts;
using Warehouse.Service.Contracts;
using Warehouse.Service.Factory;
using Warehouse.Service.Main;

namespace Warehouse.App.Forms
{
    public class CustomerManagementForm : Form
    {
        #region Fields

        private readonly AppServices _services;

        private string currentSection = "customers";

        // Controls
        private MenuStrip menuStrip = null!;
        private ToolStrip toolStrip = null!;
        private SplitContainer splitContainer = null!;
        private TreeView treeView = null!;
        private TabControl tabControl = null!;
        private DataGridView dataGrid = null!;
        private TextBox searchBox = null!;
        private ComboBox statusFilter = null!;
        private StatusStrip statusStrip = null!;
        private ToolStripStatusLabel statusConnection = null!, statusRecords = null!, statusSelection = null!;
        private ToolStripButton btnAdd = null!;
        public bool LoggedOut { get; private set; } = false;


        #endregion

        #region Constructor

        public CustomerManagementForm(AppServices service)
        {
            _services = service;
            InitializeForm();
            BuildMenuStrip();
            BuildToolStrip();
            BuildLayout();
            BuildStatusStrip();
            LoadGridForSection("customers");
        }

        #endregion

        #region Form Setup
        private void InitializeForm()
        {
            this.Text = "Warehouse — Customer Management";
            this.Size = new Size(1100, 700);
            this.MinimumSize = new Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(240, 240, 240);
        }

        #endregion

        #region Menu Strip

        private void BuildMenuStrip()
        {
            menuStrip = new MenuStrip { BackColor = Color.FromArgb(245, 245, 245) };

            var fileMenu = new ToolStripMenuItem("File");
            fileMenu.DropDownItems.Add("New", null, (s, e) => OpenAddDialog());
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add("Logout", null, (s, e) => Logout());
            fileMenu.DropDownItems.Add("Exit", null, (s, e) => Close());

            var editMenu = new ToolStripMenuItem("Edit");
            editMenu.DropDownItems.Add("Edit Selected", null, (s, e) => OpenEditDialog());
            editMenu.DropDownItems.Add("Delete Selected", null, (s, e) => DeleteSelected());

            var viewMenu = new ToolStripMenuItem("View");
            viewMenu.DropDownItems.Add("Refresh", null, (s, e) => LoadGridForSection(currentSection));

            var helpMenu = new ToolStripMenuItem("Help");
            helpMenu.DropDownItems.Add("About", null, (s, e) =>
                MessageBox.Show("Warehouse Management v1.0", "About", MessageBoxButtons.OK, MessageBoxIcon.Information));

            menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu, editMenu, viewMenu, helpMenu });
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }

        #endregion

        #region Tool Strip

        private void BuildToolStrip()
        {
            toolStrip = new ToolStrip { GripStyle = ToolStripGripStyle.Hidden, BackColor = Color.FromArgb(245, 245, 245) };

            btnAdd = new ToolStripButton("  Add Customer") { DisplayStyle = ToolStripItemDisplayStyle.ImageAndText };
            btnAdd.Click += (s, e) => OpenAddDialog();

            var btnEdit = new ToolStripButton("  Edit") { DisplayStyle = ToolStripItemDisplayStyle.ImageAndText };
            btnEdit.Click += (s, e) => OpenEditDialog();

            var btnDelete = new ToolStripButton("  Delete") { DisplayStyle = ToolStripItemDisplayStyle.ImageAndText };
            btnDelete.Click += (s, e) => DeleteSelected();

            var btnRefresh = new ToolStripButton("  Refresh") { DisplayStyle = ToolStripItemDisplayStyle.ImageAndText };
            btnRefresh.Click += (s, e) => LoadGridForSection(currentSection);

            var btnLogout = new ToolStripButton("  Logout") { DisplayStyle = ToolStripItemDisplayStyle.ImageAndText };
            btnLogout.Click += (s, e) => Logout();
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(btnLogout);


            toolStrip.Items.AddRange(new ToolStripItem[] {
                btnAdd, new ToolStripSeparator(), btnEdit, btnDelete, new ToolStripSeparator(), btnRefresh
            });
            this.Controls.Add(toolStrip);
        }

        private void Logout()
        {
            try
            {
                _services.Authorization.Logout(LocalStorage.LoggedUserId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Logout error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LocalStorage.LoggedUserId = 0;
            LocalStorage.LoggedUsername = string.Empty;
            LoggedOut = true;
            this.Close();
        }

        #endregion

        #region Layout

        private void BuildLayout()
        {
            splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                SplitterDistance = 10,
                Panel1MinSize = 10,
                BorderStyle = BorderStyle.FixedSingle
            };

            BuildTreeView();
            splitContainer.Panel1.Controls.Add(treeView);

            tabControl = new TabControl { Dock = DockStyle.Top, Height = 30, Font = new Font("Segoe UI", 8.5F) };
            tabControl.SelectedIndexChanged += (s, e) => LoadGridForSection(currentSection);

            var filterPanel = BuildFilterPanel();
            BuildDataGrid();

            var gridPanel = new Panel { Dock = DockStyle.Fill };
            gridPanel.Controls.Add(dataGrid);
            gridPanel.Controls.Add(filterPanel);
            gridPanel.Controls.Add(tabControl);

            splitContainer.Panel2.Controls.Add(gridPanel);
            this.Controls.Add(splitContainer);
            splitContainer.BringToFront();
        }

        private void BuildTreeView()
        {
            treeView = new TreeView
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                ShowLines = false,
                ShowPlusMinus = false,
                ShowRootLines = false,
                Font = new Font("Segoe UI", 9.5F),
                ItemHeight = 30,
                BackColor = Color.White,
                FullRowSelect = true,
                HideSelection = false
            };

            var custNode = treeView.Nodes.Add("customers", "Customers");
            treeView.Nodes.Add("contracts", "Contracts");
            treeView.Nodes.Add("products", "Products");

            try
            {
                _services.Roles.GetRolesForUser(LocalStorage.LoggedUserId).ToList().ForEach(roleId =>
                {
                    var role = _services.Roles.Get(roleId);
                    if (role != null && role.Name == "Admin")
                    {
                        treeView.Nodes.Add("employees", "Employees");
                        treeView.Nodes.Add("categories", "Categories");
                        treeView.Nodes.Add("regions", "Regions");
                        treeView.Nodes.Add("cities", "Cities");
                        treeView.Nodes.Add("permissions", "Permissions");
                        treeView.Nodes.Add("roles", "Roles");
                        treeView.Nodes.Add("storages", "Storages");
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user roles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            treeView.SelectedNode = custNode;
            treeView.AfterSelect += TreeView_AfterSelect;
        }

        private Panel BuildFilterPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 46,
                Padding = new Padding(10, 10, 10, 6),
                BackColor = Color.FromArgb(245, 245, 245)
            };

            statusFilter = new ComboBox
            {
                Dock = DockStyle.Right,
                Width = 90,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 8.5F)
            };
            statusFilter.Items.AddRange(new object[] { "All", "Active", "Deleted" });
            statusFilter.SelectedIndex = 0;
            statusFilter.SelectedIndexChanged += (s, e) => LoadGridForSection(currentSection);

            searchBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F),
                PlaceholderText = "Search..."
            };
            searchBox.TextChanged += (s, e) => LoadGridForSection(currentSection);

            var spacer = new Panel { Dock = DockStyle.Right, Width = 6 };
            panel.Controls.Add(searchBox);
            panel.Controls.Add(spacer);
            panel.Controls.Add(statusFilter);
            return panel;
        }

        private void BuildDataGrid()
        {
            dataGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                BackgroundColor = Color.White,
                GridColor = Color.FromArgb(232, 232, 232),
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 32,
                Font = new Font("Segoe UI", 9F),
                EnableHeadersVisualStyles = false,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    SelectionBackColor = Color.FromArgb(204, 224, 255),
                    SelectionForeColor = Color.Black
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(240, 240, 240),
                    ForeColor = Color.FromArgb(51, 51, 51),
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold)
                }
            };
            dataGrid.RowTemplate.Height = 30;
            dataGrid.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) OpenEditDialog(); };
            dataGrid.CellFormatting += DataGrid_CellFormatting;
        }

        private void BuildStatusStrip()
        {
            statusStrip = new StatusStrip { BackColor = Color.FromArgb(232, 232, 232) };
            statusConnection = new ToolStripStatusLabel("● Connected") { ForeColor = Color.FromArgb(39, 174, 96), Font = new Font("Segoe UI", 8.5F) };
            statusRecords = new ToolStripStatusLabel("0 records") { Font = new Font("Segoe UI", 8.5F) };
            statusSelection = new ToolStripStatusLabel("") { Spring = true, TextAlign = ContentAlignment.MiddleRight, Font = new Font("Segoe UI", 8.5F) };
            statusStrip.Items.AddRange(new ToolStripItem[] { statusConnection, statusRecords, statusSelection });
            this.Controls.Add(statusStrip);
        }

        #endregion

        #region Tree View Selection

        private void TreeView_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            currentSection = e.Node.Name;
            string label = e.Node.Text;

            btnAdd.Text = $"  Add {label}";
            this.Text = $"Warehouse — {label} Management";

            tabControl.TabPages.Clear();
            switch (currentSection)
            {
                case "customers":
                    tabControl.TabPages.Add("all", "All Customers");
                    tabControl.TabPages.Add("physical", "Physical");
                    tabControl.TabPages.Add("legal", "Legal");
                    break;
                case "contracts":
                    tabControl.TabPages.Add("all", "All Contracts");
                    tabControl.TabPages.Add("pending", "Pending");
                    tabControl.TabPages.Add("active", "Active");
                    tabControl.TabPages.Add("completed", "Completed");
                    break;
                case "employees":
                    tabControl.TabPages.Add("all", "All Employees");
                    break;
                case "categories":
                    tabControl.TabPages.Add("all", "All Categories");
                    break;
                case "permissions":
                    tabControl.TabPages.Add("all", "All Permissions");
                    break;
                case "storages":
                    tabControl.TabPages.Add("all", "All Storages");
                    tabControl.TabPages.Add("available", "Available");
                    tabControl.TabPages.Add("rented", "Rented");
                    tabControl.TabPages.Add("maintenance", "Maintenance");
                    break;
                default:
                    tabControl.TabPages.Add("all", $"All {label}");
                    break;
            }
            LoadGridForSection(currentSection);
        }

        #endregion

        #region Generic Grid Loader

        private void LoadGrid<T>(IEnumerable<T> data, string search, string[]? hideColumns = null) where T : class
        {
            dataGrid.Columns.Clear();
            dataGrid.Rows.Clear();

            var properties = typeof(T).GetProperties()
                .Where(p => hideColumns == null || !hideColumns.Contains(p.Name))
                .ToArray();

            foreach (var prop in properties)
                dataGrid.Columns.Add(prop.Name, FormatHeader(prop.Name));

            if (!string.IsNullOrEmpty(search))
                data = data.Where(item => properties.Any(p =>
                    (p.GetValue(item)?.ToString()?.ToLower() ?? "").Contains(search)));

            foreach (var item in data)
            {
                var values = properties.Select(p =>
                {
                    var val = p.GetValue(item);
                    if (val is DateTime dt) return dt.ToString("yyyy-MM-dd");
                    if (val is bool b) return b ? "Yes" : "No";
                    return val?.ToString() ?? "";
                }).ToArray();
                dataGrid.Rows.Add(values);
            }

            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            statusRecords.Text = $"{dataGrid.Rows.Count} records found";
        }

        private string FormatHeader(string name)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < name.Length; i++)
            {
                if (i > 0 && char.IsUpper(name[i])) sb.Append(' ');
                sb.Append(name[i]);
            }
            return sb.ToString();
        }

        #endregion

        #region Load Grid For Section

        private void LoadGridForSection(string section)
        {
            string search = searchBox?.Text?.ToLower() ?? "";
            int tabIdx = tabControl?.SelectedIndex ?? 0;
            int statusIdx = statusFilter?.SelectedIndex ?? 0; // 0=All, 1=Active, 2=Deleted

            try
            {

                switch (section)
                {
                    case "customers": LoadCustomersGrid(search, tabIdx, statusIdx); break;
                    case "contracts": LoadContractsGrid(search, tabIdx); break;
                    case "storages": LoadStoragesGrid(search, tabIdx, statusIdx); break;
                    case "regions":
                        LoadGrid(_services.Regions.GetAll(d =>
                            statusIdx == 0
                            || (statusIdx == 1 && d.IsDeleted != true)
                            || (statusIdx == 2 && d.IsDeleted == true)), search); break;
                    case "cities": LoadGrid(_services.Regions.GetAllCities(), search); break;
                    case "employees":
                        LoadGrid(_services.Employees.GetAll(d =>
                            statusIdx == 0
                            || (statusIdx == 1 && d.IsDeleted != true)
                            || (statusIdx == 2 && d.IsDeleted == true)), search); break;
                    case "categories":
                        LoadGrid(_services.Categories.GetAll(d =>
                            statusIdx == 0
                            || (statusIdx == 1 && d.IsDeleted != true)
                            || (statusIdx == 2 && d.IsDeleted == true)), search); break;
                    case "roles":
                        LoadGrid(_services.Roles.GetAll(d =>
                            statusIdx == 0
                            || (statusIdx == 1 && d.IsDeleted != true)
                            || (statusIdx == 2 && d.IsDeleted == true)), search); break;
                    case "products":
                        LoadGrid(_services.Products.GetAll(d =>
                            statusIdx == 0
                            || (statusIdx == 1 && d.IsDeleted != true)
                            || (statusIdx == 2 && d.IsDeleted == true)), search); break;
                    case "permissions":
                        LoadGrid(_services.Permissions.GetAll(d =>
                            statusIdx == 0
                            || (statusIdx == 1 && d.IsDeleted != true)
                            || (statusIdx == 2 && d.IsDeleted == true)), search); break;
                }

                statusConnection.Text = "● Connected";
                statusConnection.ForeColor = Color.FromArgb(39, 174, 96);
            }
            catch (Exception ex)
            {
                statusConnection.Text = "● Disconnected";
                statusConnection.ForeColor = Color.FromArgb(183, 28, 28);
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Custom Grid Loaders (Customers, Contracts, Storages)

        private void LoadCustomersGrid(string search, int tabIdx, int statusIdx)
        {
            dataGrid.Columns.Clear();
            dataGrid.Rows.Clear();

            dataGrid.Columns.Add("CustomerId", "ID");
            dataGrid.Columns.Add("Type", "Type");
            dataGrid.Columns.Add("Name", "Name");
            dataGrid.Columns.Add("Phone", "Phone");
            dataGrid.Columns.Add("Email", "Email");
            dataGrid.Columns.Add("IdNumber", "Personal / Tax ID");
            dataGrid.Columns.Add("CreateDate", "Created");
            if (LocalStorage.LoggedUsername == "admin") dataGrid.Columns.Add("Status", "Status");

            bool isAdmin = LocalStorage.LoggedUsername == "admin";
            var customers = _services.Customers.GetAll(d =>
                statusIdx == 0 ||
                (statusIdx == 1 && d.IsDeleted != true) ||
                (statusIdx == 2 && d.IsDeleted == true)).ToList();

            if (tabIdx == 1) customers = customers.Where(c => !c.CustomerType).ToList();
            if (tabIdx == 2) customers = customers.Where(c => c.CustomerType).ToList();

            foreach (var c in customers)
            {
                string name = "", idNumber = "";

                if (!c.CustomerType)
                {
                    var p = _services.Customers.GetPhysical(c.CustomerId ?? 0);
                    if (p != null) { name = $"{p.FirstName} {p.LastName}"; idNumber = p.PersonalId; }
                }
                else
                {
                    var l = _services.Customers.GetLegal(c.CustomerId ?? 0);
                    if (l != null) { name = l.Name; }
                }

                if (!string.IsNullOrEmpty(search) &&
                    !name.ToLower().Contains(search) &&
                    !(c.Email?.ToLower().Contains(search) ?? false) &&
                    !(c.Phone?.ToLower().Contains(search) ?? false))
                    continue;

                dataGrid.Rows.Add(c.CustomerId, c.CustomerType ? "Legal" : "Physical",
                    name, c.Phone, c.Email, idNumber,
                    c.CreateDate?.ToString("yyyy-MM-dd"),
                    c.IsDeleted == true ? "Deleted" : "Active");
            }
            statusRecords.Text = $"{dataGrid.Rows.Count} records found";
        }

        private void LoadContractsGrid(string search, int tabIdx)
        {
            dataGrid.Columns.Clear();
            dataGrid.Rows.Clear();

            dataGrid.Columns.Add("ContractId", "ID");
            dataGrid.Columns.Add("CustomerId", "Customer ID");
            dataGrid.Columns.Add("EmployeeId", "Employee ID");
            dataGrid.Columns.Add("Status", "Status");
            dataGrid.Columns.Add("CreateDate", "Created");

            var contracts = _services.Contracts.GetAll(d => d.ContractId > 0).ToList();

            if (tabIdx == 1) contracts = contracts.Where(c => c.ContractStatus == 1).ToList();
            if (tabIdx == 2) contracts = contracts.Where(c => c.ContractStatus == 2).ToList();
            if (tabIdx == 3) contracts = contracts.Where(c => c.ContractStatus == 3).ToList();

            foreach (var c in contracts)
            {
                string st = c.ContractStatus switch { 1 => "Pending", 2 => "Active", 3 => "Completed", 4 => "Cancelled", _ => "Unknown" };
                if (!string.IsNullOrEmpty(search) && !st.ToLower().Contains(search) && !c.ContractId.ToString()!.Contains(search)) continue;
                dataGrid.Rows.Add(c.ContractId, c.CustomerId, c.EmployeeId, st, c.CreateDate?.ToString("yyyy-MM-dd"));
            }
            statusRecords.Text = $"{dataGrid.Rows.Count} records found";
        }

        private void LoadStoragesGrid(string search, int tabIdx, int statusIdx)
        {
            dataGrid.Columns.Clear();
            dataGrid.Rows.Clear();

            dataGrid.Columns.Add("StorageId", "ID");
            dataGrid.Columns.Add("Name", "Name");
            dataGrid.Columns.Add("Status", "Status");
            dataGrid.Columns.Add("City", "City");
            dataGrid.Columns.Add("Address", "Address");
            dataGrid.Columns.Add("Capacity", "Capacity");
            dataGrid.Columns.Add("Price", "Price");
            dataGrid.Columns.Add("CreateDate", "Created");
            if (LocalStorage.LoggedUsername == "admin") dataGrid.Columns.Add("IsDeleted", "Status");

            var storages = _services.Storages.GetAll(d =>
                statusIdx == 0 ||
                (statusIdx == 1 && d.IsDeleted != true) ||
                (statusIdx == 2 && d.IsDeleted == true)).ToList();

            if (tabIdx == 1) storages = storages.Where(s => s.Status == 1).ToList();
            if (tabIdx == 2) storages = storages.Where(s => s.Status == 2).ToList();
            if (tabIdx == 3) storages = storages.Where(s => s.Status == 3).ToList();

            foreach (var s in storages)
            {
                string statusName = s.Status switch { 1 => "Available", 2 => "Rented", 3 => "Maintenance", _ => "Unknown" };

                string cityName = "";
                try {var city = _services.Regions.GetCity(s.CityId);}
                catch { cityName = s.CityId.ToString(); }

                if (!string.IsNullOrEmpty(search) &&
                    !s.Name.ToLower().Contains(search) &&
                    !statusName.ToLower().Contains(search) &&
                    !cityName.ToLower().Contains(search) &&
                    !s.Address.ToLower().Contains(search))
                    continue;

                dataGrid.Rows.Add(s.StorageId, s.Name, statusName, cityName, s.Address, s.Capacity, s.Price, s.CreateDate?.ToString("yyyy-MM-dd"));
            }
            statusRecords.Text = $"{dataGrid.Rows.Count} records found";
        }

        #endregion

        #region Cell Formatting

        private void DataGrid_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            var col = dataGrid.Columns[e.ColumnIndex].Name;
            var val = e.Value?.ToString();
            if (val == null) return;

            if (col is "Status" or "IsDeleted")
            {
                var (color, bold) = val switch
                {
                    "Active" or "No" => (Color.FromArgb(46, 125, 15), true),
                    "Deleted" or "Yes" or "Cancelled" => (Color.FromArgb(183, 28, 28), true),
                    "Pending" => (Color.FromArgb(200, 150, 0), true),
                    "Completed" => (Color.FromArgb(100, 100, 100), true),
                    _ => (Color.Black, false)
                };
                e.CellStyle!.ForeColor = color;
                if (bold) e.CellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            }
            else if (col is "Type" or "CustomerType")
            {
                e.CellStyle!.ForeColor = val is "Physical" or "No"
                    ? Color.FromArgb(26, 93, 184)
                    : Color.FromArgb(46, 125, 15);
            }
        }

        #endregion

        #region Add Operations

        private void OpenAddDialog()
        {
            try
            {
                switch (currentSection)
                {
                    case "customers": AddCustomer(); break;
                    case "contracts": AddContract(); break;
                    case "storages":
                        GenericAdd<StorageDto>("storages", "Storage", e => _services.Storages.Insert(e),
                        () => new StorageDto()); break;
                    case "regions":
                        GenericAdd<RegionDto>("regions", "Region", e => _services.Regions.Insert(e),
                        () => new RegionDto()); break;
                    case "cities":
                        GenericAdd<CityDto>("cities", "City", e => _services.Cities.Insert(e),
                        () => new CityDto()); break;
                    case "employees":
                        using (var regDlg = new RegisterDialog(_services))
                        {
                            if (regDlg.ShowDialog(this) == DialogResult.OK)
                            {
                                LoadGridForSection("employees");
                            }
                        }
                        break;

                    case "categories": GenericAdd<CategoryDto>("categories", "Category", e => _services.Categories.Insert(e),
                    () => new CategoryDto()); break;
                    case "permissions": GenericAdd<PermissionDto>("permissions", "Permission", e => _services.Permissions.Insert(e),
                    () => new PermissionDto()); break;
                    case "roles": GenericAdd<RoleDto>("roles", "Role", e => _services.Roles.Insert(e),
                    () => new RoleDto()); break;
                    case "products": GenericAdd<ProductDto>("products", "Product", e => _services.Products.Insert(e),
                    () => new ProductDto()); break;

                }
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }

        private void AddCustomer()
        {
            using var dlg = new CustomerFormDialog(null, null, null);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var customer = new CustomerDto { CustomerType = dlg.IsLegal, Phone = dlg.PhoneValue, Email = dlg.EmailValue };

                var physical = !dlg.IsLegal ? new PhysicalCustomerDto
                    { FirstName = dlg.FirstNameValue, LastName = dlg.LastNameValue, PersonalId = dlg.PersonalIdValue } : null;

                var legal = dlg.IsLegal ? new LegalCustomerDto
                    { Name = dlg.CompanyNameValue, Address = dlg.AddressValue } : null;

                _services.Customers.RegisterCustomer(customer, physical, legal);
                LoadGridForSection("customers");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddContract()
        {
            using var dlg = new ContractFormDialog(null, _services, LocalStorage.LoggedUsername);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var contract = new ContractDto
                {
                    CustomerId = dlg.CustomerIdValue,
                    EmployeeId = dlg.EmployeeIdValue,
                    ContractStatus = dlg.StatusValue
                };

                var details = dlg.Details.Select(d => new ContractDetailWithStorageDto
                {
                    StorageId = d.StorageId,
                    Price = d.Price,
                    ProductId = d.ProductId,
                    Quantity = d.Quantity
                }).ToList();

                _services.Contracts.CreateContract(contract, details);
                LoadGridForSection("contracts");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Edit Operations

        private void OpenEditDialog()
        {
            if (dataGrid.SelectedRows.Count == 0) return;

            try
            {
                int id = Convert.ToInt32(dataGrid.SelectedRows[0].Cells[0].Value);

                switch (currentSection)
                {
                    case "customers": EditCustomer(id); break;
                    case "regions": GenericEdit("regions", "Region", i => _services.Regions.Get(i), e => _services.Regions.Update(e)); break;
                    case "cities": GenericEdit("cities", "City", i => _services.Regions.GetCity(i), e => _services.Cities.Update(e)); break;
                    case "storages": GenericEdit("storages", "Storage", i => _services.Storages.Get(i), e => _services.Storages.Update(e)); break;
                    case "employees":
                        int empId = Convert.ToInt32(dataGrid.SelectedRows[0].Cells[0].Value);
                        var emp = _services.Employees.Get(empId);
                        if (emp == null) return;
                        using (var dlg = new EmployeeEditDialog(emp, _services))
                        {
                            dlg.ShowDialog(this);
                            LoadGridForSection("employees");
                        }
                        break;
                    case "categories": GenericEdit("categories", "Category", i => _services.Categories.Get(i), e => _services.Categories.Update(e)); break;
                    case "permissions": GenericEdit("permissions", "Permission", i => _services.Permissions.Get(i), e => _services.Permissions.Update(e)); break;
                    case "roles": GenericEdit("roles", "Role", i => _services.Roles.Get(i), e => _services.Roles.Update(e)); break;
                    case "products": GenericEdit("products", "Product", i => _services.Products.Get(i), e => _services.Products.Update(e)); break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditCustomer(int id)
        {
            try
            {
                var customer = _services.Customers.Get(id);
                if (customer == null) return;

                PhysicalCustomerDto? physical = !customer.CustomerType ? _services.PhysicalCustomers.Get(id) : null;
                LegalCustomerDto? legal = customer.CustomerType ? _services.LegalCustomers.Get(id) : null;

                using var dlg = new CustomerFormDialog(customer, physical, legal);
                if (dlg.ShowDialog(this) != DialogResult.OK) return;

                var physicalToUpdate = !dlg.IsLegal ? new PhysicalCustomerDto
                {
                    CustomerId = id,
                    FirstName = dlg.FirstNameValue,
                    LastName = dlg.LastNameValue,
                    PersonalId = dlg.PersonalIdValue
                } : null;

                var legalToUpdate = dlg.IsLegal ? new LegalCustomerDto
                {
                    CustomerId = id,
                    Name = dlg.CompanyNameValue,
                    Address = dlg.AddressValue
                } : null;

                _services.Customers.UpdateCustomer(customer, physical, legal);

                LoadGridForSection("customers");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Delete Operations

        private void DeleteSelected()
        {
            if (dataGrid.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dataGrid.SelectedRows[0].Cells[0].Value);

            if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                switch (currentSection)
                {
                    case "customers": _services.Customers.Delete(id); break;
                    case "storages": _services.Storages.Delete(id); break;
                    case "regions": _services.Regions.Delete(id); break;
                    case "cities": _services.Cities.Delete(id); break;  
                    case "employees": _services.Employees.Delete(id); break;    
                    case "categories": _services.Categories.Delete(id); break;
                    case "permissions": _services.Permissions.Delete(id); break;
                    case "roles": _services.Roles.Delete(id); break;
                    case "products": _services.Products.Delete(id); break;
                }
                LoadGridForSection(currentSection);
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        #endregion

        #region Pickers

        private Dictionary<string, Func<(int id, string displayText)?>> GetPickersForSection(string section)
        {
            var pickers = new Dictionary<string, Func<(int id, string displayText)?>>();

            switch (section)
            {
                case "products":
                    pickers["CategoryId"] = () =>
                    {
                        var categories = _services.Categories.GetAll(c => c.CategoryId > 0).ToList();
                        var displayList = categories.Select(c => (object)new
                        {
                            CategoryId = c.CategoryId ?? 0,
                            Name = c.CategoryName
                        }).ToList();
                        using var picker = new SearchPickerDialog("Select Category", displayList, "CategoryId");
                        return picker.ShowDialog(this) == DialogResult.OK
                            ? (picker.SelectedId, ((dynamic)picker.SelectedItem!).Name)
                            : null;
                    };
                    break;

                case "cities":
                    pickers["RegionId"] = () =>
                    {
                        var regions = _services.Regions.GetAll(r => r.RegionId > 0).ToList();
                        var displayList = regions.Select(r => (object)new
                        {
                            RegionId = r.RegionId ?? 0,
                            Name = r.Name
                        }).ToList();
                        using var picker = new SearchPickerDialog("Select Region", displayList, "RegionId");
                        return picker.ShowDialog(this) == DialogResult.OK
                            ? (picker.SelectedId, ((dynamic)picker.SelectedItem!).Name)
                            : null;
                    };
                    break;

                case "storages":
                    pickers["CityId"] = () =>
                    {
                        var cities = _services.Cities.GetAll(c => c.CityId > 0).ToList();
                        var displayList = cities.Select(c => (object)new
                        {
                            CityId = c.CityId ?? 0,
                            Name = c.Name
                        }).ToList();
                        using var picker = new SearchPickerDialog("Select City", displayList, "CityId");
                        return picker.ShowDialog(this) == DialogResult.OK
                            ? (picker.SelectedId, ((dynamic)picker.SelectedItem!).Name)
                            : null;
                    };
                    break;
            }

            return pickers;
        }

        private Dictionary<string, string[]> GetDropdownsForSection(string section)
        {
            return section switch
            {
                "storages" => new Dictionary<string, string[]>
                {
                    { "Status", new[] { "Available", "Rented", "Maintenance" } }
                },
                _ => new()
            };
        }

        #endregion

        #region Generic Add / Edit Helpers

        private void GenericEdit<T>(string section, string entityName, Func<int, T?> getter, Action<T> updater) where T : class
        {
            if (dataGrid.SelectedRows.Count == 0) 
                return;

            int id = Convert.ToInt32(dataGrid.SelectedRows[0].Cells[0].Value);
            var entity = getter(id);

            if (entity == null) 
                return;
            var pickers = GetPickersForSection(section);
            var dropdowns = GetDropdownsForSection(section);

            using var dlg = new DynamicEditDialog(entityName, entity, typeof(T), pickers: pickers, dropdowns: dropdowns);

            if (dlg.ShowDialog(this) != DialogResult.OK) 
                return;

            try { dlg.ApplyTo(entity); updater(entity); LoadGridForSection(section); }

            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }

        private void GenericAdd<T>(string section, string entityName, Action<T> inserter, Func<T> createInstance) where T : class
        {
            var pickers = GetPickersForSection(section);
            var dropdowns = GetDropdownsForSection(section);

            using var dlg = new DynamicEditDialog(entityName, null, typeof(T), pickers: pickers, dropdowns: dropdowns);

#if DEBUG
            var debugData = GetDebugDataForSection(section);
            if (debugData != null) dlg.SetDebugValues(debugData);
#endif

            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try { var entity = createInstance(); dlg.ApplyTo(entity); inserter(entity); LoadGridForSection(section); }

            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }

#if DEBUG
        private Dictionary<string, object>? GetDebugDataForSection(string section)
        {
            return section switch
            {
                "products" => new Dictionary<string, object>
                {
                    { "Name", "Test Product" },
                    { "SKU", "SKU-TEST-001" },
                    { "Description", "Debug test product" }
                },
                "regions" => new Dictionary<string, object>
                {
                    { "Name", "Test Region" }
                },
                "categories" => new Dictionary<string, object>
                {
                    { "CategoryName", "Test Category" }
                },
                "roles" => new Dictionary<string, object>
                {
                    { "Name", "Test Role" }
                },
                "permissions" => new Dictionary<string, object>
                {
                    { "Name", "Test Permission" },
                    { "Description", "Debug test permission" },
                    { "PermissionKey", "100" }
                },
                "storages" => new Dictionary<string, object>
                {
                    { "Name", "Test Storage" },
                    { "Description", "Debug storage" },
                    { "Capacity", "500" },
                    { "Address", "Test Address 123" },
                    { "Price", "99.99" }
                },
                "cities" => new Dictionary<string, object>
                {
                    { "Name", "Test City" }
                },
                _ => null
            };
        }
#endif



        #endregion
    }
}



