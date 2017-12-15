using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace Arena.Modules {

  public class ImList<T> {
    List<T> list;
    public int Count {
      get { return list.Count; }
    }

    public ImList() {
      list = new List<T>();
    }

    public ImList(List<T> lst) {
      list = lst;
    }

    public ImList(T item) {
      list = new List<T>() { item };
    }

    public List<T> GetList() {
      return new List<T>(list);
    }

    public bool Has(T item) {
      return list.Contains(item);
    }

    public override string ToString() {
      return "ImList: " + list.ToString();
    }

    public T this[int index] {
      get { return list[index]; }
    }

    public static ImList<T> operator +(ImList<T> imList, T item) {
      var lst = imList.GetList();
      lst.Add(item);
      return new ImList<T>(lst);
    }

    public static ImList<T> operator *(ImList<T> imList1, ImList<T> imList2) {
      return new ImList<T>(imList1.GetList().Concat(imList2.GetList()).ToList());
    }

    public static ImList<T> operator /(ImList<T> imList1, ImList<T> imList2) {
      // TODO think about more immutable friendly ways of comparing
      return new ImList<T>((imList1.GetList().Except(imList2.GetList())).ToList());
    }

    public static ImList<T> operator -(ImList<T> imList, T item) {
      var lst = imList.GetList();
      lst.Remove(item);
      return new ImList<T>(lst);
    }

    public static ImList<T> operator &(ImList<T> imList, Func<ImList<T>, ImList<T>> curry) {
      return curry(imList);
    }

    public static bool operator !(ImList<T> imList) {
      return imList.Count == 0;
    }
  }

  public struct KV<K, V> {
    public K Key { get; }
    public V Value { get; }

    public KV(K key, V val) {
      Key = key;
      Value = val;
    }
  }

  public class ImMap<K, V> {
    K CurriedKey;
    Dictionary<K, V> Map;

    public ImMap() {
      Map = new Dictionary<K, V>();
    }

    public ImMap(Dictionary<K, V> map) {
      Map = map;
    }

    public Dictionary<K, V> GetMap() {
      return new Dictionary<K, V>(Map);
    }

    public bool Has(K key) {
      return Map.ContainsKey(key);
    }

    public void SetKey(K key) {
      CurriedKey = key;
    }

    public ImMap<K, V> SetValue(V val) {
      if (CurriedKey == null) {
        return this;
      }
      var map = GetMap();
      if (map.ContainsKey(CurriedKey)) {
        map[CurriedKey] = val;
      } else {
        map.Add(CurriedKey, val);
      }
      return new ImMap<K, V>(map);
    }

    public V this[K key] {
      get { return Map[key]; }
    }

    public static ImMap<K, V> operator +(ImMap<K, V> imMap, KV<K, V> keyValuePair) {
      var map = imMap.GetMap();
      map.Add(keyValuePair.Key, keyValuePair.Value);
      return new ImMap<K, V>(map);
    }

    public static ImMap<K, V> operator /(ImMap<K, V> imMap, K key) {
      imMap.SetKey(key);
      return imMap;
    }

    public static ImMap<K, V> operator *(ImMap<K, V> imMap, V val) {
      return imMap.SetValue(val);
    }

    public static ImMap<K, V> operator -(ImMap<K, V> imMap, K key) {
      var map = imMap.GetMap();
      map.Remove(key);
      return new ImMap<K, V>(map);
    }
  }

  public static class Im {
    public static ImMap<K, V> Map<K, V>() {
      return new ImMap<K, V>();
    }

    public static ImMap<string, V> Map<V>() {
      return Map<string, V>();;
    }

    public static ImMap<K, V> Map<K, V>(Dictionary<K, V> map) {
      return new ImMap<K, V>(map);
    }

    public static ImMap<string, V> Map<V>(Dictionary<string, V> map) {
      return Map<string, V>(map);
    }

    public static KV<string, V> KV<V>(string key, V val) {
      return new KV<string, V>(key, val);
    }

    public static R Fold<T, R>(Func<R, T, R> iterator, R initialValue, ImList<T> list) {
      var lst = list.GetList();
      var acc = initialValue;
      foreach (T item in lst) {
        acc = iterator(acc, item);
      }
      return acc;
    }

    public static void Each<T>(Action<T> iterator, ImList<T> list) {
      var lst = list.GetList();
      foreach (T item in lst) {
        iterator(item);
      }
    }

    public static T Find<T>(Func<T, bool> iterator, ImList<T> list) {
      var lst = list.GetList();
      foreach (T item in lst) {
        if (iterator(item)) {
          return item;
        }
      }
      return default(T);
    }

    public static bool Has<T>(T item, ImList<T> list) {
      return list.Has(item);
    }

    public static ImList<T> Transform<T>(Func<T, T> iterator, ImList<T> list) {
      var transformedList = Im.Fold<T, List<T>>((List<T> acc, T item) => {
        acc.Insert(0, iterator(item));
        return acc;
      }, new List<T>(), list);
      return new ImList<T>(transformedList);
    }

    public static Func<ImList<T>, ImList<T>> Transform<T>(Func<T, T> iterator) {
      return Curry<Func<T, T>, ImList<T>, ImList<T>>.New(Transform)(iterator);
    }

    public static ImList<T> Sort<T>(ImList<T> list) {
      var lst = list.GetList();
      lst.Sort();
      return new ImList<T>(lst);
    }

    public static ImList<T> Uniq<T>(ImList<T> list) {
      return Fold((acc, item) => AddNew(item, acc), new ImList<T>(), list);
    }

    public static ImList<T> AddNew<T>(T item, ImList<T> list) {
      if (list.Has(item)) {
        return list;
      }
      return list + item;
    }

    public static ImList<T> Replace<T>(Func<T, bool> predicate, T newItem, ImList<T> list) {
      for (int x = 0; x < list.Count; x++) {
        if (predicate(list[x])) {
          var lst = list.GetList();
          lst[x] = newItem;
          return new ImList<T>(lst);
        }
      }
      return list + newItem;
    }

    public static ImList<T> Overlay<T>(Func<T, T, bool> predicate, ImList<T> list1, ImList<T> list2) {
      return Fold((acc, item) => Replace((item1) => predicate(item1, item), item, acc), list1, list2);
    }

    public static Func<ImList<T>, ImList<T>> Overlay<T>(Func<T, T, bool> predicate, ImList<T> list1) {
      return Curry<Func<T, T, bool>, ImList<T>, ImList<T>, ImList<T>>.New(Overlay)(predicate)(list1);
    }

    public static T Head<T>(ImList<T> list) {
      return list[0];
    }

    public static ImList<T> Tail<T>(ImList<T> list) {
      var lst = list.GetList();
      lst.RemoveAt(0);
      return new ImList<T>(lst);
    }

    public static T Last<T>(ImList<T> list) {
      var count = list.Count;
      return list[count - 1];
    }

    public static T Maybe<K, V, T>(Func<V, K, T> action, K key, ImMap<K, V> map) {
      if (!map.Has(key)) {
        return default(T);
      }
      return action(map[key], key);
    }

    public static T Maybe<V, T>(Func<V, string, T> action, string key, ImMap<string, V> map) {
      return Maybe<string, V, T>(action, key, map);
    }

    public static void Maybe<K, V>(Action<V, K> action, K key, ImMap<K, V> map) {
      if (!map.Has(key)) {
        return;
      }
      action(map[key], key);
    }

    public static void Maybe<V>(Action<V, string> action, string key, ImMap<string, V> map) {
      Maybe<string, V>(action, key, map);
    }

    public static R Maybe<T, R>(Func<T, R> action, T item, R def, ImList<T> list) {
      if (list.Has(item)) {
        return action(item);
      }
      return def;
    }

  }

}
