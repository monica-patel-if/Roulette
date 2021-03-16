namespace TurnTheGameOn.Timer
{
	using UnityEngine;
	using System;
	public class AnalogClock : MonoBehaviour
	{
		public bool useSystemTime = true;
		public Timer timer;
		public Transform secondHandPivot;
		public Transform minuteHandPivot;
		public Transform hourHandPivot;
		private float secondhandRotation;
		private float minuteRotation;
		private float hourRotation;
		private int second;
		private int minute;
		private int hour;

		void Update ()
		{
			if (useSystemTime)
			{
				second = (int)DateTime.Now.Second;
				minute = (int)DateTime.Now.Minute;
				hour = (int)DateTime.Now.Hour;
			}
			else
			{
				if (timer)
				{
					second = (int)timer.second;
					minute = (int)timer.minute;
					hour = (int)timer.hour;
				}
				else
				{
					Debug.LogWarning ("This clock is not set to use system time, please assign a timer to get values from.");
				}
			}

			secondhandRotation = second * 6f;
			minuteRotation = minute * 6f;
			hourRotation = (hour * 30) + (minuteRotation/12);

			secondHandPivot.localRotation = Quaternion.Euler(0, 0, -secondhandRotation);
			minuteHandPivot.localRotation = Quaternion.Euler(0, 0, -minuteRotation);
			hourHandPivot.localRotation = Quaternion.Euler(0, 0, -hourRotation);
		}

	}
}