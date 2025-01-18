// Kullanim;
GarantiBank garanti = (GarantiBank)BankCreator.GetInstance(BankType.GarantiBank);
garanti.SendMoney(500);
garanti.SendReceipt(true);

HalkBank halk = (HalkBank)BankCreator.GetInstance(BankType.HalkBank);
halk.Send(1500,"1561-2456");
halk.SendReceipt(true);


#region Abstract Factory
interface IBankFactory
{
    IBank CreateBankInstance();
}
#endregion

#region Concrete Factories

class GarantiBankFactory : IBankFactory
{
    public IBank CreateBankInstance()
    {
        GarantiBank garantiBank = new("gbf140", "14521");
        garantiBank.ConnectGaranti();
        return garantiBank;
    }
}
class HalkBankFactory : IBankFactory
{
    public IBank CreateBankInstance()
    {
        HalkBank halkBank = new("hbf421");
        halkBank.Password = "11hbf11";
        return halkBank;
    }
}
class VakifBankFactory : IBankFactory
{
    public IBank CreateBankInstance()
    {
        VakifBank vakifBank = new(new CredentialVakifBank { UserCode = "vnf123", Mail = "buzlukvakif@hotmail.com" }, "06v06");
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
            BankType.GarantiBank => new GarantiBankFactory(),
            BankType.HalkBank => new HalkBankFactory(),
            BankType.VakifBank => new VakifBankFactory(),
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
    public GarantiBank(string userCode, string password)
    {
        Console.WriteLine($"{nameof(GarantiBank)} nesnesi oluşturuldu.");
        _userCode = userCode;
        _password = password;
    }
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
    public HalkBank(string userCode)
    {
        Console.WriteLine($"{nameof(HalkBank)} nesnesi oluşturuldu.");
        _userCode = userCode;
    }

    public string Password { set => _password = value; }

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

