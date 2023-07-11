using Microsoft.AspNetCore.Mvc;
using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DATA.CashData
{
    public interface ICashes
    {
        //Task<bool> createCash(CashDto cash);
        Task<bool> createCash(int groupCode);
        Task<bool> createCash(int groupCode, CashDetailesDto cash);
        Task<List<Cash>> getCashesList(int groupCode);
        Task<List<Cash>>  getCashesByManagerId(int managerId);
        Task<CashDetailesDto> getCasheDetailes(int cashCode);
        Task<bool> updateCasheById(int cashCode, CashDetailesDto cashe);
        Task<bool> updateCashSettingsByGroupId(int groupId, int cashCode, CashDetailesDto cashDetailesDto);

        Task<List<MemberCashDto>> getDataToDownload(int cash, int group);
        //get remining_amount(cashId)
    }
}
