Imports System
Imports System.Globalization
Imports System.Windows
Imports DevExpress.Xpf.PivotGrid

Namespace DXPivotGrid_HidingColumnsAndRows
	Partial Public Class Window1
		Inherits Window

		Public Sub New()
			InitializeComponent()
			AddHandler pivotGrid.CustomFieldValueCells, AddressOf pivotGrid_CustomFieldValueCells
		End Sub
		Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			PivotHelper.FillPivot(pivotGrid)
			pivotGrid.DataSource = PivotHelper.GetDataTable()
			pivotGrid.BestFit()
		End Sub

		' Handles the CustomFieldValueCells event to remove
		' specific rows.
		Private Sub pivotGrid_CustomFieldValueCells(ByVal sender As Object, ByVal e As PivotCustomFieldValueCellsEventArgs)
			If pivotGrid.DataSource Is Nothing Then
				Return
			End If
			If rbDefault.IsChecked = True Then
				Return
			End If

			' Iterates through all row headers.
			For i As Integer = e.GetCellCount(False) - 1 To 0 Step -1
				Dim cell As FieldValueCell = e.GetCell(False, i)
				If cell Is Nothing Then
					Continue For
				End If

				' If the current header corresponds to the "Employee B"
				' field value, and is not the Total Row header,
				' it is removed with all corresponding rows.
				If Object.Equals(cell.Value, "Employee B") AndAlso cell.ValueType <> FieldValueType.Total Then
					e.Remove(cell)
				End If
			Next i
		End Sub
		Private Sub pivotGrid_FieldValueDisplayText(ByVal sender As Object, ByVal e As PivotFieldDisplayTextEventArgs)
			If e.Field = pivotGrid.Fields(PivotHelper.Month) Then
				e.DisplayText = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(CInt(Math.Truncate(e.Value)))
			End If
		End Sub
		Private Sub rbDefault_Checked(ByVal sender As Object, ByVal e As RoutedEventArgs)
			pivotGrid.LayoutChanged()
		End Sub
	End Class
End Namespace
