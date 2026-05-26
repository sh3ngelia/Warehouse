using Warehouse.DTO.Locations;
using Warehouse.DTO.Storage;

namespace Warehouse.App.Dialogs
{
    public class StorageFormDialog : Form
    {
        private TextBox txtName = null!, txtAddress = null!, txtDescription = null!;
        private TextBox txtCapacity = null!, txtPrice = null!;
        private ComboBox cmbStatus = null!, cmbCity = null!;
        private List<CityDto> cities;

        public string StorageNameValue => txtName.Text;
        public int StatusValue => cmbStatus.SelectedIndex + 1;
        public int CityIdValue => cmbCity.SelectedIndex >= 0 ? cities[cmbCity.SelectedIndex].CityId ?? 0 : 0;
        public string DescriptionValue => txtDescription.Text;
        public double CapacityValue => double.TryParse(txtCapacity.Text, out var v) ? v : 0;
        public string AddressValue => txtAddress.Text;
        public decimal PriceValue => decimal.TryParse(txtPrice.Text, out var v) ? v : 0;

        public StorageFormDialog(StorageDto? existing, List<CityDto> cities)
        {
            this.cities = cities;
            bool isEdit = existing != null;
            this.Text = isEdit ? "Edit Storage" : "Add Storage";
            this.Size = new Size(440, 440);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(240, 240, 240);

            int y = 12;

            // Info
            var grpInfo = new GroupBox { Text = "Storage Info", Location = new Point(12, y), Size = new Size(400, 90) };
            grpInfo.Controls.Add(new Label { Text = "Name", Location = new Point(12, 22), AutoSize = true });
            txtName = new TextBox { Location = new Point(12, 40), Size = new Size(180, 24), Text = existing?.Name ?? "" };
            grpInfo.Controls.Add(txtName);
            grpInfo.Controls.Add(new Label { Text = "Status", Location = new Point(206, 22), AutoSize = true });
            cmbStatus = new ComboBox { Location = new Point(206, 40), Size = new Size(182, 24), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(new object[] { "Available", "Rented", "Maintenance" });
            cmbStatus.SelectedIndex = existing != null ? existing.Status - 1 : 0;
            grpInfo.Controls.Add(cmbStatus);
            grpInfo.Controls.Add(new Label { Text = "Description", Location = new Point(12, 66), AutoSize = true });
            txtDescription = new TextBox { Location = new Point(90, 64), Size = new Size(298, 24), Text = existing?.Description ?? "" };
            grpInfo.Controls.Add(txtDescription);
            this.Controls.Add(grpInfo);
            y += 100;

            // Location
            var grpLoc = new GroupBox { Text = "Location", Location = new Point(12, y), Size = new Size(400, 90) };
            grpLoc.Controls.Add(new Label { Text = "City", Location = new Point(12, 22), AutoSize = true });
            cmbCity = new ComboBox { Location = new Point(12, 40), Size = new Size(180, 24), DropDownStyle = ComboBoxStyle.DropDownList };
            foreach (var c in cities) cmbCity.Items.Add(c.Name);
            if (existing != null) { var idx = cities.FindIndex(c => c.CityId == existing.CityId); if (idx >= 0) cmbCity.SelectedIndex = idx; }
            else if (cmbCity.Items.Count > 0) cmbCity.SelectedIndex = 0;
            grpLoc.Controls.Add(cmbCity);
            grpLoc.Controls.Add(new Label { Text = "Address", Location = new Point(206, 22), AutoSize = true });
            txtAddress = new TextBox { Location = new Point(206, 40), Size = new Size(182, 24), Text = existing?.Address ?? "" };
            grpLoc.Controls.Add(txtAddress);
            this.Controls.Add(grpLoc);
            y += 100;

            // Capacity & Price
            var grpCap = new GroupBox { Text = "Capacity & Pricing", Location = new Point(12, y), Size = new Size(400, 65) };
            grpCap.Controls.Add(new Label { Text = "Capacity", Location = new Point(12, 25), AutoSize = true });
            txtCapacity = new TextBox { Location = new Point(80, 23), Size = new Size(110, 24), Text = existing?.Capacity.ToString() ?? "" };
            grpCap.Controls.Add(txtCapacity);
            grpCap.Controls.Add(new Label { Text = "Price", Location = new Point(206, 25), AutoSize = true });
            txtPrice = new TextBox { Location = new Point(260, 23), Size = new Size(128, 24), Text = existing?.Price.ToString() ?? "" };
            grpCap.Controls.Add(txtPrice);
            this.Controls.Add(grpCap);
            y += 75;

            // Buttons
            var btnSave = new Button { Text = isEdit ? "Update" : "Save", Size = new Size(80, 30), Location = new Point(332, y), DialogResult = DialogResult.OK, BackColor = Color.FromArgb(26, 93, 184), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.FlatAppearance.BorderSize = 0;
            var btnCancel = new Button { Text = "Cancel", Size = new Size(80, 30), Location = new Point(244, y), DialogResult = DialogResult.Cancel };
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);
            this.AcceptButton = btnSave; this.CancelButton = btnCancel;
        }
    }
}