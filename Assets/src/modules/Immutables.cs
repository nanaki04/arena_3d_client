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

    public List<T> GetList() {
      return new List<T>(list);
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

    public static ImList<T> Transform<T>(Func<T, T> iterator, ImList<T> list) {
      var transformedList = Im.Fold<T, List<T>>((List<T> acc, T item) => {
        acc.Insert(0, iterator(item));
        return acc;
      }, new List<T>(), list);
      return new ImList<T>(transformedList);
    }

  }

}
