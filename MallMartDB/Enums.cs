namespace MallMartDB
{
    public enum Authorization 
    {
        Customer,
        DeliveryBoy,
        DeliveryManager,
        AcquisitonManager,
        Manager
    }
    public enum Job
    {
        DeliveryBoy,
        DeliveryManager,
        AcquisitonManager,
        Manager
    }
    public enum PaymentMethod
    {
        Paypal,
        Visa,
        Mastercard,
        Bitcoin,
        None
    }
}