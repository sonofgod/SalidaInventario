<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gridDeProductos
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvProductos = New System.Windows.Forms.DataGridView()
        Me.clave = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.existencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.linea = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.marca = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fabricante = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvProductos
        '
        Me.dgvProductos.AllowUserToAddRows = False
        Me.dgvProductos.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvProductos.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvProductos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvProductos.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvProductos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProductos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.clave, Me.descripcion, Me.existencia, Me.linea, Me.marca, Me.fabricante})
        Me.dgvProductos.GridColor = System.Drawing.SystemColors.ActiveBorder
        Me.dgvProductos.Location = New System.Drawing.Point(12, 12)
        Me.dgvProductos.Name = "dgvProductos"
        Me.dgvProductos.ReadOnly = True
        Me.dgvProductos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvProductos.RowHeadersVisible = False
        Me.dgvProductos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvProductos.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvProductos.Size = New System.Drawing.Size(943, 372)
        Me.dgvProductos.TabIndex = 0
        '
        'clave
        '
        Me.clave.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.clave.FillWeight = 20.0!
        Me.clave.HeaderText = "Clave"
        Me.clave.Name = "clave"
        Me.clave.ReadOnly = True
        Me.clave.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'descripcion
        '
        Me.descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.descripcion.HeaderText = "Descripción"
        Me.descripcion.Name = "descripcion"
        Me.descripcion.ReadOnly = True
        '
        'existencia
        '
        Me.existencia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.existencia.FillWeight = 30.0!
        Me.existencia.HeaderText = "Existencia"
        Me.existencia.Name = "existencia"
        Me.existencia.ReadOnly = True
        '
        'linea
        '
        Me.linea.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.linea.FillWeight = 50.0!
        Me.linea.HeaderText = "Linea"
        Me.linea.Name = "linea"
        Me.linea.ReadOnly = True
        '
        'marca
        '
        Me.marca.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.marca.FillWeight = 50.0!
        Me.marca.HeaderText = "Marca"
        Me.marca.Name = "marca"
        Me.marca.ReadOnly = True
        '
        'fabricante
        '
        Me.fabricante.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.fabricante.FillWeight = 50.0!
        Me.fabricante.HeaderText = "Fabricante"
        Me.fabricante.Name = "fabricante"
        Me.fabricante.ReadOnly = True
        '
        'gridDeProductos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(967, 396)
        Me.Controls.Add(Me.dgvProductos)
        Me.KeyPreview = True
        Me.Name = "gridDeProductos"
        Me.Text = "Lista de productos"
        CType(Me.dgvProductos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvProductos As System.Windows.Forms.DataGridView
    Friend WithEvents clave As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents descripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents existencia As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents linea As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents marca As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fabricante As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
