// Kullanim;
GarantiBank garanti = (GarantiBank)BankCreator.GetInstance(BankType.GarantiBank);
GarantiBank garanti2 = (GarantiBank)BankCreator.GetInstance(BankType.GarantiBank);
GarantiBank garanti3 = (GarantiBank)BankCreator.GetInstance(BankType.GarantiBank);

garanti.SendMoney(500);
garanti.SendReceipt(true);

HalkBank halk = (HalkBank)BankCreator.GetInstance(BankType.HalkBank);
halk.Send(1500, "1561-2456");
halk.SendReceipt(true);


#region Abstract Factory
interface IBankFactory
{
    IBank CreateBankInstance();
}
#endregion

#region Concrete Factories
// singleton factory classes
class GarantiBankFactory : IBankFactory
{
    GarantiBankFactory(){}
    static GarantiBankFactory _garantiBankFactory;
    static GarantiBankFactory()
    {
        _garantiBankFactory = new GarantiBankFactory();
    }

    public static GarantiBankFactory GetFactoryInstance() => _garantiBankFactory;
    public IBank CreateBankInstance()
    {
        GarantiBank garantiBank = GarantiBank.GetInstance();
        garantiBank.ConnectGaranti();
        return garantiBank;
    }
}
class HalkBankFactory : IBankFactory
{
    HalkBankFactory(){}
    static HalkBankFactory _halkBankFactory;
    static HalkBankFactory()
    {
        _halkBankFactory = new HalkBankFactory();
    }
    public static HalkBankFactory GetFactoryInstance() => _halkBankFactory;
    public IBank CreateBankInstance()
    {
        HalkBank halkBank = HalkBank.GetInstance();
        halkBank.Password = "11hbf11";
        return halkBank;
    }
}
class VakifBankFactory : IBankFactory
{
    VakifBankFactory(){}
    static VakifBankFactory _vakifBankFactory;
    static VakifBankFactory()
    {
        _vakifBankFactory = new VakifBankFactory();
    }
    public static VakifBankFactory GetFactoryInstance() => _vakifBankFactory;
    public IBank CreateBankInstance()
    {
        VakifBank vakifBank = VakifBank.GetInstance();
        vakifBank.ValidateCredential();
        return vakifBank;
    }
}

#endregion

#region Creator Class
class BankCreator
{
    public static IBank GetInstance(BankType bankType)
    {
        IBankFactory _bankFactory = bankType switch
        {
            BankType.GarantiBank => GarantiBankFactory.GetFactoryInstance(),
            BankType.HalkBank => HalkBankFactory.GetFactoryInstance(),
            BankType.VakifBank => VakifBankFactory.GetFactoryInstance(),
        };
        return _bankFactory.CreateBankInstance();
    }
}


#endregion


// Abstract Bank
interface IBank
{
    void SendReceipt(bool wantReceipt);
}

#region concrete nesneler
class GarantiBank : IBank
{
    string _userCode, _password, _amount;
    bool _receipt = false;
    //Singleton 
    GarantiBank(string userCode, string password)
    {
        Console.WriteLine($"{nameof(GarantiBank)} nesnesi oluşturuldu.");
        _userCode = userCode;
        _password = password;
    }
    static GarantiBank _garantiBank;
    static GarantiBank()
    {
        _garantiBank = new("gbf140", "14521");
    }
    public static GarantiBank GetInstance() => _garantiBank;
    public void ConnectGaranti()
        => Console.WriteLine($"{nameof(GarantiBank)} - bağlandı.");


    public void SendMoney(int amount)
    {
        _amount = Convert.ToString(amount);
        Console.WriteLine($"{amount}$ para gönderildi..");
    }

    public void SendReceipt(bool wantReceipt)
    {
        if (wantReceipt)
            Console.WriteLine($"{_userCode}'lu kullanıcı {nameof(GarantiBank)} bankası ile {_amount}$ tutarında para gönderiminde bulundu. ");
    }
}
class HalkBank : IBank
{
    string _userCode, _password, _amount, _acountNumber;
    HalkBank(string userCode)
    {
        Console.WriteLine($"{nameof(HalkBank)} nesnesi oluşturuldu.");
        _userCode = userCode;
    }
    public string Password { set => _password = value; }

    static HalkBank _halkbank;
    static HalkBank()
    {
        _halkbank = new("hbf421");
    }
    public static HalkBank GetInstance() => _halkbank;

    public void Send(int amount, string accountNumber)
    {
        _acountNumber = accountNumber;
        _amount = Convert.ToString(amount);
        Console.WriteLine($"{amount}$ para gönderildi..");
    }
    public void SendReceipt(bool wantReceipt)
    {
        if (wantReceipt)
            Console.WriteLine($"{_userCode}'lu kullanıcı {nameof(HalkBank)} bankası ile  {_acountNumber} hesabına {_amount}$ tutarında para gönderiminde bulundu. ");
    }
}
class VakifBank : IBank
{
    string _userCode, _email, _password, _amount, _acountNumber, _recipientName;
    public bool isAuthentcation { get; set; }
    public VakifBank(CredentialVakifBank credential, string password)
    {
        Console.WriteLine($"{nameof(VakifBank)} nesnesi oluşturuldu.");
        _userCode = credential.UserCode;
        _email = credential.Mail;
        _password = password;
    }
    static VakifBank _vakifBank;
    static VakifBank()
    {
        _vakifBank = new(new CredentialVakifBank { UserCode = "vnf123", Mail = "buzlukvakif@hotmail.com" }, "06v06");
    }
    public static VakifBank GetInstance() => _vakifBank;

    public void ValidateCredential()
    {
        if (_userCode != null && _password != null)
            isAuthentcation = true;
    }

    public void SendMoneyToAccountNumber(int amount, string recipientName, string accountNumber)
    {
        _acountNumber = accountNumber;
        _recipientName = recipientName;
        _amount = Convert.ToString(amount);
        Console.WriteLine($"{amount}$ para gönderildi..");
    }

    public void SendReceipt(bool wantReceipt)
    {
        if (wantReceipt)
            Console.WriteLine($"{_userCode}'lu kullanıcı {nameof(VakifBank)} bankası ile {_recipientName} isimli alıcının {_acountNumber} hesabına {_amount}$ tutarında para gönderiminde bulundu. ");
    }
}

#endregion
class CredentialVakifBank
{
    public string UserCode { get; set; }
    public string Mail { get; set; }
}
enum BankType
{
    GarantiBank, HalkBank, VakifBank
}

