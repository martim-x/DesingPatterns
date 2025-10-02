using System;


// -------TASk1-------
public class C1
{
    private const string PrivateConst;
    public const string PublicConst;
    protected const string ProtectedConst;

    private int privateField;
    public int publicField;
    protected int protectedField;

    private string PrivateProperty { get; set; }
    // private string PrivateProperty {
    //     get{
    //         return privateField;
    //     }
    //     set{
    //         privateField = value;
    //     }
    // }

    public string PublicProperty { get; set; }
    protected string ProtectedProperty { get; set; }

    public C1()
    {
        privateField = 100;
        publicField = 200;
        protectedField = 300;

        PrivateProperty = "PrivateDefault";
        PublicProperty = "PublicDefault";
        ProtectedProperty = "ProtectedDefault";
    }

    public C1(int pf, int pubf, int protf,
              string privProp, string pubProp, string protProp)
    {
        privateField = pf;
        publicField = pubf;
        protectedField = protf;

        PrivateProperty = privProp;
        PublicProperty = pubProp;
        ProtectedProperty = protProp;
    }

    public C1(C1 other)
    {
        privateField = other.privateField;
        publicField = other.publicField;
        protectedField = other.protectedField;

        PrivateProperty = other.PrivateProperty;
        PublicProperty = other.PublicProperty;
        ProtectedProperty = other.ProtectedProperty;
    }

    private void PrivateMethod()
    {
        Console.WriteLine("PrivateMethod: " + PrivateProperty);
    }

    public void PublicMethod()
    {
        Console.WriteLine("PublicMethod: " + PublicProperty);
    }

    protected void ProtectedMethod()
    {
        Console.WriteLine("ProtectedMethod: " + ProtectedProperty);
    }
}

// -------TASK2-------
public interface I1
{
    // Свойство
    string InterfaceProperty { get; set; }  

    // Метод
    void InterfaceMethod(string message);

    // Событие
    event EventHandler InterfaceEvent;

    // Индексатор
    string this[int index] { get; set; } 
}

// -------TASK3-------
public class C2 : C1, I1
{
    // Реализация интерфейсного свойства
    public string InterfaceProperty { get; set; }

    // Индексатор
    private string[] internalArray = new string[10];
    public string this[int index]
    {
        get => internalArray[index];
        set => internalArray[index] = value;
    }

    // Событие
    public event EventHandler InterfaceEvent;

    // Реализация интерфейсного метода
    public void InterfaceMethod(string message)
    {
        Console.WriteLine("InterfaceMethod: " + message);
        InterfaceEvent?.Invoke(this, EventArgs.Empty);
    }

    public C2() : base()
    {
        InterfaceProperty = "DefaultInterfaceProperty";
    }

    public C2(int pf, int pubf, int protf,
              string privProp, string pubProp, string protProp,
              string interfaceProp) 
        : base(pf, pubf, protf, privProp, pubProp, protProp)
    {
        InterfaceProperty = interfaceProp;
    }

    public C2(C2 other) : base(other)
    {
        InterfaceProperty = other.InterfaceProperty;
    }

    // Методы класса C2
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
        Console.WriteLine("ProtectedBaseMethod: " + protectedBaseField);
    }

    public void PublicBaseMethod()
    {
        Console.WriteLine("PublicBaseMethod: " + publicBaseProperty);
    }
}

// Производный класс
public class C4 : C3
{
    private int privateDerivedField;
    public string publicDerivedProperty { get; set; }

    public C4()
    {
        privateDerivedField = 50;
        publicDerivedProperty = "DerivedDefault";
    }

    private void PrivateDerivedMethod()
    {
        Console.WriteLine("PrivateDerivedMethod: " + privateDerivedField);
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




// ---------- TASK1: Работа с C1 ----------
Console.WriteLine("=== TASK1: C1 ===");
// Конструктор по умолчанию
C1 obj1 = new C1();
Console.WriteLine("C1 default: publicField = " + obj1.publicField);
obj1.PublicMethod();
Console.WriteLine("C1 default: PublicProperty = " + obj1.PublicProperty);

// Конструктор с параметрами
C1 obj2 = new C1(1, 2, 3, "Priv", "Pub", "Prot");
Console.WriteLine("C1 param: publicField = " + obj2.publicField);
obj2.PublicMethod();
Console.WriteLine("C1 param: PublicProperty = " + obj2.PublicProperty);

// Конструктор копирования
C1 obj3 = new C1(obj2);
Console.WriteLine("C1 copy: publicField = " + obj3.publicField);
obj3.PublicMethod();
Console.WriteLine("C1 copy: PublicProperty = " + obj3.PublicProperty);

// ---------- TASK3: Работа с C2 ----------
Console.WriteLine("\n=== TASK3: C2 ===");
C2 c2obj = new C2(10, 20, 30, "Priv2", "Pub2", "Prot2", "InterfaceValue");

// Доступ к наследуемым полям и методам
Console.WriteLine("C2 inherited publicField = " + c2obj.publicField);
c2obj.PublicMethod();

// Доступ к интерфейсному свойству и методу
Console.WriteLine("C2 InterfaceProperty = " + c2obj.InterfaceProperty);
c2obj.InterfaceMethod("Hello from Interface");

// Индексатор
c2obj[0] = "Index0";
Console.WriteLine("C2 indexer[0] = " + c2obj[0]);

// Событие
c2obj.InterfaceEvent += (sender, e) => Console.WriteLine("InterfaceEvent triggered!");
c2obj.CallInterfaceEvent();

// ---------- TASK4: Работа с C3 и C4 ----------
Console.WriteLine("\n=== TASK4: C3 & C4 ===");
C3 c3obj = new C3();
Console.WriteLine("C3 publicBaseProperty = " + c3obj.publicBaseProperty);
c3obj.PublicBaseMethod();

C4 c4obj = new C4();
Console.WriteLine("C4 publicDerivedProperty = " + c4obj.publicDerivedProperty);
c4obj.PublicDerivedMethod();
// Доступ к наследованным членам
c4obj.PublicBaseMethod();
c4obj.CallBaseProtectedMethod();
