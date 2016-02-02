using UnityEngine;
using System.Collections;
using NUnit.Framework;

namespace Lords {
	[TestFixture]
	public class StringTest {
		[Test]
		public void TestFormatElapsedTime() {
			Assert.AreEqual("Day 1", Strings.FormatElapsedTime(0.001f / Game.GameSpeed));
			Assert.AreEqual("Day 1", Strings.FormatElapsedTime(1 / Game.GameSpeed));
			Assert.AreEqual("Day 2", Strings.FormatElapsedTime(1.5f / Game.GameSpeed));
			Assert.AreEqual("Day 30", Strings.FormatElapsedTime(30 / Game.GameSpeed));
			Assert.AreEqual("Year 1, Day 30", Strings.FormatElapsedTime(395 / Game.GameSpeed));
			Assert.AreEqual("Year 2, Day 1", Strings.FormatElapsedTime((365*2 + 1) / Game.GameSpeed));
		}

		[Test]
		public void TestFormatDuration() {
			Assert.AreEqual("1 Day", Strings.FormatDuration(1 / Game.GameSpeed));
			Assert.AreEqual("1 Day", Strings.FormatDuration(1.5f / Game.GameSpeed));
			Assert.AreEqual("30 Days", Strings.FormatDuration(30 / Game.GameSpeed));
			Assert.AreEqual("1 Year, 30 Days", Strings.FormatDuration(395 / Game.GameSpeed));
			Assert.AreEqual("2 Years, 1 Day", Strings.FormatDuration((365*2 + 1) / Game.GameSpeed));
		}
	}
}
