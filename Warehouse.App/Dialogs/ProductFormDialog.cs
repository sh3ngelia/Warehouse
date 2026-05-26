namespace Warehouse.App.Dialogs
{
    // ═══════════════════════════════════════════════════
    // DIALOG: Product
    // ═══════════════════════════════════════════════════
    public class ProductFormDialog : Form
    {
        private TextBox txtName = null!, txtSKU = null!, txtDescription = null!, txtCategoryId = null!;

        public string ProductNameValue => txtName.Text;
        public string SKUValue => txtSKU.Text;
        public string DescriptionValue => txtDescription.Text;
        public int CategoryIdValue => int.TryParse(txtCategoryId.Text, out var v) ? v : 0;

        public ProductFormDialog(DTO.Products.ProductDto? existing)
        {
            bool isEdit = existing != null;
            this.Text = isEdit ? "Edit Product" : "Add Product";
            this.Size = new Size(400, 330);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(240, 240, 240);

            var grp = new GroupBox { Text = "Product Details", Location = new Point(12, 12), Size = new Size(360, 220) };

            grp.Controls.Add(new Label { Text = "Name", Location = new Point(12, 25), AutoSize = true });
            txtName = new TextBox { Location = new Point(12, 45), Size = new Size(336, 24), Text = existing?.Name ?? "" };
            grp.Controls.Add(txtName);

            grp.Controls.Add(new Label { Text = "SKU", Location = new Point(12, 75), AutoSize = true });
            txtSKU = new TextBox { Location = new Point(12, 95), Size = new Size(160, 24), Text = existing?.SKU ?? "" };
            grp.Controls.Add(txtSKU);

            grp.Controls.Add(new Label { Text = "Category ID", Location = new Point(188, 75), AutoSize = true });
            txtCategoryId = new TextBox { Location = new Point(188, 95), Size = new Size(160, 24), Text = existing?.CategoryId.ToString() ?? "" };
            grp.Controls.Add(txtCategoryId);

            grp.Controls.Add(new Label { Text = "Description", Location = new Point(12, 125), AutoSize = true });
            txtDescription = new TextBox { Location = new Point(12, 145), Size = new Size(336, 60), Multiline = true, Text = existing?.Description ?? "" };
            grp.Controls.Add(txtDescription);

            this.Controls.Add(grp);

            var btnSave = new Button { Text = isEdit ? "Update" : "Save", Size = new Size(80, 30), Location = new Point(292, 245), DialogResult = DialogResult.OK, BackColor = Color.FromArgb(26, 93, 184), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSave.FlatAppearance.BorderSize = 0;
            var btnCancel = new Button { Text = "Cancel", Size = new Size(80, 30), Location = new Point(204, 245), DialogResult = DialogResult.Cancel };
            this.Controls.Add(btnSave); this.Controls.Add(btnCancel);
            this.AcceptButton = btnSave; this.CancelButton = btnCancel;
        }
    }
}