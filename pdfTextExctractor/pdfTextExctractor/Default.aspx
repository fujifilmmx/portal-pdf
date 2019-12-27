<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="pdfTextExctractor.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Herramientas administrativas fujifilm</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body ng-app="tools">
    <div style="margin-top:25px !important;" class="container">
        <div class="card" style="width: 35rem;">
            <div class="card-body">
                <h5 class="card-title">Tratamiento de archivos PDF</h5>
                <h6 class="card-subtitle mb-2 text-muted">Programa para modificar el texto de un archivo pdf</h6>
                <br />
                <form id="form1" runat="server" ng-controller="formaController" >
                    <div class="form-check form-check-inline" style="margin-bottom:20px !important;">
                        <input class="form-check-input" type="checkbox" id="chkAutomatico" value="automatico" ng-model="esAutomatico" ng-change="RevisarAutomatico()" runat="server">
                        <label class="form-check-label" for="inlineCheckbox1">Valores automáticos</label>
                    </div>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <span class="input-group-text" style="width:112px !important;" id="basic-addon1">Texto viejo</span>
                        </div>
                        <input ng-value="txtViejo" ng-bind="txtViejo" type="text" id="txtOld" name="txtOld" runat="server" required class="form-control" aria-label="txtOld" ng-disabled="esAutomatico">
                    </div>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <span class="input-group-text" style="width:112px !important;" id="basic-addon2">Texto nuevo</span>
                        </div>
                        <input  ng-value="txtNuevo" ng-bind="txtNuevo" type="text" id="txtNew" name="txtNew" runat="server" required class="form-control" aria-label="txtNew" ng-disabled="esAutomatico">
                    </div>
                    <input type=file id=File1 name=File1 method="post" enctype="multipart/form-data" runat="server" >
                    <div class="clearfix">
                        <input class="btn btn-primary float-right" type="submit" id="Submit1" value="Convertir" runat="server" name="Submit1">
                    </div>
                </form>
            </div>
        </div>
    </div>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/angular.min.js"></script>
    <script>
        var app = angular.module('tools', []);
        app.controller('formaController', function ($scope) {
            document.getElementById('txtNew').value = "";
            document.getElementById('txtOld').value = "";
            $scope.esAutomatico = false;
            $scope.RevisarAutomatico = function () {
                if ($scope.esAutomatico) {
                    document.getElementById('txtNew').value = "Motivo de pago";
                    document.getElementById('txtOld').value = "Concepto de pago";
                }
                else {
                    document.getElementById('txtNew').value = "";
                    document.getElementById('txtOld').value = "";
                }
            };
        });
    </script>
</body>
</html>
