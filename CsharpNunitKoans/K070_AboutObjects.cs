using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TheKoans
{
	[TestFixture]
	public class K070_AboutObjects
	{
		private object FILL_ME_IN = new Object ();

		[Test]
		public void EverythingIsAnObject ()
		{
			Assert.AreEqual (true, 1 is Object);
			Assert.AreEqual (true, 1.5 is Object);
			Assert.AreEqual (true, "string" is Object);
			Assert.AreEqual (true, true is Object);
		}

		[Test]
		public void ExceptNullIsNotAnObject ()
		{
			Assert.AreEqual (false, null is Object);
		}

		[Test]
		public void ObjectLiteralIsAnObject ()
		{
			Assert.AreEqual (true, new { } is Object);
		}
		//TODO - finish remaining object tests from the RubyKoans AboutObjects
	}
}
