using AutoMapper;
using SpiritAstro.BusinessTier.Requests.Transaction;
using SpiritAstro.BusinessTier.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class TransactionModule
    {
        public static void ConfigTransactionMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CreateTransactionRequest, Transaction>();
            mc.CreateMap<Transaction, TransactionModel>().ReverseMap();
            mc.CreateMap<UpdateTransactionRequest, Transaction>();
        }
    }
}
