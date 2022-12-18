Imports System.Data
Imports DevExpress.Xpf.PivotGrid

Namespace DXPivotGrid_HidingColumnsAndRows

    Public Module PivotHelper

        Public Const Employee As String = "Employee"

        Public Const Widget As String = "Widget"

        Public Const Month As String = "Month"

        Public Const RetailPrice As String = "Retail Price"

        Public Const WholesalePrice As String = "Wholesale Price"

        Public Const Quantity As String = "Quantity"

        Public Const Remains As String = "Remains"

        Public Const EmployeeA As String = "Employee A"

        Public Const EmployeeB As String = "Employee B"

        Public Const WidgetA As String = "Widget A"

        Public Const WidgetB As String = "Widget B"

        Public Const WidgetC As String = "Widget C"

        Public Sub FillPivot(ByVal pivot As PivotGridControl)
            pivot.Fields.AddDataSourceColumn(Employee, FieldArea.RowArea)
            pivot.Fields.AddDataSourceColumn(Widget, FieldArea.RowArea)
            pivot.Fields.AddDataSourceColumn(Month, FieldArea.ColumnArea).AreaIndex = 0
            pivot.Fields.AddDataSourceColumn(RetailPrice, FieldArea.DataArea)
            pivot.Fields.AddDataSourceColumn(WholesalePrice, FieldArea.DataArea)
            pivot.Fields.AddDataSourceColumn(Quantity, FieldArea.DataArea)
            pivot.Fields.AddDataSourceColumn(Remains, FieldArea.DataArea)
            For Each field As PivotGridField In pivot.Fields
                field.AllowedAreas = GetAllowedArea(field.Area)
            Next

            pivot.RowTotalsLocation = FieldRowTotalsLocation.Near
            pivot.ShowRowGrandTotals = False
            pivot.ColumnTotalsLocation = FieldColumnTotalsLocation.Far
            pivot.DataFieldArea = DataFieldArea.ColumnArea
            pivot.DataFieldAreaIndex = 1
        End Sub

        Private Function GetAllowedArea(ByVal area As FieldArea) As FieldAllowedAreas
            Select Case area
                Case FieldArea.ColumnArea
                    Return FieldAllowedAreas.ColumnArea
                Case FieldArea.RowArea
                    Return FieldAllowedAreas.RowArea
                Case FieldArea.DataArea
                    Return FieldAllowedAreas.DataArea
                Case FieldArea.FilterArea
                    Return FieldAllowedAreas.FilterArea
                Case Else
                    Return FieldAllowedAreas.All
            End Select
        End Function

        Public Function GetDataTable() As DataTable
            Dim table As DataTable = New DataTable()
            table.Columns.Add(Employee, GetType(String))
            table.Columns.Add(Widget, GetType(String))
            table.Columns.Add(Month, GetType(Integer))
            table.Columns.Add(RetailPrice, GetType(Double))
            table.Columns.Add(WholesalePrice, GetType(Double))
            table.Columns.Add(Quantity, GetType(Integer))
            table.Columns.Add(Remains, GetType(Integer))
            table.Rows.Add(EmployeeA, WidgetA, 6, 45.6, 40, 3, 0)
            table.Rows.Add(EmployeeA, WidgetA, 7, 38.9, 30, 6, 1)
            table.Rows.Add(EmployeeA, WidgetB, 6, 24.7, 20, 7, 0)
            table.Rows.Add(EmployeeA, WidgetB, 7, 8.3, 7.5, 5, 1)
            table.Rows.Add(EmployeeA, WidgetC, 6, 10.0, 9, 4, 0)
            table.Rows.Add(EmployeeA, WidgetC, 7, 20.0, 18.5, 5, 1)
            table.Rows.Add(EmployeeB, WidgetA, 6, 77.8, 70, 2, 0)
            table.Rows.Add(EmployeeB, WidgetA, 7, 32.5, 30, 1, 1)
            table.Rows.Add(EmployeeB, WidgetB, 6, 12, 11, 10, 0)
            table.Rows.Add(EmployeeB, WidgetB, 7, 6.7, 5.5, 4, 1)
            table.Rows.Add(EmployeeB, WidgetC, 6, 30.0, 28.7, 6, 0)
            table.Rows.Add(EmployeeB, WidgetC, 7, 40.0, 38.3, 7, 1)
            Return table
        End Function
    End Module
End Namespace
