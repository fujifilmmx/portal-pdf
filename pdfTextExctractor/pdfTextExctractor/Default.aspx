<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="pdfTextExctractor.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
* {
  box-sizing: border-box;
}

/* Create two equal columns that floats next to each other */
.column {
  float: left;
  width: 50%;
  padding: 10px;
  height: 300px; /* Should be removed. Only for demonstration */
}

/* Clear floats after the columns */
.row:after {
  content: "";
  display: table;
  clear: both;
}
</style>
</head>
<body>
    <span>Cambia el texto de un archivo PDF</span>
    <br />
    <br />
    <form id="form1" runat="server">
        <div>
            <label>Texto viejo:</label>
            <input type="text" id="txtOld" name="txtOld" runat="server" required/><br /><br />
            <label>Texto Nuevo:</label>
            <input type="text" id="txtNew" name="txtNew" runat="server" required/><br /><br />
            <label>Archivo Pdf:</label>
            <input type=file id=File1 name=File1 method="post" enctype="multipart/form-data" runat="server" >
            <input type="submit" id="Submit1" value="Convertir" runat="server" name="Submit1">
        </div>
    </form>
</body>
</html>
