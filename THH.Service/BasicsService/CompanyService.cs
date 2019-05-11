using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using THH.Core;
using THH.Core.TablePage;
using THH.DAL.ReportContext;
using THH.DAL.Repository;
using THH.Model.ReportModel.DBModel;
using THH.Model.ReportModel.Dto;
using THH.Service.ReportService;

namespace THH.Service.CompanyService
{
    public class CompanyService : ReportBaseService
    {
        private IRepository<Company> CompanyRepository = null;
        public CompanyService()
        {
            CompanyRepository = new ReportRepository<Company>();
        }

        /// <summary>
        /// 获取StoreReport列表
        /// </summary>
        /// <param name="gp"></param>
        /// <returns></returns>
        public List<CompanyDto> GetCompanyGrid(TablePageParameter gp)
        {
            Expression<Func<Company, bool>> ex = t => true;
            ex = ex.And(t => !t.IsDelete);
            var CompanyList = CompanyRepository.GetEntities(ex);
            if (gp == null)
            {
                return Mapper.Map<List<Company>, List<CompanyDto>>(CompanyList.ToList());
            }
            else
            {
                return Mapper.Map<List<Company>, List<CompanyDto>>(GetTablePagedList(CompanyList, gp));
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="storeReportDtos"></param>
        /// <returns></returns>
        public bool Add(List<CompanyDto> companyDtos)
        {
            var Company = Mapper.Map<List<CompanyDto>, List<Company>>(companyDtos);
            return CompanyRepository.Insert(Company) > 0;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="storeReportDtos"></param>
        /// <returns></returns>
        public bool Add(CompanyDto companyDtos)
        {
            var Company = Mapper.Map<CompanyDto, Company>(companyDtos);
            return CompanyRepository.Insert(Company) > 0;
        }


        public CompanyDto Find(int id)
        {
            Company company = CompanyRepository.Find(id);
            return Mapper.Map<Company, CompanyDto>(company);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="CompanyDto"></param>
        /// <returns></returns>
        public bool Update(CompanyDto companyDtos)
        {
            var Company = Mapper.Map<CompanyDto, Company>(companyDtos);
            return CompanyRepository.Update(Company) > 0;
        }


    }
}
