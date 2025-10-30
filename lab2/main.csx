using namespace System.Console;

// -------TASK1-------
public class C1
{
    private const string __PrivateConst = "PrivateValue";
    public const string PublicConst = "PublicValue";
    protected const string ProtectedConst = "ProtectedValue";

    private int __privateField;
    public int publicField;
    protected int protectedField;

    private string __PrivateProperty { get; set; }
    public string PublicProperty { get; set; }
    protected string ProtectedProperty { get; set; }

    public C1() //internal
    {
        __privateField = 100;
        publicField = 200;
        protectedField = 300;

        __PrivateProperty = "PrivateDefault";
        PublicProperty = "PublicDefault";
        ProtectedProperty = "ProtectedDefault";
    }

    public C1(int __pf, int pubf, int protf, string __privProp, string pubProp, string protProp) // this: base:
    {
        __privateField = __pf;
        publicField = pubf;
        protectedField = protf;

        __PrivateProperty = __privProp;
        PublicProperty = pubProp;
        ProtectedProperty = protProp;
    }

    public C1(C1 other) //not recommended to use
    {
        __privateField = other.__privateField;
        publicField = other.publicField;
        protectedField = other.protectedField;

        __PrivateProperty = other.__PrivateProperty;
        PublicProperty = other.PublicProperty;
        ProtectedProperty = other.ProtectedProperty;
    }

    private void __PrivateMethod()
    {
        Console.WriteLine("PrivateMethod: " + __PrivateProperty + "\n");
    }

    public void PublicMethod()
    {
        Console.WriteLine("PublicMethod: " + PublicProperty + "\n");
    }

    protected void ProtectedMethod()
    {
        Console.WriteLine("ProtectedMethod: " + ProtectedProperty + "\n");
    }
}

// -------TASK2-------
public interface I1
{
    string InterfaceProperty { get; set; }
    void InterfaceMethod(string message);
    event EventHandler InterfaceEvent;

    // public delegate void EventHandler(object? sender, EventArgs e);
    string this[int id] { get; set; }
}

// -------TASK3-------

public class C2 : C1, I1
{
    public string InterfaceProperty { get; set; }

    private string[] __internalArray = new string[10];
    public string this[int id]
    {
        get => __internalArray[id];
        set => __internalArray[id] = value;
    }

    public event EventHandler InterfaceEvent;

    public void InterfaceMethod(string message)
    {
        Console.WriteLine("InterfaceMethod: " + message + "\n");
        InterfaceEvent?.Invoke(this, EventArgs.Empty);
    }

    public C2()
        : base()
    {
        InterfaceProperty = "DefaultInterfaceProperty";
    }

    public C2(
        int __pf,
        int pubf,
        int protf,
        string __privProp,
        string pubProp,
        string protProp,
        string interfaceProp
    )
        : base(__pf, pubf, protf, __privProp, pubProp, protProp)
    {
        InterfaceProperty = interfaceProp;
    }

    public C2(C2 other)
        : base(other)
    {
        InterfaceProperty = other.InterfaceProperty;
    }

    public void CallInterfaceEvent()
    {
        InterfaceEvent?.Invoke(this, EventArgs.Empty);
    }
}



// -------TASK4-------
public class C3
{
    protected int protectedBaseField;
    public string publicBaseProperty { get; set; }

    public C3()
    {
        protectedBaseField = 10;
        publicBaseProperty = "BaseDefault";
    }

    protected void ProtectedBaseMethod()
    {
        Console.WriteLine("ProtectedBaseMethod: " + protectedBaseField + "\n");
    }

    public void PublicBaseMethod()
    {
        Console.WriteLine("PublicBaseMethod: " + publicBaseProperty + "\n");
    }
}

// Производный класс
public class C4 : C3
{
    private int __privateDerivedField;
    public string publicDerivedProperty { get; set; }

    public C4()
    {
        __privateDerivedField = 50;
        publicDerivedProperty = "DerivedDefault";
    }

    private void __PrivateDerivedMethod()
    {
        Console.WriteLine("PrivateDerivedMethod: " + __privateDerivedField);
    }

    public void PublicDerivedMethod()
    {
        Console.WriteLine("PublicDerivedMethod: " + publicDerivedProperty);
    }

    public void CallBaseProtectedMethod()
    {
        ProtectedBaseMethod(); // Доступ к защищенному методу базового класса
    }
}

// ---------- MAIN ----------

// ---------- TASK1: Работа с C1 ----------
Console.WriteLine("=== TASK1: C1 ===");
C1 obj1 = new C1();
Console.WriteLine("C1 default: publicField = " + obj1.publicField);
obj1.PublicMethod();
Console.WriteLine("C1 default: PublicProperty = " + obj1.PublicProperty);

C1 obj2 = new C1(1, 2, 3, "Priv", "Pub", "Prot");
Console.WriteLine("C1 param: publicField = " + obj2.publicField);
obj2.PublicMethod();
Console.WriteLine("C1 param: PublicProperty = " + obj2.PublicProperty);

C1 obj3 = new C1(obj2);
Console.WriteLine("C1 copy: publicField = " + obj3.publicField);
obj3.PublicMethod();
Console.WriteLine("C1 copy: PublicProperty = " + obj3.PublicProperty);

// ---------- TASK3: Работа с C2 ----------
Console.WriteLine("\n=== TASK3: C2 ===");
C2 c2obj = new C2(10, 20, 30, "Priv2", "Pub2", "Prot2", "InterfaceValue");

Console.WriteLine("C2 inherited publicField = " + c2obj.publicField);
c2obj.PublicMethod();

Console.WriteLine("C2 InterfaceProperty = " + c2obj.InterfaceProperty);
c2obj.InterfaceMethod("Hello from Interface");

c2obj[0] = "Index0";
Console.WriteLine("C2 indexer[0] = " + c2obj[0]);

c2obj.InterfaceEvent += (sender, e) => Console.WriteLine("InterfaceEvent triggered!");
c2obj.CallInterfaceEvent();

// ---------- TASK4: Работа с C3 и C4 ----------
Console.WriteLine("\n=== TASK4: C3 & C4 ===");
C3 c3obj = new C3();
Console.WriteLine("C3 publicBaseProperty = " + c3obj.publicBaseProperty);
c3obj.PublicBaseMethod();

C4 c4obj = new C4();
WriteLine("C4 publicDerivedProperty = " + c4obj.publicDerivedProperty);
c4obj.PublicDerivedMethod();
c4obj.PublicBaseMethod();
c4obj.CallBaseProtectedMethod();
