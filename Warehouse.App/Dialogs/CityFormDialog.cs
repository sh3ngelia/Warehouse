using Warehouse.DTO.Locations;

namespace Warehouse.App.Dialogs
{
    // ═══════════════════════════════════════════════════
    // DIALOG: City
    // ═══════════════════════════════════════════════════
    public class CityFormDialog : Form
    {
        private TextBox txtName = null!;
        private ComboBox cmbRegion = null!;
        private List<RegionDto> regions;

        public string CityNameValue => txtName.Text;
        public int RegionIdValue => cmbRegion.SelectedIndex >= 0 ? regions[cmbRegion.SelectedIndex].RegionId ?? 0 : 0;

        public CityFormDialog(CityDto? existing, List<RegionDto> regions)
        {
            this.regions = regions;
            this.Text = existing == null ? "Add City" : "Edit City";
            this.Size = new Size(340, 220);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(240, 240, 240);

            var grp = new GroupBox { Text = "City", Location = new Point(12, 12), Size = new Size(300, 110) };
            grp.Controls.Add(new Label { Text = "Region", Location = new Point(12, 25), AutoSize = true });
            cmbRegion = new ComboBox { Location = new Point(12, 45), Size = new Size(276, 24), DropDownStyle = ComboBoxStyle.DropDownList };
            foreach (var r in regions) cmbRegion.Items.Add(r.Name);
            if (existing != null) { var idx = regions.FindIndex(r => r.RegionId == existing.RegionId); if (idx >= 0) cmbRegion.SelectedIndex = idx; }
            else if (cmbRegion.Items.Count > 0) cmbRegion.SelectedIndex = 0;
            grp.Controls.Add(cmbRegion);
            grp.Controls.Add(new Label { Text = "Name", Location = new Point(12, 75), AutoSize = true });
            txtName = new TextBox { Location = new Point(60, 73), Size = new Size(228, 24), Text = existing?.Name ?? "" };
            grp.Controls.Add(txtName);
            this.Controls.Add(grp);

            var btnSave = new Button { Text = "Save", Size = new Size(80, 30), Location = new Point(232, 135), DialogResult = DialogResult.OK, BackColor = Color.FromArgb(26, 93, 184), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.FlatAppearance.BorderSize = 0;
            var btnCancel = new Button { Text = "Cancel", Size = new Size(80, 30), Location = new Point(144, 135), DialogResult = DialogResult.Cancel };
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);
            this.AcceptButton = btnSave; this.CancelButton = btnCancel;
        }
    }
}