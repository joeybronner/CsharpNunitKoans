using NUnit.Framework;

namespace TheKoans
{
	[TestFixture]
	public class K110_AboutGenerics : KoanHelper
	{
		public interface Animal
		{
		}

		public class Dog : Animal
		{
		}

		public class Cat : Animal
		{
		}

		public class Robot
		{
		}

		public class SayHello<TAnimal>
		{
			public string HelloMessage ()
			{
				return string.Format ("Hello {0}", typeof(TAnimal).Name);
			}
		}

		public class SayHelloToAnimalsOnly<TAnimal> where TAnimal : Animal
		{
			public string HelloMessage ()
			{
				return string.Format ("Hello {0}", typeof(TAnimal).Name);
			}
		}

		[Test]
		public void GenericTypeIsACompositionOfTypes ()
		{
			var helloCat = new SayHello<Cat> ();
			Assert.AreEqual (typeof(SayHello<Cat>), helloCat.GetType ());
		}

		[Test]
		public void GenericTypeCanGetTheGenericArgumentName ()
		{
			var helloCat = new SayHello<Cat> ();
			Assert.AreEqual ("Hello Cat", helloCat.HelloMessage ());
		}

		[Test]
		public void GenericTypeCanBeUsedWithAnyTypeEvenPrimitives ()
		{
			var helloInt = new SayHello<int> ();
			Assert.AreEqual ("Hello Int32", helloInt.HelloMessage ());
		}

		[Test]
		public void IsGenericTypeInformation ()
		{
			var helloCat = new SayHello<Cat> ();

			Assert.AreEqual (true, helloCat.GetType ().IsGenericType);
			Assert.AreEqual (true, typeof(SayHello<>).IsGenericTypeDefinition);
		}

		[Test]
		public void GetGenericTypeFromUsage ()
		{
			var helloCat = new SayHello<Cat> ();
			var expectedType = typeof(SayHello<>);

			Assert.AreEqual (typeof(SayHello<Cat>).GetGenericTypeDefinition (), helloCat.GetType ().GetGenericTypeDefinition ());
			Assert.AreEqual (true, typeof(SayHello<>).IsGenericTypeDefinition);
		}

		[Test]
		public void ComposedTypesShareTheSameGenericTypeDefinition ()
		{
			var helloCat = new SayHello<Cat> ();
			var helloDog = new SayHello<Dog> ();

			var genericTypeFromDogToCompare = helloDog.GetType ().GetGenericTypeDefinition ();

			Assert.AreEqual (genericTypeFromDogToCompare, helloCat.GetType ().GetGenericTypeDefinition ());
		}

		[Test]
		public void GenericTypeParameterCanRestrictedToCertainTypes ()
		{
			//you can write this:
			var helloInt = new SayHello<int> ();

			//but you can't write this, because int do not inherits Animal
			//var helloAnimalInt = new SayHelloToAnimalsOnly<int> ();

			//you can also ensure that it:
			//- is a class:
			//     public class MyGeneric<T> where T : class
			//- has a constructor:
			//     public class MyGeneric<T> where T : new()
			//- you can add multiple conditions:
			//     public class MyGeneric<T> where T : Animal, class

			Assert.AreEqual (true, true);
		}

		public interface IHasName
		{
			string Name { get; }
		}

		public class Person : IHasName
		{
			public string Name { get; set; }
		}

		public class AnimalOwner<TOwner,TAnimal> 
			where TOwner : IHasName
			where TAnimal : Animal
		{
			TOwner owner;

			public AnimalOwner (TOwner owner)
			{
				this.owner = owner;
			}

			public string Describe ()
			{
				return string.Format ("{0} loves {0} pets", owner.Name, typeof(TAnimal).Name);
			}
		}

		[Test]
		public void CanComposeWithMultipleGenerics ()
		{
			var paul = new AnimalOwner<Person,Cat> (new Person{ Name = "Paul" });
			var expected = new[]{ typeof(Person), typeof(Cat) };

			Assert.AreEqual (2, paul.GetType ().GetGenericArguments ().Length);
			Assert.AreEqual (new[] {typeof(Person), typeof(Cat)}, paul.GetType ().GetGenericArguments ());
		}

		[Test]
		public void WhenEnforceConstrainFromTypeYouUseThatType ()
		{
			var paul = new AnimalOwner<Person,Cat> (new Person{ Name = "Paul" });

			//In the generic, in describe method, you can use owner.Name property
			//because we know that owner MUST implement IHasName interface that has a Name property 
			Assert.AreEqual ("Paul loves Paul pets", paul.Describe ());
		}
	}
}
