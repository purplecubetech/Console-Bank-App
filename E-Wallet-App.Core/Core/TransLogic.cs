using E_Wallet_App.Core.Interface;
using E_Wallet_App.Domain.Models;
using E_Wallet_App.Entity.Dtos;
using E_WalletApp.CORE.Interface.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Wallet_App.Core.Core
{
    public class TransLogic: ITransLogic
    {
        private IWalletRepository _walletRepository;
        private IUnitOfWork _unitOfWork;

        public TransLogic(IWalletRepository walletRepository, IUnitOfWork unitOfWork) 
        {
            _walletRepository = walletRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Deposite(TransDto transDto)
        {
            bool check = true;
            var wallet = await _walletRepository.GetByWalletId(transDto.WalletId);
            if(wallet == null)
            {
                check= false;
                return check;
            }
            wallet.Balance += transDto.amount;
            wallet.Date = DateTime.Now;
           // wallet.WalletId = transDto.WalletId;

            var transaction = new Transaction();
            transaction.Amount = transDto.amount;
            transaction.TransactionId = Guid.NewGuid();
            transaction.TransactionType = "Deposite";
            transaction.Description = $"your wallet: {transDto.WalletId} has been credited with {transDto.amount}";
            transaction.Date = DateTime.Now;
            transaction.WalletId = wallet.WalletId;
            transaction.CurrentBalance = wallet.Balance;
            _unitOfWork.Wallet.Update(wallet);
            _unitOfWork.Transaction.Create(transaction);
            _unitOfWork.Complete();
            return check;
        }

        public async Task<bool> Withdrawal(TransDto transDto)
        {
            bool check = true;
            bool balcheck = false;
            var wallet = await _walletRepository.GetByWalletId(transDto.WalletId);
            if (wallet != null)
            {
                if (wallet.Balance < transDto.amount)
                {
                    return false;
                }
                wallet.Balance -= transDto.amount;
                wallet.Date = DateTime.Now;
                //wallet.WalletId = transDto.WalletId;

                var transaction = new Transaction();
                transaction.Amount = transDto.amount;
                transaction.TransactionId = Guid.NewGuid();
                transaction.TransactionType = "withdrawal";
                transaction.Description = $"your wallet: {transDto.WalletId} has been debited with {transDto.amount}";
                transaction.Date = DateTime.Now;
                transaction.WalletId = wallet.WalletId;
                transaction.CurrentBalance = wallet.Balance;
                _unitOfWork.Wallet.Update(wallet);
                _unitOfWork.Transaction.Create(transaction);
                _unitOfWork.Complete();
                return true;

            }
            return false;
        }
    }
}
