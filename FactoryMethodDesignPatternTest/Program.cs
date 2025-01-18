

#region Factory
//A1? a1 = (A1)ACreator.GetInstance(AType.A1);
//a1.Run();

//interface IA
//{
//    void Run();
//}

//class ACreator
//{
//    public static IA GetInstance(AType aType)
//    {
//        IA _ia = null;
//        switch (aType)
//        {
//            case AType.A1:
//                _ia = new A1();
//                break;
//            case AType.A2:
//                _ia = new A2();
//                break;
//            case AType.A3:
//                _ia = new A3();
//                break;
//        }
//        return _ia;
//    }
//}

//enum AType
//{
//    A1, A2, A3
//}

//class A1 : IA
//{
//    public void Run()
//    {
//        Console.WriteLine($"{nameof(A1)} Running...");
//    }
//}
//class A2 : IA
//{
//    public void Run()
//    {
//        Console.WriteLine($"{nameof(A2)} Running...");
//    }
//}
//class A3 : IA
//{
//    public void Run()
//    {
//        Console.WriteLine($"{nameof(A3)} Running...");
//    }
//}
#endregion


#region Factory Method
//A1? a1 = (A1)ACreator.GetInstance(AType.A1);
//a1.Run();

ACreator.GetInstance(AType.A3);

interface IA
{
    void Run();
}

class ACreator
{
    public static IA GetInstance(AType aType)
    {
        IAFactory _aFactory = aType switch
        {
            AType.A1 => new A1Factory(),
            AType.A2 => new A2Factory(),
            AType.A3 => new A3Factory()
        };
        return _aFactory.Create();
    }
}
// Factory interface
interface IAFactory
{
    IA Create();
}

//Factory Classes
class A1Factory : IAFactory
{
    public IA Create()
    {
        A1 a1 = new A1();
        return a1;

    }
}
class A2Factory : IAFactory
{
    public IA Create()
    {
        A2 a2 = new A2();
        return a2;
    }
}
class A3Factory : IAFactory
{
    public IA Create()
    {
        A3 a3 = new A3();
        return a3;
    }
}

enum AType
{
    A1, A2, A3
}

class A1 : IA
{
    public void Run()
    {
        Console.WriteLine($"{nameof(A1)} Running...");
    }
}
class A2 : IA
{
    public void Run()
    {
        Console.WriteLine($"{nameof(A2)} Running...");
    }
}
class A3 : IA
{
    public void Run()
    {
        Console.WriteLine($"{nameof(A3)} Running...");
    }
}
#endregion


