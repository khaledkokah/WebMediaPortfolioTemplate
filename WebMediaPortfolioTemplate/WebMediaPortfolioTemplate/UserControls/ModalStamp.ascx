<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModalStamp.ascx.cs" Inherits="WebMediaPortfolioTemplate.UserControls.ModalStamp" %>

<style>
    #tblStamp td {
        vertical-align: middle;
    }

    .input-group {
        margin-bottom: 0px;
    }
</style>
<script>
    $(function () {
        $('#ValidFrom').datetimepicker({
            format: 'YYYY/MM/DD'
        });
        $('#ValidTo').datetimepicker({
            format: 'YYYY/MM/DD'
        });
    });
</script>

<table id="tblStamp" class="table table-striped table-bordered">
    <tbody>
        <tr>
            <td>Valid from</td>
            <td>
                <div class='input-group date' id='ValidFrom'>
                    <input clientidmode="static" runat="server" type="text" id="txtValidFrom" class="form-control" />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
            </td>
            <td>Valid To</td>
            <td>
                <div class='input-group date' id='ValidTo'>
                    <input clientidmode="static" runat="server" type="text" id="txtValidTo" class="form-control" />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
            </td>
        </tr>
        <tr>
            <td>Created by</td>
            <td>
                <asp:Label ClientIDMode="static" ID="lblCreateBy" runat="server" /></td>
            <td>Created date</td>
            <td>
                <asp:Label ClientIDMode="static" ID="lblCreateDate" runat="server" /></td>
        </tr>
        <tr>
            <td>Last updated by</td>
            <td>
                <asp:Label ClientIDMode="static" ID="lblUpdateBy" runat="server" /></td>
            <td>Last updated date</td>
            <td>
                <asp:Label ClientIDMode="static" ID="lblUpdateDate" runat="server" /></td>
        </tr>
    </tbody>
</table>
