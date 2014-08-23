Imports ADODB

Public Class gridDeProductos

    Private rst_PRODSFILTRADOS As Recordset
    Public Event productoChange(ByVal codigoSelected As String)


    Public Property productosFiltrados() As Recordset
        Get
            Return rst_PRODSFILTRADOS
        End Get
        Set(value As Recordset)
            rst_PRODSFILTRADOS = value
        End Set
    End Property

    Public Sub New(ByVal productosFiltrados As Recordset)
        InitializeComponent()
        Me.rst_PRODSFILTRADOS = productosFiltrados

        While Not rst_PRODSFILTRADOS.EOF

            Dim row As DataGridViewRow = New DataGridViewRow()
            Dim dgvCell As DataGridViewCell

            dgvCell = New DataGridViewTextBoxCell()
            dgvCell.Value = rst_PRODSFILTRADOS.Fields("Clave").Value.ToString
            row.Cells.Add(dgvCell)

            dgvCell = New DataGridViewTextBoxCell()
            dgvCell.Value = rst_PRODSFILTRADOS.Fields("Descripcion").Value.ToString
            row.Cells.Add(dgvCell)

            dgvCell = New DataGridViewTextBoxCell()
            dgvCell.Value = rst_PRODSFILTRADOS.Fields("Existencia").Value
            row.Cells.Add(dgvCell)

            dgvCell = New DataGridViewTextBoxCell()
            dgvCell.Value = rst_PRODSFILTRADOS.Fields("Linea").Value.ToString
            row.Cells.Add(dgvCell)

            dgvCell = New DataGridViewTextBoxCell()
            dgvCell.Value = rst_PRODSFILTRADOS.Fields("Marca").Value.ToString
            row.Cells.Add(dgvCell)

            dgvCell = New DataGridViewTextBoxCell()
            dgvCell.Value = rst_PRODSFILTRADOS.Fields("Fabricante").Value.ToString
            row.Cells.Add(dgvCell)

            dgvProductos.Rows.Add(row)

            rst_PRODSFILTRADOS.MoveNext()
        End While

    End Sub

    Private Sub dgvProductos_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellDoubleClick
        enviarCodigoSeleccionado(CStr(dgvProductos.Item(0, e.RowIndex).Value))
    End Sub


    Private Sub enviarCodigoSeleccionado(ByVal codigo As String)
        RaiseEvent productoChange(codigo)
    End Sub

    Private Sub gridDeProductos_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub dgvProductos_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvProductos.KeyDown
        If e.KeyCode = Keys.Enter Then
            RaiseEvent productoChange(CStr(dgvProductos.CurrentRow.Cells(0).Value))
        End If
    End Sub
End Class