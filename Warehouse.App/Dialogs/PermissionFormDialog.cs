using Warehouse.DTO.Lookups;

namespace Warehouse.App.Forms
{
    public class PermissionFormDialog : Form
    {
        private TextBox txtName = null!, txtKey = null!, txtDescription = null!;

        public string NameValue => txtName.Text;
        public short KeyValue => short.TryParse(txtKey.Text, out var v) ? v : (short)0;
        public string DescriptionValue => txtDescription.Text;

        public PermissionFormDialog(PermissionDto? existing)
        {
            bool isEdit = existing != null;
            this.Text = isEdit ? "Edit Permission" : "Add Permission";
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(240, 240, 240);

            var grp = new GroupBox { Text = "Permission Details", Location = new Point(12, 12), Size = new Size(360, 190) };

            grp.Controls.Add(new Label { Text = "Name", Location = new Point(12, 25), AutoSize = true });
            txtName = new TextBox { Location = new Point(12, 45), Size = new Size(336, 24), Text = existing?.Name ?? "" };
            grp.Controls.Add(txtName);

            grp.Controls.Add(new Label { Text = "Permission Key", Location = new Point(12, 75), AutoSize = true });
            txtKey = new TextBox { Location = new Point(12, 95), Size = new Size(100, 24), Text = existing?.PermissionKey.ToString() ?? "" };
            grp.Controls.Add(txtKey);

            grp.Controls.Add(new Label { Text = "Description", Location = new Point(12, 125), AutoSize = true });
            txtDescription = new TextBox { Location = new Point(12, 145), Size = new Size(336, 30), Text = existing?.Description ?? "" };
            grp.Controls.Add(txtDescription);

            this.Controls.Add(grp);

            var btnSave = new Button { Text = isEdit ? "Update" : "Save", Size = new Size(80, 30), Location = new Point(292, 215), DialogResult = DialogResult.OK, BackColor = Color.FromArgb(26, 93, 184), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.FlatAppearance.BorderSize = 0;
            var btnCancel = new Button { Text = "Cancel", Size = new Size(80, 30), Location = new Point(204, 215), DialogResult = DialogResult.Cancel };
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);
            this.AcceptButton = btnSave; this.CancelButton = btnCancel;
        }
    }
}

