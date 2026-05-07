Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Partial Class Index1
    Inherits System.Web.UI.Page
    Public urlDemoCinema As String
    Public urlDemoMaps As String
    Private Sub Index1_Init(sender As Object, e As EventArgs) Handles Me.Init

        '    LabelPageTtl.Text = "Online Data Reporting and Analytics"

        'urlDemoCinema = "https://OUReports.net/OUReports/Default.aspx?logon=demo&pass=demo"
        'urlDemoMaps = "https://OUReports.net/OUReports/Default.aspx?logon=csvdemo&pass=demo"
        Session("WEBOUR") = ConfigurationManager.AppSettings("weboureports").ToString
        If Not Request("map") Is Nothing AndAlso Not Request("srd") Is Nothing AndAlso Not Request("rep") Is Nothing AndAlso Request("map").ToString.Trim = "yes" AndAlso IsNumeric(Request("srd")) Then
            Session("srd") = CInt(Request("srd"))
            Session("map") = "yes"
            Session("logon") = Request("lgn")

        Else
            Session("srd") = 0
            Session("map") = ""
        End If
        Session("CSV") = ""
        Session("PAGETTL") = ConfigurationManager.AppSettings("pagettl").ToString
        If Not Session("PAGETTL") Is Nothing AndAlso Session("PAGETTL").ToString.Length > 0 Then
            LabelPageTtl.Text = Session("PAGETTL")
        End If
    End Sub
    Protected Sub ButtonPlayCinema_Click(sender As Object, e As EventArgs) Handles ButtonPlayCinema.Click
        Response.Redirect("https://OUReports.net/OUReports/Default.aspx?logon=demo&pass=demo")
    End Sub
    Protected Sub ButtonPlayMaps_Click(sender As Object, e As EventArgs) Handles ButtonPlayMaps.Click
        Response.Redirect("https://OUReports.net/OUReports/Default.aspx?logon=csvdemo&pass=demo")
    End Sub
    Protected Sub ButtonProjectManager_Click(sender As Object, e As EventArgs) Handles ButtonProjectManager.Click
        Response.Redirect("https://OUReports.net/HelpDesk/Default.aspx")
    End Sub

    Private Sub Index1_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Dim re As String = SendEmailAdminScheduledReports()
        'Dim ret As String = String.Empty
        'Dim flds As String = String.Empty
        'Dim i As Integer
        'Dim dv As DataView
        'Try
        '    dv = GetListOfTableColumns("ourcomparison")
        '    If dv Is Nothing OrElse dv.Count = 0 OrElse dv.Table.Rows.Count = 0 Then
        '        Exit Sub
        '    Else
        '        For i = 0 To dv.Table.Rows.Count - 1
        '            If dv.Table.Rows(i)("COLUMN_NAME").ToString.ToUpper <> "INDX" AndAlso dv.Table.Rows(i)("COLUMN_NAME").ToString.ToUpper <> "RECORDER" Then
        '                If i > 0 Then flds = flds & ","
        '                flds = flds & "[" & dv.Table.Rows(i)("COLUMN_NAME").ToString & "]"
        '            End If
        '        Next
        '    End If
        'Catch ex As Exception
        '    ret = ex.Message
        '    Exit Sub
        'End Try
        'Dim ds As DataView = mRecords("SELECT " & flds & " FROM [ourcomparison] ORDER BY [recorder]")
        'Session("ds") = ds
        'GridView1.DataSource = ds
        'GridView1.DataBind()
    End Sub
    'Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
    '    If e.Row IsNot Nothing AndAlso e.Row.RowType = DataControlRowType.Header Then
    '        For Each cell As TableCell In e.Row.Cells
    '            cell.BorderWidth = 0
    '            If cell.Text = "Comments" Then
    '                cell.Text = ""
    '            Else
    '                cell.Text = "<u>" & cell.Text & "</u>&nbsp;&nbsp;&nbsp;</br>  &nbsp;&nbsp;&nbsp;"
    '                cell.HorizontalAlign = HorizontalAlign.Left
    '                cell.VerticalAlign = VerticalAlign.Middle

    '            End If
    '            cell.ForeColor = Color.Black
    '        Next
    '    ElseIf e.Row IsNot Nothing AndAlso e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim i As Integer
    '        Dim fld As String = String.Empty
    '        Dim sql As String = String.Empty
    '        Dim tooltiptext As String = String.Empty 'e.Row.Cells(0).Text
    '        Dim ds As DataView = Session("ds")
    '        For i = 0 To e.Row.Cells.Count - 1
    '            fld = ds.Table.Columns(i).Caption
    '            If fld = "Comments" Then
    '                tooltiptext = e.Row.Cells(i).Text
    '                e.Row.Cells(i).Text = ""
    '            End If
    '            If e.Row.Cells(i).HasControls Then
    '                Dim image As New WebControls.Image
    '                If CType(e.Row.Cells(i).Controls(0), CheckBox).Checked = True Then
    '                    image.ImageUrl = "~\Controls\Images\check4.jpg"
    '                    e.Row.Cells(i).Controls(0).Visible = False
    '                    e.Row.Cells(i).Controls.Add(image)
    '                    e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center
    '                Else
    '                    e.Row.Cells(i).Controls(0).Visible = False
    '                End If
    '            End If
    '            e.Row.Cells(i).BorderWidth = 0
    '        Next
    '        For i = 0 To e.Row.Cells.Count - 1
    '            fld = ds.Table.Columns(i).Caption
    '            If fld = "Feature" Then
    '                e.Row.Cells(i).Text = "<b>" & e.Row.Cells(i).Text & "</b></br><i>" & tooltiptext & "</i>"
    '                If tooltiptext.Trim <> "" Then
    '                    e.Row.Cells(i).Text = e.Row.Cells(i).Text & "</br>  &nbsp;&nbsp;&nbsp;"
    '                End If
    '                e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Left
    '                e.Row.Cells(i).ForeColor = Color.Black
    '            End If
    '        Next
    '    End If
    'End Sub


End Class
