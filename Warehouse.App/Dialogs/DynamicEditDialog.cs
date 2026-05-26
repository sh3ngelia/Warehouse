using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Warehouse.App
{
    public class DynamicEditDialog : Form
    {
        private readonly object? existingEntity;
        private readonly Type entityType;
        private readonly Dictionary<string, Control> fieldControls = new();
        private readonly string[] hideFields;
        private readonly string[] readOnlyFields;
        private readonly Dictionary<string, Func<(int id, string displayText)?>>? _pickers;
        private readonly Dictionary<string, string[]>? _dropdowns;

        public DynamicEditDialog(string title, object? existing, Type dtoType,
            string[]? hideFields = null, string[]? readOnlyFields = null,
            Dictionary<string, Func<(int id, string displayText)?>>? pickers = null,
            Dictionary<string, string[]>? dropdowns = null)
        {
            this.existingEntity = existing;
            this.entityType = existing?.GetType() ?? dtoType;
            this.hideFields = hideFields ?? new[] { "UpdateDate", "IsDeleted" };
            this.readOnlyFields = readOnlyFields ?? new[] { "CreateDate" };

            bool isEdit = existing != null;
            this.Text = isEdit ? $"Edit {title}" : $"Add {title}";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.FromArgb(240, 240, 240);
            _pickers = pickers;
            _dropdowns = dropdowns;

            try
            {
                BuildUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error building dialog: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuildUI()
        {
            var properties = entityType.GetProperties()
                .Where(p => !this.hideFields.Contains(p.Name))
                .ToArray();

            int y = 16;
            int maxWidth = 380;

            var grp = new GroupBox
            {
                Text = "Details",
                Location = new Point(12, 12),
                Font = new Font("Segoe UI", 8.5F)
            };

            int innerY = 24;

            foreach (var prop in properties)
            {
                bool isId = prop.Name.EndsWith("Id") && prop.Name == entityType.Name.Replace("Dto", "") + "Id";
                bool isReadOnly = this.readOnlyFields.Contains(prop.Name) || isId;

                // Add-ის დროს primary ID საერთოდ არ ჩანდეს
                if (isId && existingEntity == null)
                    continue;

                // Label
                var label = new Label
                {
                    Text = FormatHeader(prop.Name),
                    Location = new Point(12, innerY),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 8.5F),
                    ForeColor = isReadOnly ? Color.Gray : Color.FromArgb(51, 51, 51)
                };
                grp.Controls.Add(label);
                innerY += 18;

                // Control — ტიპის მიხედვით
                Control control;
                var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                var currentValue = existingEntity != null ? prop.GetValue(existingEntity) : null;

                if (propType == typeof(bool))
                {
                    var cb = new ComboBox
                    {
                        Location = new Point(12, innerY),
                        Size = new Size(maxWidth - 24, 24),
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Enabled = !isReadOnly
                    };
                    cb.Items.AddRange(new object[] { "No", "Yes" });
                    cb.SelectedIndex = (currentValue is bool b && b) ? 1 : 0;
                    control = cb;
                }
                else if (propType == typeof(DateTime))
                {
                    var dtp = new DateTimePicker
                    {
                        Location = new Point(12, innerY),
                        Size = new Size(maxWidth - 24, 24),
                        Format = DateTimePickerFormat.Short,
                        Enabled = !isReadOnly
                    };
                    if (currentValue is DateTime dt) dtp.Value = dt;
                    control = dtp;
                }
                else if (propType == typeof(decimal) || propType == typeof(double) || propType == typeof(float))
                {
                    var txt = new TextBox
                    {
                        Location = new Point(12, innerY),
                        Size = new Size(maxWidth - 24, 24),
                        Text = currentValue?.ToString() ?? "",
                        ReadOnly = isReadOnly,
                        BackColor = isReadOnly ? Color.FromArgb(245, 245, 245) : Color.White
                    };
                    control = txt;
                }
                else if (propType == typeof(int) || propType == typeof(short) || propType == typeof(byte) || propType == typeof(long))
                {
                    if (_dropdowns != null && _dropdowns.ContainsKey(prop.Name))
                    {
                        var cb = new ComboBox
                        {
                            Location = new Point(12, innerY),
                            Size = new Size(maxWidth - 24, 24),
                            DropDownStyle = ComboBoxStyle.DropDownList,
                            Enabled = !isReadOnly
                        };
                        cb.Items.AddRange(_dropdowns[prop.Name]);
                        int currentIdx = (currentValue is int v) ? v - 1 : 0;
                        if (currentIdx >= 0 && currentIdx < cb.Items.Count)
                            cb.SelectedIndex = currentIdx;
                        else if (cb.Items.Count > 0)
                            cb.SelectedIndex = 0;
                        control = cb;
                    }
                    else if (_pickers != null && _pickers.ContainsKey(prop.Name))
                    {
                        var pickerPanel = new Panel
                        {
                            Location = new Point(12, innerY),
                            Size = new Size(maxWidth - 24, 26)
                        };

                        var btnPick = new Button
                        {
                            Text = "...",
                            Size = new Size(30, 24),
                            Dock = DockStyle.Right,
                            Cursor = Cursors.Hand
                        };
                        btnPick.FlatAppearance.BorderSize = 0;
                        btnPick.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 180);

                        var txt = new TextBox
                        {
                            Dock = DockStyle.Fill,
                            Text = currentValue?.ToString() ?? "",
                            ReadOnly = true,
                            BackColor = Color.White,
                            BorderStyle = BorderStyle.FixedSingle
                        };

                        var propName = prop.Name;
                        btnPick.Click += (s, e) =>
                        {
                            try
                            {
                                var result = _pickers[propName]();
                                if (result != null)
                                {
                                    txt.Tag = result.Value.id;
                                    txt.Text = result.Value.displayText;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error selecting value: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        };

                        pickerPanel.Controls.Add(txt);
                        pickerPanel.Controls.Add(btnPick);
                        grp.Controls.Add(pickerPanel);
                        control = txt;
                    }
                    else
                    {
                        var txt = new TextBox
                        {
                            Location = new Point(12, innerY),
                            Size = new Size(maxWidth - 24, 24),
                            Text = currentValue?.ToString() ?? "",
                            ReadOnly = isReadOnly,
                            BackColor = isReadOnly ? Color.FromArgb(245, 245, 245) : Color.White
                        };
                        control = txt;
                    }
                }
                else // string
                {
                    bool isDescription = prop.Name.ToLower().Contains("description") ||
                                         prop.Name.ToLower().Contains("address");
                    var txt = new TextBox
                    {
                        Location = new Point(12, innerY),
                        Size = new Size(maxWidth - 24, isDescription ? 50 : 24),
                        Text = currentValue?.ToString() ?? "",
                        Multiline = isDescription,
                        ReadOnly = isReadOnly,
                        BackColor = isReadOnly ? Color.FromArgb(245, 245, 245) : Color.White
                    };
                    control = txt;
                    if (isDescription) innerY += 26; // extra height
                }

                if (control.Parent == null) // picker-ის შემთხვევაში უკვე panel-შია
                    grp.Controls.Add(control);
                fieldControls[prop.Name] = control;
                innerY += 32;
            }

            grp.Size = new Size(maxWidth, innerY + 10);
            this.Controls.Add(grp);

            y = grp.Bottom + 10;


            // Buttons
            var btnSave = new Button
            {
                Text = existingEntity != null ? "Update" : "Save",
                Size = new Size(80, 30),
                Location = new Point(maxWidth - 56, y),
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(26, 93, 184),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;

            var btnCancel = new Button
            {
                Text = "Cancel",
                Size = new Size(80, 30),
                Location = new Point(maxWidth - 144, y),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;

            this.ClientSize = new Size(maxWidth + 24, y + 45);
        }

        public Dictionary<string, object?> GetValues()
        {
            var result = new Dictionary<string, object?>();
            var properties = entityType.GetProperties()
                .Where(p => !hideFields.Contains(p.Name))
                .ToArray();

            foreach (var prop in properties)
            {
                if (!fieldControls.ContainsKey(prop.Name)) continue;
                var control = fieldControls[prop.Name];
                var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                object? value = null;

                if (control is ComboBox cb)
                {
                    if (propType == typeof(bool))
                        value = cb.SelectedIndex == 1;
                    else if (_dropdowns != null && _dropdowns.ContainsKey(prop.Name))
                        value = cb.SelectedIndex + 1; // dropdown values are 1-based (e.g. Status: 1=Available, 2=Rented, 3=Maintenance)
                    else
                        value = cb.SelectedIndex;
                }
                else if (control is DateTimePicker dtp)
                {
                    value = dtp.Value;
                }
                else if (control is TextBox txt)
                {
                    try
                    {
                        // Picker-ის TextBox-ში Tag-ში ID ინახება
                        if (txt.Tag != null && _pickers != null && _pickers.ContainsKey(prop.Name))
                        {
                            value = Convert.ChangeType(txt.Tag, propType);
                        }
                        else if (propType == typeof(int))
                            value = int.TryParse(txt.Text, out var i) ? i : 0;
                        else if (propType == typeof(short))
                            value = short.TryParse(txt.Text, out var s) ? s : (short)0;
                        else if (propType == typeof(byte))
                            value = byte.TryParse(txt.Text, out var b) ? b : (byte)0;
                        else if (propType == typeof(long))
                            value = long.TryParse(txt.Text, out var l) ? l : 0L;
                        else if (propType == typeof(decimal))
                            value = decimal.TryParse(txt.Text, out var d) ? d : 0m;
                        else if (propType == typeof(double))
                            value = double.TryParse(txt.Text, out var db) ? db : 0.0;
                        else if (propType == typeof(float))
                            value = float.TryParse(txt.Text, out var f) ? f : 0f;
                        else
                            value = txt.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error reading value for '{prop.Name}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                result[prop.Name] = value;
            }

            return result;
        }

        public void ApplyTo(object entity)
        {
            var values = GetValues();
            var properties = entity.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (!values.ContainsKey(prop.Name)) continue;
                if (!prop.CanWrite) continue;

                // Skip readonly/auto fields
                if (readOnlyFields.Contains(prop.Name)) continue;
                if (prop.Name.EndsWith("Id") && prop.Name == entityType.Name.Replace("Dto", "") + "Id") continue;

                var val = values[prop.Name];

                try
                {
                    var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                    if (val == null)
                        prop.SetValue(entity, null);
                    else if (val.GetType() == targetType)
                        prop.SetValue(entity, val);
                    else
                        prop.SetValue(entity, Convert.ChangeType(val, targetType));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error setting '{prop.Name}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public T CreateNew<T>() where T : class, new()
        {
            var entity = new T();
            ApplyTo(entity);
            return entity;
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

#if DEBUG
        public void SetDebugValues(Dictionary<string, object> values)
        {
            foreach (var kvp in values)
            {
                if (!fieldControls.ContainsKey(kvp.Key)) continue;
                var control = fieldControls[kvp.Key];

                if (control is TextBox txt)
                    txt.Text = kvp.Value?.ToString() ?? "";
                else if (control is ComboBox cb)
                    cb.SelectedIndex = Convert.ToInt32(kvp.Value);
                else if (control is DateTimePicker dtp && kvp.Value is DateTime dt)
                    dtp.Value = dt;
            }
        }
#endif

    }
}
