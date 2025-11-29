using Syncfusion.Windows.Tools.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FrontB.Classes
{
    public class FilterHelper<T> where T : class
    {
        private readonly FilterDataContext dataContext;
        private readonly Dictionary<string, ComboBoxAdv> comboBoxes;
        private readonly Dictionary<string, TextBox> textBoxes;
        private readonly Dictionary<string, Expander> expanders;
        public Action<List<T>> OnFilterApplied { get; set; }
        public Dictionary<string, Func<T, object>> PropertySelectors { get; set; }
        public Dictionary<string, string> TextBoxToColumn { get; set; }
        public Dictionary<string, string> ComboBoxToColumn { get; set; }
        public Dictionary<string, string> HeaderToColumn { get; set; }

        public HashSet<string> NullableColumns { get; set; }

        public HashSet<string> IntegerColumns { get; set; }

        public HashSet<string> DescendingOrderColumns { get; set; }
        public DataGrid DataGrid { get; set; }
        public Func<List<T>> GetOriginalData { get; set; }

        public readonly Dictionary<string, string> SearchFields = new Dictionary<string, string>();
        public readonly Dictionary<string, List<string>> ComboBoxFilters = new Dictionary<string, List<string>>();
        
        public List<T> CurrentFilteredData { get; private set; }
        public FilterHelper(
            FilterDataContext dataContext,
            Dictionary<string, ComboBoxAdv> comboBoxes,
            Dictionary<string, TextBox> textBoxes,
            Dictionary<string, Expander> expanders)
        {
            this.dataContext = dataContext;
            this.comboBoxes = comboBoxes;
            this.textBoxes = textBoxes;
            this.expanders = expanders;
            this.NullableColumns = new HashSet<string>();
            this.IntegerColumns = new HashSet<string>();
            this.CurrentFilteredData = new List<T>();
        }

        #region Unified Filtering System
        public void Searchtb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (sender is TextBox textBox && TextBoxToColumn.TryGetValue(textBox.Name, out string columnName))
                {
                    SearchFields[columnName] = textBox.Text.Trim().ToLower();
                    ApplyAllFilters();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sender is ComboBoxAdv comboBox && ComboBoxToColumn.TryGetValue(comboBox.Name, out string columnName))
                {
                    ComboBoxFilters[columnName].Clear();
                    foreach (var selectedItem in comboBox.SelectedItems)
                    {
                        ComboBoxFilters[columnName].Add(selectedItem.ToString());
                    }
                 //   ApplyAllFilters();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ApplyAllFilters()
        {
            var originalData = GetOriginalData();

            CurrentFilteredData = originalData.Where(item => MatchesAllFilters(item)).ToList();

            if (DataGrid != null)
            {
                DataGrid.ItemsSource = null;
                DataGrid.ItemsSource = CurrentFilteredData;
            }
            OnFilterApplied?.Invoke(CurrentFilteredData);
        }

        private bool MatchesAllFilters(T item)
        {
            foreach (var column in PropertySelectors.Keys)
            {
                if (!MatchesTextSearch(item, column) || !MatchesComboBoxFilter(item, column))
                    return false;
            }
            return true;
        }

        private bool MatchesTextSearch(T item, string columnName)
        {
            if (!SearchFields.TryGetValue(columnName, out string searchValue) || string.IsNullOrEmpty(searchValue))
                return true;

            string itemValue = GetPropertyValueAsString(item, columnName);
            return itemValue.ToLower().Contains(searchValue);
        }

        private bool MatchesComboBoxFilter(T item, string columnName)
        {
            if (!ComboBoxFilters.TryGetValue(columnName, out List<string> filterValues) || filterValues.Count == 0)
                return true;

            string itemValue = GetPropertyValueAsString(item, columnName);

            if (NullableColumns.Contains(columnName))
            {
                return filterValues.Any(filter =>
                    (filter == "(näbelli)" && string.IsNullOrEmpty(itemValue)) ||
                    (filter != "(näbelli)" && itemValue == filter)
                );
            }

            return filterValues.Contains(itemValue);
        }

        private string GetPropertyValueAsString(T item, string columnName)
        {
            var value = PropertySelectors[columnName](item);

            if (value == null)
                return string.Empty;

            if (IntegerColumns.Contains(columnName))
            {
                if (value is int intValue)
                    return intValue.ToString();
                if (int.TryParse(value.ToString(), out int parsedInt))
                    return parsedInt.ToString();
            }

            return value.ToString();
        }
        #endregion

        #region Filter Items Management
        public void UpdateFilterItems()
        {
            try
            {
                ClearFilterItems();

                var allItems = GetOriginalData();
                var filterItemsCollections = dataContext.GetAllCollections();

                int index = 0;
                foreach (var column in PropertySelectors.Keys)
                {
                    var uniqueValues = allItems
                        .Select(x => GetPropertyValueAsString(x, column))
                        .Distinct()
                        .Where(v => !string.IsNullOrEmpty(v) || NullableColumns.Contains(column));

                    if (IntegerColumns.Contains(column))
                    {
                        IEnumerable<int> sortedValues = uniqueValues
                            .Where(v => int.TryParse(v, out _))
                            .Select(v => int.Parse(v));

                        sortedValues = DescendingOrderColumns.Contains(column)
                            ? sortedValues.OrderByDescending(v => v)
                            : sortedValues.OrderBy(v => v);

                        uniqueValues = sortedValues.Select(v => v.ToString());
                    }
                    else
                    {
                        uniqueValues = DescendingOrderColumns.Contains(column)
                            ? uniqueValues.OrderByDescending(x => x)
                            : uniqueValues.OrderBy(x => x);
                    }

                    foreach (string value in uniqueValues)
                    {
                        string displayValue = NullableColumns.Contains(column)
                            ? FormatDisplayValue(value, true)
                            : value;

                        if (!string.IsNullOrEmpty(displayValue) || displayValue == "(näbelli)")
                            filterItemsCollections[index].Add(displayValue);
                    }
                    index++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating filter items: {ex.Message}", "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ClearFilterItems()
        {
            try
            {
                dataContext.ClearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string FormatDisplayValue(string value, bool showBlankAsText = false)
        {
            if (showBlankAsText && string.IsNullOrEmpty(value))
            {
                return "(näbelli)";
            }
            return value;
        }
        #endregion

        #region Button and Expander Events
        public void OKbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button)
                {
                    ApplyAllFilters();
                    CollapseExpander(button);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetColumnTypeFromExpander(Expander expander)
        {
            return GetColumnTypeFromHeader(expander?.Header?.ToString());
        }

        private string GetColumnTypeFromHeader(string header)
        {
            return HeaderToColumn.TryGetValue(header ?? "", out string column) ? column : PropertySelectors.Keys.FirstOrDefault();
        }

        private void CollapseExpander(Button button)
        {
            var stackPanel = button.Parent as StackPanel;
            var expander = stackPanel?.Parent as Expander;
            if (expander != null)
            {
                expander.IsExpanded = false;
                expander.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00b25d"));
            }
        }

        public void Expander_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (sender is Expander expander)
                {
                    ClearExpander(expander);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Clear Operations
        public void ClearExpander(Expander expander)
        {
            string columnType = GetColumnTypeFromExpander(expander);
            ClearSpecificFilter(expander, columnType);
            expander.IsExpanded = false;
            expander.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F7F8FA"));
        }
        private void ClearSpecificFilter(Expander expander, string columnType)
        {
            var stackPanel = expander.Content as StackPanel;
            if (stackPanel != null)
            {
                foreach (var child in stackPanel.Children)
                {
                    if (child is Border border)
                    {
                        var innerBorder = border.Child as Border;
                        if (innerBorder?.Child is TextBox textBox)
                        {
                            textBox.Clear();
                        }
                        else if (innerBorder?.Child is ComboBoxAdv comboBox)
                        {
                            ClearComboBoxSelection(comboBox);
                        }
                    }
                }
            }

            SearchFields.Remove(columnType);
            if (ComboBoxFilters.ContainsKey(columnType))
                ComboBoxFilters[columnType].Clear();

            ApplyAllFilters();
        }
        private void ClearComboBoxSelection(ComboBoxAdv comboBox)
        {
            try
            {
                comboBox.SelectedItems = new System.Collections.ObjectModel.ObservableCollection<object>();
                comboBox.SelectedIndex = -1;
                comboBox.SelectedItem = null;
                comboBox.SelectedValue = null;
                comboBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void ClearAllFilters()
        {
            SearchFields.Clear();
            foreach (var filter in ComboBoxFilters.Values)
                filter.Clear();

            ClearComboBoxSelections();
            ApplyAllFilters();
        }
        private void ClearComboBoxSelections()
        {
            foreach (var comboBox in comboBoxes.Values)
            {
                comboBox.SelectedItems = new System.Collections.ObjectModel.ObservableCollection<object>();
                comboBox.Text = string.Empty;
            }

            foreach (var textBox in textBoxes.Values)
            {
                textBox.Text = string.Empty;
            }

            foreach (var expander in expanders.Values)
            {
                ClearExpander(expander);
            }
        }
        #endregion

        #region Loading Events
        public void SearchTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                textBoxes[textBox.Name] = textBox;
            }
        }

        public void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxAdv comboBox = sender as ComboBoxAdv;
            if (comboBox != null)
            {
                comboBoxes[comboBox.Name] = comboBox;
            }
        }

        public void Expander_Loaded(object sender, RoutedEventArgs e)
        {
            Expander expander = sender as Expander;
            if (expander != null)
            {
                expanders[expander.Name] = expander;
            }
        }
        #endregion
    }
}