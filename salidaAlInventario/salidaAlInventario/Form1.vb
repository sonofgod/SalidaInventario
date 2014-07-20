Imports ADODB
Imports System.Data.SqlClient
Imports Stimulsoft.Report
Imports Stimulsoft.Report.Dictionary
Imports MyBInventario
Imports MyBGenericFunctions
Imports MyBScripting
Imports MyBAlmacen
Imports MyBQuery

Public Class salidasainventario
    Private DB_CONN As String
    Private DB_CONN_INTERNO As String
    Private NUM_SALIDA As String
    Private ESTACION As String

    Private rst_EMPRESA As Recordset
    Private rst_CURRENT_ESTACION As Recordset
    Private rst_CONCEPTOS As Recordset
    Private rst_ALMACENES As Recordset
    Private rst_EMPLEADOS As Recordset
    Private rst_CONSECUTIVOS As Recordset
    Dim currentSalida As Integer = 0
    Dim tipoDeMovimiento As String
    Dim fechaDeEmision As String
    Dim almOrigen As String = ""
    Dim almDestino As String = ""



    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim DB_BRUTO As String
        If Environment.GetCommandLineArgs.Length >= 2 Then

            DB_BRUTO = Environment.GetCommandLineArgs(1)
            DB_CONN = Replace(DB_BRUTO, "%", " ")
            ESTACION = Environment.GetCommandLineArgs(2)
            NUM_SALIDA = Environment.GetCommandLineArgs(3)

            'ticket
            'Provider=SQLNCLI.1;Password=12345678;Persist Security Info=True;User ID=sa;Initial Catalog=C:\MyBusinessDatabase\MyBusinessPOS2012.mdf;Data Source=TCP:.\SQLEXPRESS,1400;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=SONOFGOD-PC;Use Encryption for Data=False;Tag with column collation when possible=False;MARS Connection=False;DataTypeCompatibility=0;Trust Server Certificate=False
            '19:

        Else
            'TextBox1.Text = "No se han indicado parámetros en la línea de comandos" & vbCrLf & _
            '                "El nombre (y path) del ejecutable es:" & vbCrLf & _
            ' Environment.GetCommandLineArgs(0)

            ' TIPO_DOC = "ticket"
            DB_CONN = "Provider=SQLNCLI.1;Password=12345678;Persist Security Info=True;User ID=sa;Initial Catalog=C:\MyBusinessDatabase\MyBusinessPOS2012.mdf;Data Source=SONOFGOD-PC\SQLEXPRESS,1433;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=SONOFGOD-PC;Use Encryption for Data=False;Tag with column collation when possible=False;MARS Connection=False;DataTypeCompatibility=0;Trust Server Certificate=False;"
            DB_CONN_INTERNO = "Password=12345678;Persist Security Info=True;User ID=sa;Initial Catalog=C:\MyBusinessDatabase\MyBusinessPOS2012.mdf;Data Source=SONOFGOD-PC\SQLEXPRESS,1433;Packet Size=4096;Workstation ID=SONOFGOD-PC;"
            ESTACION = "BODEGA01"
            NUM_SALIDA = "5"

        End If

        rst_EMPRESA = crearRecorset("SELECT * FROM econfig")
        rst_CURRENT_ESTACION = crearRecorset("SELECT * FROM estaciones WHERE Estacion = '" & ESTACION & "'")
        rst_ALMACENES = crearRecorset("SELECT * FROM almacen")
        rst_EMPLEADOS = crearRecorset("SELECT * FROM empleados")
        rst_CONSECUTIVOS = crearRecorset("SELECT * FROM Consec")

        If rst_CURRENT_ESTACION.EOF Then
            MsgBox("No se encuentra la estación solicitada: " & ESTACION, vbInformation)
            Exit Sub
        End If

        rst_CONCEPTOS = crearRecorset("SELECT * FROM tipominv")

        'Dim coleccionConceptos As New AutoCompleteStringCollection()
        While Not rst_CONCEPTOS.EOF
            'coleccionConceptos.Add(rst_CONCEPTOS.Fields("DESCRIP").Value.ToString)
            cbxConcepto.Items.Add(rst_CONCEPTOS.Fields("DESCRIP").Value.ToString.ToUpper)
            rst_CONCEPTOS.MoveNext()
        End While
        'txtboxConceptos.AutoCompleteCustomSource = coleccionConceptos

        txtFechaEmision.Text = Format(Now, "dd-MM-yyyy")
        cbxAlmacenO.Items.Add("NINGUNO")
        cbxAlmacenD.Items.Add("NINGUNO")
        While Not rst_ALMACENES.EOF
            cbxAlmacenO.Items.Add(rst_ALMACENES.Fields("Descrip").Value.ToString.ToUpper & "   (" & rst_ALMACENES.Fields("Almacen").Value.ToString & ")")
            cbxAlmacenD.Items.Add(rst_ALMACENES.Fields("Descrip").Value.ToString.ToUpper & "   (" & rst_ALMACENES.Fields("Almacen").Value.ToString & ")")
            rst_ALMACENES.MoveNext()
        End While

        cbxReponsableTraslado.Items.Add("NINGUNO")
        cbxReponsableRecibe.Items.Add("NINGUNO")
        While Not rst_EMPLEADOS.EOF
            cbxReponsableTraslado.Items.Add(rst_EMPLEADOS.Fields("nombre").Value.ToString.ToUpper)
            cbxReponsableRecibe.Items.Add(rst_EMPLEADOS.Fields("nombre").Value.ToString.ToUpper)
            rst_EMPLEADOS.MoveNext()
        End While

        cbxAlmacenO.SelectedIndex = 0
        cbxAlmacenD.SelectedIndex = 0
        cbxReponsableTraslado.SelectedIndex = 0
        cbxReponsableRecibe.SelectedIndex = 0


        With dgvProductos
            .DataSource = Nothing
            .Rows.Clear()
            '.Rows.Add(1)
            '.Rows(0).Cells(0).Value = "0000000000000"
            '.Rows(0).Cells(1).Value = "SALCHICHA PAVO MULTIEMPAQUE 8 X 266 G FUD"
        End With

        ' Dim Report As StiReport = New StiReport()
        '   Report.Load("report.mrt")
        '   Report.Show()

        'Dim invent As Inventario2004 = New MyBInventario.Inventario2004


        'Dim almacen As Almacen = New MyBAlmacen.Almacen

        ' Dim func As DISLibreria = New MyBGenericFunctions.DISLibreria
        'func.TraeSiguiente()

        dgvProductos.Enabled = False
        txtBoxCodigoBarras.Enabled = False
        btnAceptar.Enabled = False
        btnComenCaptura.Enabled = False

    End Sub

    'Function CreaSalida() As

    '    Dim func As DISLibreria = New MyBGenericFunctions.DISLibreria

    '    'Dim query As MyBQuery.BSQLBuilder = 

    '    Dim Query As MyBQuery.BSQLBuilder = func.NewQuery
    '    Query.Connection = DB_CONN

    '    If Salida = 0 Then
    '        Query.Reset()
    '        Query.strState = "INSERT"

    '        Salida = TraeSiguiente("MovSal", Ambiente.Connection)

    '        Query.AddField("salidas", "Salida", Salida)
    '        Query.AddField("salidas", "ocupado", 1)
    '        Query.AddField("salidas", "tipo_doc", "EX-")
    '        Query.Add()
    '        "salidas","F_EMISION", Date
    '        Query.AddField("salidas", "IMPORTE", 0)
    '        Query.AddField("salidas", "COSTO", 0)
    '        Query.AddField("salidas", "ALMACEN", 1)
    '        Query.AddField("salidas", "ESTADO", "CO")
    '        Query.AddField("salidas", "OBSERV", "Salida automÃ¡tica por excel")
    '        Query.AddField("salidas", "DATOS", "")
    '        Query.AddField("salidas", "USUARIO", Ambiente.Uid)
    '   Query.AddField "salidas","USUFECHA", Date
    '        Query.AddField("salidas", "USUHORA", Libreria.Formato(Time, "hh:mm:ss"))
    '        Query.AddField("salidas", "traspaso", 0)
    '        Query.AddField("salidas", "almt", 0)
    '        Query.AddField("salidas", "estraspaso", 0)
    '        Query.AddField("salidas", "sucursal", "")
    '        Query.AddField("salidas", "esparasucursal", 0)
    '        Query.AddField("salidas", "entsuc", 0)
    '        Query.AddField("salidas", "folio", TraeSiguiente(Trim(Ambiente.Estacion) & "salida", Ambiente.Connection))
    '        Query.AddField("salidas", "estacion", Ambiente.Estacion)
    '        Query.CreateQuery()

    '        Query.Reset()
    '        Query.strState = "INSERT"
    '        Query.AddField("salpart", "SALIDA", Salida)
    '        Query.AddField("salpart", "TIPO_DOC", "EX-")
    '        Query.AddField("salpart", "NO_REFEREN", Salida)
    '        Query.AddField("salpart", "ARTICULO", Articulo)
    '        Query.AddField("salpart", "CANTIDAD", Cantidad * -1)
    '        Query.AddField("salpart", "PRECIO", Precio)
    '        Query.AddField("salpart", "OBSERV", Descripcion)
    '        Query.AddField("salpart", "PARTIDA", 0)
    '        Query.AddField("salpart", "ID_SALIDA", TraeSiguiente("salpart", Ambiente.Connection))
    '        Query.AddField("salpart", "Usuario", Ambiente.Uid)
    'Query.AddField "salpart","UsuFecha", Date
    '        Query.AddField("salpart", "UsuHora", Libreria.Formato(Time, "hh:mm:ss"))
    '        Query.AddField("salpart", "PRCANTIDAD", 0)
    '        Query.AddField("salpart", "PRDESCRIP", "")
    '        Query.AddField("salpart", "CLAVEADD", "")
    '        Query.AddField("salpart", "costo", 0)
    '        Query.CreateQuery()
    '        Query.Execute()

    '        CreaSalida = Salida

    'End Function

    Function crearRecorset(ByVal SQLConsulta As String) As Recordset
        Dim recorset As Recordset = New Recordset

        Try
            recorset.Open(SQLConsulta, DB_CONN, CursorTypeEnum.adOpenForwardOnly, LockTypeEnum.adLockReadOnly)
        Catch SQLexc As SqlException
            MsgBox("Hubo un error al crear el recordSet" & SQLexc.Message)
        End Try
        Return recorset
    End Function

    Private Function isValidaSeleccionDeEmpleado(ByVal comboBox As ComboBox, ByRef errorMessage As String) As Boolean
        If comboBox.SelectedIndex <> 0 And comboBox.SelectedIndex <> 0 Then
            If comboBox.Name = "cbxReponsableTraslado" Then
                If comboBox.SelectedIndex = cbxReponsableRecibe.SelectedIndex Then
                    errorMessage = "El empleado responsable del traslado no puede ser el mismo que el que recibe." & vbNewLine & vbNewLine & "Favor de corregir los datos."
                    Return False
                End If
            ElseIf comboBox.Name = "cbxReponsableRecibe" Then
                If comboBox.SelectedIndex = cbxReponsableTraslado.SelectedIndex Then
                    errorMessage = "El empleado responsable que recibe no puede ser el mismo que el de traslado." & vbNewLine & vbNewLine & "Favor de corregir los datos."
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Sub salidasainventario_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        cbxConcepto.Focus()
    End Sub

    Private Sub txtBoxCodigoBarras_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtBoxCodigoBarras.KeyUp

        If e.KeyCode = Keys.Enter Then

            Dim pesoReal As Decimal
            Dim peso As String
            Dim tipoCodigo As String
            Dim codigo As String = ""

            Dim currenArticulo As String = txtBoxCodigoBarras.Text.Trim

            If (currenArticulo.Length > 4) Then

                tipoCodigo = Mid(currenArticulo, 1, 2)

                If tipoCodigo = "20" Then
                    peso = Mid(currenArticulo, 9, 4)
                    pesoReal = CDec(peso) / 100
                    codigo = Mid(currenArticulo, 3, 4)
                ElseIf tipoCodigo = "30" Then
                    peso = Mid(currenArticulo, 8, 5)
                    pesoReal = CDec(peso) / 1000
                    codigo = Mid(currenArticulo, 4, 4)
                End If

            ElseIf (currenArticulo.Length = 4) Then

            End If

            Dim rst_Producto As Recordset = crearRecorset("SELECT * FROM prods where Articulo = " & codigo)

            If Not rst_Producto.EOF Then

                Dim row As DataGridViewRow = New DataGridViewRow()
                Dim dgvCell As DataGridViewCell

                dgvCell = New DataGridViewTextBoxCell()
                dgvCell.Value = codigo
                row.Cells.Add(dgvCell)

                dgvCell = New DataGridViewTextBoxCell()
                dgvCell.Value = rst_Producto.Fields("Descrip").Value
                row.Cells.Add(dgvCell)

                dgvCell = New DataGridViewTextBoxCell()
                dgvCell.Value = pesoReal
                row.Cells.Add(dgvCell)

                dgvCell = New DataGridViewTextBoxCell()
                dgvCell.Value = rst_Producto.Fields("PRECIO1").Value
                row.Cells.Add(dgvCell)

                Dim exisActual As Decimal = CDec(rst_Producto.Fields("EXISTENCIA").Value)
                dgvCell = New DataGridViewTextBoxCell()
                dgvCell.Value = (exisActual - pesoReal)
                row.Cells.Add(dgvCell)

                dgvProductos.Rows.Add(row)
            End If

        End If

    End Sub

    Private Sub txtBoxCodigoBarras_Leave(sender As Object, e As System.EventArgs) Handles txtBoxCodigoBarras.Leave

        'btnCancelar.Focus()
    End Sub

    Private Sub btnAceptar_Leave(sender As Object, e As System.EventArgs) Handles btnAceptar.Leave
        'cbxConcepto.Focus()
    End Sub

    Private Sub cbxConcepto_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cbxConcepto.SelectedValueChanged

        'Dim comboBox As ComboBox = CType(sender, ComboBox)
        'Dim selectedConcepto = CType(comboBox.SelectedItem, String)

        'If selectedConcepto.Length > 0 Then

        '    btnComenCaptura.Enabled = True

        '    If InStr(selectedConcepto, "TRANSFERENCIA") > 0 And Not cbxAlmacenO.Enabled Then
        '        cbxAlmacenO.Enabled = True
        '        cbxAlmacenD.Enabled = True
        '        cbxReponsableTraslado.Enabled = True
        '        cbxReponsableRecibe.Enabled = True

        '        Dim almEstacion As String = ""
        '        If Not rst_CURRENT_ESTACION.EOF Then
        '            almEstacion = rst_CURRENT_ESTACION.Fields("Almacen").Value.ToString
        '        End If

        '        Dim rst_AlmacenElegido As Recordset = crearRecorset("select * from almacen where Almacen = " & almEstacion)

        '        Dim cadenaABuscar As String = ""
        '        If Not rst_AlmacenElegido.EOF Then
        '            cadenaABuscar = rst_AlmacenElegido.Fields("Descrip").Value.ToString & "   (" & rst_AlmacenElegido.Fields("Almacen").Value.ToString & ")"
        '        End If

        '        Dim index As Integer = cbxAlmacenO.FindString(cadenaABuscar)
        '        If (index < 0) Then
        '            MessageBox.Show("Item not found.")
        '            cbxAlmacenO.Focus()
        '        Else
        '            cbxAlmacenO.SelectedIndex = index
        '            cbxAlmacenD.Focus()
        '        End If

        '    Else
        '    End If

        'Else
        '    btnComenCaptura.Enabled = False
        'End If


    End Sub

    Private Sub btnComenCaptura_Click(sender As System.Object, e As System.EventArgs) Handles btnComenCaptura.Click

        iniciarSalidaEnElInventario()

        dgvProductos.Enabled = True
        btnAceptar.Enabled = True
        txtBoxCodigoBarras.Enabled = True

        cbxConcepto.Enabled = False
        cbxAlmacenO.Enabled = False
        cbxAlmacenD.Enabled = False
        cbxReponsableTraslado.Enabled = False
        cbxReponsableRecibe.Enabled = False

        btnComenCaptura.Enabled = False
        currentSalida += 1

    End Sub


    Private Sub iniciarSalidaEnElInventario()

        rst_CONSECUTIVOS.Filter = "Dato = 'Salida'"
        If Not rst_CONSECUTIVOS.EOF Then
            currentSalida = CInt(rst_CONSECUTIVOS.Fields("Consec").Value)
        End If


        If (cbxAlmacenO.SelectedIndex > 0) Then
            almOrigen = cbxAlmacenO.SelectedItem.ToString
            almOrigen = Mid(almOrigen, InStr(almOrigen, "(") + 1, 1)
        End If


        If (cbxAlmacenO.SelectedIndex > 0) Then
            almDestino = cbxAlmacenD.SelectedItem.ToString
            almDestino = Mid(almDestino, InStr(almDestino, "(") + 1, 1)
        End If

        Dim esTraspaso As Integer
        If InStr(cbxConcepto.SelectedItem.ToString, "TRANSFERENCIA") > 0 Then
            esTraspaso = 1
        Else
            esTraspaso = 0
        End If

        Dim sqlStatementSalidas As String = "INSERT INTO salidas ([Salida],[ocupado],[tipo_doc],[F_EMISION],[IMPORTE],[COSTO],[ALMACEN],[ESTADO],[OBSERV],[DATOS],[USUARIO],[USUFECHA],[USUHORA],[almt],[estraspaso],[sucursal],[esparasucursal],[empleado],[paraempleado],[direccionembarque]) "
        sqlStatementSalidas &= "VALUES (@salida,@ocupado,@tipoDoc,@fEmision,@importe,@costo,@almacen,@estado,@observacion,@datos,@usuario,@usufecha,@usuhora,@almt,@esTraspaso,@sucursal,@esParaSucursal,@empleado,@paraEmpleado,@direccionEmbarque)"

        rst_CONCEPTOS.MoveFirst()
        rst_CONCEPTOS.Move(cbxConcepto.SelectedIndex)
        tipoDeMovimiento = rst_CONCEPTOS.Fields("TIPO_MOVIM").Value.ToString

        rst_EMPLEADOS.MoveFirst()
        rst_EMPLEADOS.Move(cbxReponsableRecibe.SelectedIndex)
        Dim empleadoResponsable As String = rst_EMPLEADOS.Fields("empleado").Value.ToString

        rst_EMPLEADOS.MoveFirst()
        rst_EMPLEADOS.Move(cbxReponsableRecibe.SelectedIndex)
        Dim empleadoRecibe As String = rst_EMPLEADOS.Fields("empleado").Value.ToString

        fechaDeEmision = CType(txtFechaEmision.Text, DateTime).ToString("dd-MM-yyyy")

        Using xConn As New SqlConnection(DB_CONN_INTERNO)
            Try
                Dim xComm As New SqlCommand(sqlStatementSalidas, xConn)
                With xComm
                    .CommandType = CommandType.Text
                    .Parameters.AddWithValue("@salida", CInt(currentSalida + 1).ToString)
                    .Parameters.AddWithValue("@ocupado", "1")
                    .Parameters.AddWithValue("@tipoDoc", tipoDeMovimiento)
                    .Parameters.AddWithValue("@fEmision", fechaDeEmision)
                    .Parameters.AddWithValue("@importe", 0)
                    .Parameters.AddWithValue("@costo", 0)
                    .Parameters.AddWithValue("@almacen", almOrigen)
                    .Parameters.AddWithValue("@estado", "PE")
                    .Parameters.AddWithValue("@observacion", "")
                    .Parameters.AddWithValue("@datos", cbxConcepto.SelectedItem.ToString)
                    .Parameters.AddWithValue("@usuario", "SUP")
                    .Parameters.AddWithValue("@usufecha", Format(Now, "dd-MM-yyyy"))
                    .Parameters.AddWithValue("@usuhora", Format(Now, "hh:mm:ss"))
                    .Parameters.AddWithValue("@almt", almDestino)
                    .Parameters.AddWithValue("@esTraspaso", esTraspaso)
                    .Parameters.AddWithValue("@sucursal", "")
                    .Parameters.AddWithValue("@esParaSucursal", 0)
                    ' .Parameters.AddWithValue("@fechaRetorno", 0)
                    .Parameters.AddWithValue("@empleado", empleadoResponsable)
                    .Parameters.AddWithValue("@paraEmpleado", 1)
                    .Parameters.AddWithValue("@direccionEmbarque", "")
                End With

                xConn.Open()
                xComm.ExecuteNonQuery()
                xComm.Dispose()
            Catch ex As SqlException
                MsgBox(ex.Message, MsgBoxStyle.Critical, "SqlException")
            Catch e As SystemException
                MsgBox(e.Message, MsgBoxStyle.Critical, "SystemException")
            End Try
        End Using

    End Sub

    Private Sub cbxConcepto_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbxConcepto.KeyPress

        If Char.IsLetter(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If

    End Sub

    Private Sub cbxAlmacenO_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbxAlmacenO.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub cbxAlmacenD_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbxAlmacenD.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub cbxResponsableTraslado_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbxReponsableTraslado.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub cbxReponsableRecibe_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbxReponsableRecibe.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub cbxConcepto_LostFocus(sender As System.Object, e As System.EventArgs) Handles cbxConcepto.LostFocus
        Dim comboBox As ComboBox = CType(sender, ComboBox)

        If Not comboBox.SelectedItem Is Nothing Then

            Dim selectedConcepto = CType(comboBox.SelectedItem, String)

            btnComenCaptura.Enabled = True

            If InStr(selectedConcepto, "TRANSFERENCIA") > 0 And Not cbxAlmacenO.Enabled Then
                cbxAlmacenO.Enabled = True
                cbxAlmacenD.Enabled = True
                cbxReponsableTraslado.Enabled = True
                cbxReponsableRecibe.Enabled = True

                Dim almEstacion As String = ""
                If Not rst_CURRENT_ESTACION.EOF Then
                    almEstacion = rst_CURRENT_ESTACION.Fields("Almacen").Value.ToString
                End If

                Dim rst_AlmacenElegido As Recordset = crearRecorset("select * from almacen where Almacen = " & almEstacion)

                Dim cadenaABuscar As String = ""
                If Not rst_AlmacenElegido.EOF Then
                    cadenaABuscar = rst_AlmacenElegido.Fields("Descrip").Value.ToString & "   (" & rst_AlmacenElegido.Fields("Almacen").Value.ToString & ")"
                End If

                Dim index As Integer = cbxAlmacenO.FindString(cadenaABuscar)
                If (index < 0) Then
                    MessageBox.Show("Item not found.")
                    cbxAlmacenO.Focus()
                Else
                    cbxAlmacenO.SelectedIndex = index
                    cbxAlmacenD.Focus()
                End If

            Else

            End If

        Else
            btnComenCaptura.Enabled = False

            cbxAlmacenO.Enabled = False
            cbxAlmacenD.Enabled = False
            cbxReponsableTraslado.Enabled = False
            cbxReponsableRecibe.Enabled = False

            cbxAlmacenO.SelectedIndex = 0
            cbxAlmacenD.SelectedIndex = 0
            cbxReponsableTraslado.SelectedIndex = 0
            cbxReponsableRecibe.SelectedIndex = 0
        End If
    End Sub


    Private Sub cbxAlmacenO_Validated(sender As Object, e As System.EventArgs) Handles cbxAlmacenO.Validated
        Me.ErrorProvider1.SetError(cbxAlmacenO, "")
    End Sub

    Private Sub cbxAlmacenO_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cbxAlmacenO.Validating
        Dim errorMsg As String = ""
        If Not isValidaSeleccionDeAlmacen(cbxAlmacenO, errorMsg) Then
            e.Cancel = True
            Me.ErrorProvider1.SetError(cbxAlmacenO, errorMsg)
        End If
    End Sub

    Private Sub cbxAlmacenD_Validated(sender As Object, e As System.EventArgs) Handles cbxAlmacenD.Validated
        Me.ErrorProvider1.SetError(cbxAlmacenD, "")
    End Sub

    Private Sub cbxAlmacenD_Validating(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles cbxAlmacenD.Validating
        Dim errorMsg As String = ""
        If Not isValidaSeleccionDeAlmacen(cbxAlmacenD, errorMsg) Then
            e.Cancel = True
            Me.ErrorProvider1.SetError(cbxAlmacenD, errorMsg)
        End If
    End Sub

    Private Function isValidaSeleccionDeAlmacen(ByVal comboBox As ComboBox, ByRef errorMessage As String) As Boolean
        If comboBox.SelectedIndex <> 0 And comboBox.SelectedIndex <> 0 Then
            If comboBox.Name = "cbxAlmacenD" Then
                If comboBox.SelectedIndex = cbxAlmacenO.SelectedIndex Then
                    errorMessage = "El almacen de destino no puede ser el mismo que el de origen." & vbNewLine & vbNewLine & "Favor de corregir los datos."
                    Return False
                End If
            ElseIf comboBox.Name = "cbxAlmacenO" Then
                If comboBox.SelectedIndex = cbxAlmacenD.SelectedIndex Then
                    errorMessage = "El almacen de origen no puede ser el mismo que el de destino." & vbNewLine & vbNewLine & "Favor de corregir los datos."
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Sub cbxReponsableTraslado_Validated(sender As Object, e As System.EventArgs) Handles cbxReponsableTraslado.Validated
        Me.ErrorProvider1.SetError(cbxReponsableTraslado, "")
    End Sub

    Private Sub cbxReponsableTraslado_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cbxReponsableTraslado.Validating
        Dim errorMsg As String = ""
        If Not isValidaSeleccionDeEmpleado(cbxReponsableTraslado, errorMsg) Then
            e.Cancel = True
            Me.ErrorProvider1.SetError(cbxReponsableTraslado, errorMsg)
        End If
    End Sub

    Private Sub cbxReponsableRecibe_Validated(sender As Object, e As System.EventArgs) Handles cbxReponsableRecibe.Validated
        Me.ErrorProvider1.SetError(cbxReponsableRecibe, "")
    End Sub

    Private Sub cbxReponsableRecibe_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cbxReponsableRecibe.Validating
        Dim errorMsg As String = ""
        If Not isValidaSeleccionDeEmpleado(cbxReponsableRecibe, errorMsg) Then
            e.Cancel = True
            Me.ErrorProvider1.SetError(cbxReponsableRecibe, errorMsg)
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelar.Click

        If (currentSalida > 0) Then

            Dim sqlStatementConsecutivos As String = "DELETE salidas WHERE Salida = @salida"

            Using xConn As New SqlConnection(DB_CONN_INTERNO)
                Try
                    Dim xComm As New SqlCommand(sqlStatementConsecutivos, xConn)
                    With xComm
                        .CommandType = CommandType.Text
                        .Parameters.AddWithValue("@salida", currentSalida.ToString)
                    End With

                    xConn.Open()
                    xComm.ExecuteNonQuery()
                    xComm.Dispose()

                Catch ex As SqlException
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "SqlException")

                Catch exc As SystemException
                    MsgBox(exc.Message, MsgBoxStyle.Critical, "SystemException")

                End Try
            End Using
        End If

        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(sender As System.Object, e As System.EventArgs) Handles btnAceptar.Click

        Dim sqlStatementSalPart As String = "INSERT INTO salpart(SALIDA,TIPO_DOC,ARTICULO,CANTIDAD,PRECIO,OBSERV,PARTIDA,ID_SALIDA,Usuario,UsuFecha,UsuHora,PRCANTIDAD,PRDESCRIP,CLAVEADD) "
        sqlStatementSalPart &= "VALUES (@salida,@tipoDoc,@articulo,@cantidad,@precio,@observacion,@partida,@idSalida,@usuario,@usuFecha,@usuHora,@prCantidad,@prDescrip,@claveAdd)"

        Dim consecutivoSalPartida As Integer
        rst_CONSECUTIVOS.Filter = "Dato = 'salpart'"
        If Not rst_CONSECUTIVOS.EOF Then
            consecutivoSalPartida = CInt(rst_CONSECUTIVOS.Fields("Consec").Value)
        End If

        Dim numPartida As Integer = 0


        For Each row As DataGridViewRow In dgvProductos.Rows

            consecutivoSalPartida += 1
            numPartida += 1

            Using xConn As New SqlConnection(DB_CONN_INTERNO)
                Try
                    Dim xComm As New SqlCommand(sqlStatementSalPart, xConn)
                    With xComm
                        .CommandType = CommandType.Text
                        .Parameters.AddWithValue("@salida", currentSalida.ToString)
                        .Parameters.AddWithValue("@tipoDoc", rst_CONCEPTOS.Fields("TIPO_MOVIM").Value.ToString)
                        .Parameters.AddWithValue("@articulo", row.Cells(0).Value.ToString)
                        .Parameters.AddWithValue("@cantidad", row.Cells(2).Value.ToString)
                        .Parameters.AddWithValue("@precio", row.Cells(3).Value.ToString)
                        .Parameters.AddWithValue("@observacion", row.Cells(1).Value.ToString)
                        .Parameters.AddWithValue("@partida", numPartida.ToString)
                        .Parameters.AddWithValue("@idSalida", consecutivoSalPartida.ToString)
                        .Parameters.AddWithValue("@usuario", "SUP")
                        .Parameters.AddWithValue("@usuFecha", Format(Now, "dd-MM-yyyy"))
                        .Parameters.AddWithValue("@usuHora", Format(Now, "hh:mm:ss"))
                        .Parameters.AddWithValue("@prCantidad", 0)
                        .Parameters.AddWithValue("@prDescrip", "")
                        .Parameters.AddWithValue("@claveAdd", "")
                    End With

                    xConn.Open()
                    xComm.ExecuteNonQuery()
                    xComm.Dispose()
                Catch ex As SqlException
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "SqlException")
                Catch e As SystemException
                    MsgBox(e.Message, MsgBoxStyle.Critical, "SystemException")
                End Try
            End Using


            'ACTUALIZACION DE LA EXISTENCIA EN EL ALMACÉN================================================================================================================================================================================
            actualizarExistenciaAlmacen(almOrigen, row.Cells(0).Value.ToString, "disminuir", CDec(row.Cells(2).Value))

            actualizarExistenciaAlmacen(almDestino, row.Cells(0).Value.ToString, "disminuir", CDec(row.Cells(2).Value))


            'Dim rst_Productos As Recordset = crearRecorset("SELECT * FROM prods")
            'rst_Productos.Filter = "ARTICULO = '" & row.Cells(0).Value.ToString & "'"

            Dim sqlStmtMovInv As String
            sqlStmtMovInv = "INSERT INTO movsinv (OPERACION,MOVIMIENTO,ENT_SAL,TIPO_MOVIM,NO_REFEREN,ARTICULO,F_MOVIM,hora,CANTIDAD,COSTO,COSTOPROMEDIO,EXISTENCIA,ALMACEN,EXIST_ALM,PRECIO_VTA,POR_COSTEA,Cerrado,Usuario,UsuFecha,UsuHora,CLAVEADD,PRCANTIDAD,ID_SALIDA,ID_ENTRADA,REORDENA,donativo,afectacosto) "
            sqlStmtMovInv &= "VALUES (@operacion,@movimiento,@entSal,@tipoMivimiento,@noReferencia,@articulo,@fechaMovimiento,@hora,@cantidad,@costo,@costoPromedio,@existencia,@almacen,@existenciaAlmacen,@precioVenta,@porCostea,@cerrado,@usuario,@usuFecha,@usuHora,@claveAdd,@prCantidad,@idSalida,@idEntrada,@reordena,@donativo,@afectaCosto)"

            Using conex As New SqlConnection(DB_CONN_INTERNO)
                Try
                    Dim xComm As New SqlCommand(sqlStmtMovInv, conex)
                    With xComm
                        .CommandType = CommandType.Text
                        .Parameters.AddWithValue("@operacion", "SA")
                        .Parameters.AddWithValue("@movimiento", "1")
                        .Parameters.AddWithValue("@entSal", "S")

                        .Parameters.AddWithValue("@tipoMivimiento", tipoDeMovimiento)
                        .Parameters.AddWithValue("@noReferencia", 1)

                        .Parameters.AddWithValue("@articulo", row.Cells(0).Value.ToString)
                        .Parameters.AddWithValue("@fechaMovimiento", fechaDeEmision)
                        .Parameters.AddWithValue("@hora", "")

                        .Parameters.AddWithValue("@cantidad", (CDec(row.Cells(2).Value) * -1))
                        .Parameters.AddWithValue("@costo", row.Cells(3).Value.ToString)

                        .Parameters.AddWithValue("@costoPromedio", row.Cells(3).Value.ToString)

                        .Parameters.AddWithValue("@existencia", numPartida.ToString)
                        .Parameters.AddWithValue("@almacen", numPartida.ToString)
                        .Parameters.AddWithValue("@existenciaAlmacen", numPartida.ToString)
                        .Parameters.AddWithValue("@precioVenta", numPartida.ToString)
                        .Parameters.AddWithValue("@porCostea", numPartida.ToString)
                        .Parameters.AddWithValue("@cerrado", numPartida.ToString)

                        .Parameters.AddWithValue("@usuFecha", Format(Now, "dd-MM-yyyy"))
                        .Parameters.AddWithValue("@usuHora", Format(Now, "hh:mm:ss"))
                        .Parameters.AddWithValue("@usuario", "SUP")

                        .Parameters.AddWithValue("@claveAdd", numPartida.ToString)
                        .Parameters.AddWithValue("@prCantidad", numPartida.ToString)
                        .Parameters.AddWithValue("@idSalida", numPartida.ToString)
                        .Parameters.AddWithValue("@idEntrada", numPartida.ToString)
                        .Parameters.AddWithValue("@reordena", numPartida.ToString)
                        .Parameters.AddWithValue("@donativo", numPartida.ToString)
                        .Parameters.AddWithValue("@afectaCosto", numPartida.ToString)

                    End With

                    conex.Open()
                    xComm.ExecuteNonQuery()
                    xComm.Dispose()
                Catch ex As SqlException
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "SqlException")
                Catch e As SystemException
                    MsgBox(e.Message, MsgBoxStyle.Critical, "SystemException")
                End Try
            End Using

        Next

        Dim sqlStatementConsecutivos As String
        sqlStatementConsecutivos = "UPDATE consec SET Consec = @partidaSalida WHERE Dato = 'salpart'"
        Using xConn As New SqlConnection(DB_CONN_INTERNO)
            Try
                Dim xComm As New SqlCommand(sqlStatementConsecutivos, xConn)
                With xComm
                    .CommandType = CommandType.Text
                    .Parameters.AddWithValue("@partidaSalida", consecutivoSalPartida.ToString)
                End With

                xConn.Open()
                xComm.ExecuteNonQuery()
                xComm.Dispose()
            Catch ex As SqlException
                MsgBox(ex.Message, MsgBoxStyle.Critical, "SqlException")

            Catch exc As SystemException
                MsgBox(exc.Message, MsgBoxStyle.Critical, "SystemException")

            End Try
        End Using

        sqlStatementConsecutivos = "UPDATE consec SET Consec = @Salida WHERE Dato = 'Salida'; UPDATE consec SET Consec = @SalidaBodega WHERE Dato = 'BODEGA01salida'"
        Using xConn As New SqlConnection(DB_CONN_INTERNO)
            Try
                Dim xComm As New SqlCommand(sqlStatementConsecutivos, xConn)
                With xComm
                    .CommandType = CommandType.Text
                    .Parameters.AddWithValue("@salida", currentSalida.ToString)
                    .Parameters.AddWithValue("@SalidaBodega", currentSalida.ToString)
                End With

                xConn.Open()
                xComm.ExecuteNonQuery()
                xComm.Dispose()
            Catch ex As SqlException
                MsgBox(ex.Message, MsgBoxStyle.Critical, "SqlException")

            Catch exc As SystemException
                MsgBox(exc.Message, MsgBoxStyle.Critical, "SystemException")

            End Try
        End Using



    End Sub


    Private Sub actualizarExistenciaAlmacen(ByVal almacen As String, ByVal articulo As String, ByVal accion As String, ByVal cantidad As Decimal)

        Dim rst_ExistenciaAlmacen As Recordset = crearRecorset("SELECT * FROM existenciaalmacen")
        rst_ExistenciaAlmacen.Filter = "almacen = '" & almacen & "' AND articulo = '" & articulo & "'"


        Dim sqlUpdateExisAlamacen As String
        Dim existenciaReal As Decimal

        If Not rst_ExistenciaAlmacen.EOF Then
            sqlUpdateExisAlamacen = "UPDATE existenciaalmacen SET existencia = @existencia WHERE almacen = @almacen AND articulo = @articulo"

            Using conex As New SqlConnection(DB_CONN_INTERNO)
                Try
                    Dim xComm As New SqlCommand(sqlUpdateExisAlamacen, conex)
                    With xComm
                        .CommandType = CommandType.Text

                        .Parameters.AddWithValue("@almacen", almOrigen.ToString)
                        .Parameters.AddWithValue("@articulo", articulo)

                        If accion = "disminuir" Then

                            existenciaReal = CDec(rst_ExistenciaAlmacen.Fields("existencia").Value) - cantidad
                            .Parameters.AddWithValue("@existencia", existenciaReal)

                        Else

                            existenciaReal = CDec(rst_ExistenciaAlmacen.Fields("existencia").Value) + cantidad
                            .Parameters.AddWithValue("@existencia", existenciaReal)

                        End If

                    End With

                    conex.Open()
                    xComm.ExecuteNonQuery()
                    xComm.Dispose()
                Catch ex As SqlException
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "SqlException")
                Catch e As SystemException
                    MsgBox(e.Message, MsgBoxStyle.Critical, "SystemException")
                End Try
            End Using

        Else
            sqlUpdateExisAlamacen = "INSERT INTO existenciaalmacen(almacen,articulo,existencia) VALUES(@almacen,@articulo,@existencia)"


            Using conex As New SqlConnection(DB_CONN_INTERNO)
                Try
                    Dim xComm As New SqlCommand(sqlUpdateExisAlamacen, conex)
                    With xComm
                        .CommandType = CommandType.Text

                        If accion = "disminuir" Then

                            existenciaReal = -cantidad
                            .Parameters.AddWithValue("@existencia", existenciaReal)

                        Else

                            existenciaReal = cantidad
                            .Parameters.AddWithValue("@existencia", existenciaReal)

                        End If

                        .Parameters.AddWithValue("@almacen", almOrigen.ToString)
                        .Parameters.AddWithValue("@articulo", articulo)

                    End With

                    conex.Open()
                    xComm.ExecuteNonQuery()
                    xComm.Dispose()
                Catch ex As SqlException
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "SqlException")
                Catch e As SystemException
                    MsgBox(e.Message, MsgBoxStyle.Critical, "SystemException")
                End Try
            End Using

        End If


    End Sub

End Class
