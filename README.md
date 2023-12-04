# Polymorphic Serialization
This package provides `[Polymorphic]` attribute for your abstract fields (`abstract` \ `interface`) allowing you to choose derived implementation through an inspector.

It is an easy and convenient way of making code cleaner, more modular and extensible. New implementation can easily be added and accessed within the inspector without touching the code responsible for providing a concrete class instance, thus following the *Open / Closed Principle*.

## How to install

Go to *Window/Package Manager/Add package from git URL* and paste the link below:
```
https://github.com/MicKami/PolymorphicSerialization.git
```


## How to use

### Usage with `abstract class`
```csharp
public class Example : MonoBehaviour
{
	[SerializeReference, Polymorphic] // Both required!
	MyBaseClass myBaseClass;
}

[System.Serializable] // Required!
public abstract class MyBaseClass
{
	public abstract void Foo();
}
public class A : MyBaseClass
{
	public int myInt;

	public override void Foo()
	{		
		Debug.Log("Foo A");
	}
}
public class B : MyBaseClass
{
	public string myString;

	public override void Foo()
	{		
		Debug.Log("Foo B");
	}
}
```
#### Result in inspector:
![](https://imgur.com/jmpcrkp.jpeg) ![](https://imgur.com/URKlBdz.jpeg)
-
### Usage with `interface`
```csharp
public class Example : MonoBehaviour
{
	[SerializeReference, Polymorphic] // Both required!
	MyInterface myInterface;
}

public interface MyInterface
{
	void Foo();
}

public class A : MyInterface
{
	public int myInt;

	public void Foo()
	{		
		Debug.Log("Foo A");
	}
}
public class B : MyInterface
{
	public string myString;

	public void Foo()
	{
		Debug.Log("Foo B");
	}
}
```
#### Result in inspector:
![](https://imgur.com/rNpXvze.jpeg) ![](https://imgur.com/KMYddqX.jpeg)
-

### Usage with `List<>`
```csharp
public class Example : MonoBehaviour
{
	[SerializeReference, Polymorphic] // Both required!
	List<MyInterface> myInterface;
}

public interface MyInterface
{
	void Foo();
}

public class A : MyInterface
{
	public int myInt;

	public void Foo()
	{		
		Debug.Log("Foo A");
	}
}
public class B : MyInterface
{
	public string myString;

	public void Foo()
	{
		Debug.Log("Foo B");
	}
}
```
#### Result in inspector:
![](https://imgur.com/Q8O1j3w.jpeg) 
-

## Note:

 - Both `[SerializeReference, Polymorphic]` attributes are required in order for it to work properly.
 - `abstract class` has to be marked with `[System.Serializable]` attribute.
 - `List<>` and `Array` work with both `abstract class` and `interface`
 - Multi Object Editing is currently not supported.
 
## Required Unity version: 2021.3 (LTS) + 
## License
 [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
 
