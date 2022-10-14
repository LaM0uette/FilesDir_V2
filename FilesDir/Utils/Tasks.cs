using System.Globalization;
using System.Reflection;
using System.Text;
using CommonTasks;
using FilesDir.Core;
using FilesDir.Interfaces;
using Logger;
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

    public static string GetSearchModeReq(this MyEnum.SearchMode searchMode)
    {
        return searchMode switch
        {
            MyEnum.SearchMode.In => "%",
            MyEnum.SearchMode.Equal => "=",
            MyEnum.SearchMode.Begin => "^",
            MyEnum.SearchMode.End => "$",
            MyEnum.SearchMode.Re => "r",
            _ => "%"
        };
    }

    public static string GetReqOfSearch(this IFlags flags)
    {
        var req = "FilesDir ";

        req += $"m={flags.SearchMode.GetSearchModeReq()} ";
        req += $"w={string.Join(":", flags.Words)} ";
        req += $"e={string.Join(":", flags.Extensions)} ";
        req += $"p={flags.PoolSize} ";
        
        if (!flags.FoldersBlackList[0].Equals("")) req += $"bl={string.Join(":", flags.FoldersBlackList)} ";
        if (!flags.FoldersWhiteList[0].Equals("")) req += $"wl={string.Join(":", flags.FoldersWhiteList)} ";
        
        if (flags.Casse) req += "-c ";
        if (flags.Utf) req += "-utf ";
        if (flags.Silent) req += "-s ";
        
        return req;
    }

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
        rowHeader.CreateCell(2).SetCellValue("Date de création");
        rowHeader.CreateCell(3).SetCellValue("Date de modification");
        rowHeader.CreateCell(4).SetCellValue("Nom complet du fichier");
        rowHeader.CreateCell(5).SetCellValue("Lien du fichier");

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
            
            var cellDateCrea = row.CreateCell(2);
            cellDateCrea.SetCellValue(exp.CreaDate);
            cellDateCrea.CellStyle = styleDate;
            
            var cellDateModif = row.CreateCell(3);
            cellDateModif.SetCellValue(exp.ModifDate);
            cellDateModif.CellStyle = styleDate;
            
            var cellFullName = row.CreateCell(4);
            cellFullName.SetCellType(CellType.String);
            cellFullName.SetCellValue(exp.FullName);
            
            var cellPath = row.CreateCell(5);
            cellPath.SetCellType(CellType.String);
            cellPath.SetCellValue(exp.Path);
            
            Var.Log.Progress("Export Excel:", idx, Var.Exports.Count);
            idx++;
        }
        
        Var.Log.Del();

        Var.Log.ParamInLine("Ajout du filtre...");
        sht.SetAutoFilter(new CellRangeAddress(0, Var.Exports.Count, 1, 5));
        
        Var.Log.Del();
        Var.Log.Ok("Filtre ajouté !");
        
        wb.Write(fs);
        Var.Log.Ok("Fichier Excel sauvegardé avec succes !");
        Thread.Sleep(200);
    }

    #endregion
}