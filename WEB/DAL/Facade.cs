namespace QtIspMgntDAL
{
    public static class Facade
    {
        public static CustomerMikrotikUserDAO CustomerMikrotikUser { get { return new CustomerMikrotikUserDAO(); } }
        public static CustomerPaymentDAO CustomerPayment { get { return new CustomerPaymentDAO(); } }
        public static CustomerPaymentMethodDAO CustomerPaymentMethod { get { return new CustomerPaymentMethodDAO(); } }
        public static BillingDAO Billing { get { return new BillingDAO(); } }
        public static BillingAdditionalFeeDAO BillingAdditionalFee { get { return new BillingAdditionalFeeDAO(); } }
        public static BillingCustomerPackageDAO BillingCustomerPackage { get { return new BillingCustomerPackageDAO(); } }
        public static CustomerPaymentByBillDAO CustomerPaymentByBill { get { return new CustomerPaymentByBillDAO(); } }
        public static CustomerAdditionalFeeDAO CustomerAdditionalFee { get { return new CustomerAdditionalFeeDAO(); } }
        public static CustomerPackageDAO CustomerPackage { get { return new CustomerPackageDAO(); } }
        public static PackageDAO Package { get { return new PackageDAO(); } }
        public static PaymentMethodDAO PaymentMethod { get { return new PaymentMethodDAO(); } }
        public static CustomerDAO Customer { get { return new CustomerDAO(); } }
        public static AdditionalFeeDAO AdditionalFee { get { return new AdditionalFeeDAO(); } }
        public static CompanyDAO Company { get { return new CompanyDAO(); } }
    }
}
