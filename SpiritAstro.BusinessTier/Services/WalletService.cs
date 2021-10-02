using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.Wallet;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Wallet;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IWalletService
    {
        Task<WalletModel> GetWalletById(long walletId);
        Task<long> CreateWallet(CreateWalletRequest createWalletRequest);
    }
    public partial class WalletService
    {
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 20;
        private const int LimitPaging = 20;

        public WalletService(IUnitOfWork unitOfWork, IWalletRepository repository, IMapper mapper) : base(unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<WalletModel> GetWalletById(long walletId)
        {
            var walletModel = await Get().Where(w => w.Id == walletId).ProjectTo<WalletModel>(_mapper).FirstOrDefaultAsync();
            if (walletModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any wallet matches with id = {walletId}");
            }

            return walletModel;
        }

        public async Task<long> CreateWallet(CreateWalletRequest createWalletRequest)
        {
            var mapper = _mapper.CreateMapper();
            var famousPerson = mapper.Map<Wallet>(createWalletRequest);
            await CreateAsyn(famousPerson);
            return famousPerson.Id;
        }
    }
}
