using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Sink {

	public enum Axis { X, Y, Z }

	public static class ExtensionMethods {

		public static void DoAfterTime(this MonoBehaviour control, Action coroutine, float time) {

			control.StartCoroutine(MakeInvokedCoroutine(coroutine, time));
		}

		public static void DoAfterTimeIf(this MonoBehaviour control, Action coroutine, float time, Func<bool> cond) {

			control.StartCoroutine(MakeInvokedCoroutine(coroutine, time, cond));
		}

		public static void InvokeRepeat(this MonoBehaviour control, Action coroutine, float time) {

			control.StartCoroutine(MakeInvokedCoroutineRepeating(coroutine, time));
		}

		public static void InvokeRepeatingWhile(this MonoBehaviour control, Action coroutine, float time, Func<bool> cond) {

			control.StartCoroutine(MakeInvokedCoroutineRepeating(coroutine, time, cond));
		}

		public static void DoXTimes(this MonoBehaviour m, Action f, int amnt) {
			for (int i = 0; i < amnt; i++) {
				f();
			}
		}

		static IEnumerator MakeInvokedCoroutine(Action coroutine, float time) {
			yield return new WaitForSeconds(time);
			coroutine();
		}

		static IEnumerator MakeInvokedCoroutine(Action coroutine, float time, Func<bool> cond) {
			yield return new WaitForSeconds(time);
			if (cond()) {
				coroutine();
			}
		}

		static IEnumerator MakeInvokedCoroutineRepeating(Action coroutine, float time) {
			while (true) {
				yield return new WaitForSeconds(time);
				coroutine();
			}

		}

		static IEnumerator MakeInvokedCoroutineRepeating(Action coroutine, float time, Func<bool> cond) {
			while (cond()) {
				yield return new WaitForSeconds(time);
				coroutine();

			}
		}

		public static void StopCaller(this MonoBehaviour control) {

			StackFrame frame = new StackFrame(2);
			var method = frame.GetMethod();
			var l = frame.GetFileLineNumber();
			var type = method.DeclaringType;
			var name = method.Name;

			Debug.Log(method + "\n" + type + "\n" + name + "\n" + l);
		}

		public static Vector3 setX(this Vector3 v, float X) {

			return new Vector3(X, v.y, v.z);

		}

		public static Vector3 setY(this Vector3 v, float Y) {

			return new Vector3(v.x, Y, v.z);
		}

		public static T RandomItem<T>(this List<T> list, Predicate<T> condition) {
			List<T> temp = list.FindAll(condition);
			if (temp.Count == 0) { return default(T); }
			return temp[UnityEngine.Random.Range(0, list.Count)];

		}
		public static T RandomItem<T>(this List<T> list) {
			if(list==null||list.Count==0){return default(T);}
			return list[UnityEngine.Random.Range(0, list.Count)];

		}
		public static T RandomItem<T>(this T[] array) {
			if(array==null||array.Length==0){return default(T);}
			return array[UnityEngine.Random.Range(0, array.Length)];

		}
		public static T RandomItem<T>(this T[] array, Predicate<T> condition) {
			List<T> temp = array.ToList().FindAll(condition);
			if (temp.Count == 0) { return default(T); }
			return temp[UnityEngine.Random.Range(0, array.Length)];

		}

		public static bool HasAncestor(this Transform t, Transform parent){
			if(t.parent == null){return false;}
			if(t.parent == parent){return true;}
			return t.parent.HasAncestor(parent);
		}

	}

}