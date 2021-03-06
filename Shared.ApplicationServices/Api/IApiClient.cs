﻿using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist;
using CSharpFunctionalExtensions;
using Mandate = Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api {
    public interface IApiClient
    {
        Task<Result<ViewModel.MandateList.Mandate[]>> FetchAllMandatesAsync(string uri);
        Task<Result<Mandate>> FetchMandateDetailAsync(string uri);
        Task<Result<ViewModel.FarmDetail.Farm>> FetchFarmDetailAsync(string uri);
        Task<Result<ChecklistSample>> FetchChecklistSampleAsync(string uri);
    }
}