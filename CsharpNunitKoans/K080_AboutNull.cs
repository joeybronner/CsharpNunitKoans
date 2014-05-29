using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TheKoans
{
	// In Corey's Ruby Koans, this is called AboutNil. We've renamed it to AboutNull to match the C# convention
	[TestFixture]
	public class K080_AboutNull : KoanHelper
	{
		[Test]
		public void NilIsNotAnObject ()
		{
			//not everything is an object
			Assert.IsFalse (typeof(object).IsAssignableFrom (null), "To quote from South Park's World of Warcraft episode: 'Can you kill that which has no life?'"); 
		}

		[Test]
		public void NullIsNotAnInstanceOfObjectAndInvokingAMethodThrowsNullReferenceException ()
		{
			Object nullReference = null;

			try {
				nullReference.GetHashCode ();
			} catch (Exception exception) {
				Assert.AreEqual ("Object reference not set to an instance of an object", exception.Message, "If you're still unsure, take a look at the name of this method...");
			}
		}

		[Test]
		public void CheckingThatAnObjectIsNull ()
		{
			object obj = null;
			Assert.IsTrue (obj == null);
		}

		[Test]
		public void ABetterWayToCheckThatAnObjectIsNull ()
		{
			object obj = null;
			Assert.IsNull (obj, "If only we had an object passed in as a parameter...");
		}

		[Test]
		public void AWayNotToCheckThatAnObjectIsNull ()
		{
			object obj = null;
			Assert.IsTrue (obj == null);
		}
		// RJG Begin Under Construction
		// Passing Null Objects to C# Extension Methods... is allowed?!?!?
		// http://www.peteonsoftware.com/index.php/2012/06/21/c-extension-methods-on-null-objects/
		// RJG End
	}
}
