using E_commerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Services
{
    public class ContractService
    {
        private IOnlinePaymentService _onlinePaymentService;

        public ContractService(IOnlinePaymentService paymentService)
        {
            _onlinePaymentService= paymentService;
        }

        public void ProcessContract(Contract contract, int months)
        {
            double basicQuota = contract.TotalValue / months;
            for(int i = 0; i < months; i++)
            {
                DateTime date = contract.Date.AddMonths(i);
                double updatedQuota = basicQuota + _onlinePaymentService.Interest(basicQuota, i);
                double fullQuota = updatedQuota + _onlinePaymentService.PaymentFee(updatedQuota);
                contract.AddInstallment(new Installment(date, fullQuota));
            }
        }
    }
}
