Imports System.Globalization
Imports System.Windows
Imports DevExpress.Xpf.PivotGrid

Namespace DXPivotGrid_HidingColumnsAndRows

    Public Partial Class Window1
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            AddHandler Me.pivotGrid.CustomFieldValueCells, New PivotCustomFieldValueCellsEventHandler(AddressOf pivotGrid_CustomFieldValueCells)
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            PivotHelper.FillPivot(Me.pivotGrid)
            Me.pivotGrid.DataSource = GetDataTable()
            Me.pivotGrid.BestFit()
        End Sub

        ' Handles the CustomFieldValueCells event to remove
        ' specific rows.
        Private Sub pivotGrid_CustomFieldValueCells(ByVal sender As Object, ByVal e As PivotCustomFieldValueCellsEventArgs)
            If Me.pivotGrid.DataSource Is Nothing Then Return
            If Me.rbDefault.IsChecked = True Then Return
            ' Iterates through all row headers.
            For i As Integer = e.GetCellCount(False) - 1 To 0 Step -1
                Dim cell As FieldValueCell = e.GetCell(False, i)
                If cell Is Nothing Then Continue For
                ' If the current header corresponds to the "Employee B"
                ' field value, and is not the Total Row header,
                ' it is removed with all corresponding rows.
                If Equals(cell.Value, "Employee B") AndAlso cell.ValueType <> FieldValueType.Total Then e.Remove(cell)
            Next
        End Sub

        Private Sub pivotGrid_FieldValueDisplayText(ByVal sender As Object, ByVal e As PivotFieldDisplayTextEventArgs)
            If e.Field Is Me.pivotGrid.Fields(Month) Then
                e.DisplayText = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(CInt(e.Value))
            End If
        End Sub

        Private Sub rbDefault_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.pivotGrid.LayoutChanged()
        End Sub
    End Class
End Namespace
