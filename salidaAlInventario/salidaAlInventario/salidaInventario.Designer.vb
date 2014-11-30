<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class salidasainventario
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFechaEmision = New System.Windows.Forms.TextBox()
        Me.cbxAlmacenO = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbxAlmacenD = New System.Windows.Forms.ComboBox()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.grpAlmacenes = New System.Windows.Forms.GroupBox()
        Me.grpAsignaciones = New System.Windows.Forms.GroupBox()
        Me.cbxReponsableRecibe = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbxReponsableTraslado = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkBoxEvitarDuplicados = New System.Windows.Forms.CheckBox()
        Me.btnComenCaptura = New System.Windows.Forms.Button()
        Me.dgvProductos = New System.Windows.Forms.DataGridView()
        Me.colClave = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCostoUltimo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colExistencias = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Costo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtBoxCodigoBarras = New System.Windows.Forms.TextBox()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.cbxConcepto = New System.Windows.Forms.ComboBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpAlmacenes.SuspendLayout()
        Me.grpAsignaciones.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Concepto:"
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(76, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Fecha emisión:"
        '
        'txtFechaEmision
        '
        Me.txtFechaEmision.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFechaEmision.Enabled = False
        Me.txtFechaEmision.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFechaEmision.Location = New System.Drawing.Point(5, 37)
        Me.txtFechaEmision.Name = "txtFechaEmision"
        Me.txtFechaEmision.ReadOnly = True
        Me.txtFechaEmision.Size = New System.Drawing.Size(243, 29)
        Me.txtFechaEmision.TabIndex = 1
        Me.txtFechaEmision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbxAlmacenO
        '
        Me.cbxAlmacenO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbxAlmacenO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbxAlmacenO.Enabled = False
        Me.cbxAlmacenO.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.cbxAlmacenO.FormattingEnabled = True
        Me.cbxAlmacenO.Location = New System.Drawing.Point(14, 37)
        Me.cbxAlmacenO.Name = "cbxAlmacenO"
        Me.cbxAlmacenO.Size = New System.Drawing.Size(385, 24)
        Me.cbxAlmacenO.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(341, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Origen:"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(334, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 16)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Destino:"
        '
        'cbxAlmacenD
        '
        Me.cbxAlmacenD.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbxAlmacenD.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbxAlmacenD.Enabled = False
        Me.cbxAlmacenD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.cbxAlmacenD.FormattingEnabled = True
        Me.cbxAlmacenD.Location = New System.Drawing.Point(14, 83)
        Me.cbxAlmacenD.Name = "cbxAlmacenD"
        Me.cbxAlmacenD.Size = New System.Drawing.Size(385, 24)
        Me.cbxAlmacenD.TabIndex = 3
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'grpAlmacenes
        '
        Me.grpAlmacenes.AutoSize = True
        Me.grpAlmacenes.Controls.Add(Me.Label3)
        Me.grpAlmacenes.Controls.Add(Me.cbxAlmacenD)
        Me.grpAlmacenes.Controls.Add(Me.cbxAlmacenO)
        Me.grpAlmacenes.Controls.Add(Me.Label4)
        Me.grpAlmacenes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpAlmacenes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.grpAlmacenes.Location = New System.Drawing.Point(16, 41)
        Me.grpAlmacenes.Name = "grpAlmacenes"
        Me.grpAlmacenes.Size = New System.Drawing.Size(417, 128)
        Me.grpAlmacenes.TabIndex = 2
        Me.grpAlmacenes.TabStop = False
        Me.grpAlmacenes.Text = "Almacenes:"
        '
        'grpAsignaciones
        '
        Me.grpAsignaciones.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpAsignaciones.AutoSize = True
        Me.grpAsignaciones.Controls.Add(Me.cbxReponsableRecibe)
        Me.grpAsignaciones.Controls.Add(Me.Label6)
        Me.grpAsignaciones.Controls.Add(Me.cbxReponsableTraslado)
        Me.grpAsignaciones.Controls.Add(Me.Label5)
        Me.grpAsignaciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpAsignaciones.Location = New System.Drawing.Point(711, 41)
        Me.grpAsignaciones.Name = "grpAsignaciones"
        Me.grpAsignaciones.Size = New System.Drawing.Size(417, 128)
        Me.grpAsignaciones.TabIndex = 3
        Me.grpAsignaciones.TabStop = False
        Me.grpAsignaciones.Text = "Asignaciones:"
        '
        'cbxReponsableRecibe
        '
        Me.cbxReponsableRecibe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbxReponsableRecibe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbxReponsableRecibe.Enabled = False
        Me.cbxReponsableRecibe.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.cbxReponsableRecibe.FormattingEnabled = True
        Me.cbxReponsableRecibe.Location = New System.Drawing.Point(15, 83)
        Me.cbxReponsableRecibe.Name = "cbxReponsableRecibe"
        Me.cbxReponsableRecibe.Size = New System.Drawing.Size(385, 24)
        Me.cbxReponsableRecibe.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(217, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(183, 16)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Responsable que recibe:"
        '
        'cbxReponsableTraslado
        '
        Me.cbxReponsableTraslado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbxReponsableTraslado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbxReponsableTraslado.Enabled = False
        Me.cbxReponsableTraslado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.cbxReponsableTraslado.FormattingEnabled = True
        Me.cbxReponsableTraslado.Location = New System.Drawing.Point(15, 37)
        Me.cbxReponsableTraslado.Name = "cbxReponsableTraslado"
        Me.cbxReponsableTraslado.Size = New System.Drawing.Size(385, 24)
        Me.cbxReponsableTraslado.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(208, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(192, 16)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Responsable del traslado:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chkBoxEvitarDuplicados)
        Me.GroupBox1.Controls.Add(Me.btnComenCaptura)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtFechaEmision)
        Me.GroupBox1.Location = New System.Drawing.Point(445, 41)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(254, 128)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'chkBoxEvitarDuplicados
        '
        Me.chkBoxEvitarDuplicados.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkBoxEvitarDuplicados.AutoSize = True
        Me.chkBoxEvitarDuplicados.Checked = True
        Me.chkBoxEvitarDuplicados.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBoxEvitarDuplicados.Location = New System.Drawing.Point(58, 72)
        Me.chkBoxEvitarDuplicados.Name = "chkBoxEvitarDuplicados"
        Me.chkBoxEvitarDuplicados.Size = New System.Drawing.Size(131, 20)
        Me.chkBoxEvitarDuplicados.TabIndex = 2
        Me.chkBoxEvitarDuplicados.Text = "Evitar duplicados"
        Me.chkBoxEvitarDuplicados.UseVisualStyleBackColor = True
        '
        'btnComenCaptura
        '
        Me.btnComenCaptura.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnComenCaptura.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnComenCaptura.Location = New System.Drawing.Point(6, 95)
        Me.btnComenCaptura.Name = "btnComenCaptura"
        Me.btnComenCaptura.Size = New System.Drawing.Size(242, 27)
        Me.btnComenCaptura.TabIndex = 3
        Me.btnComenCaptura.Text = "Comenzar captura"
        Me.btnComenCaptura.UseVisualStyleBackColor = True
        '
        'dgvProductos
        '
        Me.dgvProductos.AllowUserToAddRows = False
        Me.dgvProductos.AllowUserToResizeColumns = False
        Me.dgvProductos.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvProductos.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvProductos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvProductos.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvProductos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProductos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colClave, Me.colDescripcion, Me.colQty, Me.colCostoUltimo, Me.colExistencias, Me.Costo})
        Me.dgvProductos.GridColor = System.Drawing.SystemColors.ActiveBorder
        Me.dgvProductos.Location = New System.Drawing.Point(16, 175)
        Me.dgvProductos.Name = "dgvProductos"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvProductos.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvProductos.RowHeadersVisible = False
        Me.dgvProductos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvProductos.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvProductos.RowTemplate.Height = 30
        Me.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvProductos.Size = New System.Drawing.Size(1112, 433)
        Me.dgvProductos.TabIndex = 5
        Me.dgvProductos.TabStop = False
        '
        'colClave
        '
        Me.colClave.FillWeight = 50.0!
        Me.colClave.HeaderText = "Clave"
        Me.colClave.MaxInputLength = 13
        Me.colClave.Name = "colClave"
        Me.colClave.ReadOnly = True
        '
        'colDescripcion
        '
        Me.colDescripcion.HeaderText = "Descripción"
        Me.colDescripcion.Name = "colDescripcion"
        Me.colDescripcion.ReadOnly = True
        '
        'colQty
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colQty.DefaultCellStyle = DataGridViewCellStyle3
        Me.colQty.FillWeight = 50.0!
        Me.colQty.HeaderText = "Cant / Peso "
        Me.colQty.Name = "colQty"
        '
        'colCostoUltimo
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colCostoUltimo.DefaultCellStyle = DataGridViewCellStyle4
        Me.colCostoUltimo.FillWeight = 50.0!
        Me.colCostoUltimo.HeaderText = "Precio"
        Me.colCostoUltimo.Name = "colCostoUltimo"
        Me.colCostoUltimo.ReadOnly = True
        '
        'colExistencias
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colExistencias.DefaultCellStyle = DataGridViewCellStyle5
        Me.colExistencias.FillWeight = 50.0!
        Me.colExistencias.HeaderText = "Existencia"
        Me.colExistencias.Name = "colExistencias"
        Me.colExistencias.ReadOnly = True
        '
        'Costo
        '
        Me.Costo.HeaderText = "Costo"
        Me.Costo.Name = "Costo"
        Me.Costo.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Costo.Visible = False
        '
        'txtBoxCodigoBarras
        '
        Me.txtBoxCodigoBarras.AcceptsTab = True
        Me.txtBoxCodigoBarras.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtBoxCodigoBarras.Location = New System.Drawing.Point(13, 614)
        Me.txtBoxCodigoBarras.MinimumSize = New System.Drawing.Size(4, 40)
        Me.txtBoxCodigoBarras.Name = "txtBoxCodigoBarras"
        Me.txtBoxCodigoBarras.Size = New System.Drawing.Size(310, 22)
        Me.txtBoxCodigoBarras.TabIndex = 6
        Me.txtBoxCodigoBarras.Tag = ""
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.Location = New System.Drawing.Point(1009, 614)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(119, 55)
        Me.btnAceptar.TabIndex = 9
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.Location = New System.Drawing.Point(884, 614)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(119, 55)
        Me.btnCancelar.TabIndex = 8
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'cbxConcepto
        '
        Me.cbxConcepto.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbxConcepto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbxConcepto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbxConcepto.FormattingEnabled = True
        Me.cbxConcepto.Location = New System.Drawing.Point(97, 11)
        Me.cbxConcepto.Name = "cbxConcepto"
        Me.cbxConcepto.Size = New System.Drawing.Size(1031, 24)
        Me.cbxConcepto.TabIndex = 1
        '
        'txtTotal
        '
        Me.txtTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTotal.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(726, 638)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(152, 31)
        Me.txtTotal.TabIndex = 7
        Me.txtTotal.Text = "0.0"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(723, 619)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 16)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "TOTAL"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(698, 641)
        Me.Label8.Margin = New System.Windows.Forms.Padding(0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(25, 25)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "$"
        '
        'salidasainventario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(1141, 682)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.cbxConcepto)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.txtBoxCodigoBarras)
        Me.Controls.Add(Me.dgvProductos)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpAsignaciones)
        Me.Controls.Add(Me.grpAlmacenes)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Name = "salidasainventario"
        Me.Padding = New System.Windows.Forms.Padding(10)
        Me.Text = "Salidas al inventario"
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpAlmacenes.ResumeLayout(False)
        Me.grpAlmacenes.PerformLayout()
        Me.grpAsignaciones.ResumeLayout(False)
        Me.grpAsignaciones.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvProductos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFechaEmision As System.Windows.Forms.TextBox
    Friend WithEvents cbxAlmacenO As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbxAlmacenD As System.Windows.Forms.ComboBox
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents grpAlmacenes As System.Windows.Forms.GroupBox
    Friend WithEvents grpAsignaciones As System.Windows.Forms.GroupBox
    Friend WithEvents cbxReponsableTraslado As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbxReponsableRecibe As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvProductos As System.Windows.Forms.DataGridView
    Friend WithEvents txtBoxCodigoBarras As System.Windows.Forms.TextBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents cbxConcepto As System.Windows.Forms.ComboBox
    Friend WithEvents btnComenCaptura As System.Windows.Forms.Button
    Friend WithEvents chkBoxEvitarDuplicados As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents colClave As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCostoUltimo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colExistencias As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Costo As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
