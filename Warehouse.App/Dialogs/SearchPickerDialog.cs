using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Warehouse.App
{
    /// <summary>
    /// Universal search/picker dialog.
    /// Usage: var dlg = new SearchPickerDialog("Select Customer", customers, "CustomerId");
    ///        if (dlg.ShowDialog() == DialogResult.OK) int selectedId = dlg.SelectedId;
    /// </summary>
    public class SearchPickerDialog : Form
    {
        private DataGridView grid = null!;
        private TextBox searchBox = null!;
        private PropertyInfo[] properties = null!;
        private IList<object> allData = null!;
        private string idColumnName;

        public int SelectedId { get; private set; }
        public object? SelectedItem { get; private set; }

        public SearchPickerDialog(string title, IEnumerable<object> data, string idColumn, string[]? hideColumns = null)
        {
            idColumnName = idColumn;
            allData = data.ToList();
            if (allData.Count > 0)
                properties = allData[0].GetType().GetProperties();
            else
                properties = Array.Empty<PropertyInfo>();

            this.Text = title;
            this.Size = new Size(700, 480);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Search box
            var searchPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                Padding = new Padding(10, 8, 10, 4)
            };
            searchBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "Type to search..."
            };
            searchBox.TextChanged += (s, e) => FilterData();
            searchPanel.Controls.Add(searchBox);

            // Grid
            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                BackgroundColor = Color.White,
                GridColor = Color.FromArgb(220, 220, 220),
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 32,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
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
            grid.RowTemplate.Height = 28;
            grid.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) SelectAndClose(); };

            // Setup columns
            foreach (var prop in properties)
            {
                if (hideColumns != null && hideColumns.Contains(prop.Name)) continue;
                grid.Columns.Add(prop.Name, FormatHeader(prop.Name));
            }

            // Buttons
            var btnPanel = new Panel { Dock = DockStyle.Bottom, Height = 48 };

            var btnSelect = new Button
            {
                Text = "Select",
                Size = new Size(80, 32),
                Location = new Point(600, 8),
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(26, 93, 184),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom
            };
            btnSelect.FlatAppearance.BorderSize = 0;
            btnSelect.Click += (s, e) => SelectAndClose();

            var btnCancel = new Button
            {
                Text = "Cancel",
                Size = new Size(80, 32),
                Location = new Point(510, 8),
                DialogResult = DialogResult.Cancel,
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom
            };

            btnPanel.Controls.Add(btnSelect);
            btnPanel.Controls.Add(btnCancel);

            // ═══ Controls დამატების რიგი ═══
            this.Controls.Add(btnPanel);      // Bottom — პირველი
            this.Controls.Add(grid);          // Fill — მეორე
            this.Controls.Add(searchPanel);   // Top — ბოლოს (ზემოთ ჩნდება)

            this.AcceptButton = btnSelect;
            this.CancelButton = btnCancel;

            // Load data
            FilterData();

            // Focus search
            this.Shown += (s, e) => searchBox.Focus();
        }

        private void FilterData()
        {
            grid.Rows.Clear();
            string search = searchBox.Text.ToLower();

            foreach (var item in allData)
            {
                // Search filter
                if (!string.IsNullOrEmpty(search))
                {
                    bool match = properties.Any(p =>
                        (p.GetValue(item)?.ToString()?.ToLower() ?? "").Contains(search));
                    if (!match) continue;
                }

                var values = properties
                    .Where(p => grid.Columns.Contains(p.Name))
                    .Select(p =>
                    {
                        var val = p.GetValue(item);
                        if (val is DateTime dt) return dt.ToString("yyyy-MM-dd");
                        if (val is bool b) return b ? "Yes" : "No";
                        return val?.ToString() ?? "";
                    }).ToArray();

                grid.Rows.Add(values);

                // Store the original object in Tag
                grid.Rows[grid.Rows.Count - 1].Tag = item;
            }
        }

        private void SelectAndClose()
        {
            if (grid.SelectedRows.Count == 0) return;

            try
            {
                var row = grid.SelectedRows[0];
                SelectedItem = row.Tag;

                // Get ID from the id column
                if (row.Tag != null)
                {
                    var idProp = row.Tag.GetType().GetProperty(idColumnName);
                    if (idProp != null)
                    {
                        var val = idProp.GetValue(row.Tag);
                        SelectedId = Convert.ToInt32(val);
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FormatHeader(string name)
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < name.Length; i++)
            {
                if (i > 0 && char.IsUpper(name[i])) sb.Append(' ');
                sb.Append(name[i]);
            }
            return sb.ToString();
        }
    }
}
