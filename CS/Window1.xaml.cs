using System.Globalization;
using System.Windows;
using DevExpress.Xpf.PivotGrid;

namespace DXPivotGrid_HidingColumnsAndRows {
    public partial class Window1 : Window {
        public Window1() {
            InitializeComponent();
            pivotGrid.CustomFieldValueCells += 
                new PivotCustomFieldValueCellsEventHandler(pivotGrid_CustomFieldValueCells);
        }
        void Window_Loaded(object sender, RoutedEventArgs e) {
            PivotHelper.FillPivot(pivotGrid);
            pivotGrid.DataSource = PivotHelper.GetDataTable();
            pivotGrid.BestFit();
        }

        // Handles the CustomFieldValueCells event to remove
        // specific rows.
        void pivotGrid_CustomFieldValueCells(object sender, PivotCustomFieldValueCellsEventArgs e) {
            if (pivotGrid.DataSource == null) return;
            if (rbDefault.IsChecked == true) return;

            // Iterates through all row headers.
            for (int i = e.GetCellCount(false) - 1; i >= 0; i--) {
                FieldValueCell cell = e.GetCell(false, i);
                if (cell == null) continue;

                // If the current header corresponds to the "Employee B"
                // field value, and is not the Total Row header,
                // it is removed with all corresponding rows.
                if (object.Equals(cell.Value, "Employee B") &&
                    cell.ValueType != FieldValueType.Total)
                    e.Remove(cell);
            }
        }
        void pivotGrid_FieldValueDisplayText(object sender, PivotFieldDisplayTextEventArgs e) {
            if(e.Field == pivotGrid.Fields[PivotHelper.Month]) {
                e.DisplayText = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((int)e.Value);
            }
        }
        private void rbDefault_Checked(object sender, RoutedEventArgs e) {
            pivotGrid.LayoutChanged();
        }
    }
}
