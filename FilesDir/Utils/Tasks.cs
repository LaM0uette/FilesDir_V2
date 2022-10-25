using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using CommonTasks;
using Logger;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace FilesDir.Utils;

public static class Tasks
{
    #region Windows
    
    public static void CreateDir(string path)
    {
        Directory.CreateDirectory(path);
    }
    
    public static string GetCurrentDir()
    {
        return Directory.GetCurrentDirectory();
    }
    
    public static string GetCurrentVersion()
    {
        var assVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
        return assVersion.Remove(assVersion.Length - 2);
    }
    
    public static string GetDirTemp()
    {
        return $"C:\\Users\\{TskWindows.GetGuid()}\\FilesDIR_V2_Temp";
    }

    #endregion
    
    //

    #region Functions

    public static string RemoveAccent(this string txt)
    {
        var normalizedString = txt.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    public static void GenerateExcel()
    {
        if (Var.Results.NbFiles.Equals(0)) return;
        
        var xls = @$"{Var.ExportsPath}\Export_{Func.GetTimestamp()}.xlsx";

        using var fs = new FileStream(xls, FileMode.Create, FileAccess.Write);
        
        IWorkbook wb = new XSSFWorkbook();
        var sht = wb.CreateSheet("Export");
        
        var styleDate = wb.CreateCellStyle();
        var dataFormatCustom = wb.CreateDataFormat();
        styleDate.DataFormat = dataFormatCustom.GetFormat("dd/MM/yyyy");
        
        var rowHeader = sht.CreateRow(0);
        rowHeader.CreateCell(0).SetCellValue("Id");
        rowHeader.CreateCell(1).SetCellValue("Nom du fichier");
        rowHeader.CreateCell(2).SetCellValue("Extensions");
        rowHeader.CreateCell(3).SetCellValue("Date de création");
        rowHeader.CreateCell(4).SetCellValue("Date de modification");
        rowHeader.CreateCell(5).SetCellValue("Date d'accès");
        rowHeader.CreateCell(6).SetCellValue("Nom complet du fichier");
        rowHeader.CreateCell(7).SetCellValue("Lien du fichier");
        rowHeader.CreateCell(8).SetCellValue("Propriétaire");

        Var.Log.Separator("EXPORT EXCEL");
        
        var idx = 1;
        foreach (var exp in Var.Exports)
        {
            var row = sht.CreateRow(idx);

            var cellId = row.CreateCell(0);
            cellId.SetCellType(CellType.Numeric);
            cellId.SetCellValue(exp.Id);
            
            var cellName = row.CreateCell(1);
            cellName.SetCellType(CellType.String);
            cellName.SetCellValue(exp.Name);
            
            var cellExt = row.CreateCell(2);
            cellExt.SetCellType(CellType.String);
            cellExt.SetCellValue(exp.Extension);
            
            var cellDateCrea = row.CreateCell(3);
            cellDateCrea.SetCellValue(exp.CreaDate);
            cellDateCrea.CellStyle = styleDate;
            
            var cellDateModif = row.CreateCell(4);
            cellDateModif.SetCellValue(exp.ModifDate);
            cellDateModif.CellStyle = styleDate;
            
            var cellDateAcces = row.CreateCell(5);
            cellDateAcces.SetCellValue(exp.AccesDate);
            cellDateAcces.CellStyle = styleDate;
            
            var cellFullName = row.CreateCell(6);
            cellFullName.SetCellType(CellType.String);
            cellFullName.SetCellValue(exp.FullName);
            
            var cellPath = row.CreateCell(7);
            cellPath.SetCellType(CellType.String);
            cellPath.SetCellValue(exp.Path);
            
            var cellPropio = row.CreateCell(8);
            cellPropio.SetCellType(CellType.String);
            cellPropio.SetCellValue(exp.Proprietaire);

            Var.Log.Progress("Export Excel:", idx, Var.Exports.Count);
            idx++;
        }
        
        Var.Log.Del();

        Var.Log.ParamInLine("Ajout du filtre...");
        sht.SetAutoFilter(new CellRangeAddress(0, Var.Exports.Count, 1, 8));

        Var.Log.Del();
        Var.Log.Ok("Filtre ajouté !");
        
        wb.Write(fs);
        Var.Log.Ok("Fichier Excel sauvegardé avec succes !");
        Thread.Sleep(200);
        
        Var.Log.ParamInLine("Ouverture du fichier en cours...");
        Thread.Sleep(600);
        Process.Start("explorer.exe" , xls);
        Var.Log.OkDel("Fichier ouvert !");
    }

    public static string GetCleenTimer(this TimeSpan time)
    {
        var timeTotal = time.TotalSeconds;

        return timeTotal switch
        {
            < 60 => $"{time.Seconds:D2}s",
            < 3600 => $"{time.Minutes:D2}m:{time.Seconds:D2}s",
            _ => $"{time.Hours:D2}h:{time.Minutes:D2}m:{time.Seconds:D2}s"
        };
    }

    #endregion
}