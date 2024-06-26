﻿using BLL.Businesses.Base;
using DAL.Entities.Store;
using DAL.Repositories.Base;

namespace BLL.Businesses.Store
{
    public class ProductBusiness : BaseBusiness<Product>
    {
        public ProductBusiness(IRepository<Product> repository) : base(repository)
        {
        }
    }
}