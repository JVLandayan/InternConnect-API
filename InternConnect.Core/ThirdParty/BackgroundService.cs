using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Context;
using InternConnect.Context.Models;
using InternConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InternConnect.Service.ThirdParty
{
    public interface IBackgroundService
    {
        public void CompanyStatus();
    }
    public class BackgroundService : IBackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public BackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void CompanyStatus()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<InternConnectContext>();
                var companyList = db.Companies.ToList();
                foreach (var company in companyList)
                {
                    if (DateTime.Compare(GetDate(), company.Expiration) == 1)
                    {
                        company.Status = Status.CompanyStatusList.EXPIRED.ToString();
                    }

                    if (DateTime.Compare(GetDate(), company.DateAdded.AddDays(365)) == 1)
                    {
                        company.Status = Status.CompanyStatusList.EXISTING.ToString();
                    }
                    Console.WriteLine(company.DateAdded.AddDays(365));
                }
                db.UpdateRange(companyList);
                db.SaveChanges();
            }
        }

        private DateTime GetDate()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"));
        }
    }
}
