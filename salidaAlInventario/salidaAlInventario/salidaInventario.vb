Imports ADODB
Imports System.Data.SqlClient
Imports Stimulsoft.Report
Imports Stimulsoft.Report.Dictionary



Imports ReportesMyBusinessPOS


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
    Private rst_UNIVERSOPRODUCTOS As Recordset
    Public rst_FILTROPRODUCTOS As Recordset

    Dim currentSalida As Integer = 0
    Dim currentFolioSalida As Integer = 0
    Dim currentTraspaso As Integer = 0
    Dim tipoDeMovimiento As String
    Dim fechaDeEmision As String
    Dim almOrigen As String = ""
    Dim almDestino As String = ""
    Dim codigosBarras As New List(Of String)
    Dim sumaTotal As Decimal

    Dim existenciaDictionary As Dictionary(Of String, Decimal)
    Dim countCodigos As Dictionary(Of String, Integer)


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
            'DB_CONN = "Provider=SQLNCLI.1;Password=989898;Persist Security Info=True;User ID=sa;Initial Catalog=C:\MyBusinessDatabase\MyBusinessPOS2012.mdf;Data Source=JONATHAN-PC\SQLEXPRESS,1433;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=JONATHAN-PC;Use Encryption for Data=False;Tag with column collation when possible=False;MARS Connection=False;DataTypeCompatibility=0;Trust Server Certificate=False;"
            'DB_CONN_INTERNO = "Password=989898;Persist Security Info=True;User ID=sa;Initial Catalog=C:\MyBusinessDatabase\MyBusinessPOS2012.mdf;Data Source=JONATHAN-PC\SQLEXPRESS,1433;Packet Size=4096;Workstation ID=JONATHAN-PC;"
            ESTACION = "BODEGA01"
            NUM_SALIDA = "5"

            'DB_CONN = "User ID=sa;Password=989898,Provider=SQLNCLI.1;Integrated Security=SSPI;Persist Security Info=True;Initial Catalog=C:\MyBusinessDatabase\MyBusinessPOS2012.mdf;Data Source=JONA_LAP;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=JONA_LAP;Use Encryption for Data=False;Tag with column collation when possible=False;MARS Connection=False;DataTypeCompatibility=80;Trust Server Certificate=False"
            DB_CONN = "User ID=sa;Password=979797;Initial Catalog=C:\MyBusinessDatabase\MyBusinessPOS2012.mdf;Data Source=JONA_LAP;"
            DB_CONN_INTERNO = "User ID=sa;Password=979797;Initial Catalog=C:\MyBusinessDatabase\MyBusinessPOS2012.mdf;Data Source=JONA_LAP"
            'Workstation ID=JONATHAN-PC;

            MyConnObj.Open("Initial Catalog=C:\MyBusinessDatabase\MyBusinessPOS2012.mdf;Data Source=JONA_LAP;User ID=sa;Password=979797;" & "Provider=SQLNCLI.1;")



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



        'imprimirLaSalida(1)

    End Sub


    Dim MyConnObj As New ADODB.Connection
    Function crearRecorset(ByVal SQLConsulta As String) As Recordset
        Dim recorset As Recordset = New Recordset

        Try
            recorset.Open(SQLConsulta, MyConnObj, CursorTypeEnum.adOpenForwardOnly, LockTypeEnum.adLockReadOnly)
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

    Private WithEvents formProductos As gridDeProductos
    Private Sub txtBoxCodigoBarras_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtBoxCodigoBarras.KeyUp

        If e.KeyCode = Keys.Enter Then

            rst_UNIVERSOPRODUCTOS.Requery()


            Dim pesoReal As Decimal
            Dim peso As String
            Dim tipoCodigo As String
            Dim codigo As String = ""

            Dim currenArticulo As String = txtBoxCodigoBarras.Text.Trim
            txtBoxCodigoBarras.Text = ""

            If IsNumeric(currenArticulo) Then

                If (currenArticulo.Length > 4) Then

                    If chkBoxEvitarDuplicados.Checked Then
                        If codigosBarras.IndexOf(currenArticulo) <> -1 Then
                            Exit Sub
                        End If
                    End If

                    codigosBarras.Add(currenArticulo)

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


                    AgregarFilaDeProductoSalida(codigo, pesoReal)

                ElseIf (currenArticulo.Length = 4) Then

                    codigo = currenArticulo

                    AgregarFilaDeProductoSalida(codigo, 0)

                End If


            Else

                If currenArticulo.Length > 0 Then
                    rst_UNIVERSOPRODUCTOS.Filter = "Descripcion LIKE '*" & currenArticulo & "*'"
                    formProductos = New gridDeProductos(rst_UNIVERSOPRODUCTOS)
                    formProductos.Show()
                End If

            End If

            'Dim rst_Producto As Recordset = crearRecorset("SELECT * FROM prods where Articulo = " & codigo)
            'rst_Producto.Close()

        End If
    End Sub


    Private Sub llevarControlDeExistenciaEnVivo(ByVal codigo As String, ByVal cantidadADescontar As Decimal, Optional ByVal vieneDe As String = "agregar")

        Dim tempExistencia As Decimal = 0
        If existenciaDictionary.ContainsKey(codigo) Then

            tempExistencia = existenciaDictionary.Item(codigo)

            If cantidadADescontar > 0 Then
                tempExistencia -= cantidadADescontar
            End If

            existenciaDictionary.Item(codigo) = tempExistencia
        Else

            Dim exisActual As Decimal = CDec(rst_UNIVERSOPRODUCTOS.Fields("Existencia").Value)

            If cantidadADescontar > 0 Then
                tempExistencia = (exisActual - cantidadADescontar)
            Else
                tempExistencia = exisActual
            End If

            existenciaDictionary.Add(codigo, tempExistencia)

        End If

        If vieneDe = "agregar" Then
            'AQUI TENGO QUE EVITAR QUE ENTRE CUANDO VIENE DE ACTUALIAR SOLAMENTE LA CANTIDAD
            If countCodigos.ContainsKey(codigo) Then
                Dim count As Integer = 0
                count = countCodigos.Item(codigo)
                countCodigos.Item(codigo) = count + 1
            Else
                countCodigos.Add(codigo, 1)
            End If
        End If

    End Sub

    Private Sub AgregarFilaDeProductoSalida(ByVal codigo As String, ByVal peso As Decimal)

        rst_UNIVERSOPRODUCTOS.Filter = "Clave = '" & codigo & "'"

        If Not rst_UNIVERSOPRODUCTOS.EOF Then

            llevarControlDeExistenciaEnVivo(codigo, peso)

            Dim row As DataGridViewRow = New DataGridViewRow()
            Dim dgvCell As DataGridViewCell

            dgvCell = New DataGridViewTextBoxCell()
            dgvCell.Value = codigo
            row.Cells.Add(dgvCell)

            dgvCell = New DataGridViewTextBoxCell()
            dgvCell.Value = rst_UNIVERSOPRODUCTOS.Fields("Descripcion").Value
            row.Cells.Add(dgvCell)

            dgvCell = New DataGridViewTextBoxCell()
            dgvCell.Value = peso
            row.Cells.Add(dgvCell)

            dgvCell = New DataGridViewTextBoxCell()
            dgvCell.Value = rst_UNIVERSOPRODUCTOS.Fields("Precio").Value
            row.Cells.Add(dgvCell)

            dgvCell = New DataGridViewTextBoxCell()

            If peso > 0 Then
                dgvCell.Value = existenciaDictionary.Item(codigo)
            Else
                dgvCell.Value = 0
            End If

            row.Cells.Add(dgvCell)

            Dim costo As Decimal = CDec(rst_UNIVERSOPRODUCTOS.Fields("COSTO_U").Value)
            dgvCell = New DataGridViewTextBoxCell()
            dgvCell.Value = costo
            row.Cells.Add(dgvCell)

            'If row.Cells(2).Value IsNot Nothing And row.Cells(3).Value IsNot Nothing Then
            '    sumaTotal += CDec(row.Cells(2).Value) * CDec(row.Cells(3).Value)
            '    txtTotal.Text = Format(sumaTotal, "##,##0.00")
            'End If

            dgvProductos.Rows.Add(row)

        End If

    End Sub


    Private Sub seleccionDeProducto_GridProductosCHANGE(ByVal codigo As String) Handles formProductos.productoChange
        formProductos.Close()
        AgregarFilaDeProductoSalida(codigo, 0)
    End Sub


    Private Sub btnComenCaptura_Click(sender As System.Object, e As System.EventArgs) Handles btnComenCaptura.Click

        If (cbxAlmacenO.SelectedIndex = 0) Then
            Me.ErrorProvider1.SetError(cbxAlmacenO, "Seleccionar almacen")
            MsgBox("Para continuar, es necesario seleccionar el almacén que será afectado con la salida de productos.", MsgBoxStyle.Exclamation, "Atención")
            Exit Sub
        End If

        iniciarCapturaDePartidasParaLaSalida()

        dgvProductos.Enabled = True
        btnAceptar.Enabled = True
        txtBoxCodigoBarras.Enabled = True

        cbxConcepto.Enabled = False
        cbxAlmacenO.Enabled = False
        cbxAlmacenD.Enabled = False
        cbxReponsableTraslado.Enabled = False
        cbxReponsableRecibe.Enabled = False
        chkBoxEvitarDuplicados.Enabled = False

        btnComenCaptura.Enabled = False
        existenciaDictionary = New Dictionary(Of String, Decimal)
        countCodigos = New Dictionary(Of String, Integer)


    End Sub


    Private Sub iniciarCapturaDePartidasParaLaSalida()

        Dim seInsertoCorrectamenteLaSalida As Boolean = True


        rst_CONSECUTIVOS.Filter = "Dato = 'Salida'"
        If Not rst_CONSECUTIVOS.EOF Then
            currentSalida = CInt(rst_CONSECUTIVOS.Fields("Consec").Value)
        End If
        currentSalida += 1

        rst_CONSECUTIVOS.Filter = "Dato = 'BODEGA01salida'"
        If Not rst_CONSECUTIVOS.EOF Then
            currentFolioSalida = CInt(rst_CONSECUTIVOS.Fields("Consec").Value)
        End If

        rst_CONSECUTIVOS.Filter = "Dato = 'Traspaso'"
        If Not rst_CONSECUTIVOS.EOF Then
            currentTraspaso = CInt(rst_CONSECUTIVOS.Fields("Consec").Value)
        End If

        If (cbxAlmacenO.SelectedIndex > 0) Then
            almOrigen = cbxAlmacenO.SelectedItem.ToString
            almOrigen = Mid(almOrigen, InStr(almOrigen, "(") + 1, 1)
        End If

        rst_UNIVERSOPRODUCTOS = crearRecorset(
         "SELECT p.ARTICULO AS Clave,p.DESCRIP AS Descripcion,COALESCE(EAlmacen.existencia,0) AS Existencia,linea.Descrip AS Linea" +
         " ,marca.Descrip AS Marca,fabricante.nombre AS Fabricante,p.PRECIO1 AS Precio, p.COSTO_U" +
         " FROM prods AS p" +
         " LEFT JOIN (SELECT * FROM lineas)AS linea on linea.Linea = p.LINEA" +
         " LEFT JOIN (SELECT * FROM marcas)AS marca on marca.Marca = p.MARCA" +
         " LEFT JOIN (SELECT * FROM fabricantes)AS fabricante on fabricante.fabricante = p.fabricante" +
         " LEFT JOIN (SELECT * FROM existenciaalmacen WHERE almacen = '" + almOrigen + "' ) AS EAlmacen on EAlmacen.articulo = p.ARTICULO")


        If (cbxAlmacenD.SelectedIndex > 0) Then
            almDestino = cbxAlmacenD.SelectedItem.ToString
            almDestino = Mid(almDestino, InStr(almDestino, "(") + 1, 1)
        Else
            almDestino = "0"
        End If

        Dim esTraspaso As Integer
        If InStr(cbxConcepto.SelectedItem.ToString, "TRANSFERENCIA") > 0 Then
            esTraspaso = 1
        Else
            esTraspaso = 0
        End If

        Dim sqlStatementSalidas As String = "INSERT INTO salidas ([Salida],[ocupado],[tipo_doc],[F_EMISION],[IMPORTE],[COSTO],[ALMACEN],[ESTADO],[OBSERV],[DATOS],[USUARIO],[USUFECHA],[USUHORA],[almt],[estraspaso],[sucursal],[esparasucursal],[estacion],[traspasoaestacion],[cliente],[fecharetorno],[empleado],[paraempleado],[direccionembarque]) "
        sqlStatementSalidas &= "VALUES (@salida,@ocupado,@tipoDoc,@fEmision,@importe,@costo,@almacen,@estado,@observacion,@datos,@usuario,@usufecha,@usuhora,@almt,@esTraspaso,@sucursal,@esParaSucursal,@estacion,@traspasoaestacion,@cliente,@fecharetorno,@empleado,@paraEmpleado,@direccionEmbarque)"

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
                    .Parameters.AddWithValue("@salida", currentSalida.ToString)
                    .Parameters.AddWithValue("@ocupado", "0")
                    .Parameters.AddWithValue("@tipoDoc", tipoDeMovimiento)
                    .Parameters.AddWithValue("@fEmision", fechaDeEmision)
                    .Parameters.AddWithValue("@importe", 0)
                    .Parameters.AddWithValue("@costo", 0)
                    .Parameters.AddWithValue("@almacen", CInt(almOrigen))
                    .Parameters.AddWithValue("@estado", "PE")
                    .Parameters.AddWithValue("@observacion", "")
                    .Parameters.AddWithValue("@datos", cbxConcepto.SelectedItem.ToString)
                    .Parameters.AddWithValue("@usuario", "SUP")
                    .Parameters.AddWithValue("@usufecha", Format(Now, "dd-MM-yyyy"))
                    .Parameters.AddWithValue("@usuhora", Format(Now, "hh:mm:ss"))
                    .Parameters.AddWithValue("@almt", CInt(almDestino))
                    .Parameters.AddWithValue("@esTraspaso", esTraspaso)
                    .Parameters.AddWithValue("@sucursal", "")
                    .Parameters.AddWithValue("@esParaSucursal", 0)

                    If Not rst_CURRENT_ESTACION.EOF Then
                        .Parameters.AddWithValue("@estacion", rst_CURRENT_ESTACION.Fields("Estacion").Value.ToString)
                    Else
                        .Parameters.AddWithValue("@estacion", ESTACION)
                    End If

                    .Parameters.AddWithValue("@traspasoaestacion", 0)
                    .Parameters.AddWithValue("@cliente", "")
                    .Parameters.AddWithValue("@fecharetorno", fechaDeEmision)
                    .Parameters.AddWithValue("@empleado", empleadoResponsable)
                    .Parameters.AddWithValue("@paraEmpleado", 1)
                    .Parameters.AddWithValue("@direccionEmbarque", "")
                End With

                xConn.Open()
                xComm.ExecuteNonQuery()
                xComm.Dispose()


            Catch ex As SqlException
                seInsertoCorrectamenteLaSalida = False
                MsgBox(ex.Message, MsgBoxStyle.Critical, "SqlException")
            Catch e As SystemException
                seInsertoCorrectamenteLaSalida = False
                MsgBox(e.Message, MsgBoxStyle.Critical, "SystemException")
            End Try
        End Using

        If (seInsertoCorrectamenteLaSalida) Then
            Dim sqlStatementConsecutivos As String = "UPDATE consec SET Consec = @salida WHERE Dato = 'Salida';"
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
        comenzarLlenadoDeDetallesSalidaDelInventario()
    End Sub


    Private Sub comenzarLlenadoDeDetallesSalidaDelInventario()

        If cbxConcepto.SelectedIndex = -1 Then
            Exit Sub
        End If


        If Not cbxConcepto.SelectedItem Is Nothing Then

            Dim selectedConcepto = CType(cbxConcepto.SelectedItem, String).ToLower

            btnComenCaptura.Enabled = True

            If InStr(selectedConcepto, "transferencia") > 0 Then

                cbxAlmacenO.Enabled = True
                cbxAlmacenD.Enabled = True
                cbxReponsableTraslado.Enabled = True
                'cbxReponsableRecibe.Enabled = True

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

                rst_AlmacenElegido.Close()

            ElseIf InStr(selectedConcepto, "ruta") > 0 Then

                cbxAlmacenO.Enabled = True
                cbxReponsableTraslado.Enabled = True

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
                    cbxReponsableTraslado.Focus()
                End If

                rst_AlmacenElegido.Close()

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

        'If (currentSalida > 0) Then

        '    Dim sqlStatementConsecutivos As String = "DELETE salidas WHERE Salida = @salida"
        '    Using xConn As New SqlConnection(DB_CONN_INTERNO)
        '        Try
        '            Dim xComm As New SqlCommand(sqlStatementConsecutivos, xConn)
        '            With xComm
        '                .CommandType = CommandType.Text
        '                .Parameters.AddWithValue("@salida", currentSalida.ToString)
        '            End With

        '            xConn.Open()
        '            xComm.ExecuteNonQuery()
        '            xComm.Dispose()

        '        Catch ex As SqlException
        '            MsgBox(ex.Message, MsgBoxStyle.Critical, "SqlException")

        '        Catch exc As SystemException
        '            MsgBox(exc.Message, MsgBoxStyle.Critical, "SystemException")

        '        End Try
        '    End Using
        'End If

        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(sender As System.Object, e As System.EventArgs) Handles btnAceptar.Click

        If dgvProductos.Rows.Count > 0 Then

            'Dim row As DataGridViewRow = dgvProductos.Rows.Item(0)
            'If row.Cells(0).Value Is Nothing Then
            '    Exit Sub
            'End If

            For Each row As DataGridViewRow In dgvProductos.Rows

                If CDec(row.Cells(2).Value) <= 0 Then

                    MsgBox("Existen partidas de productos con cantidad a descontar MENOR o igual a CERO, lo cual no es un valor parmitido para dar salida al almacén." _
                           & Chr(13) & Chr(13) & "Para continuar, se deben eliminar las filas con cantidad en cero, o definir una cantidad mayor a cero.", MsgBoxStyle.Exclamation, _
                           "Verificar los datos")
                    Exit Sub

                End If

            Next

        Else
            MsgBox("Debe haber al menos una partida para dar salida al inventario", MsgBoxStyle.Exclamation, "Atención")
            Exit Sub
        End If


        currentFolioSalida += 1
        currentTraspaso += 1

        Dim consecutivoSalPartida As Integer
        rst_CONSECUTIVOS.Filter = "Dato = 'salpart'"
        If Not rst_CONSECUTIVOS.EOF Then
            consecutivoSalPartida = CInt(rst_CONSECUTIVOS.Fields("Consec").Value)
        End If


        Dim sqlStatementConsecutivos As String

        Dim sqlStatementSalPart As String = "INSERT INTO salpart(SALIDA,TIPO_DOC,ARTICULO,CANTIDAD,PRECIO,OBSERV,PARTIDA,ID_SALIDA,Usuario,UsuFecha,UsuHora,PRCANTIDAD,PRDESCRIP,CLAVEADD) "
        sqlStatementSalPart &= "VALUES (@salida,@tipoDoc,@articulo,@cantidad,@precio,@observacion,@partida,@idSalida,@usuario,@usuFecha,@usuHora,@prCantidad,@prDescrip,@claveAdd)"

        Dim numPartida As Integer = 0
        For Each row As DataGridViewRow In dgvProductos.Rows

            If row.Cells(0).Value IsNot Nothing Then

                consecutivoSalPartida += 1
                numPartida += 1

                Using xConn As New SqlConnection(DB_CONN_INTERNO)
                    Try
                        Dim xComm As New SqlCommand(sqlStatementSalPart, xConn)
                        With xComm
                            .CommandType = CommandType.Text
                            .Parameters.AddWithValue("@salida", currentSalida.ToString)
                            .Parameters.AddWithValue("@tipoDoc", tipoDeMovimiento)
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
                    Catch extra As SystemException
                        MsgBox(extra.Message, MsgBoxStyle.Critical, "SystemException")
                    End Try
                End Using


                'ACTUALIZACION DE LA EXISTENCIA EN EL ALMACÉN================================================================================================================================================================================
                actualizarExistenciaAlmacen(almOrigen, row.Cells(0).Value.ToString, "disminuir", CDec(row.Cells(2).Value))
                actualizarExistenciaAlmacen(almDestino, row.Cells(0).Value.ToString, "aumentar", CDec(row.Cells(2).Value))

                actualizarMovimientoEnInventario("SA", currentFolioSalida, "S", tipoDeMovimiento, almOrigen, row, consecutivoSalPartida)
                actualizarMovimientoEnInventario("T+", currentFolioSalida, "E", "T+", almDestino, row, consecutivoSalPartida)



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

            End If

        Next


        'sqlStatementConsecutivos = "UPDATE consec SET Consec = @salidaBodega WHERE Dato = 'BODEGA01salida'; "
        'Using xConn As New SqlConnection(DB_CONN_INTERNO)
        '    Try
        '        Dim xComm As New SqlCommand(sqlStatementConsecutivos, xConn)
        '        With xComm
        '            .CommandType = CommandType.Text
        '            .Parameters.AddWithValue("@salida", currentSalida.ToString)
        '            .Parameters.AddWithValue("@salidaBodega", currentSalida.ToString)
        '            .Parameters.AddWithValue("@consecutivoInventario", consecutivoMovInventario.ToString)
        '        End With

        '        xConn.Open()
        '        xComm.ExecuteNonQuery()
        '        xComm.Dispose()
        '    Catch ex As SqlException
        '        MsgBox(ex.Message, MsgBoxStyle.Critical, "SqlException")

        '    Catch exc As SystemException
        '        MsgBox(exc.Message, MsgBoxStyle.Critical, "SystemException")

        '    End Try
        'End Using

        
        Dim sqlActualizarSalida As String = "UPDATE salidas SET ESTADO='CO',FOLIO = @folio,IMPORTE = @importe,traspaso = @traspaso WHERE Salida = @salida; UPDATE consec SET Consec = @folio WHERE Dato = 'BODEGA01salida'; UPDATE consec SET Consec = @traspaso WHERE Dato = 'Traspaso';"
        Using xConn As New SqlConnection(DB_CONN_INTERNO)
            Try
                Dim xComm As New SqlCommand(sqlActualizarSalida, xConn)
                With xComm
                    .CommandType = CommandType.Text
                    .Parameters.AddWithValue("@salida", currentSalida.ToString)
                    .Parameters.AddWithValue("@folio", currentFolioSalida.ToString)
                    .Parameters.AddWithValue("@traspaso", currentTraspaso.ToString)
                    .Parameters.AddWithValue("@importe", CDec(txtTotal.Text))
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

        dgvProductos.DataSource = Nothing
        dgvProductos.Rows.Clear()

        If currentSalida > 0 Then
            imprimirLaSalida(currentSalida)
        End If


        rst_EMPRESA.Close()
        rst_CURRENT_ESTACION.Close()
        rst_ALMACENES.Close()
        rst_EMPLEADOS.Close()
        rst_CONSECUTIVOS.Close()
        rst_CONCEPTOS.Close()

        Me.Close()
    End Sub


    Private Sub imprimirLaSalida(ByVal currentPrintSalida As Integer)

        Dim rst_SALIDA = crearRecorset("SELECT * FROM salidas WHERE Salida = '" & currentPrintSalida.ToString & "'")

        Dim mrt As ReportesMyBusiness = New ReportesMyBusinessPOS.ReportesMyBusiness

        mrt.LoadReport("FormatoSalidaInventario_v1.4.mrt")
        mrt.limpiaDatos()

        Dim e As String

        e = mrt.ReportQuery("salidas", "SELECT * FROM salidas WHERE salida = '" & currentPrintSalida & "'", DB_CONN_INTERNO)
        e = mrt.ReportQuery("partidas", "SELECT salpart.articulo, salpart.precio, prods.descrip, salpart.cantidad, salpart.prdescrip,prods.costo_u FROM salpart INNER JOIN prods ON prods.articulo = salpart.articulo WHERE salida = '" & currentPrintSalida & "'", DB_CONN_INTERNO)

        e = mrt.ReportQuery("empleados", "SELECT * FROM empleados WHERE empleado = '" & rst_SALIDA.Fields("empleado").Value.ToString & "'", DB_CONN_INTERNO)

        e = mrt.ReportQuery("infoEmpresa", "SELECT * FROM econfig", DB_CONN_INTERNO)

        mrt.SincronizaDatos()

        If e = "Ok." Then

            If Not rst_CURRENT_ESTACION.EOF Then
                Dim impresora As String = rst_CURRENT_ESTACION.Fields("impsalidas").Value.ToString
                mrt.PrintReport(impresora, False)
            End If
        Else
            MsgBox(e, MsgBoxStyle.Critical, "Verificar el reporte")
        End If

    End Sub


    Private Sub actualizarMovimientoEnInventario(ByVal operacion As String, ByVal consecFolioSalida As Integer, ByVal entradaSalida As String, ByVal tipoMov As String, ByVal almacen As String, ByVal row As DataGridViewRow, ByVal idPartidaSalida As Integer)

        Dim existenciaEnAlmacen As Decimal
        'Dim rst_ExistenciaAlmacen As Recordset = crearRecorset("SELECT * FROM existenciaalmacen")

        rst_ExistenciaAlmacen.Requery()
        rst_ExistenciaAlmacen.Filter = "almacen = '" & almacen & "' AND articulo = '" & row.Cells(0).Value.ToString & "'"


        If Not rst_ExistenciaAlmacen.EOF Then
            existenciaEnAlmacen = CDec(rst_ExistenciaAlmacen.Fields("existencia").Value)
        End If

        Dim existenciaTotal As Decimal
        rst_ExistenciaAlmacen.Filter = "articulo = '" & row.Cells(0).Value.ToString & "'"
        While Not rst_ExistenciaAlmacen.EOF
            existenciaTotal += CDec(rst_ExistenciaAlmacen.Fields("existencia").Value)
            rst_ExistenciaAlmacen.MoveNext()
        End While

        Dim costoPromedio As Decimal
        Dim conteoDeCostos As Integer
        Dim rst_CostosPromedio As Recordset = crearRecorset("SELECT * FROM histcamb WHERE articulo= '" & row.Cells(0).Value.ToString & "'")
        If Not rst_CostosPromedio.EOF Then

            While Not rst_CostosPromedio.EOF
                conteoDeCostos += 1
                If Not IsDBNull(rst_CostosPromedio.Fields("Precio1").Value) Then
                    costoPromedio += CDec(rst_CostosPromedio.Fields("Precio1").Value)
                    rst_CostosPromedio.MoveNext()
                End If
            End While

            If costoPromedio <= 0 Then
                costoPromedio = CDec(row.Cells(5).Value)
            Else
                costoPromedio = costoPromedio / conteoDeCostos
            End If
        Else
            costoPromedio = CDec(row.Cells(5).Value)

        End If



        Dim sqlStmtMovInv As String
        sqlStmtMovInv = "INSERT INTO movsinv (OPERACION,MOVIMIENTO,ENT_SAL,TIPO_MOVIM,NO_REFEREN,ARTICULO,F_MOVIM,hora,CANTIDAD,COSTO,COSTOPROMEDIO,EXISTENCIA,ALMACEN,EXIST_ALM,PRECIO_VTA,POR_COSTEA,Cerrado,Usuario,UsuFecha,UsuHora,CLAVEADD,PRCANTIDAD,ID_SALIDA,ID_ENTRADA,REORDENA,donativo,afectacosto) "
        sqlStmtMovInv &= "VALUES (@operacion,@movimiento,@entSal,@tipoMivimiento,@noReferencia,@articulo,@fechaMovimiento,@hora,@cantidad,@costo,@costoPromedio,@existencia,@almacen,@existenciaAlmacen,@precioVenta,@porCostea,@cerrado,@usuario,@usuFecha,@usuHora,@claveAdd,@prCantidad,@idSalida,@idEntrada,@reordena,@donativo,@afectaCosto)"

        Using conex As New SqlConnection(DB_CONN_INTERNO)
            Try
                Dim xComm As New SqlCommand(sqlStmtMovInv, conex)
                With xComm
                    .CommandType = CommandType.Text
                    .Parameters.AddWithValue("@operacion", operacion)
                    .Parameters.AddWithValue("@movimiento", consecFolioSalida.ToString)
                    .Parameters.AddWithValue("@entSal", entradaSalida)
                    .Parameters.AddWithValue("@tipoMivimiento", tipoMov)
                    .Parameters.AddWithValue("@noReferencia", consecFolioSalida.ToString)
                    .Parameters.AddWithValue("@articulo", row.Cells(0).Value.ToString)
                    .Parameters.AddWithValue("@fechaMovimiento", fechaDeEmision)
                    .Parameters.AddWithValue("@hora", "")

                    If entradaSalida = "E" Then
                        .Parameters.AddWithValue("@cantidad", row.Cells(2).Value.ToString)
                    Else
                        .Parameters.AddWithValue("@cantidad", (CDec(row.Cells(2).Value) * -1))
                    End If

                    .Parameters.AddWithValue("@costo", row.Cells(5).Value.ToString)
                    .Parameters.AddWithValue("@costoPromedio", costoPromedio)
                    .Parameters.AddWithValue("@existencia", existenciaTotal)
                    .Parameters.AddWithValue("@almacen", almacen)
                    .Parameters.AddWithValue("@existenciaAlmacen", existenciaEnAlmacen)
                    .Parameters.AddWithValue("@precioVenta", row.Cells(3).Value.ToString)
                    .Parameters.AddWithValue("@porCostea", "0")
                    .Parameters.AddWithValue("@cerrado", "0")
                    .Parameters.AddWithValue("@usuario", "SUP")
                    .Parameters.AddWithValue("@usuFecha", Format(Now, "dd-MM-yyyy"))
                    .Parameters.AddWithValue("@usuHora", Format(Now, "hh:mm:ss"))
                    .Parameters.AddWithValue("@claveAdd", "")
                    .Parameters.AddWithValue("@prCantidad", "0")
                    .Parameters.AddWithValue("@idSalida", idPartidaSalida.ToString)
                    .Parameters.AddWithValue("@idEntrada", "0")
                    .Parameters.AddWithValue("@reordena", "0")
                    .Parameters.AddWithValue("@donativo", "0")
                    .Parameters.AddWithValue("@afectaCosto", "1")

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


        'rst_ExistenciaAlmacen.Close()
        rst_CostosPromedio.Close()
    End Sub

    Dim rst_ExistenciaAlmacen As Recordset
    Private Sub actualizarExistenciaAlmacen(ByVal almacen As String, ByVal articulo As String, ByVal accion As String, ByVal cantidad As Decimal)

        rst_ExistenciaAlmacen = crearRecorset("SELECT * FROM existenciaalmacen")
        rst_ExistenciaAlmacen.Filter = "almacen = '" & almacen & "' AND articulo = '" & articulo & "'"


        Dim sqlUpdateExisAlamacen As String
        Dim existenciaReal As Decimal = 0

        If Not rst_ExistenciaAlmacen.EOF Then
            sqlUpdateExisAlamacen = "UPDATE existenciaalmacen SET existencia = @existencia WHERE almacen = @almacen AND articulo = @articulo"

            Using conex As New SqlConnection(DB_CONN_INTERNO)
                Try
                    Dim xComm As New SqlCommand(sqlUpdateExisAlamacen, conex)
                    With xComm
                        .CommandType = CommandType.Text

                        .Parameters.AddWithValue("@almacen", almacen)
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

                            existenciaReal = existenciaReal - cantidad
                            .Parameters.AddWithValue("@existencia", existenciaReal)

                        Else

                            existenciaReal = existenciaReal + cantidad
                            .Parameters.AddWithValue("@existencia", existenciaReal)

                        End If

                        .Parameters.AddWithValue("@almacen", almacen)
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

        'rst_ExistenciaAlmacen.Close()
    End Sub

    Private Sub dgvProductos_RowsRemoved(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvProductos.RowsRemoved
        actualizarSumaTotal()
    End Sub

    Private codigoActualizableExistenciaEnGrid As String = ""
    Private Sub dgvProductos_UserDeletingRow(sender As System.Object, e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles dgvProductos.UserDeletingRow

        Dim currentCodigo As String = e.Row.Cells(0).Value.ToString
        Dim totalDeCodigos As Integer = countCodigos.Item(currentCodigo)
        'Dim totalExistencia As Decimal = existenciaDictionary.Item(currentCodigo)
        Dim totalExistencia As Decimal

        If totalDeCodigos = 1 Then
            'Borrar el codigo de ambos arreglos 
            countCodigos.Remove(currentCodigo)
            existenciaDictionary.Remove(currentCodigo)
        Else
            'disminuir el contador de codigos y el contador de existencias
            totalDeCodigos -= 1
            countCodigos.Item(currentCodigo) = totalDeCodigos
            'existenciaDictionary.Remove(currentCodigo)

            If Not rst_UNIVERSOPRODUCTOS Is Nothing Then
                rst_UNIVERSOPRODUCTOS.Filter = "Clave = '" & currentCodigo & "'"
                If Not rst_UNIVERSOPRODUCTOS.EOF Then
                    totalExistencia = CDec(rst_UNIVERSOPRODUCTOS.Fields("Existencia").Value)
                End If
            End If

            existenciaDictionary.Item(currentCodigo) = totalExistencia
            codigoActualizableExistenciaEnGrid = currentCodigo

            End If

    End Sub

    'Private Sub actualizarExistenciasEnGrid(ByVal codigo As String)
    '    If Not rst_UNIVERSOPRODUCTOS Is Nothing Then
    '        rst_UNIVERSOPRODUCTOS.Filter = "Clave = '" & codigo & "'"

    '        If Not rst_UNIVERSOPRODUCTOS.EOF Then

    '            Dim cantinad As Decimal = CDec(rst_UNIVERSOPRODUCTOS.Fields("Existencia").Value) - CDec(dgvProductos.CurrentRow.Cells(2).Value)
    '            dgvProductos.CurrentRow.Cells(4).Value = cantinad

    '        End If
    '    End If
    'End Sub


    Private Sub dgvProductos_UserAddedRow(sender As System.Object, e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgvProductos.RowsAdded
        actualizarSumaTotal()
    End Sub

    Private Sub dgvProductos_CellValueChanged(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProductos.CellValueChanged

        If e.RowIndex > -1 And e.ColumnIndex = 2 Then
            Dim currentCodigo As String = CStr(dgvProductos.CurrentRow.Cells(0).Value)

            If CDec(dgvProductos.CurrentRow.Cells(2).Value) = 0 Then

                codigoActualizableExistenciaEnGrid = currentCodigo

                If Not rst_UNIVERSOPRODUCTOS Is Nothing Then
                    rst_UNIVERSOPRODUCTOS.Filter = "Clave = '" & currentCodigo & "'"
                    If Not rst_UNIVERSOPRODUCTOS.EOF Then
                        existenciaDictionary.Item(currentCodigo) = CDec(rst_UNIVERSOPRODUCTOS.Fields("Existencia").Value)
                    End If
                End If
            Else
                llevarControlDeExistenciaEnVivo(currentCodigo, CDec(dgvProductos.CurrentRow.Cells(2).Value), "actualizar")
                dgvProductos.CurrentRow.Cells(4).Value = existenciaDictionary.Item(currentCodigo)
            End If

            actualizarSumaTotal()

        End If

    End Sub

    Public Sub actualizarSumaTotal()

        Dim total As Decimal = 0

        If dgvProductos.Rows.Count > 0 Then

            For Each row As DataGridViewRow In dgvProductos.Rows
                If row.Cells(2).Value IsNot Nothing And row.Cells(3).Value IsNot Nothing Then
                    total += (CDec(row.Cells(2).Value) * CDec(row.Cells(3).Value))
                End If

                If Not codigoActualizableExistenciaEnGrid = Nothing And codigoActualizableExistenciaEnGrid.Trim.Length > 0 Then

                    If row.Cells(0).Value.ToString = codigoActualizableExistenciaEnGrid Then

                        If CDec(row.Cells(2).Value) > 0 Then
                            row.Cells(4).Value = existenciaDictionary.Item(codigoActualizableExistenciaEnGrid) - CDec(row.Cells(2).Value)
                            existenciaDictionary.Item(codigoActualizableExistenciaEnGrid) = CDec(row.Cells(4).Value)
                        Else
                            row.Cells(4).Value = 0
                        End If
                    End If

                End If
            Next
        End If

        txtTotal.Text = Format(total, "##,##0.00")
        codigoActualizableExistenciaEnGrid = ""
    End Sub

    Private Sub cbxConcepto_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles cbxConcepto.KeyDown

        If e.KeyCode = Keys.Enter Then
            comenzarLlenadoDeDetallesSalidaDelInventario()
        End If

    End Sub

    Private Sub cbxConcepto_Enter(sender As System.Object, e As System.EventArgs) Handles cbxConcepto.Enter
        btnComenCaptura.Enabled = False

        cbxAlmacenO.Enabled = False
        cbxAlmacenD.Enabled = False
        cbxReponsableTraslado.Enabled = False
        cbxReponsableRecibe.Enabled = False

        cbxAlmacenO.SelectedIndex = 0
        cbxAlmacenD.SelectedIndex = 0
        cbxReponsableTraslado.SelectedIndex = 0
        cbxReponsableRecibe.SelectedIndex = 0
    End Sub

End Class
