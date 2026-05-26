namespace Warehouse.App.Dialogs
{
    // ═══════════════════════════════════════════════════
    // DIALOG: Simple Name (Region, Role)
    // ═══════════════════════════════════════════════════
    public class SimpleNameDialog : Form
    {
        private TextBox txtName = null!;
        public string NameValue => txtName.Text;

        public SimpleNameDialog(string entity, string? existingName)
        {
            this.Text = existingName == null ? $"Add {entity}" : $"Edit {entity}";
            this.Size = new Size(340, 170);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(240, 240, 240);

            var grp = new GroupBox { Text = entity, Location = new Point(12, 12), Size = new Size(300, 65) };
            grp.Controls.Add(new Label { Text = "Name", Location = new Point(12, 28), AutoSize = true });
            txtName = new TextBox { Location = new Point(60, 26), Size = new Size(228, 24), Text = existingName ?? "" };
            grp.Controls.Add(txtName);
            this.Controls.Add(grp);

            var btnSave = new Button { Text = "Save", Size = new Size(80, 30), Location = new Point(232, 90), DialogResult = DialogResult.OK, BackColor = Color.FromArgb(26, 93, 184), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.FlatAppearance.BorderSize = 0;
            var btnCancel = new Button { Text = "Cancel", Size = new Size(80, 30), Location = new Point(144, 90), DialogResult = DialogResult.Cancel };
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);
            this.AcceptButton = btnSave; this.CancelButton = btnCancel;
        }
    }
}