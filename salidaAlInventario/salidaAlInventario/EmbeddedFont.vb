Imports System.Drawing.Text
Imports System.IO
Imports System.Reflection

Public Class EmbeddedFont

    Private Declare Auto Function AddFontMemResourceEx Lib "Gdi32.dll" _
    (ByVal pbFont As IntPtr, ByVal cbFont As Integer, _
    ByVal pdv As Integer, ByRef pcFonts As Integer) As IntPtr


    Public Shared Function GetFont(ByVal fontName As String) As FontFamily

        Dim exeCurrent As [Assembly] = [Assembly].GetExecutingAssembly()

        Dim nameSpc As String = exeCurrent.GetName().Name.ToString()

        Dim fontCollection As New PrivateFontCollection

        Dim loadStream As Stream = exeCurrent.GetManifestResourceStream( _
            nameSpc + "." + fontName)

        Dim byteBuffer(CType(loadStream.Length, Integer)) As Byte

        loadStream.Read(byteBuffer, 0, Int(CType(loadStream.Length, Integer)))

        Dim fontPtr As IntPtr = _
            Runtime.InteropServices.Marshal.AllocHGlobal( _
            Runtime.InteropServices.Marshal.SizeOf(GetType(Byte)) * _
                byteBuffer.Length)

        Runtime.InteropServices.Marshal.Copy(byteBuffer, 0, fontPtr, byteBuffer.Length)

        fontCollection.AddMemoryFont(fontPtr, byteBuffer.Length)

        Dim pcFonts As Int32 = 1

        AddFontMemResourceEx(fontPtr, byteBuffer.Length, 0, pcFonts)

        Runtime.InteropServices.Marshal.FreeHGlobal(fontPtr)

        Return fontCollection.Families(0)

    End Function
End Class
