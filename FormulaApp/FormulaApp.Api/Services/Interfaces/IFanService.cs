using FormulaApp.Api.Models;

namespace FormulaApp.Api.Services.Interfaces;

public interface IFanService
{
    Task<List<Fan>?> GetAllFans();
}
