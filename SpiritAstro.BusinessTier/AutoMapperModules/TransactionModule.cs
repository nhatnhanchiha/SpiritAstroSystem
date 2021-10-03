using AutoMapper;
using SpiritAstro.BusinessTier.Requests.Transaction;
using SpiritAstro.BusinessTier.ViewModels.Transaction;
using SpiritAstro.DataTier.Models;


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
